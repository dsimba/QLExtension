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

// simultabeous bootstrap
// https://github.com/kosynski/quantlib
#ifndef qlex_oisbasisratehelper_hpp
#define qlex_oisbasisratehelper_hpp

#include <instruments/iboroisbasisswap.hpp>
#include <instruments/genericswap.hpp>
#include <cashflows/overnightindexedcoupon.hpp>
#include <ql/termstructures/yield/ratehelpers.hpp>
#include <ql/instruments/overnightindexedswap.hpp>

using namespace QuantLib;

namespace QLExtension {

	//! Rate helper for bootstrapping over Ibor vs. Overnight Indexed basis Swap rates
	class IBOROISBasisRateHelper : public RelativeDateRateHelper {
	public:
		IBOROISBasisRateHelper(Natural settlementDays,
			const Period& tenor, // swap maturity
			const Handle<Quote>& overnightSpread,
			const boost::shared_ptr<IborIndex>& iborIndex,
			const boost::shared_ptr<OvernightIndex>& overnightIndex,
			// exogenous discounting curve
			const Handle<YieldTermStructure>& discountingCurve
			= Handle<YieldTermStructure>());
		//! \name RateHelper interface
		//@{
		Real impliedQuote() const;
		void setTermStructure(YieldTermStructure*);
		//@}
		//! \name inspectors
		//@{
		boost::shared_ptr<IBOROISBasisSwap> swap() const { return swap_; }
		//@}
		//! \name Visitability
		//@{
		void accept(AcyclicVisitor&);
		//@}
	protected:
		void initializeDates();

		Natural settlementDays_;
		Period tenor_;
		boost::shared_ptr<IborIndex> iborIndex_;
		boost::shared_ptr<OvernightIndex> overnightIndex_;

		boost::shared_ptr<IBOROISBasisSwap> swap_;
		RelinkableHandle<YieldTermStructure> termStructureHandle_;

		Handle<YieldTermStructure> discountHandle_;
		RelinkableHandle<YieldTermStructure> discountRelinkableHandle_;
	};

	//! Rate helper for bootstrapping over Fixed vs. Overnight Indexed basis Swap rates
	class FixedOISBasisRateHelper : public RelativeDateRateHelper {
	public:
		FixedOISBasisRateHelper(Natural settlementDays,
			const Period& tenor, // swap maturity
			const Handle<Quote>& overnightSpread,
			const Handle<Quote>& fixedRate,
			Frequency fixedFrequency,
			BusinessDayConvention fixedConvention,
			const DayCounter& fixedDayCount,
			const boost::shared_ptr<OvernightIndex>& overnightIndex,
			Frequency overnightFrequency,
			// exogenous discounting curve
			const Handle<YieldTermStructure>& discountingCurve
			= Handle<YieldTermStructure>());
		//! \name RateHelper interface
		//@{
		Real impliedQuote() const;
		void setTermStructure(YieldTermStructure*);
		//@}
		//! \name inspectors
		//@{
		boost::shared_ptr<Swap> swap() const { return swap_; }
		//@}
		//! \name Visitability
		//@{
		void accept(AcyclicVisitor&);
		//@}
		//! \name Observer interface
		//@{
		void update();
		//@}
	protected:
		void initializeDates();

		Natural settlementDays_;
		Period tenor_;
		Handle<Quote> fixedRate_;
		Real usedFixedRate_;
		Frequency fixedFrequency_;
		BusinessDayConvention fixedConvention_;
		DayCounter fixedDayCount_;
		boost::shared_ptr<OvernightIndex> overnightIndex_;
		Frequency overnightFrequency_;

		boost::shared_ptr<Swap> swap_;
		RelinkableHandle<YieldTermStructure> termStructureHandle_;

		Handle<YieldTermStructure> discountHandle_;
		RelinkableHandle<YieldTermStructure> discountRelinkableHandle_;
	};

	// Libor basis swap
	class IBORBasisRateHelper : public RelativeDateRateHelper {
	public:
		IBORBasisRateHelper(Natural settlementDays,
			const Period& tenor, // swap maturity
			const Handle<Quote>& basis,
			const boost::shared_ptr<IborIndex>& baseLegIborIndex,
			const boost::shared_ptr<IborIndex>& basisLegIborIndex, 
			// exogenous discounting curve
			const Handle<YieldTermStructure>& discountingCurve
			= Handle<YieldTermStructure>());
		//! \name RateHelper interface
		//@{
		Real impliedQuote() const;
		void setTermStructure(YieldTermStructure*);
		//@}
		//! \name inspectors
		//@{
		boost::shared_ptr<GenericSwap> swap() const { return swap_; }
		//@}
		//! \name Visitability
		//@{
		void accept(AcyclicVisitor&);
		//@}
		//! \name Observer interface
		//@{
		void update();
		//@}
	protected:
		void initializeDates();

		Natural settlementDays_;
		Period tenor_;
		boost::shared_ptr<IborIndex> baseLegIndex_;
		boost::shared_ptr<IborIndex> basisLegIndex_;

		boost::shared_ptr<GenericSwap> swap_;
		RelinkableHandle<YieldTermStructure> termStructureHandle_;

		Handle<YieldTermStructure> discountHandle_;
		RelinkableHandle<YieldTermStructure> discountRelinkableHandle_;
	};
}

#endif
