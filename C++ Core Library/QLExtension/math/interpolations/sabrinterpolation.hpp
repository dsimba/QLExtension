/****************************** Project Header ******************************\
Project:	      QuantLib Extension (QLExtension)
Author:			  Letian.zj@gmail.com
URL:			  https://github.com/letianzj/QLExtension
Copyright 2014 Letian_zj
This file is part of  QuantLib Extension (QLExtension) Project.
QuantLib Extension (QLExtension) is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
either version 3 of the License, or (at your option) any later version.
QuantTrading is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with QLExtension.
If not, see http://www.gnu.org/licenses/.
\***************************************************************************/

#ifndef qlex_sabr_interpolation_hpp
#define qlex_sabr_interpolation_hpp

#include <math/interpolations/xabrinterpolation.hpp>
#include <ql/termstructures/volatility/sabr.hpp>

#include <boost/make_shared.hpp>
#include <boost/assign/list_of.hpp>

using namespace QuantLib;

namespace QLExtension {

namespace detail {

    class SABRWrapper {
      public:
        SABRWrapper(const Time t, const Real &forward,
                    const std::vector<Real> &params)
        : t_(t), forward_(forward), params_(params) {
            validateSabrParameters(params[0], params[1], params[2], params[3]);
        }
        Real volatility(const Real x) {
            return sabrVolatility(x, forward_, t_, params_[0], params_[1],
                                  params_[2], params_[3]);
        }

      private:
        const Real t_, &forward_;
        const std::vector<Real> &params_;
    };

    struct SABRSpecs {
        Size dimension() { return 4; }
        void defaultValues(std::vector<Real> &params, std::vector<bool> &,
                           const Real &forward, const Real expiryTIme) {
            if (params[1] == Null<Real>())
                params[1] = 0.5;
            if (params[0] == Null<Real>())
                // adapt alpha to beta level
                params[0] =
                    0.2 *
                    (params[1] < 0.9999 ? std::pow(forward, 1.0 - params[1]) : 1.0);
            if (params[2] == Null<Real>())
                params[2] = std::sqrt(0.4);
            if (params[3] == Null<Real>())
                params[3] = 0.0;
        }
        void guess(Array &values, const std::vector<bool> &paramIsFixed,
                   const Real &forward, const Real expiryTime,
                   const std::vector<Real> &r) {
            Size j = 0;
            if (!paramIsFixed[1])
                values[1] = (1.0 - 2E-6) * r[j++] + 1E-6;
            if (!paramIsFixed[0]) {
                values[0] =
                    (1.0 - 2E-6) * r[j++] + 1E-6; // lognormal vol guess
                // adapt this to beta level
                if(values[1] < 0.999)
                    values[0] *=
                        std::pow(forward,
                                 1.0 - values[1]);
            }
            if (!paramIsFixed[2])
                values[2] = 1.5 * r[j++] + 1E-6;
            if (!paramIsFixed[3])
                values[3] = (2.0 * r[j++] - 1.0) * (1.0 - 1E-6);
        }
        Real eps1() { return .0000001; }
        Real eps2() { return .9999; }
        Real dilationFactor() { return 0.001; }
        Array inverse(const Array &y, const std::vector<bool>&,
                      const std::vector<Real>&, const Real) {
            Array x(4);
            x[0] = y[0] < 25.0 + eps1() ? std::sqrt(y[0] - eps1())
                                        : (y[0] - eps1() + 25.0) / 10.0;
            // y_[1] = std::tan(M_PI*(x[1] - 0.5))/dilationFactor();
            x[1] = std::sqrt(-std::log(y[1]));
            x[2] = y[2] < 25.0 + eps1() ? std::sqrt(y[2] - eps1())
                                        : (y[2] - eps1() + 25.0) / 10.0;
            x[3] = std::asin(y[3] / eps2());
            return x;
        }
        Array direct(const Array &x, const std::vector<bool>&,
                     const std::vector<Real>&, const Real) {
            Array y(4);
            y[0] = std::fabs(x[0]) < 5.0
                       ? x[0] * x[0] + eps1()
                       : (10.0 * std::fabs(x[0]) - 25.0) + eps1();
            // y_[1] = std::atan(dilationFactor_*x[1])/M_PI + 0.5;
            y[1] = std::fabs(x[1]) < std::sqrt(-std::log(eps1()))
                       ? std::exp(-(x[1] * x[1]))
                       : eps1();
            y[2] = std::fabs(x[2]) < 5.0
                       ? x[2] * x[2] + eps1()
                       : (10.0 * std::fabs(x[2]) - 25.0) + eps1();
            y[3] = std::fabs(x[3]) < 2.5 * M_PI
                       ? eps2() * std::sin(x[3])
                       : eps2() * (x[3] > 0.0 ? 1.0 : (-1.0));
            return y;
        }
        typedef SABRWrapper type;
        boost::shared_ptr<type> instance(const Time t, const Real &forward,
                                         const std::vector<Real> &params) {
            return boost::make_shared<type>(t, forward, params);
        }
    };

}

