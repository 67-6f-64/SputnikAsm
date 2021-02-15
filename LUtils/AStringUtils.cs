﻿using System;
using Sputnik.LString;
using Sputnik.LStructs;
using Sputnik.LUtils;
using SputnikAsm.LCollections;

namespace SputnikAsm.LUtils
{
    public static class AStringUtils
    {
        #region Pos
        public static int Pos(String needle, String str)
        {
            return UStringUtils.IndexOf(str, needle);
        }
        #endregion
        #region Val
        public static void Val(String str, out Int32 v, out int code)
        {
            Val(str, out Int64 vv, out code);
            v = (Int32)vv;
        }
        public static void Val(String str, out Int64 v, out int code)
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
        public static void Val(String str, out UInt32 v, out int code)
        {
            Val(str, out UInt64 vv, out code);
            v = (UInt32)vv;
        }
        public static void Val(String str, out UInt64 v, out int code)
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
        public static void Val(String str, out Single v, out int code)
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
        public static void Val(String str, out Double v, out int code)
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
        #region Copy
        public static String Copy(String str, int start, int length)
        {
            return UStringUtils.SubStr(str, start, length);
        }
        #endregion
        #region IntToHex
        public static String IntToHex(Int32 value, Int32 digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String IntToHex(Int64 value, Int32 digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String IntToHex(UInt32 value, Int32 digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String IntToHex(UInt64 value, Int32 digits)
        {
            var hex = value.ToString("x");
            if (hex.Length < digits)
            {
                while (hex.Length != digits)
                    hex = hex.Insert(0, "0");
            }
            return hex;
        }
        public static String IntToHex(IntPtr value, Int32 digits)
        {
            return IntToHex(value.ToInt64(), digits);
        }
        public static String IntToHex(UIntPtr value, Int32 digits)
        {
            return IntToHex(value.ToUInt64(), digits);
        }
        #endregion
        #region ConvertHexStrToRealStr
        public static String ConvertHexStrToRealStr(String s)
        {
            var result = "";
            int i;
            if (s == "")
            {
                result = s;
                return result;
            }
            var start = 0;
            var isHex = "$";
            for (i = start; i < s.Length; i++)
            {
                int k;
                int j;
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
                                        var bytes = Copy(s, i + 1, j - (i + 1));
                                        result = "$";
                                        for (k = bytes.Length - 1; k >= 0; k--)
                                            result += IntToHex((Byte)(bytes[k]), 2);
                                        return result; //this is it, no further process required, or appreciated...
                                    }
                                }
                            }
                        }
                        break;
                    case '#':
                        {
                            isHex = "";
                            start = 1;
                            continue;
                        }
                    case '(':
                        {
                            String t;
                            if (Copy(s, 0, 5) == "(INT)")
                            {
                                t = Copy(s, 5, s.Length);
                                Val(t, out k, out j);
                                if (j == 0)
                                {
                                    result = "$" + IntToHex(k, 4);
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
                            if (Copy(s, 0, 8) == "(DOUBLE)")
                            {
                                t = Copy(s, 8, s.Length);
                                Val(t, out Double d, out j);
                                if (j == 0)
                                {
                                    var x = new UBitUnion
                                    {
                                        Double = d
                                    };
                                    result = "$" + IntToHex(x.Int64, 8);
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
                            if (Copy(s, 0, 7) == "(FLOAT)")
                            {
                                t = Copy(s, 7, s.Length);
                                Val(t, out Single f, out j);
                                if (j == 0)
                                {
                                    var x = new UBitUnion
                                    {
                                        Single = f
                                    };
                                    result = "$" + IntToHex(x.Int32, 8);
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
                        result = "-" + isHex + Copy(s, start + 1, s.Length);
                        break;
                    case '+':
                        result = "+" + isHex + Copy(s, start + 1, s.Length);
                        break;
                    default:
                        result = isHex + Copy(s, start, s.Length);
                        break;
                }
                return result;
            }
            return result;
        }
        #endregion
        #region ConvertStringToBytes
        public static void ConvertStringToBytes(String aobStr, Boolean hex, ATByteArray bytes)
        {
            // vars
            int i, j, k;
            String temp;
            // wildcard for AoBScan and AoBAssert.Bigger than $FF is ok.
            var wildCard = (UInt16)0xf521;
            var wildcardChars = new[] {'x', 'X', '?', '*'};
            // size check
            bytes.SetLength(0);
            if (aobStr.Length == 0)
                return;
            aobStr = aobStr.Trim();
            if ((Pos("-", aobStr) != -1) || (Pos(" ", aobStr) != -1) || (Pos(",", aobStr) != -1))
            {
                /*syntax is : (1)xx-xx-xx (2)or xx xx xx (3)or xx,xx,xx*/
                j = 0;
                k = 0;
                aobStr += ' ';
                for (i = 0; i < aobStr.Length; i++)
                {
                    if (AArrayUtils.InArray(aobStr[i], ' ', '-', ','))
                    {
                        temp = Copy(aobStr, j, i - j);
                        j = i + 1;
                        bytes.SetLength(k + 1);
                        if (UStringUtils.IndexOfAny(temp, wildcardChars) == -1)
                        {
                            switch (hex)
                            {
                                case true:
                                    bytes[k] = (UInt16)StrToInt("0x" + temp);
                                    break;
                                case false:
                                    bytes[k] = (UInt16)StrToInt(temp);
                                    break;
                            }
                        }
                        else
                            bytes[k] = wildCard;
                        k += 1;
                    }
                }
            }
            else
            {
                /*syntax is: xxxxxx*/
                k = 0;
                j = 1;
                for (i = 1; i <= aobStr.Length; i++)
                {
                    if ((i % 2) != 0)
                        continue;
                    temp = Copy(aobStr, j, i - j + 1);
                    j = i + 1;
                    bytes.SetLength(k + 1);
                    if (UStringUtils.IndexOfAny(temp, wildcardChars) == -1)
                        bytes[k] = (UInt16)StrToInt("0x" + temp);
                    else
                        bytes[k] = wildCard;
                    k += 1;
                }
            }
        }
        #endregion
        #region HexStrToInt
        public static Int32 HexStrToInt(String s)
        {
            return StrToInt(ConvertHexStrToRealStr(s));
        }
        #endregion
        #region HexStrToInt64
        public static Int64 HexStrToInt64(String s)
        {
            return (Int64)StrToQWordEx(ConvertHexStrToRealStr(s));
        }
        #endregion
        #region StrToQWordEx
        public static UInt64 StrToQWordEx(String s)
        {
            s = s.Trim();
            if (s.Length == 0)
                return 0;
            UInt64 result = 0;
            switch (s[1] == '-')
            {
                case true:
                    result = (UInt64)StrToInt64(s);
                    break;
                case false:
                    result = StrToQWord(s);
                    break;
            }
            return result;
        }
        #endregion
        #region StrToInt
        public static Int32 StrToInt(String str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToInt32(str);
        }
        #endregion
        #region StrToInt64
        public static Int64 StrToInt64(String str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToInt64(str);
        }
        #endregion
        #region StrToDWord
        public static UInt32 StrToDWord(String str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToUInt32(str);
        }
        #endregion
        #region StrToQWord
        public static UInt64 StrToQWord(String str)
        {
            if (str.Length > 0 && str[0] == '$')
                str = "0x" + str.Substring(1);
            return UStringUtils.StringToUInt64(str);
        }
        #endregion
    }
}