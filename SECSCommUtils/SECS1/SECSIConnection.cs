/*
 * Copyright 2019-2022 Douglas Kaip
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

using Serilog;

using System.Collections.Concurrent;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class implements a <c>SECSConnection</c> that uses SECS-I for
	/// its transport layer.
	/// </summary>
	public class SECSIConnection : SECSConnection
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
		public new UInt32 T1
		{
			get
			{
				return base.T1;
			}
			set
			{
				if (value < 100)
					base.T1 = 100;
				else if (value > 10000)
					base.T1 = 10000;
				else
				{
					// Strip off anything less that 0.1 seconds
					UInt32 temp = value/100;
					temp *= 100;
					base.T1 = temp;
				}
			}
		}

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
		/// <li>Resolution: 0.1 second (100 milliseconds)</li>
		/// </ul>
		/// </remarks>
		public new UInt32 T2
		{
			get
			{
				return base.T2;
			}
			set
			{
				if (value < 100)
					base.T2 = 100;
				else if (value > 25000)
					base.T2 = 25000;
				else
				{
					// Strip off anything less that 0.1 seconds
					UInt32 temp = value/100;
					temp *= 100;
					base.T2 = temp;
				}
			}
		}

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
		public new UInt32 T3
		{
			get
			{
				return base.T3;
			}
			set
			{
				if (value < 1)
					base.T3 = 1;
				else if (value > 120)
					base.T3 = 120;
				else
					base.T3 = value;
			}
		}

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
		public new UInt32 T4
		{
			get
			{
				return base.T4;
			}
			set
			{
				if (value < 1)
					base.T4 = 1;
				else if (value > 120)
					base.T4 = 120;
				else
					base.T4 = value;
			}
		}

		public uint RetryCount { get; set; }
		public bool Master { get; set; }
		public string TTYPort { get; set; }

		/// <summary>
		/// Create an connection object that will use SECS-I as its transport layer.
		/// </summary>
		/// <remarks>
		/// Use this form of the constructor in the event that the required configuration 
		/// information is contained in a <c>SECSConnectionConfigInfo</c> object.
		/// </remarks>
		public SECSIConnection(SECSConnectionConfigInfo configuration) : base(configuration.Name) { }

		public SECSIConnection(string ConnectionName, string TTYPort, bool Master) : base(ConnectionName)
		{
			T1 = ConnectionDefaults.T1;
			T2 = ConnectionDefaults.T2;
			T3 = ConnectionDefaults.T3;
			T4 = ConnectionDefaults.T4;
			RetryCount = ConnectionDefaults.RetryCount;

			this.TTYPort = TTYPort;
			this.Master = Master;

		}

		override public void Start()
		{
			Log.Verbose("connection \"{0}\" starting supervisor", ConnectionName);

			SupervisorThread = new Thread(Supervisor);
			SupervisorThread.Name = ConnectionName + ":Supervisor";
			SupervisorThread.Start();
		}

		override public void Stop()
		{
			Log.Error("Not implemented yet.");
		}

		override internal void Supervisor()
		{
			Log.Error("Not implemented yet.");
		}
		
		override internal void ConnectionReader()
		{
//			Log.Debug("thread {0} started", Thread.CurrentThread.Name);
			Log.Error("Not implemented yet.");
		}

		override internal void ConnectionWriter()
		{
//			Log.Debug("thread {0} started", Thread.CurrentThread.Name);
			Log.Error("Not implemented yet.");
		}
	} // End class SECSIConnection

} // End namespace CIMthetics.SECSUtilities
