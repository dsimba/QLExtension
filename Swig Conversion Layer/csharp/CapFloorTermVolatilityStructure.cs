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

public class CapFloorTermVolatilityStructure : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CapFloorTermVolatilityStructure(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CapFloorTermVolatilityStructure obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CapFloorTermVolatilityStructure() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_CapFloorTermVolatilityStructure(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public SWIGTYPE_p_CapFloorTermVolatilityStructure __deref__() {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure___deref__(swigCPtr);
    SWIGTYPE_p_CapFloorTermVolatilityStructure ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_CapFloorTermVolatilityStructure(cPtr, false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isNull() {
    bool ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_isNull(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Observable asObservable() {
    Observable ret = new Observable(NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_asObservable(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CapFloorTermVolatilityStructure() : this(NQuantLibcPINVOKE.new_CapFloorTermVolatilityStructure(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public double volatility(Period length, double strike, bool extrapolate) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_0(swigCPtr, Period.getCPtr(length), strike, extrapolate);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double volatility(Period length, double strike) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_1(swigCPtr, Period.getCPtr(length), strike);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double volatility(Date end, double strike, bool extrapolate) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_2(swigCPtr, Date.getCPtr(end), strike, extrapolate);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double volatility(Date end, double strike) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_3(swigCPtr, Date.getCPtr(end), strike);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double volatility(double end, double strike, bool extrapolate) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_4(swigCPtr, end, strike, extrapolate);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double volatility(double end, double strike) {
    double ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_volatility__SWIG_5(swigCPtr, end, strike);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void enableExtrapolation() {
    NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_enableExtrapolation(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void disableExtrapolation() {
    NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_disableExtrapolation(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool allowsExtrapolation() {
    bool ret = NQuantLibcPINVOKE.CapFloorTermVolatilityStructure_allowsExtrapolation(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
