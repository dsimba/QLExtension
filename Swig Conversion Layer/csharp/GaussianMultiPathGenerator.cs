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

public class GaussianMultiPathGenerator : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GaussianMultiPathGenerator(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GaussianMultiPathGenerator obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~GaussianMultiPathGenerator() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_GaussianMultiPathGenerator(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public GaussianMultiPathGenerator(StochasticProcess process, DoubleVector times, GaussianRandomSequenceGenerator generator, bool brownianBridge) : this(NQuantLibcPINVOKE.new_GaussianMultiPathGenerator__SWIG_0(StochasticProcess.getCPtr(process), DoubleVector.getCPtr(times), GaussianRandomSequenceGenerator.getCPtr(generator), brownianBridge), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public GaussianMultiPathGenerator(StochasticProcess process, DoubleVector times, GaussianRandomSequenceGenerator generator) : this(NQuantLibcPINVOKE.new_GaussianMultiPathGenerator__SWIG_1(StochasticProcess.getCPtr(process), DoubleVector.getCPtr(times), GaussianRandomSequenceGenerator.getCPtr(generator)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public SampleMultiPath next() {
    SampleMultiPath ret = new SampleMultiPath(NQuantLibcPINVOKE.GaussianMultiPathGenerator_next(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SampleMultiPath antithetic() {
    SampleMultiPath ret = new SampleMultiPath(NQuantLibcPINVOKE.GaussianMultiPathGenerator_antithetic(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
