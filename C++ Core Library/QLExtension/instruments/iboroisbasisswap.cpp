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

#include <ql/cashflows/overnightindexedcoupon.hpp>
#include <ql/cashflows/iborcoupon.hpp>
#include <cashflows/overnightindexedcoupon.hpp>
#include <instruments/iboroisbasisswap.hpp>

namespace QLExtension {

	IBOROISBasisSwap::IBOROISBasisSwap(
		Type type,
		Real nominal,
		const Schedule& floatingSchedule,
		const boost::shared_ptr<IborIndex>& iborIndex,
		const DayCounter& floatingDayCount,
		const Schedule& overnightSchedule,
		const boost::shared_ptr<OvernightIndex>& overnightIndex,
		Spread spread,
		const DayCounter& overnightDayCount,
		boost::optional<BusinessDayConvention> paymentConvention,
		bool arithmeticAveragedCoupon)
		: Swap(2), type_(type), nominals_(std::vector<Real>(1, nominal)),
		floatingSchedule_(floatingSchedule), iborIndex_(iborIndex),
		floatingDayCount_(floatingDayCount), overnightSchedule_(overnightSchedule),
		overnightIndex_(overnightIndex), spread_(spread), overnightDayCount_(overnightDayCount),
		arithmeticAveragedCoupon_(arithmeticAveragedCoupon){

		if (paymentConvention)
			paymentConvention_ = *paymentConvention;
		else
			paymentConvention_ = overnightSchedule_.businessDayConvention();

		initialize();

	}

	IBOROISBasisSwap::IBOROISBasisSwap(
		Type type,
		std::vector<Real> nominals,
		const Schedule& floatingSchedule,
		const boost::shared_ptr<IborIndex>& iborIndex,
		const DayCounter& floatingDayCount,
		const Schedule& overnightSchedule,
		const boost::shared_ptr<OvernightIndex>& overnightIndex,
		Spread spread,
		const DayCounter& overnightDayCount,
		boost::optional<BusinessDayConvention> paymentConvention,
		bool arithmeticAveragedCoupon)
		: Swap(2), type_(type), nominals_(nominals),
		floatingSchedule_(floatingSchedule), iborIndex_(iborIndex),
		floatingDayCount_(floatingDayCount), overnightSchedule_(overnightSchedule),
		overnightIndex_(overnightIndex), spread_(spread), overnightDayCount_(overnightDayCount),
		arithmeticAveragedCoupon_(arithmeticAveragedCoupon){

		if (paymentConvention)
			paymentConvention_ = *paymentConvention;
		else
			paymentConvention_ = floatingSchedule_.businessDayConvention();

		initialize();

	}

	void IBOROISBasisSwap::initialize() {

		legs_[0] = IborLeg(floatingSchedule_, iborIndex_)
			.withNotionals(nominals_)
			.withPaymentDayCounter(floatingDayCount_)
			.withPaymentAdjustment(paymentConvention_);

		legs_[1] = OvernightLeg(overnightSchedule_, overnightIndex_)
			.withNotionals(nominals_)
			.withPaymentDayCounter(overnightDayCount_)
			.withPaymentAdjustment(paymentConvention_)
			.withSpreads(spread_);
		if (arithmeticAveragedCoupon_) {
			boost::shared_ptr<FloatingRateCouponPricer> arithmeticPricer(
				new ArithmeticAveragedOvernightIndexedCouponPricer());
			for (Size i = 0; i < legs_[1].size(); i++) {
				boost::shared_ptr<OvernightIndexedCoupon> c =
					boost::dynamic_pointer_cast<OvernightIndexedCoupon> (legs_[1][i]);
				c->setPricer(arithmeticPricer);
			}
		}

		for (Size j = 0; j<2; ++j) {
			for (Leg::iterator i = legs_[j].begin(); i != legs_[j].end(); ++i)
				registerWith(*i);
		}

		switch (type_) {
		case Payer:
			payer_[0] = -1.0;
			payer_[1] = +1.0;
			break;
		case Receiver:
			payer_[0] = +1.0;
			payer_[1] = -1.0;
			break;
		default:
			QL_FAIL("Unknown overnight-basis-swap type");
		}
	}


	Spread IBOROISBasisSwap::fairSpread() const {
		static Spread basisPoint = 1.0e-4;
		calculate();
		return spread_ - NPV_ / (overnightLegBPS() / basisPoint);
	}

	Real IBOROISBasisSwap::floatingLegBPS() const {
		calculate();
		QL_REQUIRE(legBPS_[0] != Null<Real>(), "result not available");
		return legBPS_[0];
	}

	Real IBOROISBasisSwap::overnightLegBPS() const {
		calculate();
		QL_REQUIRE(legBPS_[1] != Null<Real>(), "result not available");
		return legBPS_[1];
	}

	Real IBOROISBasisSwap::floatingLegNPV() const {
		calculate();
		QL_REQUIRE(legNPV_[0] != Null<Real>(), "result not available");
		return legNPV_[0];
	}

	Real IBOROISBasisSwap::overnightLegNPV() const {
		calculate();
		QL_REQUIRE(legNPV_[1] != Null<Real>(), "result not available");
		return legNPV_[1];
	}
}

