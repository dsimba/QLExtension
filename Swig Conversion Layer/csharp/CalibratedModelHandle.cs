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

public class CalibratedModelHandle : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CalibratedModelHandle(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CalibratedModelHandle obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CalibratedModelHandle() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_CalibratedModelHandle(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public CalibratedModelHandle(CalibratedModel arg0) : this(NQuantLibcPINVOKE.new_CalibratedModelHandle__SWIG_0(CalibratedModel.getCPtr(arg0)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public CalibratedModelHandle() : this(NQuantLibcPINVOKE.new_CalibratedModelHandle__SWIG_1(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public CalibratedModel __deref__() {
    CalibratedModel ret = new CalibratedModel(NQuantLibcPINVOKE.CalibratedModelHandle___deref__(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool empty() {
    bool ret = NQuantLibcPINVOKE.CalibratedModelHandle_empty(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Observable asObservable() {
    Observable ret = new Observable(NQuantLibcPINVOKE.CalibratedModelHandle_asObservable(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public QlArray parameters() {
    QlArray ret = new QlArray(NQuantLibcPINVOKE.CalibratedModelHandle_parameters(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2, Constraint constraint, DoubleVector weights) {
    NQuantLibcPINVOKE.CalibratedModelHandle_calibrate__SWIG_0(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2), Constraint.getCPtr(constraint), DoubleVector.getCPtr(weights));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2, Constraint constraint) {
    NQuantLibcPINVOKE.CalibratedModelHandle_calibrate__SWIG_1(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2), Constraint.getCPtr(constraint));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2) {
    NQuantLibcPINVOKE.CalibratedModelHandle_calibrate__SWIG_2(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
