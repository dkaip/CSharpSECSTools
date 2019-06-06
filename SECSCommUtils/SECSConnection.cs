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
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	abstract public class SECSConnection
	{
		public UInt32 T3 { get; set; }
		public string ConnectionName { get; private set; }
		protected Queue<SECSMessage> ReceivedSECSMessages;
		protected EventWaitHandle ReceivedSECSMessagesWH;
		protected Queue<TransientMessage> MessagesToSend { get; private set; }
		protected EventWaitHandle MessageToSendWaitHandle { get; private set; }
		protected Queue<TransientMessage> MessagesReceived { get; private set; } // Used to communicate with Connection user
		protected EventWaitHandle MessageReceivedWaitHandle; // Used to communicate with Connection user

		public SECSConnection(string ConnectionName, ref Queue<TransientMessage> MessagesReceived, ref EventWaitHandle MessageReceivedWaitHandle)
		{
			this.ConnectionName = ConnectionName;
			this.MessagesReceived = MessagesReceived;
			MessagesToSend = new Queue<TransientMessage>();
			MessageToSendWaitHandle = new AutoResetEvent(false);
			this.MessageReceivedWaitHandle = MessageReceivedWaitHandle;

			ReceivedSECSMessages = new Queue<SECSMessage>();
			ReceivedSECSMessagesWH = new AutoResetEvent(false);
		}

		abstract public void start();

		abstract public void sendMessage(TransientMessage Message);

	} // End public abstract class SECSConnection

} // End namespace CIMthetics.SECSUtilities
