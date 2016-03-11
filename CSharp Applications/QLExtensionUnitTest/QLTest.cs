using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using QLEX;

namespace QLEXUnitTest
{
    [TestClass]
    public class QLTest
    {
        [TestMethod]
        public void TestBlackScholes()
        {
            Option.Type optionType = Option.Type.Put;
            double underlyingPrice = 36;
            double strikePrice = 40;
            double dividendYield = 0.0;
            double riskFreeRate = 0.06;
            double volatility = 0.2;

            Date todaysDate = new Date(15, Month.May, 1998);
            Settings.instance().setEvaluationDate(todaysDate);

            Date settlementDate = new Date(17, Month.May, 1998);
            Date maturityDate = new Date(17, Month.May, 1999);

            Calendar calendar = new TARGET();

            DateVector exerciseDates = new DateVector(4);
            for (int i = 1; i <= 4; i++)
            {
                Period forwardPeriod = new Period(3 * i, TimeUnit.Months);
                Date forwardDate = settlementDate.Add(forwardPeriod);
                exerciseDates.Add(forwardDate);
            }

            EuropeanExercise europeanExercise =
                new EuropeanExercise(maturityDate);
            BermudanExercise bermudanExercise =
                new BermudanExercise(exerciseDates);
            AmericanExercise americanExercise =
                new AmericanExercise(settlementDate, maturityDate);

            // bootstrap the yield/dividend/vol curves and create a
            // BlackScholesMerton stochastic process
            DayCounter dayCounter = new Actual365Fixed();
            YieldTermStructureHandle flatRateTSH =
                new YieldTermStructureHandle(
                                new FlatForward(settlementDate, riskFreeRate,
                                                 dayCounter));
            YieldTermStructureHandle flatDividendTSH =
                new YieldTermStructureHandle(
                                new FlatForward(settlementDate, dividendYield,
                                                dayCounter));
            BlackVolTermStructureHandle flatVolTSH =
                new BlackVolTermStructureHandle(
                                new BlackConstantVol(settlementDate, calendar,
                                                     volatility, dayCounter));

            QuoteHandle underlyingQuoteH =
                new QuoteHandle(new SimpleQuote(underlyingPrice));
            BlackScholesMertonProcess stochasticProcess =
                new BlackScholesMertonProcess(underlyingQuoteH,
                                              flatDividendTSH,
                                              flatRateTSH,
                                              flatVolTSH);

            PlainVanillaPayoff payoff =
                new PlainVanillaPayoff(optionType, strikePrice);

            // options
            VanillaOption europeanOption =
                new VanillaOption(payoff, europeanExercise);
            VanillaOption bermudanOption =
                new VanillaOption(payoff, bermudanExercise);
            VanillaOption americanOption =
                new VanillaOption(payoff, americanExercise);

            try
            {
                europeanOption.setPricingEngine(
                               new AnalyticEuropeanEngine(stochasticProcess));
                double npv = europeanOption.NPV();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [TestMethod]
        public void TestLiborCurve()
        {
            Calendar cal1 = new UnitedStates(UnitedStates.Market.Settlement);
            Calendar cal2 = new UnitedKingdom(UnitedKingdom.Market.Settlement);
            Calendar cal3 = new JointCalendar(cal1, cal2, JointCalendarRule.JoinHolidays);
            Date d1 = new Date(3, Month.July, 2015);                 // independence day US
            Date d2 = new Date(3, Month.April, 2015);               // good friday in UK
            bool isbusinessday;
            isbusinessday = cal1.isBusinessDay(d1);
            isbusinessday = cal1.isBusinessDay(d2);

            isbusinessday = cal2.isBusinessDay(d1);
            isbusinessday = cal2.isBusinessDay(d2);

            isbusinessday = cal3.isBusinessDay(d1);
            isbusinessday = cal3.isBusinessDay(d2);

            DayCounter dc = new Actual360();

            // quotes
            // deposits
            double d1wQuote = 0.0382;
            double d1mQuote = 0.0372;
            double d3mQuote = 0.0363;
            double d6mQuote = 0.0353;
            double d9mQuote = 0.0348;
            double d1yQuote = 0.0345;
            // FRAs
            double fra3x6Quote = 0.037125;
            double fra6x9Quote = 0.037125;
            double fra6x12Quote = 0.037125;
            // futures
            double fut1Quote = 96.2875;
            double fut2Quote = 96.7875;
            double fut3Quote = 96.9875;
            double fut4Quote = 96.6875;
            double fut5Quote = 96.4875;
            double fut6Quote = 96.3875;
            double fut7Quote = 96.2875;
            double fut8Quote = 96.0875;
            // swaps
            double s2yQuote = 0.037125;
            double s3yQuote = 0.0398;
            double s5yQuote = 0.0443;
            double s10yQuote = 0.05165;
            double s15yQuote = 0.055175;

            // RateHelper
            // overnight, 1wk, 2wk, 1mth, 2mth, 3mth deposit rate
            RateHelper depo_on = new DepositRateHelper(0.29/100, new Period(1, TimeUnit.Days), 0, cal3,
                BusinessDayConvention.ModifiedFollowing, true, dc);

            RateHelper depo_1wk = new DepositRateHelper(0.32/100, new Period(1, TimeUnit.Weeks), 2, cal3,
                BusinessDayConvention.ModifiedFollowing, true, dc);

            RateHelper depo_2wk = new DepositRateHelper(0.34/100, new Period(2, TimeUnit.Weeks), 2, cal3,
                BusinessDayConvention.ModifiedFollowing, true, dc);

            RateHelper depo_1mth = new DepositRateHelper(0.38/100, new Period(1, TimeUnit.Months), 2, cal3,
                BusinessDayConvention.ModifiedFollowing, true, dc);
            
            // first 8 ED Futures
            //RateHelper ed1 = new FuturesRateHelper();


            // spot starting swaps: 2, 3, 4, 5, 7, 10, 12, 15, 20, 25, 30 years
            //RateHelper swp2y = new SwapRateHelper();


        }


        [TestMethod]
        public void TestCurveBuilder()
        {
            QlArray x = new QlArray(5);
            x.set(0, 0.0); x.set(1, 1.0); x.set(2, 2.0); x.set(3, 3.0); x.set(4, 4.0);
            QlArray y = new QlArray(5);
            y.set(0, 5.0); y.set(1, 4.0); y.set(2, 3.0); y.set(3, 2.0); y.set(4, 1.0);

            QLEX.LinearInterpolation linear = new LinearInterpolation(x, y);
            int s = (int)x.size(); double res = x.get(1);
            res = linear.call(2.0);
            res = linear.call(0.0);
            res = linear.call(3.5);

            QLEX.GaussianPathGenerator gs;
            QLEX.GaussianMultiPathGenerator gms;
            StochasticProcessArray sa;
            StochasticProcessVector sv = new StochasticProcessVector(100);
            QLEX.StochasticProcess sp = new GeometricBrownianMotionProcess(1.1, 0, .2);
        }

        [TestMethod]
        public void TestDiscountCurve()
        {
            QLEX.RateHelperVector rhv = new QLEX.RateHelperVector();
            QLEX.Date asofdate = new QLEX.Date(30, QLEX.Month.June, 2015);
            QLEX.Settings.instance().setEvaluationDate(asofdate);

            QLEX.Calendar cal_gbp = new QLEX.UnitedKingdom(QLEX.UnitedKingdom.Market.Exchange);
            QLEX.Calendar cal_usd = new QLEX.UnitedStates(QLEX.UnitedStates.Market.Settlement);
            QLEX.JointCalendar cal_usd_gbp = new QLEX.JointCalendar(cal_gbp, cal_usd, QLEX.JointCalendarRule.JoinHolidays);
            QLEX.DayCounter dc_act_360 = new Actual360();
            QLEX.DayCounter dc_30_360 = new Thirty360();
            QLEX.BusinessDayConvention bdc = QLEX.BusinessDayConvention.ModifiedFollowing;
            bool eom = true;
            int fixingDays = 2;
            QLEX.Date settlementdate = cal_usd.advance(asofdate, fixingDays, TimeUnit.Days);

            double[] ois_quotes = new double[]
            {
                0.0014,
                0.001425,
                0.001425,
                0.00137,
                0.00137,
                0.001695,
                0.002035,
                0.002185,
                0.002455,
                0.00274,
                0.003495,
                0.00432,
                0.006055,
                0.007795,
                0.01082,
                0.01337,
                0.01544,
                0.018465,
                0.02128,
                0.02233,
                0.023425,
                0.024435,
                0.024865,
                0.02507,
                0.025308
            };
            string[] ois_tenors = new string[]
            {
                "1D",
                "1W",
                "2W",
                "3W",
                "1M",
                "2M",
                "3M",
                "4M",
                "5M",
                "6M",
                "9M",
                "12M",
                "18M",
                "2Y",
                "3Y",
                "4Y",
                "5Y",
                "7Y",
                "10Y",
                "12Y",
                "15Y",
                "20Y",
                "25Y",
                "30Y",
                "40Y"
            };

            // "london stock exchange"; "Actual/360"; "fixingDays = 2", "MF", "eom = true"
            QLEX.IborIndex idx_usdlibor = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));
            // "us settlement"; "Actual/360"; "fixingDays = 0", "F", "eom = true"
            QLEX.FedFunds idx_ff = new FedFunds();
            int i = 0;

            QLEX.QuoteHandle quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(ois_quotes[0]));
            QLEX.Period tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(ois_tenors[0]);
            QLEX.RateHelper rh = new QLEX.DepositRateHelper(quote_, tenor_, 0, cal_usd, bdc, eom, dc_act_360);
            rhv.Add(rh);

