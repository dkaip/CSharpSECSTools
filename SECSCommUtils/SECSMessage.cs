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

using com.CIMthetics.CSharpSECSTools.SECSItems;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class represents a SECS message that will be
	/// sent to some other entity or a SECS message that has been
	/// received from some other entity.
	/// </summary>
	public class SECSMessage
	{
		private	byte[]?		_body   = null;

        /// <summary>
        /// True if this <c>SECSMessage</c> is a header only message.  In other
		/// words, it is a message that only contains a message header portion
		/// and does not have a body to go with it.
        /// </summary>
		/// <returns>
		/// <c>true</c> if this <c>SECSMessage</c> contains a <c>Header</c> without a <c>Body</c> component.
		/// </returns>
		public bool IsHeaderOnly
		{
			get
			{
				if (_body == null || _body.Length == 0)
					return true;
				else
					return false;
			}
		}

        /// <summary>
        /// The body / payload / contents of this <c>SECSMessage</c>.
        /// </summary>
		/// <returns>
		/// The message body component of this <c>SECSMessage</c>.
		/// </returns>
		/// <remarks>
		/// Returns an empty <c>byte[]</c> in the event this is a header
		/// only (<c>IsHeaderOnly</c> is <c>true</c>) message.
		/// </remarks>
		public byte[] Body
		{
			get
			{
				if (_body == null)
				return new byte[0];
				else
				    return _body;
			}
		}

		/// <summary>
		/// The SECS message header (either an <c>HSMSHeader</c> or a <c>SECSIHeader</c>)
		/// of this <c>SECSMessage</c>
		/// </summary>
		/// <returns>
		/// The message header component of this <c>SECSMessage</c>.
		/// </returns>
		public SECSHeader Header { get; private set; }

		/// <summary>
		/// This constructor is typically used in the situation where the
		/// message is to be header only.
		/// </summary>
		/// <param name="header">
		/// The SECS message header (either a <c>HSMSHeader</c> or a <c>SECSIHeader</c>)
		/// to be used in this message.
		/// </param>
		public SECSMessage(SECSHeader header)
		{
			this.Header = header;
			this._body = new byte[0];
		}

		/// <summary>
		/// This constructor is typically used in the situation where the
		/// payload of a SECS message was created by
		/// a programmer as a <c>SECSItem</c> and is ready to be sent to some other entity.
		/// </summary>
		/// <param name="header">
		/// The SECS message header (either a <c>HSMSHeader</c> or a <c>SECSIHeader</c>)
		/// to be used in this message.
		/// </param>
		/// <param name="body">
		/// The body / content / payload of the SECS message to be sent.
		/// </param>
		public SECSMessage(SECSHeader header, SECSItem body)
		{
			this.Header = header;
			this._body = body.EncodeForTransport();
		}

		/// <summary>
		/// This constructor is typically used in the situation where a
		/// SECS message was received from either a SECS-I connection or
		/// an HSMS connection.  The body in this situation will be in
		/// the form of a <c>byte[]</c> of bytes received from some other
		/// entity.
		/// </summary>
		/// <param name="header">
		/// The SECS message header (either a <c>HSMSHeader</c> or a <c>SECSIHeader</c>)
		/// to be used in this message.
		/// </param>
		/// <param name="body">
		/// The body / content / payload of the SECS message to be sent.
		/// </param>
		public SECSMessage(SECSHeader header, byte[] body)
		{
			this.Header = header;
			this._body = body;
		}

		/// <summary>
		/// This method encodes this <c>SECSMessage</c> into a form where it may
		/// written out as a <c>byte[]</c> to some entity.
		/// </summary>
		/// <returns>
		/// A <c>byte[]</c> containing the information ready to be written out
		/// on a connection to some entity.
		/// </returns>
		public byte[] EncodeForTransport()
		{
			if (_body == null)
			{
				// This is a header only message.
				return Header.EncodeForTransport();
			}

			// The body of the message is already in its binary form
			byte[] temp = new byte[10 + _body.Length];
			Header.EncodeForTransport().CopyTo(temp, 0);
			_body.CopyTo(temp, 10);
			
			return temp;
		}

        /// <summary>
        /// Returns the body / payload / contents of this <c>SECSMessage</c> as a <c>SECSItem</c>.
        /// </summary>
        /// <returns>A <c>SECSItem</c> representation of this <c>SECSMessage</c>'s body / payload.</returns>
		/// <remarks>
		/// This method returns <c>null</c> in the case where this <c>SECSMessage</c> is a header
		/// only message.
		/// </remarks>
		public SECSItem? GetBodyAsSECSItem()
		{
			SECSItem? result = null;

			if (_body != null)
			{
				result = SECSItemFactory.GenerateSECSItem(_body);
			}

			return result;
		}
	} // End class SECSMessage

} // End namespace CIMthetics.SECSUtilities
