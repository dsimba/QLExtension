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

public class DoubleExponentialCalibration : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DoubleExponentialCalibration(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DoubleExponentialCalibration obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DoubleExponentialCalibration() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_DoubleExponentialCalibration(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed, bool b2IsFixed, bool lambdaIsFixed, bool vegaWeighted, SWIGTYPE_p_boost__shared_ptrT_EndCriteria_t endCriteria, SWIGTYPE_p_boost__shared_ptrT_OptimizationMethod_t method) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_0(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed, b2IsFixed, lambdaIsFixed, vegaWeighted, SWIGTYPE_p_boost__shared_ptrT_EndCriteria_t.getCPtr(endCriteria), SWIGTYPE_p_boost__shared_ptrT_OptimizationMethod_t.getCPtr(method)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed, bool b2IsFixed, bool lambdaIsFixed, bool vegaWeighted, SWIGTYPE_p_boost__shared_ptrT_EndCriteria_t endCriteria) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_1(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed, b2IsFixed, lambdaIsFixed, vegaWeighted, SWIGTYPE_p_boost__shared_ptrT_EndCriteria_t.getCPtr(endCriteria)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed, bool b2IsFixed, bool lambdaIsFixed, bool vegaWeighted) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_2(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed, b2IsFixed, lambdaIsFixed, vegaWeighted), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed, bool b2IsFixed, bool lambdaIsFixed) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_3(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed, b2IsFixed, lambdaIsFixed), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed, bool b2IsFixed) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_4(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed, b2IsFixed), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed, bool b1IsFixed) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_5(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed, b1IsFixed), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess, bool sigmaIsFixed) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_6(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess, sigmaIsFixed), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess, double lambdaGuess) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_7(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess, lambdaGuess), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess, double b2Guess) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_8(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess, b2Guess), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess, double b1Guess) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_9(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess, b1Guess), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols, double sigmaGuess) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_10(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols), sigmaGuess), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleExponentialCalibration(DoubleVector t, DoubleVector blackVols) : this(NQuantLibcPINVOKE.new_DoubleExponentialCalibration__SWIG_11(DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public DoubleVector k(DoubleVector t, DoubleVector blackVols) {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.DoubleExponentialCalibration_k__SWIG_0(swigCPtr, DoubleVector.getCPtr(t), DoubleVector.getCPtr(blackVols)), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void compute() {
    NQuantLibcPINVOKE.DoubleExponentialCalibration_compute(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public double sigma() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_sigma(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double b1() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_b1(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double b2() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_b2(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double lambda() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_lambda(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double error() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_error(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double maxError() {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_maxError(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double value(double t1, double t2, double T) {
    double ret = NQuantLibcPINVOKE.DoubleExponentialCalibration_value(swigCPtr, t1, t2, T);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DoubleVector k() {
    DoubleVector ret = new DoubleVector(NQuantLibcPINVOKE.DoubleExponentialCalibration_k__SWIG_1(swigCPtr), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
