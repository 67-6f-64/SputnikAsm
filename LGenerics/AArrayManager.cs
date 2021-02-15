using System;
using System.Collections;
using System.Collections.Generic;
using Sputnik.LGenerics;

namespace SputnikAsm.LGenerics
{
    public class AArrayManager<T>
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
        #endregion
        #region Properties
        public int Length => Raw.Length;
        public T[] Raw { get; private set; }
        public T First
        {
            get => Get(0);
            set => Set(0, value);
        }
        public T Last
        {
            get => Get(Raw.Length - 1);
            set => Set(Raw.Length - 1, value);
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
        public AArrayManager(params T[] tokens)
        {
            Raw = tokens;
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
                {
                    Raw = new T[0];
                    return;
                }
                if (size < Raw.Length)
                {
                    var newBuffer = new T[size];
                    for (var i = 0; i < newBuffer.Length; i++)
                    {
                        if (i < Raw.Length)
                            newBuffer[i] = Raw[i];
                        else
                            newBuffer[i] = NextElement(i);
                    }
                    Raw = newBuffer;
                    return;
                }
                EnsureCapacity(size);
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
                if (capacity <= Raw.Length)
                    return;
                var newBuffer = new T[capacity];
                for (var i = 0; i < newBuffer.Length; i++)
                {
                    if (i < Raw.Length)
                        newBuffer[i] = Raw[i];
                    else
                        newBuffer[i] = NextElement(i);
                }
                Raw = newBuffer;
            }
        }
        #endregion
        #region NextElement
        public virtual T NextElement(int index)
        {
            return Activator.CreateInstance<T>();
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
                    if (comp.Equals(Raw[i], item))
                        return true;
                }
                return false;
            }
        }
        public Boolean Contains(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Raw)
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
                Raw = values;
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
            foreach (var item in array.Raw)
                Add(item);
        }
        #endregion
        #region Get
        public T Get(int index)
        {
            lock (_lock)
            {
                if (index < 0 || index >= Raw.Length)
                    throw new Exception("outside bounds of array");
                return Raw[index];
            }
        }
        #endregion
        #region Set
        public void Set(int index, T value)
        {
            lock (_lock)
            {
                if (index < 0 || index >= Raw.Length)
                    throw new Exception("outside bounds of array");
                Raw[index] = value;
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
                    if (comp.Equals(Raw[i], item))
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
                if (startIndex >= Raw.Length)
                    return;
                var removed = 0;
                for (var i = startIndex; i < Raw.Length; i++)
                {
                    if (i < startIndex)
                        continue;
                    if (i >= startIndex && i < startIndex + length)
                        removed++;
                    var iOffset = i + length;
                    if (iOffset < 0 || iOffset >= Raw.Length)
                        continue;
                    Raw[i] = Raw[iOffset];
                }
                if (removed <= 0)
                    return;
                var newBuffer = new T[Raw.Length - removed];
                Array.Copy(Raw, newBuffer, Raw.Length - removed);
                Raw = newBuffer;
            }
        }
        #endregion
        #region RemoveAt
        public void RemoveAt(int index)
        {
            Remove(index, 1);
        }
        #endregion
        #region RemoveRange
        public Boolean RemoveRange(int index, int count)
        {
            lock (_lock)
            {
                if (index < 0 || index >= Raw.Length)
                    return false;
                if (index + count > Raw.Length)
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
                    Array.Copy(Raw, index + count, Raw, index, size - index);
                Array.Clear(Raw, size, count);
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
                foreach (var item in Raw)
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
                var first = Take(0, index);
                var second = data;
                var third = Take(index);
                var len = Length + (index - Length);
                EnsureCapacity(len);
                Raw = new T[first.Length + length + third.Length];
                Array.Copy(first, 0, Raw, 0, first.Length);
                Array.Copy(second, 0, Raw, first.Length, length);
                Array.Copy(third, 0, Raw, first.Length + length, third.Length);
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
                if (Raw.Length > 0)
                {
                    ret = Raw[Raw.Length - 1];
                    RemoveAt(Raw.Length - 1);
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
                if (Raw.Length > 0)
                {
                    ret = Raw[0];
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
                Insert(Raw.Length, value);
                return true;
            }
        }
        #endregion
        #region Reverse
        public Boolean Reverse()
        {
            lock (_lock)
            {
                Array.Reverse(Raw);
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
                Array.Reverse(Raw, index, count);
                return true;
            }
        }
        #endregion
        #region Sort
        public void Sort()
        {
            lock (_lock)
            {
                Array.Sort(Raw, 0, Length);
            }
        }
        public void Sort(IComparer comparer)
        {
            lock (_lock)
            {
                Array.Sort(Raw, 0, Length, comparer);
            }
        }
        public void Sort(IComparer<T> comparer)
        {
            lock (_lock)
            {
                Array.Sort(Raw, 0, Length, comparer);
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
                Array.Sort(Raw, 0, Raw.Length, new UFunctorComparer<T>(comparison));
            }
        }
        #endregion
        #region Any
        public Boolean Any(Predicate<T> match)
        {
            lock (_lock)
            {
                foreach (var item in Raw)
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
                foreach (var item in Raw)
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
                foreach (var item in Raw)
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
                for (var i = Raw.Length - 1; i >= 0; i--)
                {
                    var item = Raw[i];
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
                foreach (var item in Raw)
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
                for (var i = 0; i < Raw.Length; i++)
                {
                    var item = Raw[i];
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
                foreach (var item in Raw)
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
                for (var i = start; i < Raw.Length; i++)
                {
                    var c = Raw[i];
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
    }
}
