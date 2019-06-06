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
	public sealed class RawSECSItemFormatCode
	{
	    private static SECSItemFormatCode[] numberToCodeMap;
	    private static int[] codeToNumberMap;

		static RawSECSItemFormatCode()
		{

	        numberToCodeMap = new SECSItemFormatCode[0x40];  // Decimal 64
	        
	        numberToCodeMap[0] = SECSItemFormatCode.L;
	        numberToCodeMap[1] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[2] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[3] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[4] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[5] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[6] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[7] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[8] = SECSItemFormatCode.B;
	        numberToCodeMap[9] = SECSItemFormatCode.BO;
	        numberToCodeMap[10] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[11] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[12] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[13] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[14] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[15] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[16] = SECSItemFormatCode.A;
	        numberToCodeMap[17] = SECSItemFormatCode.J8;
	        numberToCodeMap[18] = SECSItemFormatCode.C2;
	        numberToCodeMap[19] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[20] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[21] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[22] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[23] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[24] = SECSItemFormatCode.I8;
	        numberToCodeMap[25] = SECSItemFormatCode.I1;
	        numberToCodeMap[26] = SECSItemFormatCode.I2;
	        numberToCodeMap[27] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[28] = SECSItemFormatCode.I4;
	        numberToCodeMap[29] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[30] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[31] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[32] = SECSItemFormatCode.F8;
	        numberToCodeMap[33] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[34] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[35] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[36] = SECSItemFormatCode.F4;
	        numberToCodeMap[37] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[38] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[39] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[40] = SECSItemFormatCode.U8;
	        numberToCodeMap[41] = SECSItemFormatCode.U1;
	        numberToCodeMap[42] = SECSItemFormatCode.U2;
	        numberToCodeMap[43] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[44] = SECSItemFormatCode.U4;
	        numberToCodeMap[45] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[46] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[47] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[48] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[49] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[50] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[51] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[52] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[53] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[54] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[55] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[56] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[57] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[58] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[59] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[60] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[61] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[62] = SECSItemFormatCode.UNDEFINED;
	        numberToCodeMap[63] = SECSItemFormatCode.HeaderOnly;
	        
	        codeToNumberMap = new int[18];
	
	        codeToNumberMap[0] = 0x00;      //      L  ( 0x00 ),              // Octal 00
	        codeToNumberMap[1] = 0x08;      //      B  ( 0x08 ),              // Octal 10
	        codeToNumberMap[2] = 0x09;      //      BO ( 0x09 ),              // Octal 11
	        codeToNumberMap[3] = 0x10;      //      A  ( 0x10 ),              // Octal 20
	        codeToNumberMap[4] = 0x11;      //      J8 ( 0x11 ),              // Octal 21
	        codeToNumberMap[5] = 0x12;      //      C2 ( 0x12 ),              // Octal 22
	        codeToNumberMap[6] = 0x18;      //      I8 ( 0x18 ),              // Octal 30
	        codeToNumberMap[7] = 0x19;      //      I1 ( 0x19 ),              // Octal 31
	        codeToNumberMap[8] = 0x1A;      //      I2 ( 0x1A ),              // Octal 32
	        codeToNumberMap[9] = 0x1C;      //      I4 ( 0x1C ),              // Octal 34
	        codeToNumberMap[10] = 0x20;     //      F8 ( 0x20 ),              // Octal 40
	        codeToNumberMap[11] = 0x24;     //      F4 ( 0x24 ),              // Octal 44
	        codeToNumberMap[12] = 0x28;     //      U8 ( 0x28 ),              // Octal 50
	        codeToNumberMap[13] = 0x29;     //      U1 ( 0x29 ),              // Octal 51
	        codeToNumberMap[14] = 0x2A;     //      U2 ( 0x2A ),              // Octal 52
	        codeToNumberMap[15] = 0x2C;     //      U4 ( 0x2C ),              // Octal 54
	        codeToNumberMap[16] = 0x3E;     //      UNDEFINED ( 0x3E );
	        codeToNumberMap[17] = 0x3F;     //      HeaderOnly ( 0x3F );
		}
							

	    /**
	     * 
	     * Note: Yes, you can blow it up by specifying a number that is out of 
	     * bounds.  I'm reluctant to throw an exception for such misuse.
	     * 
	     * @param number
	     * @return
	     */
	    internal static SECSItemFormatCode mapNumberToFormatCode(byte number)
	    {
	        return numberToCodeMap[number];
	    }
	    
	    /**
	     * 
	     * @param itemFormatCode
	     * @return
	     */
	    internal static int mapFormatCodeToNumber(SECSItemFormatCode itemFormatCode)
	    {
	        return codeToNumberMap[(int)itemFormatCode];
	    }
	}
}

