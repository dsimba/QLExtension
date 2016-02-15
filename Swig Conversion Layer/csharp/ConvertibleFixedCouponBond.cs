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

public class ConvertibleFixedCouponBond : Bond {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ConvertibleFixedCouponBond(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.ConvertibleFixedCouponBond_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ConvertibleFixedCouponBond obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ConvertibleFixedCouponBond() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_ConvertibleFixedCouponBond(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public ConvertibleFixedCouponBond(Exercise exercise, double conversionRatio, DividendSchedule dividends, CallabilitySchedule callability, QuoteHandle creditSpread, Date issueDate, int settlementDays, DoubleVector coupons, DayCounter dayCounter, Schedule schedule, double redemption) : this(NQuantLibcPINVOKE.new_ConvertibleFixedCouponBond__SWIG_0(Exercise.getCPtr(exercise), conversionRatio, DividendSchedule.getCPtr(dividends), CallabilitySchedule.getCPtr(callability), QuoteHandle.getCPtr(creditSpread), Date.getCPtr(issueDate), settlementDays, DoubleVector.getCPtr(coupons), DayCounter.getCPtr(dayCounter), Schedule.getCPtr(schedule), redemption), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ConvertibleFixedCouponBond(Exercise exercise, double conversionRatio, DividendSchedule dividends, CallabilitySchedule callability, QuoteHandle creditSpread, Date issueDate, int settlementDays, DoubleVector coupons, DayCounter dayCounter, Schedule schedule) : this(NQuantLibcPINVOKE.new_ConvertibleFixedCouponBond__SWIG_1(Exercise.getCPtr(exercise), conversionRatio, DividendSchedule.getCPtr(dividends), CallabilitySchedule.getCPtr(callability), QuoteHandle.getCPtr(creditSpread), Date.getCPtr(issueDate), settlementDays, DoubleVector.getCPtr(coupons), DayCounter.getCPtr(dayCounter), Schedule.getCPtr(schedule)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
