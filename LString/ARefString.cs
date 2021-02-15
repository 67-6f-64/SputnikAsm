using System;
using Sputnik.LUtils;

namespace SputnikAsm.LString
{
    public class ARefString
    {
        #region Variables
        public String Value;
        public int Position;
        public int Length;
        public UIntPtr Pointer;
        #endregion
        #region Constructor
        public ARefString()
        {
            Value = "";
            Position = 0;
            Length = 0;
            Pointer = UIntPtr.Zero;
        }
        public ARefString(String value)
        {
            Value = value;
            Position = -1;
            Length = Value.Length;
            Pointer = UIntPtr.Zero;
        }
        public ARefString(String value, int position)
        {
            Value = value;
            Position = position;
            Length = Value.Length;
            Pointer = UIntPtr.Zero;
        }
        public ARefString(String value, UIntPtr pointer)
        {
            Value = value;
            Position = -1;
            Length = Value.Length;
            Pointer = pointer;
        }
        #endregion
        #region Equals and other checks
        public Boolean Equals(ARefString other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value && Position == other.Position && Length == other.Length;
        }
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ARefString)) return false;
            return Equals((ARefString)obj);
        }
        public override int GetHashCode()
        {
            return UMathUtils.CombineHash32(Value.GetHashCode(), Position.GetHashCode(), Length.GetHashCode());
        }
        public static Boolean operator ==(ARefString left, ARefString right)
        {
            return Equals(left, right);
        }
        public static Boolean operator !=(ARefString left, ARefString right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
