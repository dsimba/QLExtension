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
    public class FreeMarketData
    {
        [ExcelFunction(Description = "Historical quotes from Yahoo or Google.", Category = "QLExcel - Data")]
        public static object[,] qlDataHistoricalQuotes(
            [ExcelArgument(Description = "Security/Ticker ID.", Name = "security_id")] string secId,
            [ExcelArgument("Start date, defaults to one year ago.", Name = "start_date")] double dblStartDate,
            [ExcelArgument("End date, defaults to today.", Name = "end_date")] double dblEndDate,
            [ExcelArgument("d, w, m, y. Defaults to d = daily.")] string period,
            [ExcelArgument("sort dates in ascending chronological order? Defaults to true.")] bool isDecending
            )
        {
            try
            {
                DateTime startDate = (dblStartDate == 0) ? DateTime.Today.AddYears(-1) : DateTime.FromOADate(dblStartDate);
                DateTime endDate = (dblEndDate == 0) ? DateTime.Today : DateTime.FromOADate(dblEndDate);

                return QLEX.Broker.GetHistoricalQuotes("YAHOO", secId, startDate, endDate, period, isDecending);
            }
            catch (Exception e)
            {
                return new object[,] { { e.Message} };
            }
            
        }
    }
}
