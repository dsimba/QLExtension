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
            SettlementDate = "20150723";
            MaturityDate = "20200723";
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
            FirstLegNotionals.Add(0);
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
            SecondLegNotionals.Add(0);
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
        // do it in xll
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
