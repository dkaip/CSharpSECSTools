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
	public abstract class SECSItem
	{
	    protected SECSItemFormatCode  formatCode                  = SECSItemFormatCode.UNDEFINED;
	    protected int                 inboundNumberOfLengthBytes  = 0;
	    protected int                 outboundNumberOfLengthBytes = 0;
	    protected int                 lengthInBytes               = 0;
	    protected int                 numberOfElements            = 0;
	    protected int                 bytesConsumed               = 0;
		
	    /**
	     * 
	     * @param formatCode
	     * @param length - length in bytes
	     */
	    protected SECSItem(SECSItemFormatCode formatCode, int length)
	    {
	        this.formatCode = formatCode;
	        outboundNumberOfLengthBytes = calculateMinimumNumberOfLengthBytes(length);
	    }
		
		/**
	     * 
	     * @param formatCode
	     * @param length - length in bytes
	     * @param desiredNUmberOfLengthBytes
	     */
	    protected SECSItem(SECSItemFormatCode formatCode, int length, int desiredNUmberOfLengthBytes)
	    {
	        this.formatCode = formatCode;
	        outboundNumberOfLengthBytes = calculateMinimumNumberOfLengthBytes(length);
	        if (desiredNUmberOfLengthBytes > 0 &&
	            desiredNUmberOfLengthBytes < 4 &&
	            outboundNumberOfLengthBytes < desiredNUmberOfLengthBytes)
	        {
	            outboundNumberOfLengthBytes = desiredNUmberOfLengthBytes;
	        }
	    }
	
	    protected SECSItem(byte[] data, int itemOffset)
	    {
	        int incomingDataLength = 0;
	        
	        if (data.Length == 0)
	        {
	            formatCode = SECSItemFormatCode.HeaderOnly;
	            inboundNumberOfLengthBytes = 0;
	            data = null;
	            return;
	        }
	
	        formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)((data[itemOffset] >> 2) & 0x0000003F));
	        inboundNumberOfLengthBytes = (data[itemOffset] & 0x03);
	
            byte[] temp1 = new byte[4];
	        switch (inboundNumberOfLengthBytes)
	        {
	            case 1:
	            {
	                temp1[0] = 0;
	                temp1[1] = 0;
	                temp1[2] = 0;
	                temp1[3] = data[itemOffset +1];
					
	                break;
	            }
	            case 2:
	            {
	                temp1[0] = 0;
	                temp1[1] = 0;
	                temp1[2] = data[itemOffset+1];
	                temp1[3] = data[itemOffset+2];
	                break;
	            }
	            case 3:
	            {
	                temp1[0] = 0;
	                temp1[1] = data[itemOffset+1];
	                temp1[2] = data[itemOffset+2];
	                temp1[3] = data[itemOffset+3];
	                break;
	            }
	        }
	        
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp1);
		
			incomingDataLength = BitConverter.ToInt32(temp1, 0);

			if (formatCode == SECSItemFormatCode.L)
	            numberOfElements = incomingDataLength;
	        else
	            lengthInBytes = incomingDataLength;
	        
	        
	    }
	    
	    private int calculateMinimumNumberOfLengthBytes(int length)
	    {
	        int result = 1;
	        if (length > 255)
	        {
	            if (length < 65536)
	                result = 2;
	            else
	                result = 3;
	        }
	        
	        return result;
	    }
	
	    /**
	     * Returns the length of what the out bound item header will be. 
	     * @return length in bytes
	     */
	    protected int outputHeaderLength()
	    {
	        return outboundNumberOfLengthBytes + 1;
	    }
	    
	    /**
	     * This method fills in the raw "header" for a SECS item.
	     * 
	     * Make sure the buffer is large enough!!!
	     * 
	     * @param buffer  The buffer that will be used to contain an outgoing message item
	     * @Param numberOfBytes number of bytes in the payload...get this right
	     * @return The offset to where the payload data may loaded into the buffer
	     */
	    protected int populateHeaderData(byte[] buffer, int numberOfBytes)
	    {
	        int offset = 0;
	        buffer[0] = (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(formatCode) << 2) | outboundNumberOfLengthBytes);
	        
			byte[] outputLengthBytes = BitConverter.GetBytes(numberOfBytes);
			
			if (BitConverter.IsLittleEndian)
				Array.Reverse(outputLengthBytes);
		
	        if (outboundNumberOfLengthBytes == 1)
	        {
	            buffer[1] = outputLengthBytes[3];
	            offset = 2;
	        }
	        else if (outboundNumberOfLengthBytes == 2)
	        {
	            buffer[1] = outputLengthBytes[2];
	            buffer[2] = outputLengthBytes[3];
	            offset = 3;
	        }
	        else
	        {
	            buffer[1] = outputLengthBytes[1];
	            buffer[2] = outputLengthBytes[2];
	            buffer[3] = outputLengthBytes[3];
	            offset = 4;
	        }
	        
	        return offset;
	    }
	    
	    /// <summary>
	    /// Gets the SECS item format code.
		/// 
		/// This is probably not a method you need to be using.  It was
		/// originally created for unit testing because and its scope
		/// needed to be set to public for unit testing access.
		/// 
	    /// </summary>
	    /// <returns>The SECS item format code.</returns>
	    public SECSItemFormatCode getSECSItemFormatCode()
	    {
	        return formatCode;
	    }
	    
	    public int getLengthInBytes()
	    {
	        return lengthInBytes;
	    }
	    
	    public int getInboundNumberOfLengthBytes()
	    {
	        return inboundNumberOfLengthBytes;
	    }
	    
		public int getBytesConumed()
		{
			return bytesConsumed;
		}

	    public int getOutboundNumberOfLengthBytes()
	    {
	        return outboundNumberOfLengthBytes;
	    }
	    
	    public abstract override bool Equals(Object abc);
	    public abstract override int GetHashCode();
	    public abstract byte[] toRawSECSItem();
	
    }
}

