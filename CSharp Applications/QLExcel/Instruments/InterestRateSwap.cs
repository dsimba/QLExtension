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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelDna.Integration;
using ExcelDna.Integration.Rtd;
using Xl = Microsoft.Office.Interop.Excel;
using QLEX;

namespace QLExcel
{
    public class IRSwap
    {
        [ExcelFunction(Description = "Interest Rate vanilla swap", Category = "QLExcel - Instruments")]
        public static object qlInstIRVanillaSwap(
            [ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "payer/receiver (1/0) ")] bool ispayer,
            [ExcelArgument(Description = "notional ")] double notional,
            [ExcelArgument(Description = "fixed rate ")] double fixedRate,
            [ExcelArgument(Description = "start date ")] DateTime startdate,
            [ExcelArgument(Description = " (String) forward start month, e.g. 7D, 3M, 7Y ")] string Tenor,
            [ExcelArgument(Description = "id of libor index ")] string indexid,
            [ExcelArgument(Description = "floating leg spread ")] double spread,
            [ExcelArgument(Description = "id of discount curve ")] string discountId,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();

                // by default
                bool end_of_month = true;
                QLEX.DayCounter fixeddc = new QLEX.Thirty360();

                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);

                if (!indexid.Contains('@'))
                    indexid = "IDX@" + indexid;
                IborIndex idx = OHRepository.Instance.getObject<IborIndex>(indexid);
                if (!discountId.Contains('@'))
                    discountId = "CRV@" + discountId;
                YieldTermStructure discountcurve = OHRepository.Instance.getObject<YieldTermStructure>(discountId);
                YieldTermStructureHandle dch = new YieldTermStructureHandle(discountcurve);

                QLEX.Date sdate = QLEX.QLConverter.ConvertObject<QLEX.Date>(startdate);
                QLEX.Date fdate = idx.fixingDate(sdate);
                QLEX.Date tdate = idx.fixingCalendar().advance(sdate, tenor_);

                Schedule fixedsch = new Schedule(sdate, tdate, new Period(6, TimeUnit.Months),
                    idx.fixingCalendar(), idx.businessDayConvention(), idx.businessDayConvention(),
                    DateGeneration.Rule.Backward, end_of_month);
                Schedule floatingsch = new Schedule(sdate, tdate, idx.tenor(), idx.fixingCalendar(),
                    idx.businessDayConvention(), idx.businessDayConvention(),
                    DateGeneration.Rule.Backward, end_of_month);

                VanillaSwap swap = new VanillaSwap(ispayer ? _VanillaSwap.Type.Payer : _VanillaSwap.Type.Receiver,
                    notional, fixedsch, fixedRate, fixeddc, floatingsch, idx, spread, idx.dayCounter());
                DiscountingSwapEngine engine = new DiscountingSwapEngine(dch);
                swap.setPricingEngine(engine);
                
                Date refDate = discountcurve.referenceDate();

