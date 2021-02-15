using System;
using Sputnik.LString;
using Sputnik.LStructs;
using Sputnik.LUtils;
using SputnikAsm.LCollections;

namespace SputnikAsm.LUtils
{
    public static class AStringUtils
    {
        #region Properties
        public static ACharArray Brackets { get; }
        public static ACharArray StdWordDelims { get; }
        #endregion
        #region Constants
        static AStringUtils()
        {
            Brackets = new ACharArray('(', ')', '[', ']', '{', '}');
            StdWordDelims = new ACharArray(',', '.', ';', '/', '\\', ':', '\'', '"', '`', '(', ')', '[', ']', '{', '}');
            StdWordDelims.AddRange(Brackets.TakeAll());
            StdWordDelims.AddRange(ACharUtils.Range('\0', ' ').TakeAll());
        }
        #endregion
        #region Pos
        public static int Pos(Char needle, String str)
        {
            return UStringUtils.IndexOf(str, needle.ToString());
        }
        public static int Pos(Char needle, String str, Boolean ignoreCase)
        {
            return UStringUtils.IndexOf(str, needle.ToString(), ignoreCase);
        }
        public static int Pos(String needle, String str)
        {
            return UStringUtils.IndexOf(str, needle);
        }
        public static int Pos(String needle, String str, Boolean ignoreCase)
        {
            return UStringUtils.IndexOf(str, needle, ignoreCase);
        }
        #endregion
        #region PosEx
        public static int PosEx(Char needle, String str, int start)
        {
            return UStringUtils.IndexOf(str, needle.ToString(), start);
        }
        public static int PosEx(String needle, String str, int start)
        {
            return UStringUtils.IndexOf(str, needle, start);
        }
        #endregion
        #region RPos
        public static int RPos(Char needle, String str)
        {
            return UStringUtils.LastIndexOf(str, needle.ToString());
        }
        public static int RPos(Char needle, String str, Boolean ignoreCase)
        {
            return UStringUtils.LastIndexOf(str, needle.ToString(), ignoreCase);
        }
        public static int RPos(String needle, String str)
        {
            return UStringUtils.LastIndexOf(str, needle);
        }
        public static int RPos(String needle, String str, Boolean ignoreCase)
        {
            return UStringUtils.LastIndexOf(str, needle, ignoreCase);
        }
        #endregion
        #region RPosEx
        public static int RPosEx(Char needle, String str, int start)
        {
            return UStringUtils.LastIndexOf(str, needle.ToString(), start);
        }
        public static int RPosEx(String needle, String str, int start)
        {
            return UStringUtils.LastIndexOf(str, needle, start);
        }
        #endregion
        #region Val
        public static void Val(String str, out UIntPtr v, out int code)
        {
            Val(str, out UInt64 vv, out code);
            v = (UIntPtr)vv;
        }
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
        public static String Copy(String str)
        {
            return UStringUtils.Copy(str);
        }
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
        #region BinToHexStr
        public static String BinToHexStr(Byte[] bin)
        {
            var HexSymbols = "0123456789ABCDEF";
            var str = UStringUtils.StrNew('\0', 2 * bin.Length);
            using (var p = new UCharPtr(str))
            {
                for (var i = 0; i < bin.Length; i++)
                {
                    p[2 * i + 0] = HexSymbols[1 + (bin[i] >> 4) - 1];
                    p[2 * i + 1] = HexSymbols[1 + (bin[i] & 0xf) - 1];
                }
            }
            return str;
        }
        #endregion
        #region BinToStr
        public static String BinToStr(Byte[] bin)
        {
            return UBinaryUtils.BinToString(bin);
        }
        #endregion
        #region BinToAscii
        public static String BinToAscii(Byte[] bin)
        {
            return UBinaryUtils.BinToAsciiString(bin);
        }
        #endregion
        #region WordCount
        public static int WordCount(String source)
        {
            return WordCount(source, StdWordDelims);
        }
        public static int WordCount(String source, ACharArray wordDelims)
        {
            var result = 0;
            var i = 0;
            var lev = source.Length;
            while (i < lev)
            {
                while (i < lev && wordDelims.Contains(source[i]))
                    i += 1;
                if (i <= lev)
                    result += 1;
                while (i < lev && !wordDelims.Contains(source[i]))
                    i += 1;
            }
            return result;
        }
        #endregion
        #region ExtractWord
        public static String ExtractWord(int n, String s)
        {
            return ExtractWord(n, s, StdWordDelims);
        }
        public static String ExtractWord(int n, String s, ACharArray wordDelims)
        {
            using (var sb = new UStringBuilder())
            {
                var i = WordPosition(n, s, wordDelims);
                if (i == -1)
                    return sb.ToString();
                /* find the end of the current word */
                while (i < s.Length && !(wordDelims.Contains(s[i])))
                {
                    /* add the I'th character to result */
                    sb.Append(s[i]);
                    i++;
                }
                return sb.ToString();
            }
        }
        #endregion
        #region WordPosition
        public static int WordPosition(int n, String s)
        {
            return WordPosition(n, s, StdWordDelims);
        }
        public static int WordPosition(int n, String s, ACharArray wordDelims)
        {
            var count = 0;
            var i = 0;
            var result = 0;
            while (i < s.Length && count != n)
            {
                /* skip over delimiters */
                while ((i < s.Length) && (wordDelims.Contains(s[i])))
                    i++;
                /* if we're not beyond end of S, we're at the start of a word */
                if (i < s.Length)
                    count++;
                /* if not finished, find the end of the current word */
                if (count != n)
                    while ((i < s.Length) && !(wordDelims.Contains(s[i])))
                        i++;
                else
                    result = i;
            }
            return result;
        }
        #endregion
        #region IsWordPresent
        public static Boolean IsWordPresent(String w, String s)
        {
            return IsWordPresent(w, s, StdWordDelims);
        }
        public static Boolean IsWordPresent(String w, String s, ACharArray wordDelims)
        {
            var count = WordCount(s, wordDelims);
            for (var i = 1; i <= count; i++)
            {
                if (ExtractWord(i, s, wordDelims) == w)
                    return true;
            }
            return false;
        }
        #endregion
    }
}
