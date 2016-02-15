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

#include <termstructures/volatility/svi.hpp>
#include <ql/utilities/dataformatters.hpp>
#include <ql/math/comparison.hpp>
#include <ql/errors.hpp>

namespace QLExtension {

    Real unsafeSviVolatility(Rate strike,
                              Rate forward,
                              Time expiryTime,
							  Real a,
							  Real b,
							  Real rho,
							  Real m,
							  Real sigma) {
		Real k = std::log(strike / forward);
		Real totalVariance =  ( a + b*(rho*(k-m) + std::sqrt((k-m)*(k-m)+sigma*sigma)) );
		return std::sqrt(totalVariance / expiryTime);
    }

	void validateSviParameters(Real a,
								Real b,
								Real rho,
								Real m,
								Real sigma) {
        QL_REQUIRE(b>=0.0, "b must be non negative: "
                              << b << " not allowed");
		QL_REQUIRE(rho*rho<1.0, "rho square must be less than one: "
							<< rho << " not allowed");
        QL_REQUIRE(sigma>0.0, "sigma must be positive: "
                            << sigma << " not allowed");
		// QL_REQUIRE((a + b*sigma*std::sqrt(1-rho*rho))>=0.0, "total variance must be non negative: ");
        
    }

    Real sviVolatility(Rate strike,
                        Rate forward,
                        Time expiryTime,
						Real a,
						Real b,
						Real rho,
						Real m,
						Real sigma) {
        QL_REQUIRE(strike>0.0, "strike must be positive: "
                               << io::rate(strike) << " not allowed");
        QL_REQUIRE(forward>0.0, "at the money forward rate must be "
                   "positive: " << io::rate(forward) << " not allowed");
        QL_REQUIRE(expiryTime>=0.0, "expiry time must be non-negative: "
                                   << expiryTime << " not allowed");
        validateSviParameters(a, b, rho, m, sigma);
        return unsafeSviVolatility(strike, forward, expiryTime,
                                    a, b, rho, m, sigma);
    }

}
