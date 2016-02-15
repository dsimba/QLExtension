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

public class BlackVarianceCurve : BlackVolTermStructure {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal BlackVarianceCurve(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.BlackVarianceCurve_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(BlackVarianceCurve obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~BlackVarianceCurve() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_BlackVarianceCurve(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public BlackVarianceCurve(Date referenceDate, DateVector dates, DoubleVector volatilities, DayCounter dayCounter, bool forceMonotoneVariance) : this(NQuantLibcPINVOKE.new_BlackVarianceCurve__SWIG_0(Date.getCPtr(referenceDate), DateVector.getCPtr(dates), DoubleVector.getCPtr(volatilities), DayCounter.getCPtr(dayCounter), forceMonotoneVariance), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public BlackVarianceCurve(Date referenceDate, DateVector dates, DoubleVector volatilities, DayCounter dayCounter) : this(NQuantLibcPINVOKE.new_BlackVarianceCurve__SWIG_1(Date.getCPtr(referenceDate), DateVector.getCPtr(dates), DoubleVector.getCPtr(volatilities), DayCounter.getCPtr(dayCounter)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
