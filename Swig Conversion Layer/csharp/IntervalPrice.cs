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

public class IntervalPrice : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal IntervalPrice(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IntervalPrice obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~IntervalPrice() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_IntervalPrice(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public IntervalPrice(double arg0, double arg1, double arg2, double arg3) : this(NQuantLibcPINVOKE.new_IntervalPrice(arg0, arg1, arg2, arg3), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setValue(double arg0, IntervalPrice.Type arg1) {
    NQuantLibcPINVOKE.IntervalPrice_setValue(swigCPtr, arg0, (int)arg1);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setValues(double arg0, double arg1, double arg2, double arg3) {
    NQuantLibcPINVOKE.IntervalPrice_setValues(swigCPtr, arg0, arg1, arg2, arg3);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public double value(IntervalPrice.Type t) {
    double ret = NQuantLibcPINVOKE.IntervalPrice_value(swigCPtr, (int)t);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double open() {
    double ret = NQuantLibcPINVOKE.IntervalPrice_open(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double close() {
    double ret = NQuantLibcPINVOKE.IntervalPrice_close(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double high() {
    double ret = NQuantLibcPINVOKE.IntervalPrice_high(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double low() {
    double ret = NQuantLibcPINVOKE.IntervalPrice_low(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static IntervalPriceTimeSeries makeSeries(DateVector d, DoubleVector open, DoubleVector close, DoubleVector high, DoubleVector low) {
    IntervalPriceTimeSeries ret = new IntervalPriceTimeSeries(NQuantLibcPINVOKE.IntervalPrice_makeSeries(DateVector.getCPtr(d), DoubleVector.getCPtr(open), DoubleVector.getCPtr(close), DoubleVector.getCPtr(high), DoubleVector.getCPtr(low)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static DoubleVector extractValues(IntervalPriceTimeSeries arg0, IntervalPrice.Type t) {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.IntervalPrice_extractValues(IntervalPriceTimeSeries.getCPtr(arg0), (int)t), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static RealTimeSeries extractComponent(IntervalPriceTimeSeries arg0, IntervalPrice.Type t) {
    RealTimeSeries ret = new RealTimeSeries(NQuantLibcPINVOKE.IntervalPrice_extractComponent(IntervalPriceTimeSeries.getCPtr(arg0), (int)t), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum Type {
    Open,
    Close,
    High,
    Low
  }

}

}
