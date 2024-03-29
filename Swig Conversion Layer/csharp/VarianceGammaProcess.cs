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

public class VarianceGammaProcess : StochasticProcess1D {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal VarianceGammaProcess(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.VarianceGammaProcess_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(VarianceGammaProcess obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~VarianceGammaProcess() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_VarianceGammaProcess(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public VarianceGammaProcess(QuoteHandle s0, YieldTermStructureHandle dividendYield, YieldTermStructureHandle riskFreeRate, double sigma, double nu, double theta) : this(NQuantLibcPINVOKE.new_VarianceGammaProcess(QuoteHandle.getCPtr(s0), YieldTermStructureHandle.getCPtr(dividendYield), YieldTermStructureHandle.getCPtr(riskFreeRate), sigma, nu, theta), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
