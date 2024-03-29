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

public class FDEuropeanEngine : PricingEngine {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FDEuropeanEngine(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.FDEuropeanEngine_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FDEuropeanEngine obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FDEuropeanEngine() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_FDEuropeanEngine(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public FDEuropeanEngine(GeneralizedBlackScholesProcess process, uint timeSteps, uint gridPoints, bool timeDependent) : this(NQuantLibcPINVOKE.new_FDEuropeanEngine__SWIG_0(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps, gridPoints, timeDependent), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDEuropeanEngine(GeneralizedBlackScholesProcess process, uint timeSteps, uint gridPoints) : this(NQuantLibcPINVOKE.new_FDEuropeanEngine__SWIG_1(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps, gridPoints), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDEuropeanEngine(GeneralizedBlackScholesProcess process, uint timeSteps) : this(NQuantLibcPINVOKE.new_FDEuropeanEngine__SWIG_2(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDEuropeanEngine(GeneralizedBlackScholesProcess process) : this(NQuantLibcPINVOKE.new_FDEuropeanEngine__SWIG_3(GeneralizedBlackScholesProcess.getCPtr(process)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
