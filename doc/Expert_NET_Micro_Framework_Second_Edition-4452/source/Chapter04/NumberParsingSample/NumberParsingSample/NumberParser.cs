/*
 * NumberParser.cs - Implementation of "System.Private.NumberParser".
 *
 * Copyright (C) 2001  Southern Storm Software, Pty Ltd.
 * Copyright (C) 2004  Free Software Foundation, Inc.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * Ported and simplified to the .NET Micro Framework 2007 by Jens Kühner
 * blog: http://bloggingabout.net/blogs/jens
 * 
 */
#define SUPPORT_FLOATINGPOINT_NUMERICS

using System;
using System.Globalization;
using System.Text;

namespace Kuehner
{
    public static class NumberParser
    {
        // Parse integer values using localized number format information.
        private static bool TryParseUInt64Core(string str, bool parseHex,
                                               out ulong result, out bool sign)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            char ch;
            bool noOverflow = true;
            result = 0;

            // Skip leading white space.
            int len = str.Length;
            int posn = 0;
            while (posn < len && IsWhiteSpace(str[posn]))
                posn++;

            // Check for leading sign information.
            NumberFormatInfo nfi = CultureInfo.CurrentUICulture.NumberFormat;
            string posSign = nfi.PositiveSign;
            string negSign = nfi.NegativeSign;
            sign = false;
            while (posn < len)
            {
                ch = str[posn];
                if (!parseHex && ch == negSign[0])
                {
                    sign = true;
                    ++posn;
                }
                else if (!parseHex && ch == posSign[0])
                {
                    sign = false;
                    ++posn;
                }
                /*      else if (ch == thousandsSep[0])
                        {
                            ++posn;
                        }*/
                else if ((parseHex && ((ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f'))) ||
                         (ch >= '0' && ch <= '9'))
                {
                    break;
                }
                else
                {
                    return false;
                }
            }

            // Bail out if the string is empty.
            if (posn >= len)
                return false;

            // Parse the main part of the number.
            uint low = 0;
            uint high = 0;
            uint digit;
            ulong tempa, tempb;
            if (parseHex)
            {
                #region Parse a hexadecimal value.
                do
                {
                    // Get the next digit from the string.
                    ch = str[posn];
                    if (ch >= '0' && ch <= '9')
                    {
                        digit = (uint)(ch - '0');
                    }
                    else if (ch >= 'A' && ch <= 'F')
                    {
                        digit = (uint)(ch - 'A' + 10);
                    }
                    else if (ch >= 'a' && ch <= 'f')
                    {
                        digit = (uint)(ch - 'a' + 10);
                    }
                    else
                    {
                        break;
                    }

                    // Combine the digit with the result, and check for overflow.
                    if (noOverflow)
                    {
                        tempa = ((ulong)low) * ((ulong)16);
                        tempb = ((ulong)high) * ((ulong)16);
                        tempb += (tempa >> 32);
                        if (tempb > ((ulong)0xFFFFFFFF))
                        {
                            // Overflow has occurred.
                            noOverflow = false;
                        }
                        else
                        {
                            tempa = (tempa & 0xFFFFFFFF) + ((ulong)digit);
                            tempb += (tempa >> 32);
                            if (tempb > ((ulong)0xFFFFFFFF))
                            {
                                // Overflow has occurred.
                                noOverflow = false;
                            }
                            else
                            {
                                low = unchecked((uint)tempa);
                                high = unchecked((uint)tempb);
                            }
                        }
                    }
                    ++posn; // Advance to the next character.
                } while (posn < len);
                #endregion
            }
            else
            {
                #region Parse a decimal value.
                do
                {
                    // Get the next digit from the string.
                    ch = str[posn];
                    if (ch >= '0' && ch <= '9')
                        digit = (uint)(ch - '0');
                    /*       else if (ch == thousandsSep[0])
                           {
                               // Ignore thousands separators in the string.
                               ++posn;
                               continue;
                           }*/
                    else
                        break;

                    // Combine the digit with the result, and check for overflow.
                    if (noOverflow)
                    {
                        tempa = ((ulong)low) * ((ulong)10);
                        tempb = ((ulong)high) * ((ulong)10);
                        tempb += (tempa >> 32);
                        if (tempb > ((ulong)0xFFFFFFFF))
                        {
                            // Overflow has occurred.
                            noOverflow = false;
                        }
                        else
                        {
                            tempa = (tempa & 0xFFFFFFFF) + ((ulong)digit);
                            tempb += (tempa >> 32);
                            if (tempb > ((ulong)0xFFFFFFFF))
                            {
                                // Overflow has occurred.
                                noOverflow = false;
                            }
                            else
                            {
                                low = unchecked((uint)tempa);
                                high = unchecked((uint)tempb);
                            }
                        }
                    }
                    ++posn;// Advance to the next character.
                } while (posn < len);
                #endregion
            }

            // Process trailing white space.
            if (posn < len)
            {
                do
                {
                    ch = str[posn];
                    if (IsWhiteSpace(ch))
                        ++posn;
                    else
                        break;
                } while (posn < len);
                if (posn < len)
                    return false;
            }

            // Return the results to the caller.
            result = (((ulong)high) << 32) | ((ulong)low);
            return noOverflow;
        }

        public static bool TryParseInt64(string str, out long result)
        {
            result = 0;
            ulong r;
            bool sign;
            if (TryParseUInt64Core(str, false, out r, out sign))
            {
                if (!sign)
                {
                    if (r <= 9223372036854775807)
                    {
                        result = unchecked((long)r);
                        return true;
                    }
                }
                else
                {
                    if (r <= 9223372036854775808)
                    {
                        result = unchecked(-((long)r));
                        return true;
                    }
                }
            }
            return false;
        }

        public static long ParseInt64(string str)
        {
            long result;
            if (TryParseInt64(str, out result))
                return result;
            throw new Exception();
        }

        public static bool TryParseUInt64(string str, out ulong result)
        {
            bool sign;
            return TryParseUInt64Core(str, false, out result, out sign) && !sign;
        }

        public static ulong ParseUInt64(string str)
        {
            ulong result;
            if (TryParseUInt64(str, out result))
                return result;
            throw new Exception();
        }

        public static bool TryParseUInt64Hex(string str, out ulong result)
        {
            bool sign;
            return TryParseUInt64Core(str, true, out result, out sign);
        }

        public static ulong ParseUInt64Hex(string str)
        {
            ulong result;
            if (TryParseUInt64Hex(str, out result))
                return result;
            throw new Exception();
        }

        private static bool IsWhiteSpace(char ch)
        {
            return ch == ' ';
        }

#if SUPPORT_FLOATINGPOINT_NUMERICS

        private const int numDoubleDigits = 16;

        // parse the number beginning at str[start] up to maximal str[end].
        // passed parameters:
        // str character array containing the data to parse
        // style and nfi are the formatspecs
        // start and end are the indexes of the first and last character to parse
        // maxDigits must not extend 18 else an overflow may occure
        // numdigits must be 0
        // on exit start points to the first character after the last parsed digit.
        // end is unchanged. 
        // numDigits is updated to the number of significant digits parsed.
        // if numDigits > maxDigits the value has to be calculated 
        // returnvalue * Math.Pof(10, (numDigits - maxDigits)).
        private static ulong ParseNumberCore(string str,
                                             ref int start,
                                             int end, int maxDigits,
                                             ref int numDigits, bool allowGroupSep)
        {
            char curChar;
            ulong ulwork = 0;

            // now parse the real number
            string sep = CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator;
            while (start <= end)
            {
                curChar = str[start];
                if (curChar >= '0' && curChar <= '9')
                {
                    if (numDigits < maxDigits)
                        ulwork = ulwork * 10 + unchecked((uint)(curChar - '0'));
                    start++;
                    numDigits++;
                }
                else
                {
                    // check for groupseparator if allowed
                    if (!allowGroupSep || !CheckSeparator(str, sep, ref start, end))
                        break;
                }
            }
            return ulwork;
        }

        public static bool TryParseDouble(string str, out double result)
        {
            ulong decValue = 0;
            bool hasExpSign = false;
            bool isExpNeg = false;
            int expDigits = 0;
            ulong expValue = 0;
            result = 0;

            if (str == null)
                throw new ArgumentNullException("str");

            int end = str.Length - 1;
            int start = 0;

            // skip whitespaces
            SkipWhiteSpace(str, ref start, ref end);

            // check for leading sign
            bool hasSign = false;
            bool isNeg = false;
            if (start <= end)
                CheckSign(str, ref start, end, ref hasSign, ref isNeg);

            // now parse the real number
            int intDigits = 0;
            ulong intValue = ParseNumberCore(str, ref start, end,
                                       numDoubleDigits, ref intDigits, true);

            int decDigits = 0;
            if (start <= end)
            {
                // now check for the decimal point and the decimalplaces
                if (CheckSeparator(str, CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator,
                                   ref start, end))
                {
                    if (start <= end)
                    {
                        decValue = ParseNumberCore(str, ref start, end,
                                                  numDoubleDigits - intDigits,
                                                  ref decDigits, false);
                    }
                }
            }

            // now check for the exponent
            if (start <= end)
            {
                char curChar = str[start];
                if (curChar == 'E' || curChar == 'e')
                {
                    start++;
                    if (start <= end)
                    {
                        // check for sign
                        CheckSign(str, ref start, end, ref hasExpSign, ref isExpNeg);
                        // get exponent
                        if (start <= end)
                            expValue = ParseNumberCore(str, ref start, end, 5, ref expDigits, false);
                        if (expDigits <= 0)
                            return false;
                    }
                }
            }

            if (start <= end) // characters left 
                return false;

            // now calculate the value
            result = (double)intValue;
            if (intDigits > numDoubleDigits)
                result *= Math.Pow(10, (double)(intDigits - numDoubleDigits));
            if (decDigits > 0)
                result += (decValue * Math.Pow(10d, (double)(-decDigits)));
            if (isNeg)
                result *= -1;
            //now the exponent
            if (expDigits > 0)
            {
                if (isExpNeg)
                    result *= Math.Pow(10d, (double)expValue * -1);
                else
                    result *= Math.Pow(10d, (double)expValue);
            }
            return true;
        }

        public static double ParseDouble(string str)
        {
            double result;
            if (TryParseDouble(str, out result))
                return result;
            throw new Exception();
        }

        private static void SkipWhiteSpace(string str, ref int start, ref int end)
        {
            while (start <= end && IsWhiteSpace(str[start]))
                start++;

            // remove trailing whitespaces
            if (start <= end)
            {
                while (start <= end && IsWhiteSpace(str[end]))
                    end--;
            }
        }

        private static void CheckSign(string str,
                              ref int start, int end,
                              ref bool hasSign, ref bool isNeg)
        {
            int counter;
            int current;
            string sign = CultureInfo.CurrentUICulture.NumberFormat.NegativeSign;
            char signChar = sign[0];
            int signLength = sign.Length;
            char curChar = str[start];

            // check for negative sign at the beginning
            if (curChar == signChar)
            {
                counter = 1;
                current = start + 1; ;

                if (signLength > 1)
                {
                    while (counter < signLength && current <= end)
                    {
                        if (str[current] != sign[counter])
                            break;
                        current++;
                        counter++;
                    }
                }
                if (counter >= signLength)
                {
                    hasSign = true;
                    isNeg = true;
                    start = current;
                    return;
                }
            }

            // check for positive sign at the beginning
            sign = CultureInfo.CurrentUICulture.NumberFormat.PositiveSign;
            signChar = sign[0];
            signLength = sign.Length;
            if (curChar == signChar)
            {
                counter = 1;
                current = start + 1;

                if (signLength > 1)
                {
                    while (counter < signLength && current <= end)
                    {
                        if (str[current] != sign[counter])
                            break;
                        current++;
                        counter++;
                    }
                }
                if (counter >= signLength)
                {
                    hasSign = true;
                    start = current;
                }
            }
        }

        private static bool CheckSeparator(string str, string sep, ref int start, int end)
        {
            int strLength = sep.Length;

            if (strLength > 0)
            {
                char curChar = str[start];
                char strChar = sep[0];

                // check for first Character at the beginning
                if (curChar == strChar)
                {
                    int counter = 1;
                    int current = start + 1;
                    while (counter < strLength && current <= end)
                    {
                        if (str[current] != sep[counter])
                            break;
                        current++;
                        counter++;
                    }
                    if (counter >= strLength) // string found
                    {
                        // so update to new start position
                        start = current;
                        return true;
                    }
                }
            }
            return false;
        }
#endif // CONFIG_EXTENDED_NUMERICS

    };
};
