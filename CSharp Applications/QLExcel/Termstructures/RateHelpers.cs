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

namespace QLExcel
{
    
    public class RateHelpers
    {
        //static QLEX.Calendar cal_gbp = new QLEX.UnitedKingdom(QLEX.UnitedKingdom.Market.Exchange);
        //static QLEX.Calendar cal_usd = new QLEX.UnitedStates(QLEX.UnitedStates.Market.Settlement);
        //static QLEX.JointCalendar cal_usd_libor = new QLEX.JointCalendar(cal_gbp, cal_usd, QLEX.JointCalendarRule.JoinHolidays);

        #region LIB3M
        [ExcelFunction(Description = "create deposit rate helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveDepositRateHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(double) quote of deposit rate ")] double Quote,
            [ExcelArgument(Description = "(String) forward start month, e.g. 7D, 3M ")] String Tenor,
            [ExcelArgument(Description = "int fixingDays ")] int fixingDays,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value
                QLEX.IborIndex idx = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));
                if (ExcelUtil.isNull(fixingDays))
                {
                    fixingDays = (int)idx.fixingDays();
                }

                QLEX.QuoteHandle quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(Quote));
                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);

                QLEX.RateHelper rh = new QLEX.DepositRateHelper(quote_, tenor_, (uint)fixingDays, idx.fixingCalendar(),
                    idx.businessDayConvention(), idx.endOfMonth(), idx.dayCounter());

                string id = "RHDEP@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "create future rate helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveFuturesRateHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(double) quote of ED futures e.g. 99.5 ")] double price,
            [ExcelArgument(Description = "(double) convexity adjustment default 0 ")] double convadj,
            [ExcelArgument(Description = "order of ED futures start from 1 ")] int order,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value
                QLEX.IborIndex idx = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));

                QLEX.Date today = QLEX.Settings.instance().getEvaluationDate();
                QLEX.Date settlementdate = idx.fixingCalendar().advance(today, (int)idx.fixingDays(), QLEX.TimeUnit.Days);

                QLEX.QuoteHandle quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(price));
                QLEX.QuoteHandle conv_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(convadj));
                
                QLEX.Date immdate = QLEX.IMM.nextDate(settlementdate, false);
                for (int i = 0; i < order-1; i++)
                {
                    immdate = QLEX.IMM.nextDate(idx.fixingCalendar().advance(immdate,1, QLEX.TimeUnit.Days), false);
                }

                QLEX.Date enddate = QLEX.IMM.nextDate(idx.fixingCalendar().advance(immdate, 1, QLEX.TimeUnit.Days), false);
                for (int i = 0; i < 3-1; i++)
                {
                    enddate = QLEX.IMM.nextDate(idx.fixingCalendar().advance(enddate, 1, QLEX.TimeUnit.Days), false);
                }

                QLEX.RateHelper rh = new QLEX.FuturesRateHelper(quote_, immdate, enddate, 
                    idx.dayCounter(), conv_);

                string id = "RHED@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "create swap rate helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveSwapRateHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(double) quote of swap rate ")] double quote,
            [ExcelArgument(Description = "(String) forward start month, e.g. 7D, 3M ")] String Tenor,
            [ExcelArgument(Description = " spread ")] double spread,
            [ExcelArgument(Description = " id of discount curve (USDOIS) ")] string discount,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value
                QLEX.IborIndex idx = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));
                QLEX.DayCounter fixeddc = new QLEX.Thirty360();

                QLEX.QuoteHandle rate_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(quote));
                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);

                if (ExcelUtil.isNull(spread))
                {
                    spread = 0.0;
                }
                QLEX.QuoteHandle spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(spread));

                QLEX.RateHelper rh = null;

                if (ExcelUtil.isNull(discount))
                {
                    rh = new QLEX.SwapRateHelper(rate_, tenor_,
                        idx.fixingCalendar(), QLEX.Frequency.Semiannual, idx.businessDayConvention(), fixeddc, idx);
                }
                else
                {
                    if (!discount.Contains('@'))
                        discount = "CRV@" + discount;

                    QLEX.YieldTermStructure curve = OHRepository.Instance.getObject<QLEX.YieldTermStructure>(discount);
                    QLEX.YieldTermStructureHandle yth = new QLEX.YieldTermStructureHandle(curve);
                    rh = new QLEX.SwapRateHelper(rate_, tenor_,
                        idx.fixingCalendar(), QLEX.Frequency.Semiannual, idx.businessDayConvention(), fixeddc,
                        idx, spread_, new QLEX.Period(0, QLEX.TimeUnit.Days), yth);
                }

                string id = "RHSWP@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }
        #endregion

        #region OIS
        // 0. deposit rate
        [ExcelFunction(Description = "create ois rate helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveOISRateHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(double) quote of swap rate ")] double quote,
            [ExcelArgument(Description = "(String) forward start month, e.g. 7D, 3M ")] String Tenor,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value. Eonia and ois has same convention
                QLEX.OvernightIndex idx = new QLEX.Eonia();
                
                QLEX.QuoteHandle rate_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(quote));
                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);

                // USSO
                QLEX.RateHelper rh = new QLEX.OISRateHelper(2, tenor_,  rate_, idx);

                string id = "RHOIS@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }

        [ExcelFunction(Description = "create Fed fund basis swap helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveOISFFBasisSwapHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(double) quote of swap rate ")] double quote,
            [ExcelArgument(Description = "(double) basis spread ")] double basisspread,
            [ExcelArgument(Description = "(String) forward start month, e.g. 7D, 3M ")] String Tenor,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value. Eonia and ois has same convention
                QLEX.OvernightIndex idx = new QLEX.Eonia();
                
                QLEX.QuoteHandle rate_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(quote));
                QLEX.QuoteHandle spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(basisspread));
                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(Tenor);
                QLEX.DayCounter dc = new QLEX.Actual360();

                // arithmetic average, not compounded. USBG
                QLEX.RateHelper rh = new QLEX.FixedOISBasisRateHelper(2, tenor_, spread_, rate_, 
                    QLEX.Frequency.Quarterly, QLEX.BusinessDayConvention.ModifiedFollowing, 
                    dc, idx, QLEX.Frequency.Quarterly);

                string id = "RHFFB@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }
        #endregion

        #region LIB Basis
        // http://papers.ssrn.com/sol3/papers.cfm?abstract_id=2219548
        // 3M - (1M+basis) = (3M-fixed) + fixed - (1M+basis) = fixed - (1M+basis)
        [ExcelFunction(Description = "create libor basis swap helper", Category = "QLExcel - Rates")]
        public static string qlIRCurveLiborBasisSwapHelper(
            [ExcelArgument(Description = "(String) id of rate helper object ")] String ObjectId,
            [ExcelArgument(Description = "(String) base leg (usually USDLIB3M) ")] String baseLeg,
            [ExcelArgument(Description = "(String) basis leg (USDLIB1M, USDLIB6M, etc) ")] String basisLeg,
            [ExcelArgument(Description = "(double) basis spread ")] double basis,
            [ExcelArgument(Description = "(String) basis swap tenor (1Y, 2Y, etc) ")] String tenor,
            [ExcelArgument(Description = "Discount Curve (USDLIB3M or USDOIS) ")] String discount,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();

            try
            {
                // use default value. Eonia and ois has same convention
                if (!baseLeg.Contains('@'))
                    baseLeg = "IDX" + baseLeg;
                QLEX.IborIndex baseidx = OHRepository.Instance.getObject<QLEX.IborIndex>(baseLeg);
                QLEX.IborIndex basisidx = null;
                switch (basisLeg.ToUpper())
                {
                    case "USDLIB1M":
                        basisidx = new QLEX.USDLibor(new QLEX.Period(1, QLEX.TimeUnit.Months));
                        break;
                    case "USDLIB6M":
                        basisidx = new QLEX.USDLibor(new QLEX.Period(6, QLEX.TimeUnit.Months));
                        break;
                    case "USDLIB12M":
                        basisidx = new QLEX.USDLibor(new QLEX.Period(12, QLEX.TimeUnit.Months));
                        break;
                    default:
                        break;
                }

                QLEX.YieldTermStructure curve = null;
                QLEX.YieldTermStructureHandle yth = null;
                
                if (!discount.Contains('@'))
                {
                    discount = "CRV@"+discount;
                }
                if (!ExcelUtil.isNull(discount))
                {
                    curve = OHRepository.Instance.getObject<QLEX.YieldTermStructure>(discount);
                    yth = new QLEX.YieldTermStructureHandle(curve);
                }

                QLEX.QuoteHandle basis_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(basis));
                QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tenor);

                // arithmetic average, not compounded. USBG
                QLEX.RateHelper rh = new QLEX.IBORBasisRateHelper(2, tenor_, basis_, baseidx, basisidx, yth);

                string id = "RHBAS@" + ObjectId;
                OHRepository.Instance.storeObject(id, rh, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }
        #endregion

        #region boostrapping
        [ExcelFunction(Description = "create curve ", Category = "QLExcel - Rates")]
        public static string qlIRCurveLinearZero(
            [ExcelArgument(Description = "(String) id of curve (USDOIS, USDLIB3M) ")] string ObjectId,
            [ExcelArgument(Description = "array of rate helpers ")] object[] ratehelpers,
            [ExcelArgument(Description = "Interpolation Method (Linear, LogLinear) ")] string interp,
            [ExcelArgument(Description = "trigger ")]object trigger)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = ExcelUtil.getActiveCellAddress();
            Xl.Range rng = ExcelUtil.getActiveCellRange();

            try
            {
                string interpmethod;
                if (ExcelUtil.isNull(interp))
                {
                    interpmethod = "LOGLINEAR";
                }
                else
                {
                    interpmethod = interp.ToUpper();
                }

                QLEX.IborIndex idx = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));
                QLEX.RateHelperVector rhv = new QLEX.RateHelperVector();

                QLEX.Date today = QLEX.Settings.instance().getEvaluationDate();
                QLEX.Date settlementdate = idx.fixingCalendar().advance(today, 2, QLEX.TimeUnit.Days);
                List<QLEX.Date> dates = new List<QLEX.Date>();
                //dates.Add(settlementdate);
                dates.Add(today);       // today has discount 1

                foreach(var rid in ratehelpers)
                {
                    if (ExcelUtil.isNull(rid))
                        continue;

                    try
                    {
                        QLEX.RateHelper rh = OHRepository.Instance.getObject<QLEX.RateHelper>((string)rid);
                        rhv.Add(rh);
                        dates.Add(rh.latestDate());
                    }
                    catch (Exception)   
                    {
                        // skip null instruments
                    }
                }

                // set reference date to today. or discount to 1
                QLEX.YieldTermStructure yth = new QLEX.PiecewiseLinearZero(today, rhv, idx.dayCounter());

                QLEX.DateVector dtv = new QLEX.DateVector();
                QLEX.DoubleVector discv = new QLEX.DoubleVector();
                foreach(var dt in dates)
                {
                    double disc = yth.discount(dt);
                    dtv.Add(dt);
                    discv.Add(disc);
                }

                // reconstruct the discount curve
                // note that discount curve is LogLinear
                QLEX.YieldTermStructure yth2 = null;

                if (interpmethod == "LINEAR")
                {
                    yth2 = new QLEX.LinearDiscountCurve(dtv, discv, idx.dayCounter(), idx.fixingCalendar());
                }
                else
                {
                    yth2 = new QLEX.DiscountCurve(dtv, discv, idx.dayCounter(), idx.fixingCalendar());
                } 

                if (!ObjectId.Contains('@'))
                    ObjectId = "CRV@" + ObjectId;

                //string id = "IRCRV@" + ObjectId;
                string id = ObjectId;
                OHRepository.Instance.storeObject(id, yth2, callerAddress);
                return id + "#" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "#QL_ERR!";
            }
        }
        #endregion
    }
}
