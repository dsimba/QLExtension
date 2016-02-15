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

public class Seasonality : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Seasonality(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Seasonality obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Seasonality() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_Seasonality(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public SWIGTYPE_p_Seasonality __deref__() {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.Seasonality___deref__(swigCPtr);
    SWIGTYPE_p_Seasonality ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Seasonality(cPtr, false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isNull() {
    bool ret = NQuantLibcPINVOKE.Seasonality_isNull(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Seasonality() : this(NQuantLibcPINVOKE.new_Seasonality(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public double correctZeroRate(Date d, double r, SWIGTYPE_p_InflationTermStructure iTS) {
    double ret = NQuantLibcPINVOKE.Seasonality_correctZeroRate(swigCPtr, Date.getCPtr(d), r, SWIGTYPE_p_InflationTermStructure.getCPtr(iTS));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double correctYoYRate(Date d, double r, SWIGTYPE_p_InflationTermStructure iTS) {
    double ret = NQuantLibcPINVOKE.Seasonality_correctYoYRate(swigCPtr, Date.getCPtr(d), r, SWIGTYPE_p_InflationTermStructure.getCPtr(iTS));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isConsistent(SWIGTYPE_p_InflationTermStructure iTS) {
    bool ret = NQuantLibcPINVOKE.Seasonality_isConsistent(swigCPtr, SWIGTYPE_p_InflationTermStructure.getCPtr(iTS));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}