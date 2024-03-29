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

public class RiskStatistics : Statistics {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal RiskStatistics(global::System.IntPtr cPtr, bool cMemoryOwn) : base(NQuantLibcPINVOKE.RiskStatistics_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RiskStatistics obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RiskStatistics() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_RiskStatistics(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public double semiVariance() {
    double ret = NQuantLibcPINVOKE.RiskStatistics_semiVariance(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double semiDeviation() {
    double ret = NQuantLibcPINVOKE.RiskStatistics_semiDeviation(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double downsideVariance() {
    double ret = NQuantLibcPINVOKE.RiskStatistics_downsideVariance(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double downsideDeviation() {
    double ret = NQuantLibcPINVOKE.RiskStatistics_downsideDeviation(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double regret(double target) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_regret(swigCPtr, target);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double potentialUpside(double percentile) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_potentialUpside(swigCPtr, percentile);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double valueAtRisk(double percentile) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_valueAtRisk(swigCPtr, percentile);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double expectedShortfall(double percentile) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_expectedShortfall(swigCPtr, percentile);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double shortfall(double target) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_shortfall(swigCPtr, target);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double averageShortfall(double target) {
    double ret = NQuantLibcPINVOKE.RiskStatistics_averageShortfall(swigCPtr, target);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RiskStatistics() : this(NQuantLibcPINVOKE.new_RiskStatistics(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
