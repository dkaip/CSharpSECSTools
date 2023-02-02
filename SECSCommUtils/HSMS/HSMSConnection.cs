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
 * See the License for the specific language governing private permissions and
 * limitations under the License.
 */
using Serilog;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// derived class
	/// </summary>
	public class HSMSConnection : SECSConnection
	{
		volatile bool _exitNow;
		volatile bool _establishConnection;
		private CancellationTokenSource _cancellationTokenSource;
		private CancellationToken _cancellationToken;
		public TCPState TCPState { get; set; }
        private NetworkStream _ioStream;


		public UInt32 T5 { get; set; }
		public UInt32 T6 { get; set; }
		public UInt32 T7 { get; set; }
		public UInt32 T8 { get; set; }
		public HSMSConnectionMode ConnectionMode { get; private set; }

		public string IPAddress { get; set; }

		public UInt16 PortNumber { get; set; }

		public HSMSConnectionState ConnectionState { get; private set; }

		private IPEndPoint _ipEndPoint = null;
		private TcpClient 	_tcpClient = null;

		private EventWaitHandle _autoResetEvent = new EventWaitHandle(false, EventResetMode.AutoReset);

		private Thread _connectionReaderThread = null;
		private Thread _connectionWriterThread = null;

		private HSMSConnectionSM hsmsConnectionSM = null;

		public HSMSConnection(string connectionName, BlockingCollection<SECSMessage> messagesReceivedQueue, IPAddress ipAddress, UInt16 ipPortNumber, HSMSConnectionMode connectionMode) : base(connectionName, messagesReceivedQueue)
		{
			_exitNow = false;
			_establishConnection = true;

			T3 = ConnectionDefaults.T3;
			T5 = ConnectionDefaults.T5;
			T6 = ConnectionDefaults.T6;
			T7 = ConnectionDefaults.T7;
			T8 = ConnectionDefaults.T8;

			this.IPAddress = IPAddress;
			this.ConnectionMode = connectionMode;

			_ipEndPoint = new IPEndPoint(ipAddress, ipPortNumber);

			Log.Debug("Connection {0} IP IPEndPoint {1}", connectionName, _ipEndPoint.ToString());

			// Create the state machine...its initial state will be NoState
			hsmsConnectionSM = new HSMSConnectionSM();
			// Transition to state NotConnected
			hsmsConnectionSM.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
		}

		override public void SendMessage(SECSMessage message)
		{
			MessagesToSendQueue.Add(message);
		}

		override public void Start()
		{
			Log.Verbose("connection \"{0}\" starting supervisor", ConnectionName);

			SupervisorThread = new Thread(Supervisor);
			SupervisorThread.Name = ConnectionName + ":Supervisor";
			SupervisorThread.Start();

		} // End public void start()

		override public void Stop()
		{
			Log.Debug("{0} shutdown initiated", ConnectionName);
			_exitNow = true;
			_establishConnection = false;
			Log.Debug("{0} _exitNow set to true, _establishConnection set to false, issuing cancel", ConnectionName);
			_cancellationTokenSource.Cancel();
			Log.Debug("{0} closing tcpClient", ConnectionName);
			if (_tcpClient != null)
			{
				_tcpClient.Close();
			}
			Log.Debug("{0} tcpClient closed issuing set on _autoResetEvent", ConnectionName);
			_autoResetEvent.Set();

			// Do not return until the Supervisor has shutdown.
			Log.Verbose("{0} waiting for supervisor to terminate", ConnectionName);
			SupervisorThread.Join();
			Log.Verbose("{0} supervisor has terminated", ConnectionName);
		}

		override internal void Supervisor()
		{

			TcpListener tcpListener = null;

			Log.Debug("{0} Supervisor thread  has started", ConnectionName);
			Log.Verbose("{0}'s connection mode is {1}", ConnectionName, ConnectionMode.ToString());

			while(_exitNow == false)
			{
				_cancellationTokenSource = new CancellationTokenSource();
				_cancellationToken = _cancellationTokenSource.Token;
				while(_establishConnection == true)
				{
					if (ConnectionMode == HSMSConnectionMode.Passive)
					{
						/*
						    We are basically in server mode so setup to
							receive a connection from a client.
						*/
						tcpListener = new(_ipEndPoint);
						tcpListener.Start();
						_tcpClient = tcpListener.AcceptTcpClient();
						_ioStream = _tcpClient.GetStream();
						Log.Debug("{0} connection established with client {1}", ConnectionName + ":Supervisor", _ipEndPoint.ToString());
					}
					else
					{
						/*
						    We are in a client mode so just connect to the
							"server" and get ready for work.
						*/
						_tcpClient = new TcpClient();

						int retryCount = 10;
						int retryInterval = 10000;
						bool successfullyConnected = false;
						while(retryCount > 0 && successfullyConnected == false)
						{
							try
							{
								Log.Verbose("{0} attempting to connect with \"server\" {1}", ConnectionName + ":Supervisor", _ipEndPoint.ToString());
								_tcpClient.Connect(_ipEndPoint);
								Log.Verbose("{0} connected with \"server\" {1}", ConnectionName + ":Supervisor", _ipEndPoint.ToString());
							}
							catch(Exception e)
							{
								if ((e.Message.IndexOf("Connection refused", StringComparison.OrdinalIgnoreCase) >= 0) &&
								    (e.GetType().ToString().IndexOf("System.Net.Internals.SocketExceptionFactory+ExtendedSocketException", StringComparison.OrdinalIgnoreCase) >= 0))
								{
									retryCount--;
									Log.Debug("{0} retries remaining {1}", retryCount, _exitNow);
									Thread.Sleep(retryInterval);
									continue;
								}

								Log.Error("BAAAAAck");
								Log.Warning("{0}  {1}", e.Message, e.GetType().ToString());
								throw;
							}

							successfullyConnected = true;
						}

						if (successfullyConnected == false)
						{
							Log.Fatal("{0} gave up trying to connected", ConnectionName + ":Supervisor");
							Environment.Exit(-1);
						}

						_ioStream = _tcpClient.GetStream();

					}

					_connectionReaderThread = new Thread(ConnectionReader);
					_connectionReaderThread.Name = ConnectionName + ":Reader";
					_connectionReaderThread.Start();
					Log.Debug("{0} supervisor started reader thread", ConnectionName);
					
					_connectionWriterThread = new Thread(ConnectionWriter);
					_connectionWriterThread.Name = ConnectionName + ":Writer";
					_connectionWriterThread.Start();
					Log.Debug("{0} supervisor started writer thread", ConnectionName);

					// wait for a notification of from the Stop method
					Log.Debug("{0} supervisor awaiting _autoResetEvent", ConnectionName);
					_autoResetEvent.WaitOne();
					Log.Debug("{0} supervisor _autoResetEvent has been received _establishConnection is {1}, _exitNow is {2}", ConnectionName, _establishConnection, _exitNow);

				}
				
				_ioStream.Dispose();
				if (_tcpClient != null)
				{
					_tcpClient.Dispose();
				}
				if (tcpListener != null)
				{
					tcpListener.Stop();
				}

				_ioStream = null;
				_tcpClient = null;
				tcpListener = null;

			}

			/*
				_connectionWriterThread and / or _connectionWriterThread
				may be null in the case where a connection was not 
				established before the Stop method was called.
			*/
			if (_connectionWriterThread != null)
			{
				_connectionWriterThread.Join();
			}

			if (_connectionReaderThread != null)
			{
				_connectionReaderThread.Join();
			}
			
			Log.Debug("{0} Supervisor thread terminating", ConnectionName);
		}
		

		override internal void ConnectionReader()
		{
			Log.Debug("{0} started", Thread.CurrentThread.Name);

			/*
				These two are always going to be required and their lengths
				will not change so we will declare them here so the GC
				does not have to worry about them until the end.
			*/
			byte[] messageLengthBytes = new byte[4];
			byte[] messageHeaderBytes = new byte[10];

			while(_exitNow == false)
			{
				try
				{
					/*
						Read the length of the incomming message it is the
						first 4 bytes of the message.  It is Big Endian though
						so it needs to be converted into the "current" endianess.
					*/
					int currentPosition = 0;
					while(currentPosition < messageLengthBytes.Length)
					{
						int numberOfBytesRead = _ioStream.Read(messageLengthBytes, currentPosition, messageLengthBytes.Length - currentPosition);
						currentPosition += numberOfBytesRead;
					}

					if (BitConverter.IsLittleEndian == true)
					{
						/*
							The data elements in a SECS message are in
							Big Endian format...If this platform is
							Little Endian we need to convert in order
							to extract the correct value.
						*/
						Array.Reverse(messageLengthBytes);
					}
					
					UInt32 messageLength = BitConverter.ToUInt32(messageLengthBytes, 0);
					Log.Verbose("{0} incoming message length is {1} bytes, attempting to read them", Thread.CurrentThread.Name, messageLength);

					currentPosition = 0;
					while(currentPosition < messageHeaderBytes.Length)
					{
						int numberOfBytesRead = _ioStream.Read(messageHeaderBytes, currentPosition, messageHeaderBytes.Length - currentPosition);
						currentPosition += numberOfBytesRead;
					}

					HSMSHeader hsmsHeader = new HSMSHeader(messageHeaderBytes);

					byte[] messageBody = null;
					if (messageLength > 10)
					{
						/*
							The message length is greater than 10...this means
							that the message is not "header only" so we need
							to read the rest of the message.
						*/
						messageBody = new byte[messageLength - 10];
						Log.Verbose("{0} message length and header retrieved, {1} byte(s) remaining to be read", Thread.CurrentThread.Name, messageBody.Length);
						currentPosition = 0;
						while(currentPosition < messageBody.Length)
						{
							int numberOfBytesRead = _ioStream.Read(messageBody, currentPosition, messageBody.Length - currentPosition);
							currentPosition += numberOfBytesRead;
						}

						Log.Verbose("{0} {1} bytes successfully read (header + body)", Thread.CurrentThread.Name, messageLength);
					}
					else
					{
						Log.Verbose("{0} {1} bytes successfully read (header only)", Thread.CurrentThread.Name, messageLength);
					}

					// Construct the SECS message from the bytes received
					SECSMessage secsMessage = new SECSMessage(hsmsHeader, messageBody);

					/*
						Put the inbound message on a queue for another
						asynchronous thread.
					*/
					Log.Verbose("{0} adding received message to MessagesReceivedQueue", Thread.CurrentThread.Name);
					MessagesReceivedQueue.Add(secsMessage);

					// Reset and go for another inbound message
					Array.Clear(messageLengthBytes);
					Array.Clear(messageHeaderBytes);
				}
				catch(IOException)
				{
					Log.Debug("IO e exit now is {0}", _exitNow);
				}
				catch(ObjectDisposedException)
				{
					Log.Debug("disposed e exit now is {0}", _exitNow);
				}
				catch(Exception e)
				{
					Log.Error("{0} exception {1} {2}", Thread.CurrentThread.Name, e.Message, e.GetType().ToString());
					throw;
				}
			}

			Log.Debug("{0} has terminated", Thread.CurrentThread.Name);
			_autoResetEvent.Set();
		}

		override internal void ConnectionWriter()
		{
			Log.Debug("{0} started", Thread.CurrentThread.Name);

			while(_exitNow == false)
			{
				try
				{
					SECSMessage secsMessage = MessagesToSendQueue.Take(_cancellationToken);

					byte[] binaryMessage = secsMessage.EncodeForTransport();

					UInt32 messageLength = (UInt32)binaryMessage.Length;
					byte[] messageLengthBytes = BitConverter.GetBytes(messageLength);

					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(messageLengthBytes);
					}
				
					if (messageLength == 10)
						Log.Debug("{0} writing out header only message + 4 message length bytes", Thread.CurrentThread.Name, messageLength);
					else
						Log.Debug("{0} writing out header and body message + 4 message length bytes", Thread.CurrentThread.Name, messageLength);

					_ioStream.Write(messageLengthBytes);
					_ioStream.Write(binaryMessage);
				}
				catch(OperationCanceledException)
				{
					Log.Debug("{0} ConnectionWriter cancelled, _exitnow is {1}", Thread.CurrentThread.Name, _exitNow);
					_exitNow = true; // just to be sure this sucker is going to die
				}
			}

			Log.Debug("{0} ConnectionWriter terminating", Thread.CurrentThread.Name);
		}
	} // End class HSMSConnection

} // End namespace CIMthetics.SECSUtilities
