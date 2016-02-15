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

#ifndef qlex_ibor_ois_basis_swap_hpp
#define qlex_ibor_ois_basis_swap_hpp

#include <ql/instruments/swap.hpp>
#include <ql/indexes/iborindex.hpp>
#include <ql/time/daycounter.hpp>
#include <ql/time/schedule.hpp>
#include <boost/optional.hpp>

using namespace QuantLib;

namespace QLExtension {

	class QuantLib::IborIndex;
	class QuantLib::OvernightIndex;

	//! Ibor OIS basis swap: ibor vs compounded overnight rate + spread
	class IBOROISBasisSwap : public Swap {
	public:
		enum Type { Receiver = -1, Payer = 1 };
		IBOROISBasisSwap(
			Type type,
			Real nominal,
			const Schedule& floatSchedule,
			const boost::shared_ptr<IborIndex>& iborIndex,
			const DayCounter& floatingDayCount,
			const Schedule& overnightSchedule,
			const boost::shared_ptr<OvernightIndex>& overnightIndex,
			Spread spread,
			const DayCounter& overnightDayCount,
			boost::optional<BusinessDayConvention> paymentConvention =
			boost::none,
			bool arithmeticAveragedCoupon = true);
		IBOROISBasisSwap(
			Type type,
			std::vector<Real> nominals,
			const Schedule& floatSchedule,
			const boost::shared_ptr<IborIndex>& iborIndex,
			const DayCounter& floatingDayCount,
			const Schedule& overnightSchedule,
			const boost::shared_ptr<OvernightIndex>& overnightIndex,
			Spread spread,
			const DayCounter& overnightDayCount,
			boost::optional<BusinessDayConvention> paymentConvention =
			boost::none,
			bool arithmeticAveragedCoupon = true);
		//! \name Inspectors
		//@{
		Type type() const { return type_; }
		Real nominal() const;
		std::vector<Real> nominals() const { return nominals_; }

		const Schedule& floatingSchedule() const;
		const boost::shared_ptr<IborIndex>& iborIndex() const { return iborIndex_; }
		const DayCounter& floatingDayCount() const;

		const Schedule& overnightSchedule() { return overnightSchedule_; }
		const boost::shared_ptr<OvernightIndex>& overnightIndex() { return overnightIndex_; }
		Spread spread() { return spread_; }

		BusinessDayConvention paymentConvention() const { return paymentConvention_; }

		const Leg& floatingLeg() const { return legs_[0]; }
		const Leg& overnightLeg() const { return legs_[1]; }
		//@}

		//! \name Results
		//@{
		Real floatingLegBPS() const;
		Real floatingLegNPV() const;

		Real overnightLegBPS() const;
		Real overnightLegNPV() const;
		Spread fairSpread() const;
		//@}
	private:
		void initialize();
		Type type_;
		std::vector<Real> nominals_;

		Schedule floatingSchedule_;
		boost::shared_ptr<IborIndex> iborIndex_;
		DayCounter floatingDayCount_;

		Schedule overnightSchedule_;
		boost::shared_ptr<OvernightIndex> overnightIndex_;
		Spread spread_;
		DayCounter overnightDayCount_;

		BusinessDayConvention paymentConvention_;

		bool arithmeticAveragedCoupon_;
	};


	// inline

	inline Real IBOROISBasisSwap::nominal() const {
		QL_REQUIRE(nominals_.size() == 1, "varying nominals");
		return nominals_[0];
	}
}

#endif