            List<QLEX.Date> dates = new List<QLEX.Date>();
            dates.Add(asofdate);
            for (i = 1; i < ois_quotes.Length; i++)
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(ois_quotes[i]));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(ois_tenors[i]);

                rh = new QLEX.OISRateHelper((uint)fixingDays, tenor_, quote_, idx_ff);
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            // set reference date to today. or discount to 1
            QLEX.PiecewiseLogLinearDiscount yth = new QLEX.PiecewiseLogLinearDiscount(asofdate, rhv, dc_act_360);


            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            QLEX.DateVector dtv = new QLEX.DateVector();
            QLEX.DoubleVector discv = new QLEX.DoubleVector();
            foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }

            QLEX.YieldTermStructure yth_ois = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd);
            
            QLEX.DoubleVector discv2 = new QLEX.DoubleVector();
            foreach (var dt in dates)
            {
                double disc = yth_ois.discount(dt);
                discv2.Add(disc);
            }

            i = 0;
            QLEX.FedFunds idx_ff2 = new FedFunds(new YieldTermStructureHandle(yth_ois));       // set forward term strucutre
            List<double> npv_new = new List<double>();
            List<double> npv_new2 = new List<double>();
            foreach (string tr in ois_tenors)
            {
                /*
                QLEX.Date sdate = cal_usd.advance(asofdate, new Period(fixingDays, QLEX.TimeUnit.Days));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tr);
                QLEX.Date tdate = cal_usd.advance(sdate, tenor_);
                Schedule sch = new Schedule(sdate, tdate, new Period(1, TimeUnit.Years),
                    cal_usd, bdc, bdc, DateGeneration.Rule.Backward, eom);
                OvernightIndexedSwap swap = new OvernightIndexedSwap(_OvernightIndexedSwap.Type.Payer, 1000, sch, 0.0, dc_act_360, idx_ff2);
                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));       // set discount term structure
                swap.setPricingEngine(engine);
                double npv = swap.fairRate();
                npv_new.Add(npv);

                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                _genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                _genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.SecondLegIndex = "USDOIS";
                _genswap.SecondLegFrequency = "Annual";
                _genswap.Tenor = tr;
                _genswap.IsScheduleGiven = false;
                _genswap.ConstructSwap(null, idx_ff2);
                _genswap.qlswap_.setPricingEngine(engine);
                npv = _genswap.qlswap_.fairRate();
                npv_new2.Add(npv);
                i++;*/
            }

            ////////////////////////////////////////// LIB3M /////////////////////////////////////////////////////
            double[] eurodollars = new double[]
            {
                99.665,
                99.6,
                99.535,
                99.485,
                99.43,
                99.375,
                99.25,
                99.05,
                98.835,
                98.625,
                98.45,
                98.28,
                98.135,
                97.995,
                97.88,
                97.77,
            };
            double[] eurodollars_conv = new double[]            // in bps, need to divide by 10000
            {
                0.004,
                0.017,
                0.040,
                0.064,
                0.093,
                0.136,
                0.220,
                0.414,
                0.650,
                0.915,
                1.273,
                1.663,
                2.108,
                2.604,
                3.143,
                3.740
            };
            int[] eurodollar_order = new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                9,
                12,
                15,
                18,
                21,
                24,
                27,
                30,
                33,
                36
            };
            double[] swap_quotes = new double[]
            {
                0.005915,
                0.009743,
                0.013015,
                0.01563,
                0.017748,
                0.01946,
                0.02083,
                0.021951,
                0.02285,
                0.023595,
                0.024231,
                0.024745,
                0.025853,
                0.026853,
                0.027307,
                0.027543,
                0.027733
            };
            string[] swap_tenors = new string[]
            {
                "1Y",
                "2Y",
                "3Y",
                "4Y",
                "5Y",
                "6Y",
                "7Y",
                "8Y",
                "9Y",
                "10Y",
                "11Y",
                "12Y",
                "15Y",
                "20Y",
                "25Y",
                "30Y",
                "40Y"
            };

            rhv.Clear();
            dates.Clear();
            dates.Add(asofdate);

            quote_ = new QuoteHandle(new QLEX.SimpleQuote(0.003109));
            tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>("3M");
            rh = new QLEX.DepositRateHelper(quote_, tenor_, (uint)fixingDays, cal_usd, bdc, eom, dc_act_360); 
            rhv.Add(rh);
            dates.Add(rh.latestDate());

            // eurodollars. it settles/starts at an IMM date
            for (i = 0; i < eurodollars.GetLength(0); i++)
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(eurodollars[i]));
                QLEX.QuoteHandle conv_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(eurodollars_conv[i]/10000));

                QLEX.Date imm_startdate = QLEX.IMM.nextDate(settlementdate, false);
                for (int j = 0; j < eurodollar_order[i]-1; j++)
                {
                    imm_startdate = QLEX.IMM.nextDate(cal_usd_gbp.advance(imm_startdate, 1, QLEX.TimeUnit.Days), false);
                }
                QLEX.Date enddate = imm_startdate + 90;
                rh = new QLEX.FuturesRateHelper(quote_, imm_startdate, enddate, dc_act_360, conv_);
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            QLEX.QuoteHandle spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(0.0));
            for (i=3; i < swap_quotes.GetLength(0); i++)        // skip 1Y,2Y,3Y
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(swap_quotes[i]));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(swap_tenors[i]);

                // fixed leg convention given; floating leg from idx
                rh = new QLEX.SwapRateHelper(quote_, tenor_, cal_usd_gbp, QLEX.Frequency.Semiannual, bdc, dc_30_360,
                    idx_usdlibor, spread_, new QLEX.Period(0, QLEX.TimeUnit.Days), new YieldTermStructureHandle(yth_ois));
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            // set reference date to today. or discount to 1
            yth = new QLEX.PiecewiseLogLinearDiscount(asofdate, rhv, dc_act_360);

            dtv.Clear();
            discv.Clear();
            foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }

            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            QLEX.YieldTermStructure yth_usdlib3m = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd_gbp);
            discv2.Clear();
            foreach (var dt in dates)
            {
                double disc = yth_usdlib3m.discount(dt);
                discv2.Add(disc);
            }

            i = 0;
            QLEX.IborIndex idx_usdlibor3m_2 = new FedFunds(new YieldTermStructureHandle(yth_usdlib3m));       // set forward term strucutre
            npv_new.Clear();
            npv_new2.Clear();

            foreach (string tr in swap_tenors)
            {
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tr);
                Date enddate = cal_usd_gbp.advance(settlementdate, tenor_);

                Schedule fixedsch = new Schedule(settlementdate, enddate, new Period(Frequency.Semiannual),
                    cal_usd_gbp, bdc, bdc, DateGeneration.Rule.Backward, eom);
                Schedule floatingsch = new Schedule(settlementdate, enddate, new Period(Frequency.Quarterly), 
                    cal_usd_gbp, bdc, bdc, DateGeneration.Rule.Backward, eom);

                VanillaSwap swap = new VanillaSwap(_VanillaSwap.Type.Payer, 1000.0, 
                    fixedsch, 0.0, dc_30_360, 
                    floatingsch, idx_usdlibor3m_2, 0.0, dc_act_360);
                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));
                swap.setPricingEngine(engine);

                double npv = swap.fairRate();
                npv_new.Add(npv);

                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = tr;
                _genswap.IsScheduleGiven = false;
                _genswap.ConstructSwap(null, idx_usdlibor3m_2);
                _genswap.qlswap_.setPricingEngine(engine);
                npv = _genswap.qlswap_.fairRate();
                npv_new2.Add(npv);
                i++;
            }

            ////////////////////////////////////////// LIB 1M /////////////////////////////////////////////////////
            QLEX.IborIndex idx_usdlib1m = new QLEX.USDLibor(new QLEX.Period(1, QLEX.TimeUnit.Months));
            double[] usdlib1m_quote = new double[]
            {
                5.808,
                7,
                7.9,
                8.789,
                9.75,
                10.6875,
                11.8705,
                12.4375,
                12.8125,
                12.625,
                12.3125,
                11.1875,
                10.8125,
                10.45,
                10.6875,
                10.875,
                11.0625
            };
            string[] usdlib1m_tenor = new string[]
            {
                "3M",
                "6M",
                "9M",
                "1Y",
                "18M",
                "2Y",
                "3Y",
                "4Y",
                "5Y",
                "6Y",
                "7Y",
                "10Y",
                "12Y",
                "15Y",
                "20Y",
                "25Y",
                "30Y"
            };

            rhv.Clear();
            dates.Clear();
            dates.Add(asofdate);

            QLEX.IborIndex idx_usdlibor1m = new QLEX.USDLibor(new QLEX.Period(1, QLEX.TimeUnit.Months));
            spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(0.0));
            for (i = 0; i < usdlib1m_quote.GetLength(0); i++) 
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(usdlib1m_quote[i]/10000.0));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(usdlib1m_tenor[i]);

                // get fixed rate
                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = usdlib1m_tenor[i];
                _genswap.IsScheduleGiven = false;
                _genswap.ConstructSwap(null, idx_usdlibor3m_2);
                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairRate();

                // fixed leg convention given; floating leg from idx
                rh = new QLEX.SwapRateHelper(npv, tenor_, cal_usd_gbp, QLEX.Frequency.Semiannual, bdc, dc_30_360,
                    idx_usdlibor1m, quote_, new QLEX.Period(0, QLEX.TimeUnit.Days), new YieldTermStructureHandle(yth_ois));
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            // set reference date to today. or discount to 1
            yth = new QLEX.PiecewiseLogLinearDiscount(asofdate, rhv, dc_act_360);

            dtv.Clear();
            discv.Clear();
            foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }

            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            QLEX.YieldTermStructure yth_usdlib1m = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd_gbp);
            discv2.Clear();
            foreach (var dt in dates)
            {
                double disc = yth_usdlib1m.discount(dt);
                discv2.Add(disc);
            }

            i = 0;
            QLEX.IborIndex idx_usdlibor1m_2 = new FedFunds(new YieldTermStructureHandle(yth_usdlib1m));       // set forward term strucutre
            npv_new.Clear();
            npv_new2.Clear();

            foreach (string tr in usdlib1m_tenor)
            {
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tr);
                Date enddate = cal_usd_gbp.advance(settlementdate, tenor_);

                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));

                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = tr;
                _genswap.IsScheduleGiven = false;
                _genswap.FirstLegIndex = "USDLIB3M";
                _genswap.FirstLegFrequency = "Quarterly";
                _genswap.FirstLegDayCounter = "Actual360";
                _genswap.SecondLegIndex = "USDLIB1M";
                _genswap.SecondLegFrequency = "Monthly";

                _genswap.ConstructSwap(idx_usdlibor3m_2, idx_usdlibor1m_2);
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairSpread() * 10000;
                npv_new.Add(npv);
                i++;
            }


            ////////////////////////////////////////// LIB 6M /////////////////////////////////////////////////////
            QLEX.IborIndex idx_usdlib6m = new QLEX.USDLibor(new QLEX.Period(6, QLEX.TimeUnit.Months));
            double[] usdlib6m_quote = new double[]
            {
                8.9375,
                8.413,
                8.0625,
                7.4375,
                7.1875,
                7.1875,
                7.25,
                7.875,
                8.3125,
                8.85,
                9.3125,
                9.3125,
                9.3125
            };
            string[] usdlib6m_tenor = new string[]
            {
                "1Y",
                "18M",
                "2Y",
                "3Y",
                "4Y",
                "5Y",
                "7Y",
                "10Y",
                "12Y",
                "15Y",
                "20Y",
                "25Y",
                "30Y"
            };

            rhv.Clear();
            dates.Clear();
            dates.Add(asofdate);

            QLEX.IborIndex idx_usdlibor6m = new QLEX.USDLibor(new QLEX.Period(6, QLEX.TimeUnit.Months));
            spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(0.0));
            for (i = 0; i < usdlib6m_quote.GetLength(0); i++)
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(usdlib6m_quote[i] / 10000.0));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(usdlib6m_tenor[i]);

                // get fixed rate
                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = usdlib6m_tenor[i];
                _genswap.IsScheduleGiven = false;
                _genswap.ConstructSwap(null, idx_usdlibor3m_2);
                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairRate();

                // fixed leg convention given; floating leg from idx
                rh = new QLEX.SwapRateHelper(npv, tenor_, cal_usd_gbp, QLEX.Frequency.Semiannual, bdc, dc_30_360,
                    idx_usdlibor6m, quote_, new QLEX.Period(0, QLEX.TimeUnit.Days), new YieldTermStructureHandle(yth_ois));
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            // set reference date to today. or discount to 1
            yth = new QLEX.PiecewiseLogLinearDiscount(asofdate, rhv, dc_act_360);

            dtv.Clear();
            discv.Clear();
            foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }

            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            QLEX.YieldTermStructure yth_usdlib6m = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd_gbp);
            discv2.Clear();
            foreach (var dt in dates)
            {
                double disc = yth_usdlib6m.discount(dt);
                discv2.Add(disc);
            }

            i = 0;
            QLEX.IborIndex idx_usdlibor6m_2 = new FedFunds(new YieldTermStructureHandle(yth_usdlib6m));       // set forward term strucutre
            npv_new.Clear();
            npv_new2.Clear();

            foreach (string tr in usdlib6m_tenor)
            {
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tr);
                Date enddate = cal_usd_gbp.advance(settlementdate, tenor_);

                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));

                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = tr;
                _genswap.IsScheduleGiven = false;
                _genswap.FirstLegIndex = "USDLIB3M";
                _genswap.FirstLegFrequency = "Quarterly";
                _genswap.FirstLegDayCounter = "Actual360";
                _genswap.SecondLegIndex = "USDLIB6M";
                _genswap.SecondLegFrequency = "Semiannual";

                _genswap.ConstructSwap(idx_usdlibor3m_2, idx_usdlibor6m_2);
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairSpread() * 10000;
                npv_new.Add(npv);
                i++;
            }

            ////////////////////////////////////////// LIB 12M /////////////////////////////////////////////////////
            QLEX.IborIndex idx_usdlib12m = new QLEX.USDLibor(new QLEX.Period(12, QLEX.TimeUnit.Months));
            double[] usdlib12m_quote = new double[]
            {
                25,
                21.5625,
                22,
                21.5,
                21.375,
                21.625,
                21.875,
                21.75,
                22.75,
                23.25,
                23,
                23
            };
            string[] usdlib12m_tenor = new string[]
            {
                "1Y",
                "2Y",
                "3Y",
                "4Y",
                "5Y",
                "7Y",
                "10Y",
                "12Y",
                "15Y",
                "20Y",
                "25Y",
                "30Y"
            };

            rhv.Clear();
            dates.Clear();
            dates.Add(asofdate);

            QLEX.IborIndex idx_usdlibor12m = new QLEX.USDLibor(new QLEX.Period(12, QLEX.TimeUnit.Months));
            spread_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(0.0));
            for (i = 0; i < usdlib12m_quote.GetLength(0); i++)
            {
                quote_ = new QLEX.QuoteHandle(new QLEX.SimpleQuote(usdlib12m_quote[i] / 10000.0));
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(usdlib12m_tenor[i]);

                // get fixed rate
                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = usdlib12m_tenor[i];
                _genswap.IsScheduleGiven = false;
                _genswap.ConstructSwap(null, idx_usdlibor3m_2);
                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairRate();

                // fixed leg convention given; floating leg from idx
                rh = new QLEX.SwapRateHelper(npv, tenor_, cal_usd_gbp, QLEX.Frequency.Semiannual, bdc, dc_30_360,
                    idx_usdlibor12m, quote_, new QLEX.Period(0, QLEX.TimeUnit.Days), new YieldTermStructureHandle(yth_ois));
                rhv.Add(rh);
                dates.Add(rh.latestDate());
            }

            // set reference date to today. or discount to 1
            yth = new QLEX.PiecewiseLogLinearDiscount(asofdate, rhv, dc_act_360);

            dtv.Clear();
            discv.Clear();
            foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }

            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            QLEX.YieldTermStructure yth_usdlib12m = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd_gbp);
            discv2.Clear();
            foreach (var dt in dates)
            {
                double disc = yth_usdlib12m.discount(dt);
                discv2.Add(disc);
            }

            i = 0;
            QLEX.IborIndex idx_usdlibor12m_2 = new FedFunds(new YieldTermStructureHandle(yth_usdlib12m));       // set forward term strucutre
            npv_new.Clear();
            npv_new2.Clear();

            foreach (string tr in usdlib12m_tenor)
            {
                tenor_ = QLEX.QLConverter.ConvertObject<QLEX.Period>(tr);
                Date enddate = cal_usd_gbp.advance(settlementdate, tenor_);

                DiscountingSwapEngine engine = new DiscountingSwapEngine(new YieldTermStructureHandle(yth_ois));

                QLEX.Instruments.InterestRateGenericSwap _genswap = new QLEX.Instruments.InterestRateGenericSwap();
                _genswap.TradeDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(asofdate));
                //_genswap.SettlementDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(sdate));
                //_genswap.MaturityDate = QLConverter.DateTimeToString(QLConverter.DateToDateTime(tdate));
                _genswap.Tenor = tr;
                _genswap.IsScheduleGiven = false;
                _genswap.FirstLegIndex = "USDLIB3M";
                _genswap.FirstLegFrequency = "Quarterly";
                _genswap.FirstLegDayCounter = "Actual360";
                _genswap.SecondLegIndex = "USDLIB12M";
                _genswap.SecondLegFrequency = "Annual";

                _genswap.ConstructSwap(idx_usdlibor3m_2, idx_usdlibor12m_2);
                _genswap.qlswap_.setPricingEngine(engine);
                double npv = _genswap.qlswap_.fairSpread() * 10000;
                npv_new.Add(npv);
                i++;
            }

            Console.WriteLine("Test complete.");
        }

        [TestMethod]
        public void TestGovBonds()
        {
            string curve = "USDGOVBOND";
            // T+0, T+1, or T+3?
            // treasury bond: Actual/Actual
            // T-bills nad MM: Actual/360
            // Corp and Muni: 30/360
            // FixedRateBondHelper
        }

        [TestMethod]
        public void TestCds()
        {
            // UpfrontCdsHelper
            // SpreadCdsHelper

            Calendar cal_cds = new WeekendsOnly();
            Date today = Settings.instance().getEvaluationDate();
            today = cal_cds.adjust(today);
            int settlementDays = 1;
            DayCounter dc = new Actual360();

            QuoteHandle hazardrate = new QuoteHandle(new SimpleQuote(0.01));
            FlatHazardRate fhr = new FlatHazardRate(today, hazardrate, dc);
            Date enddate = cal_cds.advance(today, new Period(1, TimeUnit.Years));
            double answer = fhr.defaultDensity(enddate);        // h(t,T) * Q(t,T) = - dQ(t,T)/dT = unconditional instantaneous default
            answer = fhr.hazardRate(enddate);                   // h(t,T) = E[r(t)] = instanstaneous forward conditional default
            answer = fhr.survivalProbability(enddate);          // Q(t,T) = P(t,T) zero coupon bond price
            answer = fhr.defaultProbability(enddate);           // 1 - Q(t,T)

            List<QLEX.Date> dates = new List<QLEX.Date>();
            QLEX.DateVector dtv = new QLEX.DateVector();
            QLEX.DoubleVector discv = new QLEX.DoubleVector();

            double[] quote = new double[]
            {
                0.005,
                0.006,
                0.007,
                0.009
            };

            int[] n = new int[]
            {
                1,
                2,
                3,
                5
            };

            Frequency frequency = Frequency.Quarterly;
            BusinessDayConvention convention = BusinessDayConvention.Following;
            DateGeneration.Rule rule = DateGeneration.Rule.CDS;
            DayCounter dayCounter = new Thirty360();
            double recoveryRate = 0.4;

            RelinkableYieldTermStructureHandle discountCurve = new RelinkableYieldTermStructureHandle();
            discountCurve.linkTo(new FlatForward(today, 0.06, new Actual360()));


            DefaultProbabilityHelperVector dphv = new DefaultProbabilityHelperVector();

            dates.Add(today);
            for (int i = 0; i < n.GetLength(0); i++)
            {
                DefaultProbabilityHelper dph = new SpreadCdsHelper(quote[i], new Period(n[i], TimeUnit.Years), settlementDays, cal_cds,
                    frequency, convention, rule, dayCounter, recoveryRate, discountCurve);
                dphv.Add(dph);
            }
            
            RelinkableDefaultProbabilityTermStructureHandle piecewiseCurve = new RelinkableDefaultProbabilityTermStructureHandle();
            piecewiseCurve.linkTo(new PiecewiseFlatHazardRate(today, dphv, dayCounter));

            dtv.Clear();
            discv.Clear();
            /*foreach (var dt in dates)
            {
                double disc = yth.discount(dt);
                dtv.Add(dt);
                discv.Add(disc);
            }*/

            ////////////////////////////////////////// rebuild curve and reprice /////////////////////////////////////////////////////
            /*QLEX.YieldTermStructure yth_usdlib12m = new QLEX.DiscountCurve(dtv, discv, dc_act_360, cal_usd_gbp);
            discv2.Clear();
            foreach (var dt in dates)
            {
                double disc = yth_usdlib12m.discount(dt);
                discv2.Add(disc);
            }*/

            /////////////////////// reprice /////////////////////////////////////////
            /*
            double notional = 1.0;
            double tolerance = 1.0e-6;

            // ensure apple-to-apple comparison
            Settings.instance().includeTodaysCashFlows(true);

            for (int i = 0; i < n.Length; i++)
            {
                Date protectionStart = today + settlementDays;
                Date startDate = cal_cds.adjust(protectionStart, convention);
                Date endDate = today + n[i] * TimeUnit.Years;

                Schedule schedule = new Schedule(startDate, endDate, new Period(frequency), cal_cds,
                                  convention, Unadjusted, rule, false);

            CreditDefaultSwap cds = new CreditDefaultSwap(Protection::Buyer, notional, quote[i],
                                  schedule, convention, dayCounter,
                                  true, true, protectionStart);
                PricingEngine engine = new MidPointCdsEngine();
                
            cds.setPricingEngine(boost::shared_ptr<PricingEngine>(
                           new MidPointCdsEngine(piecewiseCurve, recoveryRate,
                                                 discountCurve)));

            // test
            double inputRate = quote[i];
            double computedRate = cds.fairSpread();*/
        }

        [TestMethod]
        public void TestLoadFixing()
        {
            string[] curves = new string[] { "USDOIS", "USDLIB3M", "USDLIB1M", "USDLIB6M", "USDLIB12M" };
            string path = QLEX.ConfigManager.Instance.IRRootDir + @"Curves\";
            string name;
            foreach (var s in curves)
            {
                string[] data = System.IO.File.ReadAllLines(path + s + "_Fixing.csv");
                DateVector dt = new DateVector(data.Length);
                DoubleVector qs = new DoubleVector(data.Length);

                
                foreach (var fixing in data)
                {
                    if (fixing == "")
                        continue;

                    string[] ff = fixing.Split(',');

                    dt.Add(QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(ff[0])));
                    qs.Add(Convert.ToDouble(ff[1]));
                }
                
                name = s;
                if (!s.Contains("@"))
                    name = "IDX@" + s;

                
                RealTimeSeries fixings = new RealTimeSeries(dt, qs);
                IndexManager.instance().setHistory(name, fixings);
            }
        }

        [TestMethod]
        public void TestIRSwap()
        {
            try
            {
                QLEX.Instruments.InterestRateGenericSwap swap = new QLEX.Instruments.InterestRateGenericSwap();
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(QLEX.Instruments.InterestRateGenericSwap));
                using (var writer = new System.IO.StreamWriter(@"C:\Workspace\temp\swap.xml"))
                {
                    serializer.Serialize(writer, swap);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [TestMethod]
        public void TestCommodityCurve()
        {
            Date todaysDate = new Date(15, Month.May, 1998);
            Settings.instance().setEvaluationDate(todaysDate);
            Date settlementDate = todaysDate;

            DateVector forwardDates = new DateVector(6);  
            DoubleVector forwardPrices = new DoubleVector(6);
            for (int i = 0; i < 6; i++)
            {
                Period forwardPeriod = new Period(3 * i, TimeUnit.Months);
                Date forwardDate = settlementDate.Add(forwardPeriod);
                forwardDates.Add(forwardDate);
                forwardPrices.Add(i + 1);
            }

            PricingPeriodExts pps = new PricingPeriodExts(6);
            for (int i = 0; i < 5; i++)
            {
                PricingPeriodExt pp = new PricingPeriodExt(forwardDates[i], forwardDates[i + 1], forwardDates[i], 1.0);
                Date d = pp.paymentDate();
                double d2 = pp.quantity();
                pps.Add(pp);
            }

            CommodityCurveExt cc = new CommodityCurveExt("NG", forwardDates, forwardPrices, new NullCalendar(), new Actual365Fixed());
            DoubleVector times = cc.times();
            double interpolatedprice = cc.price(settlementDate.Add(30));

            CommodityIndexExt ci = new CommodityIndexExt("NG", cc, new NullCalendar());
            //ci.addQuote(settlementDate, 2.0);
            interpolatedprice = ci.price(settlementDate);
            interpolatedprice = ci.forwardPrice(settlementDate);
            interpolatedprice = ci.forwardPrice(settlementDate.Add(30));

            DayCounter dayCounter = new Actual365Fixed();
            YieldTermStructureHandle flatRateTSH =
                new YieldTermStructureHandle(
                                new FlatForward(settlementDate, 0.1,
                                                 dayCounter));

            EnergyFutureExt ef = new EnergyFutureExt(1, pps[1], 3.5, ci, "NG", flatRateTSH);
            ef.tradePrice();
            // ef.quantity();
            ef.NPV();
            CommodityIndexExt ci2 = ef.index();
            ci2.forwardPrice(settlementDate);


            EnergyVanillaSwapExt evs = new EnergyVanillaSwapExt(true, 2.5, ci, pps,
                "NG", flatRateTSH, flatRateTSH);
            evs.NPV();
            evs.isExpired();
            evs.payReceive();
            evs.fixedPrice();
            evs.index();

            EnergyBasisSwapExt ebs = new EnergyBasisSwapExt(ci, ci, pps, "ccgt", flatRateTSH, flatRateTSH);
            ebs.NPV();
        }

        [TestMethod]
        public void TestAbcdVolCurve()
        {
            Calendar calendar = new NullCalendar();
            Date todaysDate = Settings.instance().getEvaluationDate();
            Date endDate = calendar.advance(todaysDate, 5, TimeUnit.Years);
            Schedule dates = new Schedule(todaysDate, endDate, new Period(Frequency.Semiannual), calendar,
                BusinessDayConvention.Following, BusinessDayConvention.Following,
                DateGeneration.Rule.Backward, false);
            DayCounter dc = new SimpleDayCounter();
         
            DoubleVector t = new DoubleVector();
            for (int i = 0; i < dates.size(); i++)
            {
                t.Add(dc.yearFraction(todaysDate, dates.date((uint)i)));
            }
            DoubleVector vol = new DoubleVector();
            double[] voldata = new double[]
                {
                    0.15541283,
                    0.18719678,
                    0.20890740,
                    0.22318179,
                    0.23212717,
                    0.23731450,
                    0.23988649,
                    0.24066384,
                    0.24023111,
                    0.23900189,
                    0.23726699
                    //0.23522952,
                    //0.23303022,
                    //0.23076564,
                    //0.22850101,
                    //0.22627951,
                    //0.22412881,
                    //0.22206569,
                    //0.22009939
                };
            for (int i = 0; i < voldata.Length; i++)
            {
                vol.Add(voldata[i]);
            }
            
            AbcdCalibration instVol = new AbcdCalibration(t, vol);
            double a0 = instVol.a();
            double b0 = instVol.b();
            double c0 = instVol.c();
            double d0 = instVol.d();

            instVol.compute();
            double a1 = instVol.a();
            double b1 = instVol.b();
            double c1 = instVol.c();
            double d1 = instVol.d();

            List<double> ret = new List<double>();
            for (int i = 0; i < vol.Count; i++)
            {
                ret.Add(instVol.value(t[i]));
            }
        }

        [TestMethod]
        public void TestHullWhiteSimulation()
        {
            Date asofDate = new Date((int)DateTime.Today.ToOADate());
            double a = 0.0464041, sigma = 0.00579912;

            YieldTermStructure ts = new FlatForward(asofDate, 0.04875825, new Actual365Fixed());
            QLEX.Calendar calendar = new UnitedStates();

            Date TDate = calendar.advance(asofDate, 7, TimeUnit.Years);
            Schedule fixedschedule = new Schedule(asofDate, TDate, new Period(Frequency.Semiannual), calendar, BusinessDayConvention.ModifiedFollowing, BusinessDayConvention.ModifiedFollowing,
                DateGeneration.Rule.Forward, true);
            Schedule floatschedule = new Schedule(asofDate, TDate, new Period(Frequency.Quarterly), calendar, BusinessDayConvention.ModifiedFollowing, BusinessDayConvention.ModifiedFollowing,
                DateGeneration.Rule.Forward, true);
            RelinkableYieldTermStructureHandle usdForwardingTSHandle = new RelinkableYieldTermStructureHandle(ts);
            IborIndex usd3mIndex = new IborIndex("USDLIB3M", new Period(Frequency.Quarterly), 0, new USDCurrency(), calendar, BusinessDayConvention.ModifiedFollowing, true, new Actual365Fixed(), usdForwardingTSHandle);

            VanillaSwap swap = new VanillaSwap(_VanillaSwap.Type.Payer, 1000, fixedschedule, 0.049, new Actual365Fixed(), floatschedule, usd3mIndex, 0.0, new Actual365Fixed());
            DiscountingSwapEngine pricingEngine = new DiscountingSwapEngine(usdForwardingTSHandle);
            swap.setPricingEngine(pricingEngine);
            double rate = swap.fairRate();

            InstrumentVector portfolio = new InstrumentVector();
            portfolio.Add(swap);
            QLEX.RiskHullWhiteSimulationEngine engine = new QLEX.RiskHullWhiteSimulationEngine(DateTime.Today, new YieldTermStructureHandle(ts), a, sigma, portfolio, usd3mIndex, usdForwardingTSHandle);
            engine.SampleSize = 300;
            engine.calculate();
            Dictionary<Date, double> ret = engine.getPFECurve(0.95);
        }
    }
}
