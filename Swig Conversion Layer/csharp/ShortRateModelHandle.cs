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

public class ShortRateModelHandle : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ShortRateModelHandle(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ShortRateModelHandle obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ShortRateModelHandle() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_ShortRateModelHandle(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public ShortRateModelHandle(ShortRateModel arg0) : this(NQuantLibcPINVOKE.new_ShortRateModelHandle__SWIG_0(ShortRateModel.getCPtr(arg0)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ShortRateModelHandle() : this(NQuantLibcPINVOKE.new_ShortRateModelHandle__SWIG_1(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public ShortRateModel __deref__() {
    ShortRateModel ret = new ShortRateModel(NQuantLibcPINVOKE.ShortRateModelHandle___deref__(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool empty() {
    bool ret = NQuantLibcPINVOKE.ShortRateModelHandle_empty(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Observable asObservable() {
    Observable ret = new Observable(NQuantLibcPINVOKE.ShortRateModelHandle_asObservable(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public QlArray parameters() {
    QlArray ret = new QlArray(NQuantLibcPINVOKE.ShortRateModelHandle_parameters(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2, Constraint constraint, DoubleVector weights, BoolVector fixParameters) {
    NQuantLibcPINVOKE.ShortRateModelHandle_calibrate__SWIG_0(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2), Constraint.getCPtr(constraint), DoubleVector.getCPtr(weights), BoolVector.getCPtr(fixParameters));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2, Constraint constraint, DoubleVector weights) {
    NQuantLibcPINVOKE.ShortRateModelHandle_calibrate__SWIG_1(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2), Constraint.getCPtr(constraint), DoubleVector.getCPtr(weights));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2, Constraint constraint) {
    NQuantLibcPINVOKE.ShortRateModelHandle_calibrate__SWIG_2(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2), Constraint.getCPtr(constraint));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void calibrate(CalibrationHelperVector arg0, OptimizationMethod arg1, EndCriteria arg2) {
    NQuantLibcPINVOKE.ShortRateModelHandle_calibrate__SWIG_3(swigCPtr, CalibrationHelperVector.getCPtr(arg0), OptimizationMethod.getCPtr(arg1), EndCriteria.getCPtr(arg2));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
