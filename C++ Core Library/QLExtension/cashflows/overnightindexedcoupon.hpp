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

#ifndef qlex_overnight_indexed_coupon_hpp
#define qlex_overnight_indexed_coupon_hpp

#include <ql/cashflows/floatingratecoupon.hpp>
#include <ql/indexes/iborindex.hpp>
#include <ql/time/schedule.hpp>
#include <ql/cashflows/overnightindexedcoupon.hpp>
#include <ql/cashflows/couponpricer.hpp>
#include <ql/quotes/simplequote.hpp>

using namespace QuantLib;

namespace QLExtension {
	/*! pricer for arithmetically averaged overnight indexed coupons
		Reference: Katsumi Takada 2011, Valuation of Arithmetically Average of Fed Funds Rates and Construction of the US Dollar Swap Yield Curve
	*/
	class ArithmeticAveragedOvernightIndexedCouponPricer : public FloatingRateCouponPricer {
		public:
			ArithmeticAveragedOvernightIndexedCouponPricer(Handle<Quote> meanReversion = Handle<Quote>(boost::shared_ptr<Quote>(new SimpleQuote(0.03))),
				Handle<Quote> vol = Handle<Quote>(boost::shared_ptr<Quote>(new SimpleQuote(0.00))))
				 : meanReversion_(meanReversion), vol_(vol) {}
			void initialize(const FloatingRateCoupon& coupon);
			Rate swapletRate() const;
			
				Real swapletPrice() const { QL_FAIL("swapletPrice not available"); }
			Real capletPrice(Rate) const { QL_FAIL("capletPrice not available"); }
			Rate capletRate(Rate) const { QL_FAIL("capletRate not available"); }
			Real floorletPrice(Rate) const { QL_FAIL("floorletPrice not available"); }
			Rate floorletRate(Rate) const { QL_FAIL("floorletRate not available"); }
			
				Real meanReversion() const { meanReversion_->value(); };
			Real volatility() const { vol_->value(); };
		protected:
			Real convAdj1(Time ts, Time te) const;
			Real convAdj2(Time ts, Time te) const;
			const OvernightIndexedCoupon* coupon_;
			Handle<Quote> meanReversion_;
			Handle<Quote> vol_;
	};
}

#endif
