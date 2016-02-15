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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Xl = Microsoft.Office.Interop.Excel;
using ExcelDna.Integration;
using ExcelDna.ComInterop;
using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;


namespace QLExcel
{
    [ComVisible(true)]
    public class Startup : IExcelAddIn
    {
        public void AutoOpen()
        {
            string xllName = (string)XlCall.Excel(XlCall.xlGetName);
            string rootPath = System.IO.Path.GetDirectoryName(xllName);
            rootPath = System.IO.Path.Combine(rootPath, @"..\..\");
            rootPath = System.IO.Path.GetFullPath(rootPath);
            QLEX.ConfigManager.Instance.RootDir = rootPath;

            System.Windows.Forms.MessageBox.Show("Welcome to QLExcel.");
            // System.Windows.Forms.MessageBox.Show("QLExcel Loaded from " + xllName);
            // System.Windows.Forms.MessageBox.Show("Root Path is " + rootPath);

            ComServer.DllRegisterServer();
        }

        public void AutoClose()
        {
            //QLEX.ConfigManager.Instance.REngine.Dispose();
            ComServer.DllUnregisterServer();
            System.Windows.Forms.MessageBox.Show("Thanks for using QLExcel");
        }
    }
}
