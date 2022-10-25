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
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class SECSIConnection : SECSConnection
	{
		public UInt32 T1 { get; set; } // in ms
		public UInt32 T2 { get; set; }
		public UInt32 T4 { get; set; }
		public uint RetryCount { get; set; }
		public bool Master { get; set; }
		public string TTYPort { get; set; }

		public SECSIConnection(string ConnectionName, BlockingCollection<SECSMessage> MessagesReceivedQueue, string TTYPort, bool Master)
			: base(ConnectionName, MessagesReceivedQueue)
		{
			T1 = ConnectionDefaults.T1;
			T2 = ConnectionDefaults.T2;
			T3 = ConnectionDefaults.T3;
			T4 = ConnectionDefaults.T4;
			RetryCount = ConnectionDefaults.RetryCount;

			this.TTYPort = TTYPort;
			this.Master = Master;

		}

		override public void SendMessage(SECSMessage message)
		{
			MessagesToSendQueue.Add(message);
		} // End override public void sendMessage(TransientMessage Message)

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
