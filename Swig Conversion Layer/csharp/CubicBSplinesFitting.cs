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

public class CubicBSplinesFitting : FittingMethod {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal CubicBSplinesFitting(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.CubicBSplinesFitting_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CubicBSplinesFitting obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~CubicBSplinesFitting() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_CubicBSplinesFitting(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public CubicBSplinesFitting(DoubleVector knotVector, bool constrainAtZero) : this(NQuantLibcPINVOKE.new_CubicBSplinesFitting__SWIG_0(DoubleVector.getCPtr(knotVector), constrainAtZero), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public CubicBSplinesFitting(DoubleVector knotVector) : this(NQuantLibcPINVOKE.new_CubicBSplinesFitting__SWIG_1(DoubleVector.getCPtr(knotVector)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
