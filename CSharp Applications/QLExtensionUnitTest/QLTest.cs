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
            DateTime asofdate = new DateTime(2015, 7, 28);
            DateTime newdate = new DateTime(2016, 12, 12);
            string[] curves = new string[] { "USDOIS", "USDLIB3M", "USDLIB1M", "USDLIB6M", "USDLIB12M" };
            string path;
            QLEX.IborIndex idx = new QLEX.USDLibor(new QLEX.Period(3, QLEX.TimeUnit.Months));

            foreach (var s in curves)
            {
                string targetcurve;
                if (s.Contains("@"))
                {
                    targetcurve = s;
                }
                else
                {
                    targetcurve = "CRV@" + s;
                }

                path = QLEX.ConfigManager.Instance.IRRootDir + @"Curves\"
                    + QLEX.QLConverter.DateTimeToString(asofdate) + "\\";

                string[] crv = System.IO.File.ReadAllLines(path + s + ".csv");

                QLEX.DateVector dtv = new QLEX.DateVector();
                QLEX.DoubleVector discv = new QLEX.DoubleVector();
                foreach (var c in crv)
                {
                    string[] cc = c.Split(',');
                    Date dt = new Date(Convert.ToInt32(cc[0]));
                    dtv.Add(dt);
                    discv.Add(Convert.ToDouble(cc[1]));
                }

                QLEX.YieldTermStructure yth = new QLEX.LinearDiscountCurve(dtv, discv, idx.dayCounter(), idx.fixingCalendar());
                double dis = yth.discount(QLEX.QLConverter.DateTimeToDate(newdate));

            }
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
