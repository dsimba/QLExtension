QLExtension
============

## Proejct Description
An Excel Add-in tool for pricing and risk management. It uses QuantLib as the pricing engine for interest rate products, CDS, equities, and commodities. Simulation engine extends QuantLib into applications such as PFEs and CVAs.

## Project Summary
This is a project built on top of QuantLib. Therefore it requires boost library and QuantLib library.
It also needs QuantLib-Swig and ExcelDna in order to develop Excel Add-ins.
Current worksheets include Interest rate dual curve bootstrapping, CDS curve, RTD real time stock quotes, PFE simulations.

### Keywords
<b>Quantitative</b>: Mathematical Finance, Asset Pricing, Derivatives, Risk Management.

<b>Technical</b>: QuantLib, ExcelDna, C++/C#, SWIG, RTD.

### Run Compiled
Download and upzip the file Compiled.zip located in the project root directory. Start Excel and open QLExcel-addin-x86.xll from where you unzipped the file. A Ribbon tab called QLExLIB should show up. All functions start with ql, for example, call Excel function qltimetoday() it will return today's date. There are demos such as Black-Scholes and Dual Curve bootstrapping in the Demo and IRDemo workbooks dropdown menu respectively.

### Run Source Code
(1) Put boost, compiled QuantLib, and swigwin in folder c:\CLib. If you use different path, you have to change project reference path accordingly.

(2) Download and build the solution.

(3) Open the xll file from Excel and the Ribbon will show up.

### Figures
![alt tag](https://letianquant.files.wordpress.com/2016/02/qldemo3.png)
