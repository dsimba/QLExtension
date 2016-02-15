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

#include <cashflows/overnightindexedcoupon.hpp>
#include <ql/cashflows/overnightindexedcoupon.hpp>
#include <ql/cashflows/couponpricer.hpp>
#include <ql/termstructures/yieldtermstructure.hpp>
#include <ql/utilities/vectors.hpp>
#include <ql/termstructures/yieldtermstructure.hpp>

using std::vector;
using boost::shared_ptr;
using boost::dynamic_pointer_cast;

namespace QLExtension {

	void ArithmeticAveragedOvernightIndexedCouponPricer::initialize(const FloatingRateCoupon& coupon) {
		coupon_ = dynamic_cast<const OvernightIndexedCoupon*>(&coupon);
		QL_ENSURE(coupon_, "wrong coupon type");
	}
	
	Rate ArithmeticAveragedOvernightIndexedCouponPricer::swapletRate() const {
		shared_ptr<OvernightIndex> index =
			dynamic_pointer_cast<OvernightIndex>(coupon_->index());
		const vector<Date>& fixingDates = coupon_->fixingDates();
		const vector<Time>& dt = coupon_->dt();

		Size n = dt.size(), i = 0;
		Real accumulatedRate = 0.0;

		// already fixed part
		Date today = Settings::instance().evaluationDate();
		while (i < n && fixingDates[i] < today) {
			// rate must have been fixed
			Rate pastFixing = IndexManager::instance().getHistory(
				index->name())[fixingDates[i]];
			QL_REQUIRE(pastFixing != Null<Real>(),
				"Missing " << index->name() <<
				" fixing for " << fixingDates[i]);
			accumulatedRate += pastFixing*dt[i];
			++i;
		}

		// today is a border case
		if (i < n && fixingDates[i] == today) {
			// might have been fixed
			try {
				Rate pastFixing = IndexManager::instance().getHistory(
					index->name())[fixingDates[i]];
				if (pastFixing != Null<Real>()) {
					accumulatedRate += pastFixing*dt[i];
					++i;
				}
				else {
					;   // fall through and forecast
				}
			}
			catch (Error&) {
				;       // fall through and forecast
			}
		}

		// forward part using telescopic property in order
		// to avoid the evaluation of multiple forward fixings
		if (i < n) {
			Handle<YieldTermStructure> curve =
				index->forwardingTermStructure();
			QL_REQUIRE(!curve.empty(),
				"null term structure set to this instance of " <<
				index->name());

			const vector<Date>& dates = coupon_->valueDates();
			DiscountFactor startDiscount = curve->discount(dates[i]);
			DiscountFactor endDiscount = curve->discount(dates[n]);

			accumulatedRate += log(startDiscount / endDiscount) - convAdj1(curve->timeFromReference(dates[i]),
				curve->timeFromReference(dates[n]))
				- convAdj2(curve->timeFromReference(dates[i]),
				curve->timeFromReference(dates[n]));
		}
		Rate rate = accumulatedRate / coupon_->accrualPeriod();
		return coupon_->gearing() * rate + coupon_->spread();
	}

	Real ArithmeticAveragedOvernightIndexedCouponPricer::convAdj1(Time ts, Time te) const {
		Real vol = vol_->value();
		Real k = meanReversion_->value();
		return vol * vol / (4.0 * pow(k, 3.0)) * (1.0 - exp(-2.0*k*ts)) * pow((1.0 - exp(-k*(te - ts))), 2.0);
	}

	Real ArithmeticAveragedOvernightIndexedCouponPricer::convAdj2(Time ts, Time te) const {
		Real vol = vol_->value();
		Real k = meanReversion_->value();
		return vol * vol / (2.0 * pow(k, 2.0)) * ((te - ts) -
			pow(1.0 - exp(-k*(te - ts)), 2.0) / k -
			(1.0 - exp(-2.0*k*(te - ts))) / (2.0 * k));
	}
}