    //! %SABR smile interpolation between discrete volatility points.
    class SABRInterpolation : public Interpolation {
      public:
        template <class I1, class I2>
        SABRInterpolation(const I1 &xBegin,  // x = strikes
                          const I1 &xEnd,
                          const I2 &yBegin,  // y = volatilities
                          Time t,            // option expiry
                          const Real& forward,
                          Real alpha,
                          Real beta,
                          Real nu,
                          Real rho,
                          bool alphaIsFixed,
                          bool betaIsFixed,
                          bool nuIsFixed,
                          bool rhoIsFixed,
                          bool vegaWeighted = true,
                          const boost::shared_ptr<EndCriteria>& endCriteria
                                  = boost::shared_ptr<EndCriteria>(),
                          const boost::shared_ptr<OptimizationMethod>& optMethod
                                  = boost::shared_ptr<OptimizationMethod>(),
                          const Real errorAccept = 0.0020,
                          const bool useMaxError = false,
                          const Size maxGuesses = 50) {

            impl_ = boost::shared_ptr<Interpolation::Impl>(
                new detail::XABRInterpolationImpl<I1, I2, detail::SABRSpecs>(
                    xBegin, xEnd, yBegin, t, forward,
                    boost::assign::list_of(alpha)(beta)(nu)(rho),
                    boost::assign::list_of(alphaIsFixed)(betaIsFixed)(nuIsFixed)(rhoIsFixed),
                    vegaWeighted, endCriteria, optMethod, errorAccept, useMaxError,
                    maxGuesses));
            coeffs_ = boost::dynamic_pointer_cast<
                detail::XABRCoeffHolder<detail::SABRSpecs> >(impl_);
        }
        Real expiry()  const { return coeffs_->t_; }
        Real forward() const { return coeffs_->forward_; }
        Real alpha()   const { return coeffs_->params_[0]; }
        Real beta()    const { return coeffs_->params_[1]; }
        Real nu()      const { return coeffs_->params_[2]; }
        Real rho()     const { return coeffs_->params_[3]; }
        Real rmsError() const { return coeffs_->error_; }
        Real maxError() const { return coeffs_->maxError_; }
        const std::vector<Real>& interpolationWeights() const {
            return coeffs_->weights_;
        }
        EndCriteria::Type endCriteria() { return coeffs_->XABREndCriteria_; }

      private:
        boost::shared_ptr<detail::XABRCoeffHolder<detail::SABRSpecs> > coeffs_;
    };

    //! %SABR interpolation factory and traits
    class SABR {
      public:
        SABR(Time t, Real forward,
             Real alpha, Real beta, Real nu, Real rho,
             bool alphaIsFixed, bool betaIsFixed,
             bool nuIsFixed, bool rhoIsFixed,
             bool vegaWeighted = false,
             const boost::shared_ptr<EndCriteria> endCriteria
                 = boost::shared_ptr<EndCriteria>(),
             const boost::shared_ptr<OptimizationMethod> optMethod
                 = boost::shared_ptr<OptimizationMethod>(),
             const Real errorAccept = 0.0020, const bool useMaxError = false,
             const Size maxGuesses = 50)
        : t_(t), forward_(forward),
          alpha_(alpha), beta_(beta), nu_(nu), rho_(rho),
          alphaIsFixed_(alphaIsFixed), betaIsFixed_(betaIsFixed),
          nuIsFixed_(nuIsFixed), rhoIsFixed_(rhoIsFixed),
          vegaWeighted_(vegaWeighted),
          endCriteria_(endCriteria),
          optMethod_(optMethod), errorAccept_(errorAccept),
          useMaxError_(useMaxError), maxGuesses_(maxGuesses) {}
        template <class I1, class I2>
        Interpolation interpolate(const I1 &xBegin, const I1 &xEnd,
                                  const I2 &yBegin) const {
            return SABRInterpolation(
                xBegin, xEnd, yBegin, t_, forward_, alpha_, beta_, nu_, rho_,
                alphaIsFixed_, betaIsFixed_, nuIsFixed_, rhoIsFixed_, vegaWeighted_,
                endCriteria_, optMethod_, errorAccept_, useMaxError_, maxGuesses_);
        }
        static const bool global = true;

      private:
        Time t_;
        Real forward_;
        Real alpha_, beta_, nu_, rho_;
        bool alphaIsFixed_, betaIsFixed_, nuIsFixed_, rhoIsFixed_;
        bool vegaWeighted_;
        const boost::shared_ptr<EndCriteria> endCriteria_;
        const boost::shared_ptr<OptimizationMethod> optMethod_;
        const Real errorAccept_;
        const bool useMaxError_;
        const Size maxGuesses_;
    };

}

#endif
