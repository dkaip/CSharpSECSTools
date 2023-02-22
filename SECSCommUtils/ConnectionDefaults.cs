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
	/// This class provides default values for various parameters related to 
	/// SECS-I and HSMS connections.
	/// </summary>
	public class ConnectionDefaults
	{
		/// <summary>
		/// Inter-Character Timeout
		/// </summary>
		/// <value>
		/// The Inter-Character timeout in milliseconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 0.5 seconds (500 milliseconds)</li>
		/// <li>Range: 0.1 - 10.0 seconds</li>
		/// <li>Resolution: 0.1 second (100 milliseconds)</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T1 = 500;

		/// <summary>
		/// Protocol Timeout
		/// </summary>
		/// <value>
		/// The Protocol timeout in milliseconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 10 seconds (10000 milliseconds)</li>
		/// <li>Range: 0.2 - 25.0 seconds</li>
		/// <li>Resolution: 0.2 second (200 milliseconds)</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T2 = 10000;

		/// <summary>
		/// Transaction Reply Timeout
		/// </summary>
		/// <value>
		/// The transaction reply timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 45 seconds</li>
		/// <li>Range: 1 - 120 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T3 = 45;

		/// <summary>
		/// Inter-Block Timeout
		/// </summary>
		/// <value>
		/// The Inter-Block timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 45 seconds</li>
		/// <li>Range: 1 - 120 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T4 = 45;

		/// <summary>
		/// Connection Separation Timeout
		/// </summary>
		/// <value>
		/// The Connection Separation timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 10 seconds</li>
		/// <li>Range: 1 - 240 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T5 = 10;

		/// <summary>
		/// Control Transaction Timeout
		/// </summary>
		/// <value>
		/// The Control Transaction timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 5 seconds</li>
		/// <li>Range: 1 - 240 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T6 = 5;

		/// <summary>
		/// NOT SELECTED Timeout
		/// </summary>
		/// <value>
		/// The NOT SELECTED timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 10 seconds</li>
		/// <li>Range: 1 - 240 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T7 = 10;

		/// <summary>
		/// Network Inter-Character Timeout
		/// </summary>
		/// <value>
		/// The Network Inter-Character timeout in seconds.
		/// </value>
		/// <remarks>
		/// <ul>
		/// <li>Default: 5 seconds</li>
		/// <li>Range: 1 - 120 seconds</li>
		/// <li>Resolution: 1 second</li>
		/// </ul>
		/// </remarks>
		public const UInt32 T8 = 5;

		public const UInt32 RetryCount = 3;

		public const UInt16 Port = 5000;

		public const UInt32 BaudRate = 19200;
		public const byte	Bits = 8;
		public const byte	StopBits = 1;
		public const string Parity = "none";
	}

}

