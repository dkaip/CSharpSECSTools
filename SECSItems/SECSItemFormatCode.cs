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
	public enum SECSItemFormatCode
	{
	    /*
	     * Don't change this list without performing the appropriate
	     * corrections in the RawSECSItemFormatCodeSingleton Class.
	     */
	    L,
	    B,
	    BO,
	    A,
	    J8,
	    C2,
	    I8,
	    I1,
	    I2,
	    I4,
	    F8,
	    F4,
	    U8,
	    U1,
	    U2,
	    U4,
	    UNDEFINED,
	    HeaderOnly
	}
	    
	public static class SECSItemFormatCodeFunctions
	{
	    /**
	     * 
	     * @return The numerical equivalent of the SECS Item Format Code
	     */
	    public static int getNumberFromSECSItemFormatCode(SECSItemFormatCode formatCode)
	    {
	        return RawSECSItemFormatCode.mapFormatCodeToNumber(formatCode);
	    }
	    
	    /**
	     * 
	     * @param number - 
	     * @return
	     */
	    public static SECSItemFormatCode getSECSItemFormatCodeFromNumber(byte number)
	    {
	        return RawSECSItemFormatCode.mapNumberToFormatCode(number);
	    }
	}
}

