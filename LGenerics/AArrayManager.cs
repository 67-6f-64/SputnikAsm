using System;
using System.Collections;
using System.Collections.Generic;
using Sputnik.LGenerics;
using SputnikAsm.LUtils;

namespace SputnikAsm.LGenerics
{
    public class AArrayManager<T> : IEnumerable<T>
    {
        #region UFunctorComparer
        internal sealed class UFunctorComparer<T> : IComparer<T>
        {
            #region Variable
            private readonly Comparison<T> _comparison;
            #endregion
            #region UFunctorComparer
            public UFunctorComparer(Comparison<T> c)
            {
                _comparison = c;
            }
            #endregion
            #region Compare
            public int Compare(T x, T y)
            {
                return _comparison(x, y);
            }
            #endregion
        }
        #endregion
        #region Variables
        private readonly Object _lock;
        public T[] Buffer;
        #endregion
        #region Properties
        public int Length { get; private set; }
        public T First
        {
            get => Get(0);
            set => Set(0, value);
        }
        public T Last
        {
            get => Get(Length - 1);
            set => Set(Length - 1, value);
        }
        #endregion
        #region Override []
        public T this[int i]
        {
            get => Get(i);
            set => Set(i, value);
        }
        #endregion
        #region Constructor
        public AArrayManager(int size)
        {
            Buffer = new T[size];
            for (var i = 0; i < size; i++)
                Buffer[i] = NextElement(i);
            _lock = new Object();
        }
        public AArrayManager(params T[] tokens)
        {
            Buffer = tokens;
            Length = Buffer.Length;
            _lock = new Object();
        }
        #endregion
        #region Clear
        public void Clear()
        {
            SetLength(0);
        }
        #endregion
        #region SetLength
        public void SetLength(int size)
        {
            lock (_lock)
            {
                if (size <= 0)
                    size = 0;
                if (size > Length)
                    EnsureCapacity(size);
                else
                    Length = size;
            }
        }
        #endregion
        #region Inc
        public void Inc()
        {
            SetLength(Length + 1);
        }
        public void IncBy(int size)
        {
            SetLength(Length + size);
        }
        #endregion
        #region Dec
        public void Dec()
        {
            SetLength(Length + 1);
        }
        public void DecBy(int size)
        {
            var sz = Length - size;
            if (sz <= 0)
            {
                Clear();
                return;
            }
            SetLength(sz);
        }
        #endregion
        #region EnsureCapacity
        public void EnsureCapacity(int capacity)
        {
            lock (_lock)
            {
                if (capacity <= Buffer.Length)
                {
                    if (capacity > Length)
                        Length = capacity;
                    return;
                }
                if (capacity < Buffer.Length)
                {
                    if (capacity > Length)
                        Length = capacity;
                    return;
                }
                var oldLength = Length;
                var sz = Math.Max(Buffer.Length << 1, Length + capacity + 10);
                Array.Resize(ref Buffer, sz);
                for (var i = oldLength; i < Buffer.Length; i++)
                    Buffer[i] = NextElement(i);
                Length = capacity;
            }
        }
        #endregion
        #region NextElement
        public virtual T NextElement(int index)
        {
            return typeof(T).GetConstructor(Type.EmptyTypes) != null ? Activator.CreateInstance<T>() : default;
        }
        #endregion
        #region Contains
        public Boolean Contains(T item)
        {
            lock (_lock)
            {
                var comp = EqualityComparer<T>.Default;
                for (var i = 0; i < Length; ++i)
                {
                    if (comp.Equals(Buffer[i], item))
                        return true;
                }
                return false;
            }
        }
        public Boolean Contains(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Buffer)
                {
                    if (match(item))
                        return true;
                }
                return false;
            }
        }
        #endregion
        #region Assign
        public void Assign(params T[] values)
        {
            lock (_lock)
            {
                Buffer = values;
                Length = Buffer.Length;
            }
        }
        #endregion
        #region Add
        public void Add(T item)
        {
            lock (_lock)
            {
                EnsureCapacity(Length + 1);
                Last = item;
            }
        }
        #endregion
        #region AddRange
        public void AddRange(IEnumerable<T> array)
        {
            foreach (var item in array)
                Add(item);
        }
        public void AddRange(AArrayManager<T> array)
        {
            foreach (var item in array.Buffer)
                Add(item);
        }
        #endregion
        #region Get
        public T Get(int index)
        {
            lock (_lock)
            {
                if (index < 0 || index >= Length)
                    throw new Exception("outside bounds of array");
                return Buffer[index];
            }
        }
        #endregion
        #region Set
        public void Set(int index, T value)
        {
            lock (_lock)
            {
                if (index < 0 || index >= Length)
                    throw new Exception("outside bounds of array");
                Buffer[index] = value;
            }
        }
        #endregion
        #region Remove
        public void Remove(T item)
        {
            lock (_lock)
            {
                var comp = EqualityComparer<T>.Default;
                for (var i = 0; i < Length; ++i)
                {
                    if (comp.Equals(Buffer[i], item))
                    {
                        RemoveAt(i);
                        return;
                    }
                }
            }
        }
        public void Remove(int startIndex, int length)
        {
            lock (_lock)
            {
                if (length <= 0)
                    return;
                if (startIndex >= Length)
                    return;
                var removed = 0;
                for (var i = startIndex; i < Length; i++)
                {
                    if (i < startIndex)
                        continue;
                    if (i >= startIndex && i < startIndex + length)
                        removed++;
                    var iOffset = i + length;
                    if (iOffset < 0 || iOffset >= Length)
                        continue;
                    Buffer[i] = Buffer[iOffset];
                }
                if (removed <= 0)
                    return;
                var newBuffer = new T[Length - removed];
                Array.Copy(Buffer, newBuffer, Length - removed);
                Buffer = newBuffer;
                Length = Buffer.Length;
            }
        }
        #endregion
        #region RemoveAt
        public Boolean RemoveAt(int index)
        {
            if (index < 0 || index >= Length)
                return false;
            Length--;
            if (index < Length)
                Array.Copy(Buffer, index + 1, Buffer, index, Length - index);
            Buffer[Length] = default;
            return true;
        }
        #endregion
        #region RemoveRange
        public Boolean RemoveRange(int index, int count)
        {
            lock (_lock)
            {
                if (count == 1)
                    return RemoveAt(index);
                if (index < 0 || index >= Length)
                    return false;
                if (index + count > Length)
                    return false;
                //_list.RemoveRange(index, count);
                if (count < 0)
                    return false;
                if (Length - index < count)
                    return false;
                if (count <= 0)
                    return true;
                var size = Length - count;
                if (index < size)
                    Array.Copy(Buffer, index + count, Buffer, index, size - index);
                Array.Clear(Buffer, size, count);
                Length -= count;
                return true;
            }
        }
        #endregion
        #region RemoveAll
        public Boolean RemoveAll(T[] values)
        {
            lock (_lock)
            {
                var removedAny = false;
                foreach (var item in values)
                {
                    Remove(item);
                    removedAny = true;
                }
                return removedAny;
            }
        }
        public Boolean RemoveAll(Predicate<T> match)
        {
            lock (_lock)
            {
                var removedAny = false;
                var removeList = new USafeList<T>();
                foreach (var item in Buffer)
                {
                    if (!match(item))
                        continue;
                    removeList.Add(item);
                    removedAny = true;
                }
                foreach (var item in removeList.Content)
                    Remove(item);
                return removedAny;
            }
        }
        #endregion
        #region RemoveAtWithSwap
        public void RemoveAtWithSwap(int index)
        {
            if (index < 0 || index >= Length)
                return;
            Buffer[index] = Buffer[Length - 1];
            Buffer[Length - 1] = default;
            --Length;
        }
        #endregion
        #region Insert
        public void Insert(int index, T data)
        {
            Insert(index, new[] { data }, 1);
        }
        public void Insert(int index, T[] data)
        {
            Insert(index, data, data.Length);
        }
        public void Insert(int index, T[] data, int length)
        {
            lock (_lock)
            {
                if (index < 0)
                    return;
                var freeCapacity = Buffer.Length - Length;
                if (freeCapacity >= length)
                {
                    var space = Buffer.Length - index;
                    AArrayUtils.CopyMemory(Buffer, index + length, Take(index, space), 0, space);
                    AArrayUtils.CopyMemory(Buffer, index, data, 0, length);
                    Length += length;
                    return;
                }
                var first = Take(0, index);
                var second = data;
                var third = Take(index);
                var len = Length + (index - Length);
                EnsureCapacity(len);
                Buffer = new T[((first.Length + length + third.Length) << 1) + 10];
                Array.Copy(first, 0, Buffer, 0, first.Length);
                Array.Copy(second, 0, Buffer, first.Length, length);
                Array.Copy(third, 0, Buffer, first.Length + length, third.Length);
                Length += length;
            }
        }
        #endregion
        #region Push
        public void Push(T value)
        {
            lock (_lock)
            {
                Add(value);
            }
        }
        #endregion
        #region Pop
        public T Pop()
        {
            lock (_lock)
            {
                T ret;
                if (Length > 0)
                {
                    ret = Buffer[Length - 1];
                    RemoveAt(Length - 1);
                }
                else
                    ret = default;
                return ret;
            }
        }
        #endregion
        #region Shift
        public T Shift()
        {
            lock (_lock)
            {
                T ret;
                if (Length > 0)
                {
                    ret = Buffer[0];
                    RemoveAt(0);
                }
                else
                    ret = default;
                return ret;
            }
        }
        #endregion
        #region Unshift
        public Boolean Unshift(T value)
        {
            lock (_lock)
            {
                Insert(Length, value);
                return true;
            }
        }
        #endregion
        #region Reverse
        public Boolean Reverse()
        {
            lock (_lock)
            {
                Array.Reverse(Buffer);
                return true;
            }
        }
        public Boolean Reverse(int index, int count)
        {
            lock (_lock)
            {
                if (index < 0)
                    return false;
                if (count < 0)
                    return false;
                if (Length - index < count)
                    return false;
                Array.Reverse(Buffer, index, count);
                return true;
            }
        }
        #endregion
        #region Sort
        public void Sort()
        {
            lock (_lock)
            {
                Array.Sort(Buffer, 0, Length);
            }
        }
        public void Sort(IComparer comparer)
        {
            lock (_lock)
            {
                Array.Sort(Buffer, 0, Length, comparer);
            }
        }
        public void Sort(IComparer<T> comparer)
        {
            lock (_lock)
            {
                Array.Sort(Buffer, 0, Length, comparer);
            }
        }
        public void Sort(Comparison<T> comparison)
        {
            lock (_lock)
            {
                if (comparison == null)
                    return;
                if (Length <= 1)
                    return;
                Array.Sort(Buffer, 0, Length, new UFunctorComparer<T>(comparison));
            }
        }
        #endregion
        #region Any
        public Boolean Any(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Buffer)
                {
                    if (match(item))
                        return true;
                }
                return false;
            }
        }
        #endregion
        #region CountMatches
        public int CountMatches(Predicate<T> match)
        {
            lock (_lock)
            {
                var i = 0;
                foreach (var item in Buffer)
                {
                    if (match(item))
                        i++;
                }
                return i;
            }
        }
        #endregion
        #region FirstOrDefault
        public T FirstOrDefault(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Buffer)
                {
                    if (match(item))
                        return item;
                }
                return default;
            }
        }
        #endregion
        #region LastOrDefault
        public T LastOrDefault(Predicate<T> match)
        {
            lock (_lock)
            {
                for (var i = Length - 1; i >= 0; i--)
                {
                    var item = Buffer[i];
                    if (match(item))
                        return item;
                }
                return default;
            }
        }
        #endregion
        #region Find
        public T Find(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Buffer)
                {
                    if (match(item))
                        return item;
                }
                return default;
            }
        }
        #endregion
        #region FindIndex
        public int FindIndex(Predicate<T> match)
        {
            lock (_lock)
            {
                for (var i = 0; i < Length; i++)
                {
                    var item = Buffer[i];
                    if (match(item))
                        return i;
                }

                return -1;
            }
        }
        #endregion
        #region ForEach
        public void ForEach(Action<T> action)
        {
            lock (_lock)
            {
                foreach (var item in Buffer)
                    action(item);
            }
        }
        #endregion
        #region Take
        public T[] Take(int start)
        {
            return Take(start, -1);
        }
        public T[] Take(int start, int count)
        {
            lock (_lock)
            {
                var ret = new USafeList<T>();
                for (var i = start; i < Length; i++)
                {
                    var c = Buffer[i];
                    if (count != -1 && count <= 0)
                        return ret.Content;
                    ret.Add(c);
                    if (count != -1)
                        count--;
                }
                return ret.Content;
            }
        }
        #endregion
        #region TakeAll
        public T[] TakeAll()
        {
            lock (_lock)
            {
                var ret = new T[Length];
                Array.Copy(Buffer, 0, ret, 0, Length);
                return ret;
            }
        }
        #endregion
        #region IndexOf
        public int IndexOf(T item)
        {
            lock (_lock)
            {
                var comp = EqualityComparer<T>.Default;
                for (var i = 0; i < Length; ++i)
                {
                    if (comp.Equals(Buffer[i], item))
                        return i;
                }
                return -1;
            }
        }
        #endregion
        #region LastIndexOf
        public int LastIndexOf(T item)
        {
            lock (_lock)
            {
                var comp = EqualityComparer<T>.Default;
                for (var i = Length - 1; i >= 0; --i)
                {
                    if (comp.Equals(Buffer[i], item))
                        return i;
                }
                return -1;
            }
        }
        #endregion
        #region IEnumerable<T>
        #region GetEnumerator
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
            {
                var c = Buffer[i];
                yield return c;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #endregion
    }
}
