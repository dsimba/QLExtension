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

public class RelinkableDeltaVolQuoteHandle : DeltaVolQuoteHandle {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal RelinkableDeltaVolQuoteHandle(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.RelinkableDeltaVolQuoteHandle_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RelinkableDeltaVolQuoteHandle obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RelinkableDeltaVolQuoteHandle() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_RelinkableDeltaVolQuoteHandle(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public RelinkableDeltaVolQuoteHandle(SWIGTYPE_p_boost__shared_ptrT_DeltaVolQuote_t arg0) : this(NQuantLibcPINVOKE.new_RelinkableDeltaVolQuoteHandle__SWIG_0(SWIGTYPE_p_boost__shared_ptrT_DeltaVolQuote_t.getCPtr(arg0)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public RelinkableDeltaVolQuoteHandle() : this(NQuantLibcPINVOKE.new_RelinkableDeltaVolQuoteHandle__SWIG_1(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void linkTo(SWIGTYPE_p_boost__shared_ptrT_DeltaVolQuote_t arg0) {
    NQuantLibcPINVOKE.RelinkableDeltaVolQuoteHandle_linkTo(swigCPtr, SWIGTYPE_p_boost__shared_ptrT_DeltaVolQuote_t.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
