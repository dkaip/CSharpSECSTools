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

using System.Collections.Concurrent;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This is the <c>abstract</c> base class for classes supporting communication of SECS-II
	/// messages to an end point.
	/// </summary>
	/// <remarks>
	/// SECS messages (<c>SECSMessage</c>s) are sent using the <c>SendMessage</c> method.  When this method returns
	/// it does not imply that the message has already been sent.  It just means the message
	/// has been queued to be sent.  In normal situations the message will be sent almost
	/// immediately.
	/// <para>
	/// When messages are received by this connection they are placed at the end of the
	/// thread safe <c>MessagesReceivedQueue</c>.  The user is able to remove message
	/// from this queue and process them.
	/// </para>
	/// <para>
	/// A <c>SECSConnection</c> is used in the following manner.
	/// <ol>
	/// <li>Use <c>SECSConnectionFactory.CreateConnection</c> to create a <c>SECSConnection</c> object.</li>
	/// <li>
	/// Start the <c>SECSConnection</c> using the <c>Start</c> method.  This method basically
	/// &quot;wakes up&quot; the connection in that it will actively prepare itself for communicating
	/// with an end point.  Returning from the <c>Start</c> method does not mean that a connection
	/// has been successfully established with the end point.  It just means that this connection is
	/// ready and willing to communicate depending on the end point.
	/// </li>
	/// <li>
	/// Once a connection to the end point has been established.  (Which may not happen immediately after
	/// the completion of the <c>Start</c> method.)  The user may use the <c>SendMessage</c> method to
	/// send a message or they may add a message directly to the end of the <c>MessagesToSendQueue</c>.
	/// <para>
	/// Received messages will be on the <c>MessagesReceivedQueue</c>.  The user can either block on
	/// the queue awaiting the receipt or a message or poll the queue if blocking is not acceptable.
	/// In normal cases a separate thread will block on the queue awaiting messages to process.
	/// </para>
	/// </li>
	/// <li>
	/// When the connection is no longer needed use the <c>Stop</c> method to perform a more graceful shutdown.
	/// </li>
	/// </ol>
	/// </para>
	/// </remarks>
	public abstract class SECSConnection
	{
		internal UInt32 T1 { get; set; } = ConnectionDefaults.T1;
		internal UInt32 T2 { get; set; } = ConnectionDefaults.T2;
		internal UInt32 T3 { get; set; } = ConnectionDefaults.T3;
		internal UInt32 T4 { get; set; } = ConnectionDefaults.T4;
		internal UInt32 T5 { get; set; } = ConnectionDefaults.T5;
		internal UInt32 T6 { get; set; } = ConnectionDefaults.T6;
		internal UInt32 T7 { get; set; } = ConnectionDefaults.T7;
		internal UInt32 T8 { get; set; } = ConnectionDefaults.T8;
		public string ConnectionName { get; private set; }

		/// <summary>
		/// This is a thread safe queue where <c>SECSMessage</c>s that need to be sent should
		/// be placed.
		/// </summary>
	    public BlockingCollection<SECSMessage> MessagesToSendQueue { get; private set; } = new BlockingCollection<SECSMessage>();

		/// <summary>
		/// This is a thread safe queue where <c>SECSMessage</c>s that that have been received
		/// will be placed.
		/// </summary>
        public BlockingCollection<SECSMessage> MessagesReceivedQueue { get; private set; } = new BlockingCollection<SECSMessage>();

		/// <summary>
		/// This is the handle for the Supervisor Thread.
		/// </summary>
		internal Thread? SupervisorThread { get; set; }

		/// <summary>
		/// This is the handle for the connection Reader Thread.
		/// </summary>
		internal Thread? ReaderThread { get; set; }

		/// <summary>
		/// This is the handle for the connection writer Thread.
		/// </summary>
		internal Thread? WriterThread { get; set; }


		protected SECSConnection(string ConnectionName)
		{
			this.ConnectionName = ConnectionName;
		}

		// internal Thread GetThread()
		// {
		// 	return SupervisorThread;
		// }
		
		/// <summary>
		/// Calling this method effectively starts up this <c>SECSConnection</c>
		/// and gets it into a state whereby it will attempt to establish a
		/// connection to an end point using the configured information.
		/// </summary>
		/// <remarks>
		/// When this method returns a connection with the configured end point may or may
		/// not be established.  This connection has been transitioned into a &quot;mode&quot;
		/// in which it will actively attempt to establish a connection.
		/// </remarks>
		abstract public void Start();

		/// <summary>
		/// Calling this method results in the shutting down of this <c>SECSConnection</c>.
		/// The communication connection is closed and any worker <c>Threads</c> are
		/// terminated.
		/// </summary>
		abstract public void Stop();

		abstract internal void Supervisor();
		abstract internal void ConnectionReader();
		abstract internal void ConnectionWriter();

		/// <summary>
		/// This method is used to place an outbound <c>SECSMessage</c> into
		/// the queue of messages that are to be sent to the other end of a
		/// connection.
		/// </summary>
		/// <remarks>
		/// The message is sent as soon as possible.  It may or may not be sent
		/// before this method returns.
		/// </remarks>
		public void SendMessage(SECSMessage message)
		{
			MessagesToSendQueue.Add(message);
		}


	} // End public abstract class SECSConnection

} // End namespace CIMthetics.SECSUtilities
