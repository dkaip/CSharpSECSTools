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

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
    /// <summary>
    /// This is an <c>abstract</c> base class for a SECS message header.
    /// </summary>
	abstract public class SECSHeader
	{
		/// <summary>
        /// System Bytes - The system bytes in the header of each message 
        /// for a given device ID must satisfy the following requirements. 
        /// <para/> 
        /// Distinction - The system bytes of a primary message must be 
        /// distinct from those of all currently open transactions 
        /// initiated from the same end of the communications link.
		/// <para/>
        /// They must also be distinct from those of the most recently 
        /// completed transaction. They must also be distinct from any 
        /// system bytes of blocks that were not successfully sent since 
        /// the last successful block send.
        /// <para/> 
        /// Reply Message - The system bytes of the reply message are 
        /// required to be the same as the system bytes of the corresponding
        /// primary message.
        /// <para/>
        /// Multi-Block Messages - When using SECS-I as a transport layer the
        /// system bytes of all blocks of a multi-block message must be the same.
        /// </summary>
		public UInt32 SystemBytes { get; set; } = 0;

        /// <summary>
        /// This method encodes the header (this object) into its &quot;transmission&quot; form.
        /// </summary>
        /// <returns>
        /// A <c>byte[]</c> that is this object's &quot;value&quot; in a transmittable form.
        /// </returns>
		abstract public byte[] EncodeForTransport();

	} // End abstract public class SECSHeader
}
