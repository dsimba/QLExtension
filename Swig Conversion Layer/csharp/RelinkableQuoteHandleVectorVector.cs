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

public class RelinkableQuoteHandleVectorVector : global::System.IDisposable, global::System.Collections.IEnumerable
    , global::System.Collections.Generic.IEnumerable<RelinkableQuoteHandleVector>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal RelinkableQuoteHandleVectorVector(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RelinkableQuoteHandleVectorVector obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RelinkableQuoteHandleVectorVector() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NQuantLibcPINVOKE.delete_RelinkableQuoteHandleVectorVector(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public RelinkableQuoteHandleVectorVector(global::System.Collections.ICollection c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (RelinkableQuoteHandleVector element in c) {
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

  public RelinkableQuoteHandleVector this[int index]  {
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

  public void CopyTo(RelinkableQuoteHandleVector[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(RelinkableQuoteHandleVector[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, RelinkableQuoteHandleVector[] array, int arrayIndex, int count)
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

  global::System.Collections.Generic.IEnumerator<RelinkableQuoteHandleVector> global::System.Collections.Generic.IEnumerable<RelinkableQuoteHandleVector>.GetEnumerator() {
    return new RelinkableQuoteHandleVectorVectorEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new RelinkableQuoteHandleVectorVectorEnumerator(this);
  }

  public RelinkableQuoteHandleVectorVectorEnumerator GetEnumerator() {
    return new RelinkableQuoteHandleVectorVectorEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class RelinkableQuoteHandleVectorVectorEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<RelinkableQuoteHandleVector>
  {
    private RelinkableQuoteHandleVectorVector collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public RelinkableQuoteHandleVectorVectorEnumerator(RelinkableQuoteHandleVectorVector collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public RelinkableQuoteHandleVector Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (RelinkableQuoteHandleVector)currentObject;
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
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Clear(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Add(RelinkableQuoteHandleVector x) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Add(swigCPtr, RelinkableQuoteHandleVector.getCPtr(x));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_size(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint capacity() {
    uint ret = NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_capacity(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void reserve(uint n) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_reserve(swigCPtr, n);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public RelinkableQuoteHandleVectorVector() : this(NQuantLibcPINVOKE.new_RelinkableQuoteHandleVectorVector__SWIG_0(), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public RelinkableQuoteHandleVectorVector(RelinkableQuoteHandleVectorVector other) : this(NQuantLibcPINVOKE.new_RelinkableQuoteHandleVectorVector__SWIG_1(RelinkableQuoteHandleVectorVector.getCPtr(other)), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public RelinkableQuoteHandleVectorVector(int capacity) : this(NQuantLibcPINVOKE.new_RelinkableQuoteHandleVectorVector__SWIG_2(capacity), true) {
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  private RelinkableQuoteHandleVector getitemcopy(int index) {
    RelinkableQuoteHandleVector ret = new RelinkableQuoteHandleVector(NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_getitemcopy(swigCPtr, index), true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private RelinkableQuoteHandleVector getitem(int index) {
    RelinkableQuoteHandleVector ret = new RelinkableQuoteHandleVector(NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_getitem(swigCPtr, index), false);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, RelinkableQuoteHandleVector val) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_setitem(swigCPtr, index, RelinkableQuoteHandleVector.getCPtr(val));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(RelinkableQuoteHandleVectorVector values) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_AddRange(swigCPtr, RelinkableQuoteHandleVectorVector.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public RelinkableQuoteHandleVectorVector GetRange(int index, int count) {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_GetRange(swigCPtr, index, count);
    RelinkableQuoteHandleVectorVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new RelinkableQuoteHandleVectorVector(cPtr, true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, RelinkableQuoteHandleVector x) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Insert(swigCPtr, index, RelinkableQuoteHandleVector.getCPtr(x));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, RelinkableQuoteHandleVectorVector values) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_InsertRange(swigCPtr, index, RelinkableQuoteHandleVectorVector.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_RemoveAt(swigCPtr, index);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_RemoveRange(swigCPtr, index, count);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public static RelinkableQuoteHandleVectorVector Repeat(RelinkableQuoteHandleVector value, int count) {
    global::System.IntPtr cPtr = NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Repeat(RelinkableQuoteHandleVector.getCPtr(value), count);
    RelinkableQuoteHandleVectorVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new RelinkableQuoteHandleVectorVector(cPtr, true);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Reverse__SWIG_0(swigCPtr);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Reverse(int index, int count) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_Reverse__SWIG_1(swigCPtr, index, count);
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, RelinkableQuoteHandleVectorVector values) {
    NQuantLibcPINVOKE.RelinkableQuoteHandleVectorVector_SetRange(swigCPtr, index, RelinkableQuoteHandleVectorVector.getCPtr(values));
    if (NQuantLibcPINVOKE.SWIGPendingException.Pending) throw NQuantLibcPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
