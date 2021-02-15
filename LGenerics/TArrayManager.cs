using System;

namespace Tack.LGenerics
{
    public class TArrayManager<T>
    {
        #region Variables
        private T[] _buffer;
        #endregion
        #region Properties
        public int Length => _buffer.Length;
        public T[] Raw => _buffer;
        public T First
        {
            get => Get(0);
            set => Set(0, value);
        }
        public T Last
        {
            get => Get(_buffer.Length - 1);
            set => Set(_buffer.Length - 1, value);
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
        public TArrayManager(params T[] tokens)
        {
            _buffer = tokens;
        }
        #endregion
        #region SetLength
        public void SetLength(int size)
        {
            if (size < _buffer.Length)
            {
                var newBuffer = new T[size];
                for (var i = 0; i < newBuffer.Length; i++)
                {
                    if (i < _buffer.Length)
                        newBuffer[i] = _buffer[i];
                    else
                        newBuffer[i] = NextElement(i);
                }
                _buffer = newBuffer;
                return;
            }
            EnsureCapacity(size);
        }
        #endregion
        #region EnsureCapacity
        public void EnsureCapacity(int capacity)
        {
            if (capacity <= _buffer.Length)
                return;
            var newBuffer = new T[capacity];
            for (var i = 0; i < newBuffer.Length; i++)
            {
                if (i < _buffer.Length)
                    newBuffer[i] = _buffer[i];
                else
                    newBuffer[i] = NextElement(i);
            }
            _buffer = newBuffer;
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
            if (index < 0 || index >= _buffer.Length)
                return default;
            return _buffer[index];
        }
        #endregion
        #region Set
        public void Set(int index, T value)
        {
            if (index < 0 || index >= _buffer.Length)
                throw new Exception("outside bounds of array");
            _buffer[index] = value;
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
            if (startIndex >= _buffer.Length)
                return;
            var removed = 0;
            for (var i = startIndex; i < _buffer.Length; i++)
            {
                if (i < startIndex)
                    continue;
                if (i >= startIndex && i < startIndex + length)
                    removed++;
                var iOffset = i + length;
                if (iOffset < 0 || iOffset >= _buffer.Length)
                    continue;
                _buffer[i] = _buffer[iOffset];
            }
            if (removed <= 0)
                return;
            var newBuffer = new T[_buffer.Length - removed];
            Array.Copy(_buffer, newBuffer, _buffer.Length - removed);
            _buffer = newBuffer;
        }
        #endregion
    }
}
