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

public class TridiagonalOperator : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TridiagonalOperator(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TridiagonalOperator obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TridiagonalOperator() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_TridiagonalOperator(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public TridiagonalOperator(QlArray low, QlArray mid, QlArray high) : this(NQuantLibcPINVOKE.new_TridiagonalOperator(QlArray.getCPtr(low), QlArray.getCPtr(mid), QlArray.getCPtr(high)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public QlArray solveFor(QlArray rhs) {
    QlArray ret = new QlArray(NQuantLibcPINVOKE.TridiagonalOperator_solveFor(swigCPtr, QlArray.getCPtr(rhs)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public QlArray applyTo(QlArray v) {
    QlArray ret = new QlArray(NQuantLibcPINVOKE.TridiagonalOperator_applyTo(swigCPtr, QlArray.getCPtr(v)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint size() {
    uint ret = NQuantLibcPINVOKE.TridiagonalOperator_size(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setFirstRow(double arg0, double arg1) {
    NQuantLibcPINVOKE.TridiagonalOperator_setFirstRow(swigCPtr, arg0, arg1);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setMidRow(uint arg0, double arg1, double arg2, double arg3) {
    NQuantLibcPINVOKE.TridiagonalOperator_setMidRow(swigCPtr, arg0, arg1, arg2, arg3);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setMidRows(double arg0, double arg1, double arg2) {
    NQuantLibcPINVOKE.TridiagonalOperator_setMidRows(swigCPtr, arg0, arg1, arg2);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setLastRow(double arg0, double arg1) {
    NQuantLibcPINVOKE.TridiagonalOperator_setLastRow(swigCPtr, arg0, arg1);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public static TridiagonalOperator identity(uint size) {
    TridiagonalOperator ret = new TridiagonalOperator(NQuantLibcPINVOKE.TridiagonalOperator_identity(size), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
