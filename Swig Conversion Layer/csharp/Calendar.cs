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

public class Calendar : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Calendar(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Calendar obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Calendar() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_Calendar(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public bool isWeekend(Weekday w) {
    bool ret = NQuantLibcPINVOKE.Calendar_isWeekend(swigCPtr, (int)w);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date endOfMonth(Date arg0) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_endOfMonth(swigCPtr, Date.getCPtr(arg0)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isBusinessDay(Date arg0) {
    bool ret = NQuantLibcPINVOKE.Calendar_isBusinessDay(swigCPtr, Date.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isHoliday(Date arg0) {
    bool ret = NQuantLibcPINVOKE.Calendar_isHoliday(swigCPtr, Date.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isEndOfMonth(Date arg0) {
    bool ret = NQuantLibcPINVOKE.Calendar_isEndOfMonth(swigCPtr, Date.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void addHoliday(Date arg0) {
    NQuantLibcPINVOKE.Calendar_addHoliday(swigCPtr, Date.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void removeHoliday(Date arg0) {
    NQuantLibcPINVOKE.Calendar_removeHoliday(swigCPtr, Date.getCPtr(arg0));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public Date adjust(Date d, BusinessDayConvention convention) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_adjust__SWIG_0(swigCPtr, Date.getCPtr(d), (int)convention), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date adjust(Date d) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_adjust__SWIG_1(swigCPtr, Date.getCPtr(d)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, int n, TimeUnit unit, BusinessDayConvention convention, bool endOfMonth) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_0(swigCPtr, Date.getCPtr(d), n, (int)unit, (int)convention, endOfMonth), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, int n, TimeUnit unit, BusinessDayConvention convention) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_1(swigCPtr, Date.getCPtr(d), n, (int)unit, (int)convention), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, int n, TimeUnit unit) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_2(swigCPtr, Date.getCPtr(d), n, (int)unit), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, Period period, BusinessDayConvention convention, bool endOfMonth) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_3(swigCPtr, Date.getCPtr(d), Period.getCPtr(period), (int)convention, endOfMonth), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, Period period, BusinessDayConvention convention) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_4(swigCPtr, Date.getCPtr(d), Period.getCPtr(period), (int)convention), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date advance(Date d, Period period) {
    Date ret = new Date(NQuantLibcPINVOKE.Calendar_advance__SWIG_5(swigCPtr, Date.getCPtr(d), Period.getCPtr(period)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int businessDaysBetween(Date from, Date to, bool includeFirst, bool includeLast) {
    int ret = NQuantLibcPINVOKE.Calendar_businessDaysBetween__SWIG_0(swigCPtr, Date.getCPtr(from), Date.getCPtr(to), includeFirst, includeLast);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int businessDaysBetween(Date from, Date to, bool includeFirst) {
    int ret = NQuantLibcPINVOKE.Calendar_businessDaysBetween__SWIG_1(swigCPtr, Date.getCPtr(from), Date.getCPtr(to), includeFirst);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int businessDaysBetween(Date from, Date to) {
    int ret = NQuantLibcPINVOKE.Calendar_businessDaysBetween__SWIG_2(swigCPtr, Date.getCPtr(from), Date.getCPtr(to));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string name() {
    string ret = NQuantLibcPINVOKE.Calendar_name(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string __str__() {
    string ret = NQuantLibcPINVOKE.Calendar___str__(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
