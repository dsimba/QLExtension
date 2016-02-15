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
using System.Threading.Tasks;
using System.Reflection;
using Xl = Microsoft.Office.Interop.Excel;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;

namespace QLExcel
{
    public sealed class Version
    {
        [ExcelFunction(Description = "Display xll build time", IsMacroType = true, Category = "QLExcel - Operation")]
        public static DateTime qlOpLibXllBuildTime()
        {
            DateTime buildTime = new DateTime();
            try
            {
                buildTime = getXllBuildTime();
            }
            catch (Exception ex)
            {
                ExcelUtil.logError("", "", ex.Message);
            }
            return buildTime;
        }

        [ExcelFunction(Description = "Display who built xll", IsMacroType = true, Category = "QLExcel - Operation")]
        public static string qlOpLibXllBuiltBy()
        {
            string user;
            try
            {
                user = getBuildUser();
            }
            catch (Exception ex)
            {
                ExcelUtil.logError("", "", ex.Message);
                user = ex.Message;
            }
            return user;
        }

        [ExcelFunction(Description = "Display xll version", IsMacroType = true, Category = "QLExcel - Operation")]
        public static string qlOpVersion()
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                return getVerStr();
            }
            catch (Exception exception_)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), exception_.Message);
                return "";
            }
        }

        [ExcelFunction(Description = "Display xll Path", IsMacroType = true, Category = "QLExcel - Operation")]
        public static string qlOpRootPath(
            [ExcelArgument(Description = "Method 1 or 2 ")]double method)
        {
            if (ExcelUtil.CallFromWizard())
                return "";

            string callerAddress = "";
            callerAddress = ExcelUtil.getActiveCellAddress();
            OHRepository.Instance.removeErrorMessage(callerAddress);

            try
            {
                if (method == 1.0)
                {
                    return getXllPath();
                }
                else
                {
                    return QLEX.ConfigManager.Instance.RootDir;
                }
            }
            catch (Exception e)
            {
                ExcelUtil.logError(callerAddress, System.Reflection.MethodInfo.GetCurrentMethod().Name.ToString(), e.Message);
                return "";
            }
        }

        public static string getVerStr()
        {
            string readme = System.Environment.GetEnvironmentVariable("QLExcelInstallDir") + "/documents/README.txt";
            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(readme);
            string text = objReader.ReadLine();

            if (!(text.Contains("version") || text.Contains("Version")))
            {
                text = objReader.ReadLine();
            }
            string ver = text.Substring(text.IndexOf("ersion") + 7, 6);

            return ver;
        }

        public static string getXllPath()
        {
            string appName = new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath;
            string path = System.IO.Path.GetDirectoryName(appName);
            appName = System.IO.Path.Combine(path, "QLExcel.xll");
            return appName;
        }

        public static string getBuildUser()
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(BuildInfoAttribute), false);
            BuildInfoAttribute attribute = (BuildInfoAttribute)attributes[0];
            return attribute.UserName;
        }

        public static DateTime getXllBuildTime()
        {
            DateTime buildDate = new System.IO.FileInfo(getXllPath()).LastWriteTime;
            return buildDate;
        }
    }
}
