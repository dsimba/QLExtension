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

public class FixedRateCoupon : Coupon {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FixedRateCoupon(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.FixedRateCoupon_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FixedRateCoupon obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FixedRateCoupon() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_FixedRateCoupon(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public FixedRateCoupon(Date paymentDate, double nominal, double rate, DayCounter dayCounter, Date startDate, Date endDate, Date refPeriodStart, Date refPeriodEnd, Date exCouponDate) : this(NQuantLibcPINVOKE.new_FixedRateCoupon__SWIG_0(Date.getCPtr(paymentDate), nominal, rate, DayCounter.getCPtr(dayCounter), Date.getCPtr(startDate), Date.getCPtr(endDate), Date.getCPtr(refPeriodStart), Date.getCPtr(refPeriodEnd), Date.getCPtr(exCouponDate)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FixedRateCoupon(Date paymentDate, double nominal, double rate, DayCounter dayCounter, Date startDate, Date endDate, Date refPeriodStart, Date refPeriodEnd) : this(NQuantLibcPINVOKE.new_FixedRateCoupon__SWIG_1(Date.getCPtr(paymentDate), nominal, rate, DayCounter.getCPtr(dayCounter), Date.getCPtr(startDate), Date.getCPtr(endDate), Date.getCPtr(refPeriodStart), Date.getCPtr(refPeriodEnd)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FixedRateCoupon(Date paymentDate, double nominal, double rate, DayCounter dayCounter, Date startDate, Date endDate, Date refPeriodStart) : this(NQuantLibcPINVOKE.new_FixedRateCoupon__SWIG_2(Date.getCPtr(paymentDate), nominal, rate, DayCounter.getCPtr(dayCounter), Date.getCPtr(startDate), Date.getCPtr(endDate), Date.getCPtr(refPeriodStart)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FixedRateCoupon(Date paymentDate, double nominal, double rate, DayCounter dayCounter, Date startDate, Date endDate) : this(NQuantLibcPINVOKE.new_FixedRateCoupon__SWIG_3(Date.getCPtr(paymentDate), nominal, rate, DayCounter.getCPtr(dayCounter), Date.getCPtr(startDate), Date.getCPtr(endDate)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public InterestRate interestRate() {
    InterestRate ret = new InterestRate(NQuantLibcPINVOKE.FixedRateCoupon_interestRate(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
