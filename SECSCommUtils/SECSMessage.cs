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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class is for representing a SECS message.
	/// </summary>
	public class SECSMessage
	{
		private	byte[]		_binaryBody = null;
		private SECSItem	_secsItemBody = null;

		public SECSHeader Header { get; set; }

		public SECSMessage(SECSHeader Header, SECSItem body)
		{
			this.Header = Header;
			this._secsItemBody = body;
			this._binaryBody = null;
		}

		public SECSMessage(SECSHeader Header, byte[] body)
		{
			this.Header = Header;
			this._binaryBody = body;
			this._secsItemBody = null;
		}

		public byte[] EncodeForTransport()
		{
			byte[] temp = null;

			if (_binaryBody == null && _secsItemBody == null)
			{
				// This is a header only message.
				temp = Header.EncodeForTransport();
			}
			else if (_binaryBody != null)
			{
				// The body of the message is already in its binary form
				temp = new byte[10 + _binaryBody.Length];
				Header.EncodeForTransport().CopyTo(temp, 0);
				_binaryBody.CopyTo(temp, 10);
			}
			else
			{
				// The body of the message needs to be converted to its binary form
				byte[] temp2 = _secsItemBody.EncodeForTransport();
				_binaryBody = temp2;
				_secsItemBody = null;
				temp = new byte[10 + _binaryBody.Length];
				Header.EncodeForTransport().CopyTo(temp, 0);
				_binaryBody.CopyTo(temp, 10);
			}

			return temp;
		}

        /// <summary>
        /// Returns the body / payload of this <c>SECSMessage</c> as a <c>SECSItem</c>.
        /// </summary>
        /// <returns>A <c>SECSItem</c> representation of this <c>SECSMessage</c>'s body / payload.</returns>
		public SECSItem GetBodyAsSECSItem()
		{
			if (_secsItemBody == null)
			{
				_secsItemBody = SECSItemFactory.GenerateSECSItem(_binaryBody);
				_binaryBody = null;
			}

			return _secsItemBody;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that contains this <c>SECSMessages</c>'s body / payload.
        /// <para>
        /// This <c>byte[]</c> is in the same format that would need to be used for transmission via <c>HSMS</c> or <c>SECS-I</c>.
        /// </para>
        /// </summary>
        /// <returns>A<c>byte []</c> representation of this <c>SECSMessage</c>'s body / payload that is suitable for transmission.</returns>
		public byte[] GetBodyAsByteArray()
		{
			if (_binaryBody == null)
			{
				_binaryBody = _secsItemBody.EncodeForTransport();
				_secsItemBody = null;
			}

			return _binaryBody;
		}
	} // End class SECSMessage

} // End namespace CIMthetics.SECSUtilities
