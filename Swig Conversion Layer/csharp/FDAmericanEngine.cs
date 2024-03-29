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

public class FDAmericanEngine : PricingEngine {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FDAmericanEngine(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.FDAmericanEngine_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FDAmericanEngine obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~FDAmericanEngine() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_FDAmericanEngine(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public FDAmericanEngine(GeneralizedBlackScholesProcess process, uint timeSteps, uint gridPoints, bool timeDependent) : this(NQuantLibcPINVOKE.new_FDAmericanEngine__SWIG_0(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps, gridPoints, timeDependent), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDAmericanEngine(GeneralizedBlackScholesProcess process, uint timeSteps, uint gridPoints) : this(NQuantLibcPINVOKE.new_FDAmericanEngine__SWIG_1(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps, gridPoints), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDAmericanEngine(GeneralizedBlackScholesProcess process, uint timeSteps) : this(NQuantLibcPINVOKE.new_FDAmericanEngine__SWIG_2(GeneralizedBlackScholesProcess.getCPtr(process), timeSteps), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public FDAmericanEngine(GeneralizedBlackScholesProcess process) : this(NQuantLibcPINVOKE.new_FDAmericanEngine__SWIG_3(GeneralizedBlackScholesProcess.getCPtr(process)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
