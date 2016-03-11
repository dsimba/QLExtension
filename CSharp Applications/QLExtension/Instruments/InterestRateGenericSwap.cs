using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using QLEX;

namespace QLEX.Instruments
{
    [Serializable]
    public class InterestRateGenericSwap
    {
        #region Common area
        // [XmlElement("ContractId")]
        public string ContractId { get; set; }

        public string Entity { get; set; }
        public string EntityID { get; set; }
        public string Counterparty { get; set; }
        public string CounterpartyID { get; set; }

        public string SwapType { get; set; }                // Payer / Receiver

        public int FixingDays { get; set; }
        public string TradeDate { get; set; }               // trade date in yyyyMMdd
        public string SettlementDate { get; set; }              // (forward) Start date, in yyyyMMdd
        public string MaturityDate { get; set; }                // maturity date, in yyyyMMdd
        public string Tenor { get; set; }                   // 1Y, 6Y, 6M, etc
        
        #endregion

        #region First Leg
        public string FirstLegIndex { get; set; }           // FIXED, USDOIS, USDLIB3M, etc
        public string FirstLegFrequency { get; set; }           // Base leg/Fixed Leg, e.g. 6M
        public string FirstLegConvention { get; set; }
        public string FirstLegCalendar { get; set; }
        public string FirstLegDayCounter { get; set; }
        public string FirstLegDateGenerationRule { get; set; }
        public bool FirstLegEOM { get; set; }
        public List<double> FirstLegNotionals {get; set;}      // from start to end, n+1 days and n periods ==> n (beginning) notionals
        public List<string> FirstLegSchedules { get; set; }            // flexible schedules, e.g., counting for stub

        public double FixedRate { get; set; }
        
        #endregion

        #region Second Leg
        public string SecondLegIndex { get; set; }          // USDLIB3M, USDLIB6M, etc
        public string SecondLegFrequency { get; set; }          // floating/basis leg, e.g. 1M
        public string SecondLegConvention { get; set; }
        public string SecondLegCalendar { get; set; }
        public string SecondLegDayCounter { get; set; }
        public string SecondLegDateGenerationRule { get; set; }
        public bool SecondLegEOM { get; set; }
        public List<double> SecondLegNotionals {get; set;}
        public List<string> SecondLegSchedules { get; set; }             // flexible schedules, e.g., counting for stub

        public double Spread { get; set; }

        [XmlIgnore]
        public QLEX.GenericSwap qlswap_ = null;
        #endregion

        public bool IsScheduleGiven { get; set; } 
  
        public InterestRateGenericSwap()
        {
            ContractId = "DEMO000";
            Entity = "GS";
            EntityID = "";
            Counterparty = "JP Morgan";
            CounterpartyID = "";

            SwapType = "Payer";
            
            FixingDays = 2;
            TradeDate = "20150721";
            SettlementDate = "";
            MaturityDate = "";
            Tenor = "5Y";

            FirstLegIndex = "FIXED";
            FirstLegFrequency = "Semiannual";
            FirstLegConvention = "ModifiedFollowing";
            FirstLegCalendar = "NYC";
            FirstLegDayCounter = "Thirty360";
            FirstLegDateGenerationRule = "Backward";
            FirstLegEOM = true;
            FirstLegNotionals = new List<double>();
            FirstLegNotionals.Add(1e6);
            //FirstLegNotionals.Add(1e6);             // the last one is used if size() < # of periods
            FirstLegSchedules = new List<string>();
            FirstLegSchedules.Add(SettlementDate);
            FirstLegSchedules.Add(MaturityDate);
            FixedRate = 0.04;

            SecondLegIndex = "USDLIB3M";
            SecondLegFrequency = "Quarterly";
            SecondLegConvention = "ModifiedFollowing";
            SecondLegCalendar = "NYC";
            SecondLegDayCounter = "Actual360";
            SecondLegDateGenerationRule = "Backward";
            SecondLegNotionals = new List<double>();
            SecondLegNotionals.Add(1e6);
            //SecondLegNotionals.Add(1e6);
            SecondLegSchedules = new List<string>();
            SecondLegSchedules.Add(SettlementDate);
            SecondLegSchedules.Add(MaturityDate);
            SecondLegEOM = true;
            
            Spread = 0.0;

            IsScheduleGiven = true;
        }

