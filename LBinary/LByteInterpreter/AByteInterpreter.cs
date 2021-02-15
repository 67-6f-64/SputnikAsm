using System;
using System.Globalization;
using Sputnik.LBinary;
using Sputnik.LInterfaces;
using Sputnik.LUtils;
using SputnikAsm.LBinary.LByteInterpreter.LEnums;
using SputnikAsm.LExtensions;
using SputnikAsm.LMemScan.LEnums;
using SputnikAsm.LProcess;
using SputnikAsm.LProcess.Utilities;
using SputnikAsm.LSymbolHandler;
using SputnikAsm.LUtils;

namespace SputnikAsm.LBinary.LByteInterpreter
{
    public class AByteInterpreter : IUDisposable
    {
        #region Properties
        public AProcessSharp Proc => SymbolHandler.Process;
        public Boolean IsDisposed { get; set; }
        #endregion
        #region Variables
        public ASymbolHandler SymbolHandler;
        #endregion
        #region Constructor
        public AByteInterpreter(ASymbolHandler symbolHandler)
        {
            IsDisposed = false;
            SymbolHandler = symbolHandler;
        }
        #endregion
        #region Dispose
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
        }
        #endregion
        #region FindTypeOfData
        public AVariableType FindTypeOfData(UIntPtr address, UBytePtr buf, int size, AFindTypeOption findOption = AFindTypeOption.None)
        {
            var v = 0UL;
            //check if it matches a string
            var result = AVariableType.DWord;
            try
            {
                var floatHasSeparator = false;
                int i;
                if (!UEnumUtils.HasFlag(findOption, AFindTypeOption.NoString))
                {
                    var isString = true;
                    var couldBeStringCounter = true;
                    i = 0;
                    while (i < 4)
                    {
                        //check if the first 4 characters match with a standard ascii values (32 to 127)
                        if (i < size)
                        {
                            if (buf[i] < 32 || buf[i] > 127)
                            {
                                isString = false;
                                if (i > 0)
                                    couldBeStringCounter = false;
                                if (!couldBeStringCounter)
                                    break;
                            }
                        }
                        else
                        {
                            isString = false;
                            couldBeStringCounter = false;
                        }

                        i += 1;
                    }
                    if (isString)
                    {
                        result = AVariableType.String;
                        return result;
                    }
                    if (couldBeStringCounter && size > 4 && (buf[4] >= 32 || buf[4] <= 127))  //check if the 4th byte of the 'string' is a char or not
                    {
                        //this is a string counter
                        result = AVariableType.Byte;
                        return result;
                    }
                    //check if unicode
                    isString = true;
                    i = 0;
                    if (size >= 8)
                    {
                        while (i < 8)
                        {
                            //check if the first 4 characters match with a standard ascii values (32 to 127)
                            if (buf[i] < 32 || buf[i] > 127)
                            {
                                isString = false;
                                break;
                            }
                            i += 1;
                            if (buf[i] != 0)
                            {
                                isString = false;
                                break;
                            }
                            i += 1;
                        }
                    }
                    else
                        isString = false;
                    if (isString)
                    {
                        result = AVariableType.UnicodeString;
                        return result;
                    }
                }
                i = (int)(address.ToUInt64() % 4);
                if (size == 4 || size == 8)
                    i = 0; //skip this test, it's a known size data block
                switch (i)
                {
                    case 1: //1 byte
                        result = AVariableType.Byte;
                        return result;
                    case 2:
                    case 3: //2 byte
                        if (buf.ReadUInt16() < 255 || buf.ReadUInt16() % 10 > 0)  //less than 2 byte or not dividable by 10
                            result = AVariableType.Byte;
                        else
                            result = AVariableType.Word;
                        return result;
                }
                if (size >= Proc.PointerSize)
                {
                    //named addresses
                    int n;
                    if (Proc.IsX64)
                    {
                        if ((int)(address.ToUInt64() % 8) == 0)
                            AStringUtils.Val("0x" + SymbolHandler.GetNameFromAddress((UIntPtr)buf.ReadUInt64(), true, true, false, null, out _, 8, false), out v, out n);
                        else
                            n = 0;
                    }
                    else
                        AStringUtils.Val("0x" + SymbolHandler.GetNameFromAddress((UIntPtr)buf.ReadUInt32(), true, true, false, null, out _, 8, false), out v, out n);
                    if (n > 0)  //named
                    {
                        result = AVariableType.Pointer;
                        return result;
                    }
                }
                String x;
                if (size >= 2 && size < 4)
                {
                    result = AVariableType.Word;
                    return result;
                }
                else if (size == 1)
                {
                    result = AVariableType.Byte;
                    return result;
                }
                else if (Math.Abs(buf.ReadFloat()) > Single.Epsilon) // todo this seems unreliable since anything can be a single..
                {
                    x = buf.ReadFloat().ToString(CultureInfo.InvariantCulture);
                    if (AStringUtils.Pos("E", x) == -1)   //no exponent
                    {
                        //check if the value isn't bigger or smaller than 100000 or smaller than -100000
                        if (AMathUtils.InRange(buf.ReadFloat(), -100000.0, 100000.0))
                        {
                            if (AStringUtils.Pos(AConstants.DecimalSeparator, x) != -1)
                                floatHasSeparator = true;
                            result = AVariableType.Single;
                            if (x.Length <= 4 || !floatHasSeparator)
                                return result;  //it's a full floating point value or small enough to fit in 3 digits and a separator (1.01, 1.1 ....)
                        }
                    }
                }
                if (!UEnumUtils.HasFlag(findOption, AFindTypeOption.NoDouble))
                {
                    if (size >= 8)   //check if a double can be used
                    {
                        if (Math.Abs(buf.ReadDouble()) > Double.Epsilon) // todo this seems unreliable since anything can be a double..
                        {
                            x = buf.ReadDouble().ToString(CultureInfo.InvariantCulture);
                            if (AStringUtils.Pos("E", x) == -1)   //no exponent
                            {
                                //check if the value isn't bigger or smaller than 100000 or smaller than -100000
                                if (buf.ReadDouble() < 100000 && buf.ReadDouble() > -100000)
                                {
                                    if (result == AVariableType.Single)
                                    {
                                        if (buf.ReadDouble() > buf.ReadFloat())
                                            return result; //float has a smaller value
                                    }
                                    result = AVariableType.Double;
                                    if (buf.ReadUInt32() != 0)
                                    {
                                        x = buf.ReadFloat().ToString(CultureInfo.InvariantCulture);
                                        if (AStringUtils.Pos("E", x) == -1)   //no exponent
                                        {
                                            //if 4 bytes after this address is a float then override this double to a single type
                                            if (FindTypeOfData(address + 4, buf.Shift(4), size - 4) == AVariableType.Single)
                                                result = AVariableType.Single;
                                        }
                                    }
                                    return result;
                                }
                            }
                        }
                    }
                }
                //check if it's a pointer
                if (Proc.IsX64)
                {
                    if (((int)(address.ToUInt64() % 8) == 0) & AMemoryHelper.IsReadable(Proc.Handle, ((UIntPtr)buf.ReadUInt64()).ToIntPtr()))
                    {
                        result = AVariableType.Pointer;
                        return result;
                    }
                }
                else
                {
                    if (AMemoryHelper.IsReadable(Proc.Handle, ((UIntPtr)buf.ReadUInt32()).ToIntPtr()))
                    {
                        result = AVariableType.Pointer;
                        // if inrange(pdword(@buf[0])^, $3d000000, $44800000)=false then //if it's not in this range, assume it's a pointer. Otherwise, could be a float
                        return result;
                    }
                }
                // todo implement custom type
                //if customtype is not nil check if the dword is humanreadable or not
                // if ((customtype != nil) && (result == vtdword) && (ishumanreadableinteger(pdword(&buf[0])) == false))
                // {
                //     //not human readable, see if there is a custom type that IS human readable
                //     for (i = 0; i <= customtypes.count - 1; i++)
                //     {
                //         if (tcustomtype(customtypes[i]).scriptusesfloat)
                //         {
                //             //float check
                //             f = tcustomtype(customtypes[i]).convertdatatofloat(&buf[0], address);
                //             x = floattostr(f);
                // 
                //             if ((pos("E", x) == 0) && (f != 0) & inrange(f, -100000.0, 100000.0))
                //             {
                //                 result = vtcustom;
                //                 customtype = customtypes[i];
                // 
                //                 if (pos(defaultformatsettings.decimalseparator, x) == 0)
                //                     flush(); //found one that has no decimal seperator
                // 
                //             }
                //         }
                //         else
                //         {
                //             //dword check
                //             if (ishumanreadableinteger(tcustomtype(customtypes[i]).convertdatatointeger(&buf[0], address)))
                //             {
                //                 result = vtcustom;
                //                 customtype = customtypes[i];
                //                 flush();
                //             }
                //         }
                //     }
                // }
            }
            finally
            {
                // todo implement custom types
                // if (assigned(onautoguessroutine))
                //     result = onautoguessroutine(address, result);
            }
            return result;
        }
        #endregion
        #region IsHumanReadableInteger
        public Boolean IsHumanReadableInteger(int v)
        {
            //check if the value is a human usable value (between 0 and 10000 or dividable by at least 100)
            //Human readable if:
            //The value is in the range of -10000 and 10000
            //The value is dividable by 100
            var result = AMathUtils.InRange(v, -10000, 10000) | (v % 100 == 0);
            return result;
        }
        #endregion
        #region DataToString
        public String DataToString(UBytePtr buf, int size, AVariableType varType, Boolean clean = false)
            /*note: If type is of string unicode, the last 2 bytes will get set to 0, so watch what you're calling*/
        {
            String result;
            switch (varType)
            {
                case AVariableType.Byte:
                {
                    if (clean)
                        result = AStringUtils.IntToHex(buf[0], 2);
                    else
                        result = "(byte)" + AStringUtils.IntToHex(buf[0], 2) + '(' + buf[0] + ')';
                    break;
                }
                case AVariableType.Word:
                {
                    if (clean)
                        result = AStringUtils.IntToHex(buf.ReadUInt16(), 4);
                    else
                        result = "(word)" + AStringUtils.IntToHex(buf.ReadUInt16(), 4) + '(' + buf.ReadUInt16() + ')';
                    break;
                }
                case AVariableType.DWord:
                {
                    if (clean)
                        result = AStringUtils.IntToHex(buf.ReadUInt32(), 8);
                    else
                        result = "(dword)" + AStringUtils.IntToHex(buf.ReadUInt32(), 8) + '(' + buf.ReadUInt32() + ')';
                    break;
                }
                case AVariableType.QWord:
                {
                    if (clean)
                        result = AStringUtils.IntToHex(buf.ReadUInt64(), 16);
                    else
                        result = "(qword)" + AStringUtils.IntToHex(buf.ReadUInt64(), 16) + '(' + buf.ReadUInt64() + ')';
                    break;
                }
                case AVariableType.Single:
                {
                    if (clean)
                        result = UStringUtils.Sprintf("%.2f", buf.ReadFloat());
                    else
                        result = "(float)" + UStringUtils.Sprintf("%.2f", buf.ReadFloat());
                    break;
                }
                case AVariableType.Double:
                {
                    if (clean)
                        result = UStringUtils.Sprintf("%.2f", buf.ReadDouble());
                    else
                        result = "(double)" + UStringUtils.Sprintf("%.2f", buf.ReadDouble());
                    break;
                }
                case AVariableType.String:
                {
                    using (var p = new UBytePtr(size + 1))
                    {
                        AArrayUtils.CopyMemory(p, buf, size);
                        p[size] = 0;
                        result = UBitConverter.UnpackSingle("z1", 0, p.ToByteArray());
                    }
                    break;
                }
                case AVariableType.UnicodeString:
                {
                    using (var p = new UBytePtr(size + 2))
                    {
                        AArrayUtils.CopyMemory(p, buf, size);
                        p[size] = 0;
                        p[size + 1] = 0;
                        result = UBitConverter.UnpackSingle("z7", 0, p.ToByteArray());
                    }
                    break;
                }
                case AVariableType.Pointer:
                {
                    UIntPtr a;
                    if (Proc.IsX64)
                        a = (UIntPtr)buf.ReadUInt64();
                    else
                        a = (UIntPtr)buf.ReadUInt32();
                    if (clean)
                        result = "";
                    else
                        result = "(pointer)";
                    result += SymbolHandler.GetNameFromAddress(a, true, true, false);
                    break;
                }
                default:
                {
                    result = "(...)";
                    for (var i = 0; i <= Math.Min(size, 8) - 1; i++)
                        result = result + AStringUtils.IntToHex(buf[i], 2) + ' ';
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
