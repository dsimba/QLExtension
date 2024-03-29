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

public class ZeroCurve : YieldTermStructure {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ZeroCurve(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.ZeroCurve_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ZeroCurve obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ZeroCurve() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_ZeroCurve(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public ZeroCurve(DateVector dates, DoubleVector yields, DayCounter dayCounter, Calendar calendar, Linear i, Compounding compounding, Frequency frequency) : this(NQuantLibcPINVOKE.new_ZeroCurve__SWIG_0(DateVector.getCPtr(dates), DoubleVector.getCPtr(yields), DayCounter.getCPtr(dayCounter), Calendar.getCPtr(calendar), Linear.getCPtr(i), (int)compounding, (int)frequency), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ZeroCurve(DateVector dates, DoubleVector yields, DayCounter dayCounter, Calendar calendar, Linear i, Compounding compounding) : this(NQuantLibcPINVOKE.new_ZeroCurve__SWIG_1(DateVector.getCPtr(dates), DoubleVector.getCPtr(yields), DayCounter.getCPtr(dayCounter), Calendar.getCPtr(calendar), Linear.getCPtr(i), (int)compounding), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ZeroCurve(DateVector dates, DoubleVector yields, DayCounter dayCounter, Calendar calendar, Linear i) : this(NQuantLibcPINVOKE.new_ZeroCurve__SWIG_2(DateVector.getCPtr(dates), DoubleVector.getCPtr(yields), DayCounter.getCPtr(dayCounter), Calendar.getCPtr(calendar), Linear.getCPtr(i)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ZeroCurve(DateVector dates, DoubleVector yields, DayCounter dayCounter, Calendar calendar) : this(NQuantLibcPINVOKE.new_ZeroCurve__SWIG_3(DateVector.getCPtr(dates), DoubleVector.getCPtr(yields), DayCounter.getCPtr(dayCounter), Calendar.getCPtr(calendar)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ZeroCurve(DateVector dates, DoubleVector yields, DayCounter dayCounter) : this(NQuantLibcPINVOKE.new_ZeroCurve__SWIG_4(DateVector.getCPtr(dates), DoubleVector.getCPtr(yields), DayCounter.getCPtr(dayCounter)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleVector times() {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.ZeroCurve_times(swigCPtr), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DoubleVector data() {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.ZeroCurve_data(swigCPtr), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DateVector dates() {
    DateVector ret = new DateVector(NQuantLibcPINVOKE.ZeroCurve_dates(swigCPtr), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DoubleVector zeroRates() {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.ZeroCurve_zeroRates(swigCPtr), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NodeVector nodes() {
    NodeVector ret = new NodeVector(NQuantLibcPINVOKE.ZeroCurve_nodes(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