                // Store the futures and return its id
                string id = "SWP@" + tradeid;
                OHRepository.Instance.storeObject(id, swap, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "Interest Rate vanilla OIS swap", Category = "QLExcel - Instruments")]
        public static object qlInstIROISSwap(
            [ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "payer/receiver (1/0) ")] bool ispayer,
            [ExcelArgument(Description = "notional ")] double notional,
            [ExcelArgument(Description = "fixed rate ")] double fixedRate,
            [ExcelArgument(Description = "start date ")] DateTime startdate,
            [ExcelArgument(Description = " (String) forward start month, e.g. 7D, 3M, 7Y ")] string Tenor,
            [ExcelArgument(Description = "id of overnight index ")] string indexid,
            [ExcelArgument(Description = "floating leg spread ")] double spread,
            [ExcelArgument(Description = "id of discount curve ")] string discountId,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();

                // by default
                // endOfMonth_(1*Months<=swapTenor && swapTenor<=2*Years ? true : false),
                bool end_of_month = false;
                QLEX.DayCounter fixeddc = new QLEX.Actual360();

                if (!indexid.Contains('@'))
                    indexid = "IDX@" + indexid;
                OvernightIndex idx = OHRepository.Instance.getObject<OvernightIndex>(indexid);
                if (!discountId.Contains('@'))
                    discountId = "CRV@" + discountId;
                YieldTermStructure discountcurve = OHRepository.Instance.getObject<YieldTermStructure>(discountId);
                YieldTermStructureHandle dch = new YieldTermStructureHandle(discountcurve);

                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);
                QLEX.Date sdate = QLEX.QLConverter.ConvertObject<QLEX.Date>(startdate);
                QLEX.Date fdate = idx.fixingDate(sdate);
                QLEX.Date tdate = idx.fixingCalendar().advance(sdate, tenor_);

                // fixed leg 1 yr. Forward?
                Schedule fixedsch = new Schedule(sdate, tdate, new Period(1, TimeUnit.Years),
                    idx.fixingCalendar(), idx.businessDayConvention(), idx.businessDayConvention(),
                    DateGeneration.Rule.Forward, end_of_month);

                OvernightIndexedSwap swap = new OvernightIndexedSwap(ispayer ? _OvernightIndexedSwap.Type.Payer : _OvernightIndexedSwap.Type.Receiver,
                    notional, fixedsch, fixedRate, fixeddc, idx, spread);

                DiscountingSwapEngine engine = new DiscountingSwapEngine(dch);
                swap.setPricingEngine(engine);

                Date refDate = discountcurve.referenceDate();

                // Store the futures and return its id
                string id = "SWP@" + tradeid;
                OHRepository.Instance.storeObject(id, swap, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "Interest Rate vanilla basis swap", Category = "QLExcel - Instruments")]
        public static object qlInstIRBasisSwap(
            [ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "payer/receiver (1/0) ")] bool ispayer,
            [ExcelArgument(Description = "notional ")] double notional,
            [ExcelArgument(Description = "start date ")] DateTime startdate,
            [ExcelArgument(Description = " (String) forward start month, e.g. 7D, 3M, 7Y ")] string Tenor,
            [ExcelArgument(Description = "id of base index ")] string baseindexid,
            [ExcelArgument(Description = "id of basis index ")] string basisindexid,
            [ExcelArgument(Description = "basis leg spread ")] double spread,
            [ExcelArgument(Description = "id of discount curve ")] string discountId,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();

                // by default
                // endOfMonth_(1*Months<=swapTenor && swapTenor<=2*Years ? true : false),
                bool end_of_month = true;
                QLEX.DayCounter fixeddc = new QLEX.Actual360();

                if (!baseindexid.Contains('@'))
                    baseindexid = "IDX@" + baseindexid;
                IborIndex baseidx = OHRepository.Instance.getObject<IborIndex>(baseindexid);

                if (!basisindexid.Contains('@'))
                    basisindexid = "IDX@" + basisindexid;
                IborIndex basisidx = OHRepository.Instance.getObject<IborIndex>(basisindexid);

                if (!discountId.Contains('@'))
                    discountId = "CRV@" + discountId;
                YieldTermStructure discountcurve = OHRepository.Instance.getObject<YieldTermStructure>(discountId);
                YieldTermStructureHandle dch = new YieldTermStructureHandle(discountcurve);

                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);
                QLEX.Date sdate = QLEX.QLConverter.ConvertObject<QLEX.Date>(startdate);
                QLEX.Date fdate = baseidx.fixingDate(sdate);
                QLEX.Date tdate = baseidx.fixingCalendar().advance(sdate, tenor_);

                // fixed leg 1 yr. Forward?
                Schedule basesch = new Schedule(sdate, tdate, baseidx.tenor(),
                    baseidx.fixingCalendar(), baseidx.businessDayConvention(), baseidx.businessDayConvention(),
                    DateGeneration.Rule.Backward, end_of_month);

                Schedule basissch = new Schedule(sdate, tdate, basisidx.tenor(),
                    basisidx.fixingCalendar(), basisidx.businessDayConvention(), basisidx.businessDayConvention(),
                    DateGeneration.Rule.Backward, end_of_month);

                //GenericSwap swap = new GenericSwap((ispayer ? _GenericSwap.Type.Payer : _GenericSwap.Type.Receiver), notional,
                //    basesch, baseidx, baseidx.dayCounter(), basissch, basisidx, basisidx.dayCounter(), spread);

                //DiscountingSwapEngine engine = new DiscountingSwapEngine(dch);
                //swap.setPricingEngine(engine);

                //Date refDate = discountcurve.referenceDate();

                // Store the futures and return its id
                //string id = "SWP@" + tradeid;
                //OHRepository.Instance.storeObject(id, swap, callerAddress);
                //id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                //return id;
                return 0;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        // schedule has 0:n; notionals has 0:n-1 elements
        // in cashflowvectors.hpp, it gets i or the last one if runs out
        [ExcelFunction(Description = "Interest Rate generic swap template (TPL) ", Category = "QLExcel - Instruments")]
        public static object qlInstIRGenericSwapTemplate(
            // trade info
            /*[ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "Entity ")] string entity,
            [ExcelArgument(Description = "Entity ID ")] string entityid,
            [ExcelArgument(Description = "Counterparty ")] string counterparty,
            [ExcelArgument(Description = "Counterparty ID ")] string counterpartyid,
            [ExcelArgument(Description = "swap type ")] string swaptype,*/
            [ExcelArgument(Description = "swap type ")] object[] tradeinfo,
            // date info
            [ExcelArgument(Description = "Fixing Days ")] object fixingdays,        // use object to catch missing
            [ExcelArgument(Description = "Trade Date ")] object tradedate,
            [ExcelArgument(Description = "Start date ")] object startdate,
            [ExcelArgument(Description = "Maturity date ")] object maturitydate,
            [ExcelArgument(Description = "Tenor ")] string Tenor,
            // first leg
            /*[ExcelArgument(Description = "id of first leg index ")] string firstlegindex,
            [ExcelArgument(Description = "first leg frequency ")] string firstlegfreq,
            [ExcelArgument(Description = "first leg convention ")] string firstlegconv,
            [ExcelArgument(Description = "first leg calendar ")] string firstlegcalendar,
            [ExcelArgument(Description = "first leg day counter ")] string firstlegdc,
            [ExcelArgument(Description = "first leg date generation rule ")] string firstlegdgrule,*/
            [ExcelArgument(Description = "first leg info ")] object[] firstleginfo,
            [ExcelArgument(Description = "first leg end of month ")] bool firstlegeom,
            [ExcelArgument(Description = "first leg notional(s) ")] object[] firstlegnotionals,     // only object[] works
            [ExcelArgument(Description = "first leg schedule(s) ")] object[,] firstlegschedule,
            [ExcelArgument(Description = "first leg fixed rate ")] double firstlegrate,
            // second leg
            /*[ExcelArgument(Description = "id of second leg index ")] string secondlegindex,
            [ExcelArgument(Description = "second leg frequency  ")] string secondlegfreq,
            [ExcelArgument(Description = "second leg convention ")] string secondlegconv,
            [ExcelArgument(Description = "second leg calendar ")] string secondlegcalendar,
            [ExcelArgument(Description = "second leg day counter ")] string secondlegdc,
            [ExcelArgument(Description = "second leg date generation rule ")] string secondlegdgrule,*/
            [ExcelArgument(Description = "second leg info ")] object[] secondleginfo,
            [ExcelArgument(Description = "second leg end of month ")] bool secondlegeom,
            [ExcelArgument(Description = "second leg notional(s) ")] object[] secondlegnotionals,
            [ExcelArgument(Description = "second leg schedule(s) ")] object[,] secondlegschedule,
            [ExcelArgument(Description = "second leg spread ")] double secondlegspread,
            // given schedule
            [ExcelArgument(Description = "is notional schedule given ")] bool isschedulegiven)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();

                QLEX.Instruments.InterestRateGenericSwap genswap = new QLEX.Instruments.InterestRateGenericSwap();

                #region parameters
                if (ExcelUtil.isNull(tradeinfo[0]))
                    return "#QL_ERR!";
                else
                    genswap.ContractId = (string)tradeinfo[0];

                if (ExcelUtil.isNull(tradeinfo[1]))
                    genswap.Entity = "NA";
                else
                    genswap.Entity = (string)tradeinfo[1];

                if (ExcelUtil.isNull(tradeinfo[2]))
                    genswap.EntityID = "NA";
                else
                    genswap.EntityID = (string)tradeinfo[2];

                if (ExcelUtil.isNull(tradeinfo[3]))
                    genswap.Counterparty = "NA";
                else
                    genswap.Counterparty = (string)tradeinfo[3];

                if (ExcelUtil.isNull(tradeinfo[4]))
                    genswap.CounterpartyID = "NA";
                else
                    genswap.CounterpartyID = (string)tradeinfo[4];

                if (ExcelUtil.isNull(tradeinfo[5]))
                    genswap.SwapType = "Payer";
                else
                    genswap.SwapType = (string)tradeinfo[5];

                if (ExcelUtil.isNull(fixingdays))
                    genswap.FixingDays = 2;
                else
                    genswap.FixingDays = (int)(double)fixingdays;

                if (ExcelUtil.isNull(tradedate))
                    genswap.TradeDate = QLEX.QLConverter.DateTimeToString(DateTime.Today);
                else
                    genswap.TradeDate = QLEX.QLConverter.DateTimeToString(DateTime.FromOADate((double)tradedate));

                // set it temporarily to ""
                if (ExcelUtil.isNull(startdate))
                    genswap.SettlementDate = string.Empty;
                else
                    genswap.SettlementDate = QLEX.QLConverter.DateTimeToString(DateTime.FromOADate((double)startdate));

                // set it temporarily to today
                if (ExcelUtil.isNull(maturitydate))
                    genswap.MaturityDate = string.Empty;
                else
                    genswap.MaturityDate = QLEX.QLConverter.DateTimeToString(DateTime.FromOADate((double)maturitydate));

                // set it temporarily to blank
                if (ExcelUtil.isNull(Tenor))
                    genswap.Tenor = string.Empty;
                else
                    genswap.Tenor = Tenor;

                if (ExcelUtil.isNull(isschedulegiven))
                    genswap.IsScheduleGiven = false;
                else
                    genswap.IsScheduleGiven = isschedulegiven;
                genswap.IsScheduleGiven = false;        // set to false always

                //***************  First Leg *************************//
                if (ExcelUtil.isNull(firstleginfo[0]))
                    genswap.FirstLegIndex = "FIXED";
                else
                    genswap.FirstLegIndex = (string)firstleginfo[0];

                if (ExcelUtil.isNull(firstleginfo[1]))
                    genswap.FirstLegFrequency = "SEMIANNUAL";
                else
                    genswap.FirstLegFrequency = (string)firstleginfo[1];

                if (ExcelUtil.isNull(firstleginfo[2]))
                    genswap.FirstLegConvention = "MODIFIEDFOLLOWING";
                else
                    genswap.FirstLegConvention = (string)firstleginfo[2];

                if (ExcelUtil.isNull(firstleginfo[3]))
                    genswap.FirstLegCalendar = "NYC";           // nor NYC|LON
                else
                    genswap.FirstLegCalendar = (string)firstleginfo[3];

                if (ExcelUtil.isNull(firstleginfo[4]))
                    genswap.FirstLegDayCounter = "ACTUAL360";
                else
                    genswap.FirstLegDayCounter = (string)firstleginfo[4];

                if (ExcelUtil.isNull(firstleginfo[5]))
                    genswap.FirstLegDateGenerationRule = "BACKWARD";
                else
                    genswap.FirstLegDateGenerationRule = (string)firstleginfo[5];

                if (ExcelUtil.isNull(firstlegeom))
                    genswap.FirstLegEOM = true;
                else
                    genswap.FirstLegEOM = firstlegeom;

                if (ExcelUtil.isNull(firstlegnotionals))
                {
                    genswap.FirstLegNotionals.Clear();
                    genswap.FirstLegNotionals.Add(0);       // size = 1
                }
                else
                {
                    genswap.FirstLegNotionals.Clear();
                    foreach (var nl in firstlegnotionals)
                    {
                        if (ExcelUtil.isNull(nl))
                            continue;

                        genswap.FirstLegNotionals.Add((double)nl);
                    }
                }

                if (ExcelUtil.isNull(firstlegschedule) || (!genswap.IsScheduleGiven))
                {
                    genswap.FirstLegSchedules.Clear();
                    genswap.FirstLegSchedules.Add(genswap.SettlementDate);
                    genswap.FirstLegSchedules.Add(genswap.MaturityDate);
                }
                else
                {
                    genswap.FirstLegSchedules.Clear();
                    for (int a = 0; a < firstlegschedule.GetLength(0);a++ )
                    {
                        DateTime d;
                        if (ExcelUtil.isNull(firstlegschedule[a, 0]))
                        {
                            // add one more date
                            d = DateTime.FromOADate((double)firstlegschedule[a-1, 1]);
                            genswap.FirstLegSchedules.Add(QLEX.QLConverter.DateTimeToString(d));
                            break;
                        }
                            

                        d = DateTime.FromOADate((double)firstlegschedule[a,0]);
                        genswap.FirstLegSchedules.Add(QLEX.QLConverter.DateTimeToString(d));
                    }
                }

                if (ExcelUtil.isNull(firstlegrate))
                    genswap.FixedRate = 0.0;
                else
                    genswap.FixedRate = firstlegrate;

                //***************  Second Leg *************************//

                if (ExcelUtil.isNull(secondleginfo[0]))
                    genswap.SecondLegIndex = "USDLIB3M";
                else
                    genswap.SecondLegIndex = (string)secondleginfo[0];

                if (ExcelUtil.isNull(secondleginfo[1]))
                    genswap.SecondLegFrequency = "QUARTERLY";
                else
                    genswap.SecondLegFrequency = (string)secondleginfo[1];

                if (ExcelUtil.isNull(secondleginfo[2]))
                    genswap.SecondLegConvention = "MODIFIEDFOLLOWING";
                else
                    genswap.SecondLegConvention = (string)secondleginfo[2];

                if (ExcelUtil.isNull(secondleginfo[3]))
                    genswap.SecondLegCalendar = "NYC";           // nor NYC|LON
                else
                    genswap.SecondLegCalendar = (string)secondleginfo[3];

                if (ExcelUtil.isNull(secondleginfo[4]))
                    genswap.SecondLegDayCounter = "ACTUAL360";
                else
                    genswap.SecondLegDayCounter = (string)secondleginfo[4];

                if (ExcelUtil.isNull(secondleginfo[5]))
                    genswap.SecondLegDateGenerationRule = "BACKWARD";
                else
                    genswap.SecondLegDateGenerationRule = (string)secondleginfo[5];

                if (ExcelUtil.isNull(secondlegeom))
                    genswap.SecondLegEOM = true;
                else
                    genswap.SecondLegEOM = secondlegeom;

                if (ExcelUtil.isNull(secondlegnotionals))
                {
                    genswap.SecondLegNotionals.Clear();
                    genswap.SecondLegNotionals.Add(0);
                }
                else
                {
                    genswap.SecondLegNotionals.Clear();
                    foreach (var nl in secondlegnotionals)
                    {
                        if (ExcelUtil.isNull(nl))
                            continue;

                        genswap.SecondLegNotionals.Add((double)nl);
                    }
                }

                if (ExcelUtil.isNull(secondlegschedule) || (!genswap.IsScheduleGiven))
                {
                    genswap.SecondLegSchedules.Clear();
                    genswap.SecondLegSchedules.Add(genswap.SettlementDate);
                    genswap.SecondLegSchedules.Add(genswap.MaturityDate);
                }
                else
                {
                    genswap.SecondLegSchedules.Clear();
                    for (int a = 0; a < secondlegschedule.GetLength(0); a++)
                    {
                        DateTime d;
                        if (ExcelUtil.isNull(secondlegschedule[a, 0]))
                        {
                            // add one more date
                            d = DateTime.FromOADate((double)secondlegschedule[a - 1, 1]);
                            genswap.SecondLegSchedules.Add(QLEX.QLConverter.DateTimeToString(d));
                            break;
                        }


                        d = DateTime.FromOADate((double)secondlegschedule[a, 0]);
                        genswap.SecondLegSchedules.Add(QLEX.QLConverter.DateTimeToString(d));
                    }
                }

                if (ExcelUtil.isNull(secondlegspread))
                    genswap.Spread = 0.0;
                else
                    genswap.Spread = secondlegspread;
                #endregion

                #region convert Interest rate generic swap to swap obj
                // do elsewhere
                #endregion

                // update settlement date and maturity date

                string id = "SWP@" + genswap.ContractId + "_TPL";
                OHRepository.Instance.storeObject(id, genswap, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }


        // TODO: if isxchedulegiven, read from the excel schedule input instead of generating schedule
        // for now customerized schedule is not supported because in schedule fullInterface_(false) is turned off
        //      leading to crash in FixedRateLeg::operator Leg() 
        [ExcelFunction(Description = "Construct genswap from genswap template ", Category = "QLExcel - Instruments")]
        public static object qlInstIRGenericSwap(
            [ExcelArgument(Description = "trade id ")] string tradeid,
            [ExcelArgument(Description = "id of discount curve ")] string discountId,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                //string tradeid_ = tradeid;
                /*if (tradeid_.IndexOf('#') != -1)            // get rid of time
                {
                    tradeid_ = tradeid_.Substring(0, tradeid_.IndexOf('#'));
                }
                if (tradeid_.IndexOf("_TPL") != -1)            // get rid of _TPL
                {
                    tradeid_ = tradeid_.Substring(0, tradeid_.IndexOf("_TPL"));
                }*/
                string genswaptplid_ = tradeid;
                if (!genswaptplid_.Contains('@'))
                {
                    genswaptplid_ = "SWP@" + genswaptplid_;
                }
                if (!genswaptplid_.Contains("_TPL"))
                {
                    genswaptplid_ = genswaptplid_ + "_TPL";
                }

                QLEX.Instruments.InterestRateGenericSwap genswaptpl = OHRepository.Instance.getObject<QLEX.Instruments.InterestRateGenericSwap>(genswaptplid_);

                if (!discountId.Contains('@'))
                    discountId = "CRV@" + discountId;
                YieldTermStructure discountcurve = OHRepository.Instance.getObject<YieldTermStructure>(discountId);
                YieldTermStructureHandle dch = new YieldTermStructureHandle(discountcurve);

                #region conversion
                //************************** trade info *******************************
                genswaptpl.ConstructSwap(null, null);
                #endregion
                DiscountingSwapEngine engine = new DiscountingSwapEngine(dch);
                genswaptpl.qlswap_.setPricingEngine(engine);

                string id = "SWP@" + genswaptpl.ContractId;
                OHRepository.Instance.storeObject(id, genswaptpl, callerAddress);
                id += "#" + (String)DateTime.Now.ToString(@"HH:mm:ss");
                return id;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "Display interest rate swap pay/rec schedule", Category = "QLExcel - Instruments")]
        public static object qlInstDisplayIRSwap(
            [ExcelArgument(Description = "id of IR Swap ")] string tradeid,
            [ExcelArgument(Description = "id of discount curve ")] string discountId,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            object[,] ret;
            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();


                if (!tradeid.Contains('@'))
                    tradeid = "SWP@" + tradeid;

                if (!discountId.Contains('@'))
                    discountId = "CRV@" + discountId;
                YieldTermStructure discountcurve = OHRepository.Instance.getObject<YieldTermStructure>(discountId);
                Date asofdate = Settings.instance().getEvaluationDate();

                GenericSwap inst = OHRepository.Instance.getObject<GenericSwap>(tradeid);
                
                int rows = Math.Max(inst.firstLegInfo().Count, inst.secondLegInfo().Count);
                ret = new object[rows, 20];     // 10 cols each leg
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        ret[i, j] = "";         // initialization. null will be posted as 0; so explicitly set it to ""
                    }
                }

                // first leg
                string[] s;
                DateTime startdate, enddate, paymentdate, resetdate;
                double balance = 0, rate = 0, spread = 0, payment = 0, discount = 0, pv = 0;
                for (int i = 0; i < inst.firstLegInfo().Count; i++ )
                {
                    s = inst.firstLegInfo()[i].Split(',');
                    startdate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[0])));
                    enddate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[1])));
                    paymentdate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[2])));
                    resetdate = (s[3]=="") ? DateTime.MinValue : QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[3])));
                    balance = Convert.ToDouble(s[4]);
                    rate = Convert.ToDouble(s[5]);
                    spread = Convert.ToDouble(s[6]);
                    payment = Convert.ToDouble(s[7]);
                    
                    // today's cashflow is not included
                    if (QLEX.QLConverter.DateTimeToDate(paymentdate).serialNumber() <= asofdate.serialNumber())
                    {
                        discount = 0.0;
                    }
                    else
                    {
                        discount = discountcurve.discount(QLEX.QLConverter.DateTimeToDate(paymentdate));
                    }
                    
                    pv = payment * discount;

                    // and return the matrix to vba
                    ret[i, 0] = (object)startdate;
                    ret[i, 1] = (object)enddate;
                    ret[i, 2] = (object)paymentdate;
                    ret[i, 3] = (s[3]=="") ? "":(object)resetdate;
                    ret[i, 4] = (object)(balance == 0 ? "" : (object)balance);
                    ret[i, 5] = (object)(rate == 0 ? "" : (object)rate);
                    ret[i, 6] = (object)(spread == 0 ? "" : (object)spread);
                    ret[i, 7] = (object)(payment == 0 ? "" : (object)payment);
                    ret[i, 8] = (object)(discount == 0 ? "" : (object)discount);
                    ret[i, 9] = (object)(pv == 0 ? "" : (object)pv);
                }
                for (int i = 0; i < inst.secondLegInfo().Count; i++)
                {
                    s = inst.secondLegInfo()[i].Split(',');
                    startdate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[0])));
                    enddate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[1])));
                    paymentdate = QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[2])));
                    resetdate = (s[3] == "") ? DateTime.MinValue : QLEX.QLConverter.DateToDateTime(new Date(Convert.ToInt32(s[3])));
                    balance = Convert.ToDouble(s[4]);
                    rate = Convert.ToDouble(s[5]);
                    spread = Convert.ToDouble(s[6]);
                    payment = Convert.ToDouble(s[7]);

                    // today's cashflow is not included
                    if (QLEX.QLConverter.DateTimeToDate(paymentdate).serialNumber() <= asofdate.serialNumber())
                    {
                        discount = 0.0;
                    }
                    else
                    {
                        discount = discountcurve.discount(QLEX.QLConverter.DateTimeToDate(paymentdate));
                    }
                    
                    pv = payment * discount;

                    // and return the matrix to vba
                    ret[i, 10] = (object)startdate;
                    ret[i, 11] = (object)enddate;
                    ret[i, 12] = (object)paymentdate;
                    ret[i, 13] = (s[3] == "") ? "" : (object)resetdate;
                    ret[i, 14] = (object)(balance == 0 ? "" : (object)balance);
                    ret[i, 15] = (object)(rate == 0 ? "" : (object)rate);
                    ret[i, 16] = (object)(spread == 0 ? "" : (object)spread);
                    ret[i, 17] = (object)(payment == 0 ? "" : (object)payment);
                    ret[i, 18] = (object)(discount == 0 ? "" : (object)discount);
                    ret[i, 19] = (object)(pv == 0 ? "" : (object)pv);
                }

                return ret;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "Save interest rate swap to Disk ", Category = "QLExcel - Instruments")]
        public static object qlInstSaveIRSwapToDisk(
            [ExcelArgument(Description = "id of IR Swap ")] string tradeid,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();

                string genswaptplid_ = tradeid;
                if (!genswaptplid_.Contains('@'))
                {
                    genswaptplid_ = "SWP@" + genswaptplid_;
                }
                if (!genswaptplid_.Contains("_TPL"))
                {
                    genswaptplid_ = genswaptplid_ + "_TPL";
                }

                QLEX.Instruments.InterestRateGenericSwap genswaptpl = OHRepository.Instance.getObject<QLEX.Instruments.InterestRateGenericSwap>(genswaptplid_);

                string path = QLEX.ConfigManager.Instance.IRRootDir + @"Trades\";

                QLEX.Instruments.InterestRateGenericSwap.Serialize(genswaptpl,
                    path + tradeid + ".xml");
                return tradeid;
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        // load return load info, do not save to object repository. Let GenSWapTmpl to deal with repository
        [ExcelFunction(Description = "Load interest rate swap from Disk ", Category = "QLExcel - Instruments")]
        public static object qlInstLoadIRSwapFromDisk(
            [ExcelArgument(Description = "id of IR Swap ")] string tradeid, // should not include @ and _TPL
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                Xl.Range rng = ExcelUtil.getActiveCellRange();
                string genswaptplid_ = tradeid;
                if (!genswaptplid_.Contains('@'))
                {
                    genswaptplid_ = "SWP@" + genswaptplid_;
                }
                if (!genswaptplid_.Contains("_TPL"))
                {
                    genswaptplid_ = genswaptplid_ + "_TPL";
                }

                string path = QLEX.ConfigManager.Instance.IRRootDir + @"Trades\";

                QLEX.Instruments.InterestRateGenericSwap genswaptpl =
                    QLEX.Instruments.InterestRateGenericSwap.Deserialize(path + tradeid + ".xml");

                // preserve same order as excel
                List<object> ret = new List<object>();
                ret.Add(genswaptpl.ContractId);
                ret.Add(genswaptpl.Entity);
                ret.Add(genswaptpl.EntityID);
                ret.Add(genswaptpl.Counterparty);
                ret.Add(genswaptpl.CounterpartyID);
                ret.Add(genswaptpl.SwapType);
                ret.Add(genswaptpl.FixingDays.ToString());
                ret.Add(string.IsNullOrEmpty(genswaptpl.TradeDate) ? "" : (object)QLEX.QLConverter.StringToDateTime(genswaptpl.TradeDate));
                ret.Add(string.IsNullOrEmpty(genswaptpl.SettlementDate) ? "" : (object)QLEX.QLConverter.StringToDateTime(genswaptpl.SettlementDate));
                ret.Add(string.IsNullOrEmpty(genswaptpl.MaturityDate) ? "" : (object)QLEX.QLConverter.StringToDateTime(genswaptpl.MaturityDate));
                ret.Add(genswaptpl.Tenor);
                if ((genswaptpl.FirstLegNotionals.Count > 1) || (genswaptpl.SecondLegNotionals.Count > 1))
                {
                    ret.Add("TRUE");
                }
                else
                {
                    ret.Add(genswaptpl.IsScheduleGiven.ToString());    // always false for now
                }
                ret.Add(genswaptpl.FirstLegIndex);
                ret.Add(genswaptpl.FirstLegFrequency);
                ret.Add(genswaptpl.FirstLegConvention);
                ret.Add(genswaptpl.FirstLegCalendar);
                ret.Add(genswaptpl.FirstLegDayCounter);
                ret.Add(genswaptpl.FirstLegDateGenerationRule);
                ret.Add(genswaptpl.FirstLegEOM.ToString());
                ret.Add(genswaptpl.FixedRate.ToString());
                if (genswaptpl.FirstLegNotionals.Count > 1)
                {
                    ret.Add("Collection...");
                }
                else
                {
                    ret.Add(genswaptpl.FirstLegNotionals[0].ToString());
                }   
                ret.Add("");
                ret.Add(genswaptpl.SecondLegIndex);
                ret.Add(genswaptpl.SecondLegFrequency);
                ret.Add(genswaptpl.SecondLegConvention);
                ret.Add(genswaptpl.SecondLegCalendar);
                ret.Add(genswaptpl.SecondLegDayCounter);
                ret.Add(genswaptpl.SecondLegDateGenerationRule);
                ret.Add(genswaptpl.SecondLegEOM.ToString());
                ret.Add(genswaptpl.Spread.ToString());
                if (genswaptpl.SecondLegNotionals.Count > 1)
                {
                    ret.Add("Collection...");
                }
                else
                {
                    ret.Add(genswaptpl.SecondLegNotionals[0].ToString());
                }  
                ret.Add("");

                return ret.ToArray();
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }
    }
}
