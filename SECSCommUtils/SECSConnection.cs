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
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SECSCommUtils;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// base class
	/// </summary>
	public abstract class SECSConnection
	{

		public UInt32 T3 { get; set; }
		public string ConnectionName { get; private set; }
        public BlockingCollection<SECSMessage> MessagesToSendQueue { get; private set; }
        public BlockingCollection<SECSMessage> MessagesReceivedQueue { get; private set; }

		internal Thread SupervisorThread { get; set; }
		internal Thread ReaderThread { get; set; }
		internal Thread WriterThread { get; set; }


		protected SECSConnection(string ConnectionName, BlockingCollection<SECSMessage> MessagesReceivedQueue)
		{
			this.ConnectionName = ConnectionName;
			this.MessagesReceivedQueue = MessagesReceivedQueue;
			MessagesToSendQueue = new BlockingCollection<SECSMessage>();
		}

		public Thread GetThread()
		{
			return SupervisorThread;
		}
		
		abstract public void Start();
		abstract public void Stop();

		abstract internal void Supervisor();
		abstract internal void ConnectionReader();
		abstract internal void ConnectionWriter();

		/// <summary>
		/// This method is used to place an outbound <c>SECSMessage</c> into
		/// the queue of messages that are to be sent to the other end of
		/// connection.
		/// <para/>
		/// Note: The message is sent as soon as possible.
		/// </summary>
		abstract public void SendMessage(SECSMessage Message);

	} // End public abstract class SECSConnection

} // End namespace CIMthetics.SECSUtilities
