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

public class CallableFixedRateBond : Bond {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal CallableFixedRateBond(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.CallableFixedRateBond_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CallableFixedRateBond obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CallableFixedRateBond() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_CallableFixedRateBond(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public CallableFixedRateBond(int settlementDays, double faceAmount, Schedule schedule, DoubleVector coupons, DayCounter accrualDayCounter, BusinessDayConvention paymentConvention, double redemption, Date issueDate, SWIGTYPE_p_CallabilitySchedule putCallSchedule) : this(NQuantLibcPINVOKE.new_CallableFixedRateBond(settlementDays, faceAmount, Schedule.getCPtr(schedule), DoubleVector.getCPtr(coupons), DayCounter.getCPtr(accrualDayCounter), (int)paymentConvention, redemption, Date.getCPtr(issueDate), SWIGTYPE_p_CallabilitySchedule.getCPtr(putCallSchedule)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
