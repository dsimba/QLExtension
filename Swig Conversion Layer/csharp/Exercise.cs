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

public class Exercise : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Exercise(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Exercise obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Exercise() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_Exercise(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public _Exercise __deref__() {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.Exercise___deref__(swigCPtr);
    _Exercise ret = (cPtr == global::System.IntPtr.Zero) ? null : new _Exercise(cPtr, false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool isNull() {
    bool ret = NQuantLibcPINVOKE.Exercise_isNull(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public _Exercise.Type exerciseType() {
    _Exercise.Type ret = (_Exercise.Type)NQuantLibcPINVOKE.Exercise_exerciseType(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Exercise() : this(NQuantLibcPINVOKE.new_Exercise(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public _Exercise.Type type() {
    _Exercise.Type ret = (_Exercise.Type)NQuantLibcPINVOKE.Exercise_type(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DateVector dates() {
    DateVector ret = new DateVector(NQuantLibcPINVOKE.Exercise_dates(swigCPtr), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Date lastDate() {
    Date ret = new Date(NQuantLibcPINVOKE.Exercise_lastDate(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static readonly _Exercise.Type American = (_Exercise.Type)NQuantLibcPINVOKE.Exercise_American_get();
  public static readonly _Exercise.Type Bermudan = (_Exercise.Type)NQuantLibcPINVOKE.Exercise_Bermudan_get();
  public static readonly _Exercise.Type European = (_Exercise.Type)NQuantLibcPINVOKE.Exercise_European_get();
}

}