        public double Notional()
        {
            if (FirstLegNotionals[0] == SecondLegNotionals[0])
                return FirstLegNotionals[0];
            else
                return 0.0;
        }

        public bool IsValid()
        {
            if (IsScheduleGiven)
            {
                if ( (FirstLegNotionals.Count != FirstLegSchedules.Count) 
                    || (SecondLegNotionals.Count != SecondLegSchedules.Count) )
                {
                    return false;
                }
            }

            return true;
        }

        #region conversion
        public void ConstructSwap(IborIndex idx1 = null, IborIndex idx2 = null)
        {
            _GenericSwap.Type type = SwapType == "Payer" ? _GenericSwap.Type.Payer : _GenericSwap.Type.Receiver;
            QLEX.Calendar cal_gbp = new QLEX.UnitedKingdom(QLEX.UnitedKingdom.Market.Exchange);
            bool hasois = SecondLegIndex.Contains("OIS") ? true : false;            // OIS is only in FIXED-OIS pair

            if (IsScheduleGiven)
            {
                //************************** First Leg *******************************                    
                DoubleVector notional1 = new DoubleVector();
                foreach (var nl in FirstLegNotionals)
                {
                    notional1.Add(nl);
                }
                DateVector sch1 = new DateVector();
                foreach (var dt in FirstLegSchedules)
                {
                    sch1.Add(QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(dt)));
                }
                
                Calendar cal1 = QLEX.QLConverter.ConvertObject<Calendar>(FirstLegCalendar);
                if (!hasois)
                {
                    cal1 = new QLEX.JointCalendar(cal_gbp, cal1, QLEX.JointCalendarRule.JoinHolidays);
                }

                DayCounter dc1 = QLEX.QLConverter.ConvertObject<DayCounter>(FirstLegDayCounter);
                BusinessDayConvention bdc1 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(FirstLegConvention);
                Schedule Schedule1 = new Schedule(sch1, cal1, bdc1);

                //************************** Second Leg *******************************                 
                DoubleVector notional2 = new DoubleVector();
                foreach (var nl in SecondLegNotionals)
                {
                    notional2.Add(nl);
                }
                DateVector sch2 = new DateVector();
                foreach (var dt in SecondLegSchedules)
                {
                    sch2.Add(QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(dt)));
                }

                Calendar cal2 = QLEX.QLConverter.ConvertObject<Calendar>(SecondLegCalendar);
                if (!hasois)
                {
                    cal2 = new QLEX.JointCalendar(cal_gbp, cal2, QLEX.JointCalendarRule.JoinHolidays);
                }

                DayCounter dc2 = QLEX.QLConverter.ConvertObject<DayCounter>(SecondLegDayCounter);
                BusinessDayConvention bdc2 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(SecondLegConvention);
                Schedule Schedule2 = new Schedule(sch2, cal2, bdc2);

                //************************** swap ******************************
                if (FirstLegIndex == "FIXED")
                {
                    if (hasois)
                    {
                        qlswap_ = new GenericSwap(type, notional2, Schedule2, Schedule2, FixedRate, dc2,
                                                 notional2, Schedule2, Schedule2, idx2, dc2, Spread, hasois);
                    }
                    else
                    {
                        qlswap_ = new GenericSwap(type, notional1, Schedule1, Schedule1, FixedRate, dc1,
                                                 notional2, Schedule2, Schedule2, idx2, dc2, Spread, hasois);
                    }
                    
                }
                else
                {
                    qlswap_ = new GenericSwap(type, notional1, Schedule1, Schedule1, idx1, dc1,
                                                 notional2, Schedule2, Schedule2, idx2, dc2, 0.0, Spread, hasois);
                }
            }  // end of schedule given swap construction
            else
            {
                //************************** First Leg *******************************                    
                Calendar cal1 = QLEX.QLConverter.ConvertObject<Calendar>(FirstLegCalendar);
                if (!hasois)
                {
                    cal1 = new QLEX.JointCalendar(cal_gbp, cal1, QLEX.JointCalendarRule.JoinHolidays);
                }
                DayCounter dc1 = QLEX.QLConverter.ConvertObject<DayCounter>(FirstLegDayCounter);
                BusinessDayConvention bdc1 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(FirstLegConvention);
                Date tradedate = QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(TradeDate));
                tradedate = cal1.adjust(tradedate);

