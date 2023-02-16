/*
 * Copyright 2019-2023 Douglas Kaip
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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    ///  This is the base class for all of the <c>SECSItems</c> that are implemented in this
    /// library. Its purpose is to conceal the tedium of managing the information
    /// that will be needed for encoding/decoding the various SECSItems to/from
    /// the wire/transmission format that the transport layer(s) use.
    /// </summary>
    public abstract class SECSItem
    {
        private SECSItemFormatCode _formatCode  = SECSItemFormatCode.UNDEFINED;

        /// <summary>
        /// The Format Code of this <c>SECSItem</c>.
        /// </summary>
        public SECSItemFormatCode  ItemFormatCode
        {
            get { return _formatCode; }
        }

        /// <summary>
        /// Gets the <c>SECSItemFormatCode</c> of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItemFormatCode</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property ItemFormatCode instead.")]
        public SECSItemFormatCode GetSECSItemFormatCode()
        {
            return _formatCode;
        }

        private SECSItemNumLengthBytes _numberOfLengthBytes = SECSItemNumLengthBytes.NOT_INITIALIZED;

        /// <summary>
        /// The number of length bytes associated with this <c>SECSItem</c>.
        /// </summary>
        /// <remarks>
        /// This property when set will end up with the greater of the
        /// value specified or the minimum value required to contain a <c>LengthInBytes</c>
        /// amount of data.
        /// <para>
        /// If the value is needed as an <c>int</c> use the <c>ValueOf()</c> method
        /// of <c>SECSItemNumLengthBytes</c>to get it.
        /// </para>
        /// </remarks>
        public SECSItemNumLengthBytes NumberOfLengthBytes
        {
            get
            {
                return _numberOfLengthBytes;
            }
            set
            {
                SECSItemNumLengthBytes minLengthBytes = SECSItemNumLengthBytes.ONE;
                if (LengthInBytes > 255)
                {
                    if (LengthInBytes < 65536)
                        minLengthBytes = SECSItemNumLengthBytes.TWO;
                    else
                        minLengthBytes = SECSItemNumLengthBytes.THREE;
                }

                if (value.ValueOf() > minLengthBytes.ValueOf())
                {
                    _numberOfLengthBytes = value;
                }
                else
                {
                    _numberOfLengthBytes = minLengthBytes;
                }
            }
        }

        private int _lengthInBytes = 0;
        /// <summary>
        /// This is the length in bytes that this SECSItem requires, 
        /// NOT including its item header.
        /// </summary>
        public virtual int LengthInBytes { get { return _lengthInBytes; } }

        // /// <summary>
        // /// This contains the number of bytes that have been consumed when
        // /// a <c>SECSItem</c> is created from a SECS message that is in 
        // /// wire/transmission format.
        // /// </summary>
        // protected int                 bytesConsumed               = 0;
        
        /// <summary>
        /// This is a base class constructor for a <c>SECSItem</c>.  When using this constructor, the
        /// outbound number of length bytes will be set to the minimum number required to 
        /// handle an item with the length that is specified by the value of the 
        /// <c>length</c> parameter.
        /// <para/>
        /// This constructor sets the following base class attributes from the provided data: 
        /// <c>formatCode</c>, <c>lengthInBytes</c>, and <c>outboundNumberOfLengthBytes</c>.
        /// </summary>
        /// <param name="formatCode">The Format Code of the created <c>SECSItem</c>.</param>
        /// <param name="lengthInBytes">The length in bytes of the item, or in the case of a 
        /// <c>ListSECSItem</c> the number of items it contains.It must be between 0 
        /// and 16777215 inclusive..</param>
        protected SECSItem(SECSItemFormatCode formatCode, int lengthInBytes)
        {
            if (lengthInBytes < 0 || lengthInBytes > (int)0x00FFFFFF)
            {
                throw new ArgumentException(
                        "The value for the length argument must be between 0 and 16777215 inclusive.");
            }

            _formatCode = formatCode;
            _lengthInBytes = lengthInBytes;
            _numberOfLengthBytes = CalculateMinimumNumberOfLengthBytes(lengthInBytes);
        }
        
        /// <summary>
        /// This is a base class constructor for a SECSItem.  When using this constructor, the
        /// number of length bytes will be set to the greater of, the specified number of 
        /// length bytes, or the minimum number required to
        /// handle an item with the length that is specified by the value of the
        /// <c>lengthInBytes</c> parameter.
        /// This constructor sets the following base class attributes from the provided data:
        /// <c>formatCode</c>,
        /// <c>lengthInBytes</c>, and 
        /// <c>outboundNumberOfLengthBytes</c>.
        /// <para/>
        /// Note: An <c>ArgumentException</c> will be thrown if the value of the
        /// <c>lengthInBytes</c> parameter is outside the range of 0 - 16777215
        /// inclusive.
        /// </summary>
        /// <param name="formatCode">The SECS Item Format code for this SECSItem.</param>
        /// <param name="lengthInBytes">The length in bytes that this SECSItem will take up in
        /// its wire/transmission format.</param>
        /// <param name="desiredNumberOfLengthBytes">This parameter expresses the desired
        /// number or length bytes to be used for this SECSItem when it is converted
        /// to its wire/transmission format.</param>
        protected SECSItem(SECSItemFormatCode formatCode, int lengthInBytes, SECSItemNumLengthBytes desiredNumberOfLengthBytes)
        {
            if (lengthInBytes < 0 || lengthInBytes > (int)0x00FFFFFF)
            {
                throw new ArgumentException(
                        "The value for the length argument must be between 0 and 16777215 inclusive.");
            }

            _formatCode = formatCode;
            _lengthInBytes = lengthInBytes;
            NumberOfLengthBytes = desiredNumberOfLengthBytes;
        }
    
        /// <summary>
        /// FIXME
        ///  This is a base class constructor for a SECSItem.  This form of the constructor
        /// is used when parsing wire/transmission format data and converting it into
        /// its "C# form".
        /// <para/>
        /// This constructor sets the following base class attributes from the provided data:
        /// <c>formatCode</c>,
        /// <c>inboundNumberOfLengthBytes</c>,
        /// <c>outboundNumberOfLengthBytes</c>
        /// <c>lengthInBytes</c> or if the <c>formatCode</c> is of type <c>L</c>(List) <c>lengthInBytes</c>
        /// will be the number of items in the list.
        /// <para/>
        /// The exception <c>ArgumentException</c> will be thrown in the following circumstances:
        /// the <c>data</c> argument is <c>null</c>, the <c>data</c> argument has a length of zero, or
        /// the number of length bytes parsed out is zero.
        /// <para/>
        /// In normal use the only time the <c>ArgumentException</c> 
        /// exception should be caught is if you are reading data from a piece of
        /// equipment that does not properly speak SECS and you want to be able to
        /// recover from the error gracefully.Typically the ACM process will have
        /// detected this equipment and it will not be allowed into the factory in
        /// the first place.
        /// </summary>
        protected SECSItem(byte[] data, int itemOffset)
        {
            if (data == null)
            {
                throw new ArgumentNullException("\"data\" argument must not be null.");
            }

            if (data.Length < 2) {
                throw new ArgumentException ("\"data\" argument must have a length >= 2.");
            }

            /*
            if (data.Length == 0)
            {
                throw new ArgumentException ("The number of length bytes is not allowed to be ZERO.");
            }
            */
    
            _formatCode = SECSItemFormatCodeFunctions.GetSECSItemFormatCodeFromNumber((byte)((data[itemOffset] >> 2) & 0x0000003F));
    
            byte[] temp1 = new byte[4];
            switch (data[itemOffset] & 0x03)
            {
                case 0:
                {
                    throw new ArgumentException ("The number of length bytes is not allowed to be ZERO.");
                }
                case 1:
                {
                    _numberOfLengthBytes = SECSItemNumLengthBytes.ONE;
                    temp1[0] = 0;
                    temp1[1] = 0;
                    temp1[2] = 0;
                    temp1[3] = data[itemOffset +1];
                    
                    break;
                }
                case 2:
                {
                    _numberOfLengthBytes = SECSItemNumLengthBytes.TWO;
                    if (data.Length < 3) {
                        throw new ArgumentException ("With two length bytes the minimum length for the \"data\" argument is 3.");
                    }

                    temp1[0] = 0;
                    temp1[1] = 0;
                    temp1[2] = data[itemOffset+1];
                    temp1[3] = data[itemOffset+2];
                    break;
                }
                case 3:
                {
                    _numberOfLengthBytes = SECSItemNumLengthBytes.THREE;
                    if (data.Length < 4) {
                        throw new ArgumentException ("With three length bytes the minimum length for the \"data\" argument is 4.");
                    }

                    temp1[0] = 0;
                    temp1[1] = data[itemOffset+1];
                    temp1[2] = data[itemOffset+2];
                    temp1[3] = data[itemOffset+3];
                    break;
                }
            }
            
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp1);
        
            _lengthInBytes = BitConverter.ToInt32(temp1, 0);
        }
        
        /// <summary>
        /// Return the minimum number of length bytes required based on the
        /// specified length.  The result should be SECSItemNumLengthBytes.ONE,
        /// SECSItemNumLengthBytes.TWO, or SECSItemNumLengthBytes.THREE.
        /// The maximum length of a <c>SECSItem</c> in its &quot; wire/transmission format&quot;
        /// is 16777215 (stored in 24 bits).
        /// </summary>
        /// <returns>The minimum number of length bytes required.</returns>
        /// <param name="lengthOfData">The length of the data in bytes.</param>
        private SECSItemNumLengthBytes CalculateMinimumNumberOfLengthBytes(int lengthOfData)
        {
            SECSItemNumLengthBytes result = SECSItemNumLengthBytes.ONE;
            if (lengthOfData > 255)
            {
                if (lengthOfData < 65536)
                    result = SECSItemNumLengthBytes.TWO;
                else
                    result = SECSItemNumLengthBytes.THREE;
            }
            
            return result;
        }
    
        /// <summary>
        /// Returns the length in bytes of what the outbound item header will require.
        /// </summary>
        /// <returns>The header length in bytes.</returns>
        protected int OutputHeaderLength()
        {
            return NumberOfLengthBytes.ValueOf() + 1;
        }
        
        /// <summary>
        /// This method fills in the first 2 to 4 bytes of a <c>SECSItem</c>'s
        /// value (the <c>SECSItem</c>'s header) in its raw wire/transmission format.
        /// The first byte contains value of the item's format code and number of length 
        /// bytes and the next 1 to 3 bytes contain the value of the item's length in bytes.
        /// <para>
        /// Make sure <c>buffer</c> is large enough!!!
        /// </para>
        /// </summary>
        /// <returns>The offset to where the payload data may loaded into the buffer.</returns>
        /// <param name="buffer">The buffer that will be used to contain an outgoing message item.</param>
        /// <param name="numberOfBytes">The number of bytes in the payload...get this right.</param>
        protected int PopulateSECSItemHeaderData(byte[] buffer, int numberOfBytes)
        {
            int offset = 0;
            buffer[0] = (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(_formatCode) << 2) | NumberOfLengthBytes.ValueOf());
            
            byte[] outputLengthBytes = BitConverter.GetBytes(numberOfBytes);
            
            if (BitConverter.IsLittleEndian)
                Array.Reverse(outputLengthBytes);

            if (_numberOfLengthBytes == SECSItemNumLengthBytes.ONE)
            {
                buffer [1] = outputLengthBytes [3];
                offset = 2;
            }
            else if (_numberOfLengthBytes == SECSItemNumLengthBytes.TWO)
            {
                buffer [1] = outputLengthBytes [2];
                buffer [2] = outputLengthBytes [3];
                offset = 3;
            }
            else if (_numberOfLengthBytes == SECSItemNumLengthBytes.THREE)
            {
                buffer [1] = outputLengthBytes [1];
                buffer [2] = outputLengthBytes [2];
                buffer [3] = outputLengthBytes [3];
                offset = 4;
            }
            else
            {
                Console.WriteLine ("The case where outboundNumberOfLengthBytes is still in its NOT_INITIALIZED state should never happen.");
            }
            
            return offset;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object? obj)
        {
            if (this == obj)
				return true;

			if (obj == null)
				return false;

            if (obj.GetType().IsSubclassOf(typeof(SECSItem)) == false)
            {
                return false;
            }

            SECSItem other = (SECSItem)obj;
            if (_formatCode == other._formatCode &&
                _numberOfLengthBytes == other._numberOfLengthBytes &&
                _lengthInBytes == other._lengthInBytes)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int) 2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ _formatCode.GetHashCode();
                hash = (hash * 16777619) ^ _numberOfLengthBytes.GetHashCode();
                hash = (hash * 16777619) ^ _lengthInBytes.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Creates and returns a <code>byte[]</code> that represents this SECSItem 
        /// in its &quot;wire/transmission format&quot;.
        /// <para>
        /// Each object that extends this class will need to correctly implement this method.
        /// </para>
        /// </summary>
        /// <returns>A<c>byte []</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
        public abstract byte[] EncodeForTransport();
    }
}

