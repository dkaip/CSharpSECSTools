/*
 * Copyright 2019 Douglas Kaip
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This <c>enum</c> contains the valid values for the format code of a SECSItem.
    /// </summary>
	public enum SECSItemFormatCode
	{
	    /*
	     * Don't change this list without performing the appropriate
	     * corrections in the RawSECSItemFormatCodeSingleton Class.
	     */
        /// <summary>
        /// List
        /// </summary>
	    L,
        /// <summary>
        /// Binary
        /// </summary>
	    B,
        /// <summary>
        /// Boolean
        /// </summary>
	    BO,
        /// <summary>
        /// ASCII
        /// </summary>
	    A,
        /// <summary>
        /// JIS-8 - Japanese Industrial Standard 8
        /// </summary>
	    J8,
        /// <summary>
        /// 2 Byte Character format
        /// </summary>
	    C2,
        /// <summary>
        /// 8 Byte Signed Integer
        /// </summary>
	    I8,
        /// <summary>
        /// 1 Byte Signed Integer
        /// </summary>
	    I1,
        /// <summary>
        /// 2 Byte Signed Integer
        /// </summary>
	    I2,
        /// <summary>
        /// 4 Byte Signed Integer
        /// </summary>
	    I4,
        /// <summary>
        /// 8 Byte Floating Point
        /// </summary>
	    F8,
        /// <summary>
        /// 4 Byte Floating Point
        /// </summary>
	    F4,
        /// <summary>
        /// 8 Byte Unsigned Integer
        /// </summary>
	    U8,
        /// <summary>
        /// 1 Byte Unsigned Integer
        /// </summary>
	    U1,
        /// <summary>
        /// 2 Byte Unsigned Integer
        /// </summary>
	    U2,
        /// <summary>
        /// 4 Byte Unsigned Integer
        /// </summary>
	    U4,
        /// <summary>
        /// UNDEFINED - not typically used by the developer.
        /// </summary>
	    UNDEFINED,
        /// <summary>
        /// HeaderOnly - not typically used by the developer
        /// </summary>
	    HeaderOnly
	}
	    
    /// <summary>
    /// A class containing a couple of helper methods useful when converting a
    /// <c>SECSItem</c>'s format code to and from &quot;wire / transmission&quot; format.
    /// </summary>
    public static class SECSItemFormatCodeFunctions
	{
        /// <summary>
        /// This method is typically only used the retrieve the proper numeric value
        /// of an Item Format Code during the conversion of a <c>SECSItem</c> into its
        /// &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>The number value of the specified <c>SECSItemFormatCode</c>.</returns>
        /// <param name="formatCode">Item format code.</param>
	    public static int GetNumberFromSECSItemFormatCode(SECSItemFormatCode formatCode)
	    {
	        return RawSECSItemFormatCode.mapFormatCodeToNumber(formatCode);
	    }
	    
        /// <summary>
        /// This method is typically only used when converting an Item Format Code
        /// from its &quot;wire/transmission format&quot; into the format used 
        /// (a type of SECSItem) in the C# environment.
        /// </summary>
        /// <returns>The SECSI tem format code from number.</returns>
        /// <param name="number">Number.</param>
	    public static SECSItemFormatCode GetSECSItemFormatCodeFromNumber(byte number)
	    {
	        return RawSECSItemFormatCode.mapNumberToFormatCode(number);
	    }
	}
}

