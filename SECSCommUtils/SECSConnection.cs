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
