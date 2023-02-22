/*
 * Copyright 2023 Douglas Kaip
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
 * See the License for the specific language governing private permissions and
 * limitations under the License.
 */

using Serilog;

using System.Collections.Concurrent;

using com.CIMthetics.CSharpSECSTools.TextFormatter;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class' purpose is simple.  It takes <c>SECSMessage</c>s received
	/// from the source connection and causes them to be sent via the destination
	/// connection.
	/// </summary>
	/// <remarks>
	/// This class is intended to run as an independent <c>Thread</c>.  Following
	/// is a code snippet showing its use.
	/// <code>
	/// 		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	/// 		CancellationToken 	    cancellationToken       = cancellationTokenSource.Token;
	///
	/// 		MessageProcessor connection1ToConnection2 = 
	///             new MessageProcessor("Connection1",
	///                                   "Connection2",
	///                                   ReceivedMessagesQueue,
	///                                   MessagesToSendQueue,
	///                                   cancellationToken);
	/// 
	/// 		Thread cp1Thread = new Thread(connection1ToConnection2.Run);
	/// 
	/// 		cp1Thread.Start();
	/// 
	///			// Terminate cp1Thread
	/// 		cancellationTokenSource.Cancel();
	/// 
	/// </code>
	/// </remarks>
	internal class MessageProcessor
	{
		private string							_sourceName;
		private string							_destinationName;
		private BlockingCollection<SECSMessage> _inboundQueue;
		private BlockingCollection<SECSMessage> _outboundQueue;

		private CancellationToken 				_cancellationToken;
		private SECSFormatter					_secsFormatter;

		/// <summary>
		/// The constructor for an instance of this class.
        /// <param name="sourceName">a name for the source connection for logging purposes</param>
        /// <param name="destinationName">a name for the destination connection for logging purposes</param>
        /// <param name="inboundMessageQueue">a queue associated with the source connection that will be waited upon for messages that should be forwarded to the <c>outboundMessageQueue</c>></param>
        /// <param name="outboundMessageQueue">a queue associated with the destination connection where the messages from the <c>inboundMessageQueue</c> will be placed</param>
        /// <param name="cancellationToken">a <c>CancellationToken</c> that may be used to shut</param>
		/// </summary>
		internal MessageProcessor(string sourceName,
									  string destinationName,
									  BlockingCollection<SECSMessage> inboundMessageQueue,
									  BlockingCollection<SECSMessage> outboundMessageQueue,
									  CancellationToken cancellationToken,
									  SECSFormatter secsFormatter)
		{
			_sourceName         = sourceName;
			_destinationName    = destinationName;
			_inboundQueue       = inboundMessageQueue;
			_outboundQueue 		= outboundMessageQueue;
			_cancellationToken  = cancellationToken;
			_secsFormatter		= secsFormatter;
		}

		internal void Run()
		{
			Log.Debug("Message processor {0}->{1} started", _sourceName, _destinationName);

			while(true)
			{
				try
				{
					SECSMessage secsMessage = _inboundQueue.Take(_cancellationToken);
					Log.Verbose("Message processor {0}->{1} received a message", _sourceName, _destinationName);
					_outboundQueue.Add(secsMessage);
					Log.Verbose("Message processor {0}->{1} forwarded a message", _sourceName, _destinationName);

					Log.Information(_secsFormatter.GetSECSMessageAsText(_sourceName, _destinationName, secsMessage));
					Log.Information("");
				}
				catch(OperationCanceledException)
				{
					Log.Verbose("Message processor {0}->{1} cancelled", _sourceName, _destinationName);
					break;
				}
			}

			Log.Debug("Message processor {0}->{1} terminating", _sourceName, _destinationName);
		}
    }
}