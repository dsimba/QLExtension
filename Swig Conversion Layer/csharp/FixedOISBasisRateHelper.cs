//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.8
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace QLEX {

public class FixedOISBasisRateHelper : RateHelper {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FixedOISBasisRateHelper(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.FixedOISBasisRateHelper_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FixedOISBasisRateHelper obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FixedOISBasisRateHelper() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_FixedOISBasisRateHelper(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public FixedOISBasisRateHelper(int settlementDays, Period tenor, QuoteHandle overnightSpread, QuoteHandle fixedRate, Frequency fixedFrequency, BusinessDayConvention fixedConvention, DayCounter fixedDayCount, OvernightIndex overnightIndex, Frequency overnightFrequency, YieldTermStructureHandle discountingCurve) : this(NQuantLibcPINVOKE.new_FixedOISBasisRateHelper__SWIG_0(settlementDays, Period.getCPtr(tenor), QuoteHandle.getCPtr(overnightSpread), QuoteHandle.getCPtr(fixedRate), (int)fixedFrequency, (int)fixedConvention, DayCounter.getCPtr(fixedDayCount), OvernightIndex.getCPtr(overnightIndex), (int)overnightFrequency, YieldTermStructureHandle.getCPtr(discountingCurve)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FixedOISBasisRateHelper(int settlementDays, Period tenor, QuoteHandle overnightSpread, QuoteHandle fixedRate, Frequency fixedFrequency, BusinessDayConvention fixedConvention, DayCounter fixedDayCount, OvernightIndex overnightIndex, Frequency overnightFrequency) : this(NQuantLibcPINVOKE.new_FixedOISBasisRateHelper__SWIG_1(settlementDays, Period.getCPtr(tenor), QuoteHandle.getCPtr(overnightSpread), QuoteHandle.getCPtr(fixedRate), (int)fixedFrequency, (int)fixedConvention, DayCounter.getCPtr(fixedDayCount), OvernightIndex.getCPtr(overnightIndex), (int)overnightFrequency), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public Swap swap() {
    Swap ret = new Swap(NQuantLibcPINVOKE.FixedOISBasisRateHelper_swap(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
