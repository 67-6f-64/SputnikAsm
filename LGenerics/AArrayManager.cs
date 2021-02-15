using System;

namespace SputnikAsm.LGenerics
{
    public class AArrayManager<T>
    {
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
        }
        #endregion
        #region SetLength
        public void SetLength(int size)
        {
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
        #endregion
        #region EnsureCapacity
        public void EnsureCapacity(int capacity)
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
        #endregion
        #region NextElement
        public virtual T NextElement(int index)
        {
            return default;
        }
        #endregion
        #region Get
        public T Get(int index)
        {
            if (index < 0 || index >= Raw.Length)
                return default;
            return Raw[index];
        }
        #endregion
        #region Set
        public void Set(int index, T value)
        {
            if (index < 0 || index >= Raw.Length)
                throw new Exception("outside bounds of array");
            Raw[index] = value;
        }
        #endregion
        #region Remove
        public void Remove(int startIndex)
        {
            Remove(startIndex, 1);
        }
        public void Remove(int startIndex, int length)
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
        #endregion
    }
}
