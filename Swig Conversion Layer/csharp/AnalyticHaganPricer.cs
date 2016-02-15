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

public class AnalyticHaganPricer : CmsCouponPricer {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal AnalyticHaganPricer(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.AnalyticHaganPricer_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AnalyticHaganPricer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AnalyticHaganPricer() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_AnalyticHaganPricer(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public AnalyticHaganPricer(SwaptionVolatilityStructureHandle v, GFunctionFactory.YieldCurveModel model, QuoteHandle meanReversion) : this(NQuantLibcPINVOKE.new_AnalyticHaganPricer(SwaptionVolatilityStructureHandle.getCPtr(v), (int)model, QuoteHandle.getCPtr(meanReversion)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
