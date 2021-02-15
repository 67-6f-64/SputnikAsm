using System;
using System.Linq;
using Sputnik.LString;
using Sputnik.LStructs;
using Sputnik.LUtils;

namespace Tack
{
    public class TUtils
    {
        #region inarray
        public static Boolean inarray(Byte needle, params Byte[] haystack)
        {
            if (haystack == null || haystack.Length <= 0)
                return false;
            return haystack.Any(c => needle == c);
        }
        public static Boolean inarray(Char needle, params Char[] haystack)
        {
            if (haystack == null || haystack.Length <= 0)
                return false;
            return haystack.Any(c => needle == c);
        }
        #endregion
        #region pos
        public static int pos(String needle, String str)
        {
            return UStringUtils.IndexOf(str, needle);
        }
        #endregion
        #region val
        public static void val(String str, out Int32 v, out int code)
        {
            val(str, out Int64 vv, out code);
            v = (Int32)vv;
        }
        public static void val(String str, out Int64 v, out int code)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            v = 0;
            code = 0;
            using (var s = new UCharPtr(str))
            {
                var n = 0;
                var ret = UCharPtr.StrToL(s, 0, ref n, 0);
                if (n > str.Length)
                {
                    v = ret;
                    code = 0;
                    return;
                }
                code = n;
                v = 0;
            }
        }
        public static void val(String str, out UInt32 v, out int code)
        {
            val(str, out UInt64 vv, out code);
            v = (UInt32)vv;
        }
        public static void val(String str, out UInt64 v, out int code)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            v = 0;
            code = 0;
            using (var s = new UCharPtr(str))
            {
                var n = 0;
                var ret = UCharPtr.StrTouL(s, 0, ref n, 0);
                if (n > str.Length)
                {
                    v = ret;
                    code = 0;
                    return;
                }
                code = n;
                v = 0;
            }
        }
        public static void val(String str, out Single v, out int code)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            v = 0.0f;
            code = 0;
            using (var s = new UCharPtr(str))
            {
                var n = 0;
                var ret = UCharPtr.StrToF(s, 0, ref n);
                if (n >= str.Length)
                {
                    v = ret;
                    code = 0;
                    return;
                }
                code = n;
                v = 0;
            }
        }
        public static void val(String str, out Double v, out int code)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            v = 0.0;
            code = 0;
            using (var s = new UCharPtr(str))
            {
                var n = 0;
                var ret = UCharPtr.StrToD(s, 0, ref n);
                if (n >= str.Length)
                {
                    v = ret;
                    code = 0;
                    return;
                }
                code = n;
                v = 0;
            }
        }
        #endregion
        #region copy
        public static String copy(String str, int start, int length)
        {
            return UStringUtils.SubStr(str, start, length);
        }
        #endregion
        #region inttohex
        public static String inttohex(Int32 value, int digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String inttohex(Int64 value, int digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String inttohex(UInt32 value, int digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String inttohex(UInt64 value, int digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String inttohex(IntPtr value, int digits)
        {
            return inttohex(value.ToInt64(), digits);
        }
        public static String inttohex(UIntPtr value, int digits)
        {
            return inttohex(value.ToUInt64(), digits);
        }
        #endregion
        #region converthexstrtorealstr
        public static String converthexstrtorealstr(String s)
        {
            var result = "";
            String ishex;
            int start;
            int i, j, k;
            String bytes;
            String t;
            Single f;
            Double d;
            if (s == "")
            {
                result = s;
                return result;
            }
            start = 0;
            ishex = "$";
            for (i = start; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '\'':
                    case '"':
                        {
                            //char
                            if ((i + 1) < s.Length)
                            {
                                for (j = i + 1; j < s.Length; j++)
                                {
                                    if (s[j] == '\'' || s[j] == '"')
                                    {
                                        bytes = copy(s, i + 1, j - (i + 1));
                                        result = "$";
                                        for (k = bytes.Length - 1; k >= 0; k--)
                                            result += inttohex((Byte)(bytes[k]), 2);
                                        return result; //this is it, no further process required, or appreciated...
                                    }
                                }
                            }
                        }
                        break;
                    case '#':
                        {
                            ishex = "";
                            start = 1;
                            continue;
                        }
                    case '(':
                        {
                            if (copy(s, 0, 5) == "(INT)")
                            {
                                t = copy(s, 5, s.Length);
                                val(t, out k, out j);
                                if (j == 0)
                                {
                                    result = "$" + inttohex(k, 4);
                                    switch (s[0])
                                    {
                                        case '-':
                                            result = "-" + result;
                                            break;
                                        case '+':
                                            result = "+" + result;
                                            break;
                                    }
                                    return result;
                                }
                            }
                            if (copy(s, 0, 8) == "(DOUBLE)")
                            {
                                t = copy(s, 8, s.Length);
                                val(t, out d, out j);
                                if (j == 0)
                                {
                                    var x = new UBitUnion
                                    {
                                        Double = d
                                    };
                                    result = "$" + inttohex(x.Int64, 8);
                                    switch (s[0])
                                    {
                                        case '-':
                                            result = "-" + result;
                                            break;
                                        case '+':
                                            result = "+" + result;
                                            break;
                                    }
                                    return result;
                                }
                            }
                            if (copy(s, 0, 7) == "(FLOAT)")
                            {
                                t = copy(s, 7, s.Length);
                                val(t, out f, out j);
                                if (j == 0)
                                {
                                    var x = new UBitUnion
                                    {
                                        Single = f
                                    };
                                    result = "$" + inttohex(x.Int32, 8);
                                    switch (s[0])
                                    {
                                        case '-':
                                            result = "-" + result;
                                            break;
                                        case '+':
                                            result = "+" + result;
                                            break;
                                    }
                                    return result;
                                }
                            }
                        }
                        break;
                }
                switch (s[0])
                {
                    case '-':
                        result = "-" + ishex + copy(s, start + 1, s.Length);
                        break;
                    case '+':
                        result = "+" + ishex + copy(s, start + 1, s.Length);
                        break;
                    default:
                        result = ishex + copy(s, start, s.Length);
                        break;
                }
                return result;
            }
            return result;
        }
        #endregion
        public static int hexstrtoint(string s)
        {
            return UStringUtils.StringToInt32(converthexstrtorealstr(s));
        }
        public static Int64 hexstrtoint64(string s)
        {
            return (Int64)strtoqwordex(converthexstrtorealstr(s));
        }
        public static UInt64 strtoqwordex(string s)
        {
            s = s.Trim();
            if (s.Length == 0)
                return 0;
            UInt64 result = 0;
            switch (s[1] == '-')
            {
                case true:
                    result = (UInt64)strtoint64(s);
                    break;
                case false:
                    result = strtoqword(s);
                    break;
            }
            return result;
        }
        public static Int32 strtoint(string str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToInt32(str);
        }
        public static Int64 strtoint64(string str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToInt64(str);
        }
        public static UInt32 strtodword(string str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToUInt32(str);
        }
        public static UInt64 strtoqword(string str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToUInt64(str);
        }
    }
}