                Date startdate;
                if (string.IsNullOrEmpty(SettlementDate))
                {
                    startdate = cal1.advance(tradedate, FixingDays, TimeUnit.Days);
                }
                else
                {
                    startdate = QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(SettlementDate));
                }

                Date terminatedate;
                if (string.IsNullOrEmpty(MaturityDate))
                {
                    // then tenor should not be zero
                    Period tenor = QLEX.QLConverter.ConvertObject<Period>(Tenor);
                    //Calendar nullcal = new NullCalendar();
                    terminatedate = cal1.advance(startdate, tenor);
                }
                else
                {
                    terminatedate = QLEX.QLConverter.DateTimeToDate(QLEX.QLConverter.StringToDateTime(MaturityDate));
                }

                DateGeneration.Rule dgr1 = QLEX.QLConverter.ConvertObject<DateGeneration.Rule>(FirstLegDateGenerationRule);

                Frequency freq1 = QLEX.QLConverter.ConvertObject<Frequency>(FirstLegFrequency);
                Schedule schedule1 = new Schedule(startdate, terminatedate, new Period(freq1), cal1,
                    bdc1, bdc1, dgr1, FirstLegEOM);

                //************************** Second Leg *******************************
                Calendar cal2 = QLEX.QLConverter.ConvertObject<Calendar>(SecondLegCalendar);
                if (!hasois)
                {
                    cal2 = new QLEX.JointCalendar(cal_gbp, cal2, QLEX.JointCalendarRule.JoinHolidays);
                }
                DayCounter dc2 = QLEX.QLConverter.ConvertObject<DayCounter>(SecondLegDayCounter);
                BusinessDayConvention bdc2 = QLEX.QLConverter.ConvertObject<BusinessDayConvention>(SecondLegConvention);


                DateGeneration.Rule dgr2 = QLEX.QLConverter.ConvertObject<DateGeneration.Rule>(SecondLegDateGenerationRule);

                Frequency freq2 = QLEX.QLConverter.ConvertObject<Frequency>(SecondLegFrequency);
                Schedule schedule2 = new Schedule(startdate, terminatedate, new Period(freq2), cal2,
                    bdc2, bdc2, dgr2, SecondLegEOM);

                //************************** swap ******************************
                DoubleVector notional1 = new DoubleVector();
                DoubleVector notional2 = new DoubleVector();
                foreach (var nl in FirstLegNotionals)
                {
                    notional1.Add(nl);
                }
                foreach (var nl in SecondLegNotionals)
                {
                    notional2.Add(nl);
                }

                if (FirstLegIndex == "FIXED")
                {
                    if (hasois)
                    {
                        qlswap_ = new GenericSwap(type, notional2, schedule2, schedule2, FixedRate, dc2,
                                                 notional2, schedule2, schedule2, idx2, dc2, Spread, hasois);
                    }
                    else
                    {
                        qlswap_ = new GenericSwap(type, notional1, schedule1, schedule1, FixedRate, dc1,
                                                 notional2, schedule2, schedule2, idx2, dc2, Spread, hasois);
                    }
                }
                else
                {
                    qlswap_ = new GenericSwap(type, notional1, schedule1, schedule1, idx1, dc1,
                                                 notional2, schedule2, schedule2, idx2, dc2, 0.0, Spread, hasois);
                }
            } // end of schedule not given swap construction
        }
        #endregion

        #region serialization
        public static void Serialize(InterestRateGenericSwap swap, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InterestRateGenericSwap));
            System.IO.TextWriter writer = new System.IO.StreamWriter(filename);
            serializer.Serialize(writer, swap);
            writer.Close();
        }

        public static InterestRateGenericSwap Deserialize(string filename)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(InterestRateGenericSwap));

            InterestRateGenericSwap swap = (InterestRateGenericSwap)serializer.Deserialize(reader);
            reader.Close();
            return swap;
        }
        #endregion
    }
}
