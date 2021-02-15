using System;
using Sputnik.LBinary;
using Sputnik.LMarshal;
using SputnikAsm.LCollections;

namespace SputnikAsm.LBinary
{
    public class AMultiByte
    {
        #region Index Override
        public Int64 this[int offset, int size]
        {
            get => (int)_vec.Get(offset, size);
            set => _vec.Set(offset, size, value);
        }
        public Int64 this[int offset, Boolean directOffset, int size]
        {
            get => (int)_vec.Get(offset, size, directOffset);
            set => _vec.Set(offset, size, directOffset, value);
        }
        #endregion
        #region Variables
        private readonly int _size;
        private readonly UVec _vec;
        private readonly AByteArray _target;
        private readonly int _targetPos;
        private readonly IntPtr _pointer;
        #endregion
        #region Constructor
        public AMultiByte(int size)
        {
            _size = size;
            _pointer = IntPtr.Zero;
            _vec = new UVec(_size);
            _target = null;
            _targetPos = -1;
            _pointer = IntPtr.Zero;
        }
        public AMultiByte(int size, AByteArray bytes, int index)
        {
            _size = size;
            _pointer = IntPtr.Zero;
            _target = bytes;
            _targetPos = index;
            var buf = new Byte[_size];
            for (var i = 0; i < _size && _targetPos + i < _target.Length; i++)
                buf[i] = _target[_targetPos + i];
            _vec = new UVec(buf);
        }
        public AMultiByte(int size, IntPtr bytesPointer)
            : this(size, bytesPointer, 0)
        {
        }
        public AMultiByte(int size, IntPtr bytesPointer, int index)
        {
            _size = size;
            _pointer = bytesPointer;
            _target = null;
            _targetPos = index;
            var buf = new Byte[_size];
            for (var i = 0; i < _size; i++)
                buf[i] = bytesPointer.ReadByte(_targetPos + i);
            _vec = new UVec(buf);
        }
        #endregion
        #region Apply
        public void Apply()
        {
            if (_pointer != IntPtr.Zero)
            {
                for (var i = 0; i < _size && i < _vec.Raw.Length; i++)
                    _pointer.WriteByte(_vec.Raw[i], _targetPos + i);
            }
            if (_target != null)
            {
                for (var i = 0; i < _size && _targetPos + i < _target.Length && i < _vec.Raw.Length; i++)
                    _target[_targetPos + i] = _vec.Raw[i];
            }
        }
        #endregion
        #region Get
        public Int64 Get(int offset, int size)
        {
            return Get(offset, size, true);
        }
        public Int64 Get(int offset, int size, Boolean directOffset)
        {
            return _vec.Get(offset, size, directOffset);
        }
        #endregion
        #region Set
        public void Set(int offset, int size, Int64 value)
        {
            Set(offset, size, true, value);
        }
        public void Set(int offset, int size, Boolean directOffset, Int64 value)
        {
            _vec.Set(offset, size, directOffset, value);
        }
        #endregion
    }
}
