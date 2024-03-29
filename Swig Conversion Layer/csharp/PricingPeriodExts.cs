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

public class PricingPeriodExts : global::System.IDisposable, global::System.Collections.IEnumerable
    , global::System.Collections.Generic.IList<PricingPeriodExt>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PricingPeriodExts(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PricingPeriodExts obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~PricingPeriodExts() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_PricingPeriodExts(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public PricingPeriodExts(global::System.Collections.ICollection c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (PricingPeriodExt element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public PricingPeriodExt this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(PricingPeriodExt[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(PricingPeriodExt[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, PricingPeriodExt[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  global::System.Collections.Generic.IEnumerator<PricingPeriodExt> global::System.Collections.Generic.IEnumerable<PricingPeriodExt>.GetEnumerator() {
    return new PricingPeriodExtsEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new PricingPeriodExtsEnumerator(this);
  }

  public PricingPeriodExtsEnumerator GetEnumerator() {
    return new PricingPeriodExtsEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class PricingPeriodExtsEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<PricingPeriodExt>
  {
    private PricingPeriodExts collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public PricingPeriodExtsEnumerator(PricingPeriodExts collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public PricingPeriodExt Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (PricingPeriodExt)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
  }

  public void Clear() {
    NQuantLibcPINVOKE.PricingPeriodExts_Clear(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Add(PricingPeriodExt x) {
    NQuantLibcPINVOKE.PricingPeriodExts_Add(swigCPtr, PricingPeriodExt.getCPtr(x));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = NQuantLibcPINVOKE.PricingPeriodExts_size(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint capacity() {
    uint ret = NQuantLibcPINVOKE.PricingPeriodExts_capacity(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void reserve(uint n) {
    NQuantLibcPINVOKE.PricingPeriodExts_reserve(swigCPtr, n);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public PricingPeriodExts() : this(NQuantLibcPINVOKE.new_PricingPeriodExts__SWIG_0(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public PricingPeriodExts(PricingPeriodExts other) : this(NQuantLibcPINVOKE.new_PricingPeriodExts__SWIG_1(PricingPeriodExts.getCPtr(other)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public PricingPeriodExts(int capacity) : this(NQuantLibcPINVOKE.new_PricingPeriodExts__SWIG_2(capacity), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  private PricingPeriodExt getitemcopy(int index) {
    PricingPeriodExt ret = new PricingPeriodExt(NQuantLibcPINVOKE.PricingPeriodExts_getitemcopy(swigCPtr, index), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private PricingPeriodExt getitem(int index) {
    PricingPeriodExt ret = new PricingPeriodExt(NQuantLibcPINVOKE.PricingPeriodExts_getitem(swigCPtr, index), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, PricingPeriodExt val) {
    NQuantLibcPINVOKE.PricingPeriodExts_setitem(swigCPtr, index, PricingPeriodExt.getCPtr(val));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(PricingPeriodExts values) {
    NQuantLibcPINVOKE.PricingPeriodExts_AddRange(swigCPtr, PricingPeriodExts.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public PricingPeriodExts GetRange(int index, int count) {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.PricingPeriodExts_GetRange(swigCPtr, index, count);
    PricingPeriodExts ret = (cPtr == global::System.IntPtr.Zero) ? null : new PricingPeriodExts(cPtr, true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, PricingPeriodExt x) {
    NQuantLibcPINVOKE.PricingPeriodExts_Insert(swigCPtr, index, PricingPeriodExt.getCPtr(x));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, PricingPeriodExts values) {
    NQuantLibcPINVOKE.PricingPeriodExts_InsertRange(swigCPtr, index, PricingPeriodExts.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    NQuantLibcPINVOKE.PricingPeriodExts_RemoveAt(swigCPtr, index);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    NQuantLibcPINVOKE.PricingPeriodExts_RemoveRange(swigCPtr, index, count);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public static PricingPeriodExts Repeat(PricingPeriodExt value, int count) {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.PricingPeriodExts_Repeat(PricingPeriodExt.getCPtr(value), count);
    PricingPeriodExts ret = (cPtr == global::System.IntPtr.Zero) ? null : new PricingPeriodExts(cPtr, true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    NQuantLibcPINVOKE.PricingPeriodExts_Reverse__SWIG_0(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Reverse(int index, int count) {
    NQuantLibcPINVOKE.PricingPeriodExts_Reverse__SWIG_1(swigCPtr, index, count);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, PricingPeriodExts values) {
    NQuantLibcPINVOKE.PricingPeriodExts_SetRange(swigCPtr, index, PricingPeriodExts.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Contains(PricingPeriodExt value) {
    bool ret = NQuantLibcPINVOKE.PricingPeriodExts_Contains(swigCPtr, PricingPeriodExt.getCPtr(value));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int IndexOf(PricingPeriodExt value) {
    int ret = NQuantLibcPINVOKE.PricingPeriodExts_IndexOf(swigCPtr, PricingPeriodExt.getCPtr(value));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int LastIndexOf(PricingPeriodExt value) {
    int ret = NQuantLibcPINVOKE.PricingPeriodExts_LastIndexOf(swigCPtr, PricingPeriodExt.getCPtr(value));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Remove(PricingPeriodExt value) {
    bool ret = NQuantLibcPINVOKE.PricingPeriodExts_Remove(swigCPtr, PricingPeriodExt.getCPtr(value));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
