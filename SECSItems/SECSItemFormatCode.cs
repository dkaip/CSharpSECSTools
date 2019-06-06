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

