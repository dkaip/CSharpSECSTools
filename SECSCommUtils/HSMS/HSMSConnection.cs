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

using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

using Serilog;

using com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class implements a <c>SECSConnection</c> that uses HSMS for
	/// its transport layer.
	/// </summary>
	/// <inheritdoc/>
	public class HSMSConnection : SECSConnection
	{
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
		public new UInt32 T5
		{
			get
			{
				return base.T5;
			}
			set
			{
				if (value < 1)
					base.T5 = 1;
				else if (value > 240)
					base.T5 = 240;
				else
					base.T5 = value;
			}
		}
	
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
		public new UInt32 T6
		{
			get
			{
				return base.T6;
			}
			set
			{
				if (value < 1)
					base.T6 = 1;
				else if (value > 240)
					base.T6 = 240;
				else
					base.T6 = value;
			}
		}

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
		public new UInt32 T7
		{
			get
			{
				return base.T7;
			}
			set
			{
				if (value < 1)
					base.T7 = 1;
				else if (value > 240)
					base.T7 = 240;
				else
					base.T7 = value;
			}
		}

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
		public new UInt32 T8
		{
			get
			{
				return base.T8;
			}
			set
			{
				if (value < 1)
					base.T8 = 1;
				else if (value > 120)
					base.T8 = 120;
				else
					base.T8 = value;
			}
		}

		private Object _exitNowLock = new Object();
		private volatile bool _exitNow = false;
		/// <summary>
		/// If <c>true</c> the various threads will start their shutdown process.
		/// </summary>
		private bool ExitNow
		{
			get
			{
				bool result;
				lock(_exitNowLock)
				{
					result = _exitNow;
				}

				return result;
			}
			set
			{
				lock(_exitNowLock)
				{
					_exitNow = value;
				}
			}
		}

		/// <summary>
		/// This is used as a retry timer.
		/// </summary>
		private readonly BlockingCollection<int> _activeConnectionRetryTimer = new BlockingCollection<int>();

		private CancellationTokenSource _cancellationTokenSource;

		/// <summary>
		/// This cancellation token use used to interrupt the connection writer.
		/// </summary>
		private CancellationToken _cancellationToken;

		/// <summary>
		/// This is the <c>HSMSConnectionMode</c> that this <c>SECSConnection</c>
		/// has been configured to operate in.
		/// </summary>
		public HSMSConnectionMode ConnectionMode { get; private set; }

		/// <summary>
		/// The network address family of this HSMS connection.
		/// </summary>
		/// <value>
		/// The network address family of this connection.
		/// </value>
		/// <remarks>
		/// The value should be either &quot;IPV4&quot; or &quot;IPV6&quot;
		/// <para>
		/// It is possible that its value may be &quot;Not Set&quot;.  This will
		/// occur in the event the user uses the constructor where they supply
		/// their own <c>IPEndPoint</c>.
		/// </para>
		/// </remarks>
		public string AddressFamily { get; } = "Not Set";

//		public HSMSConnectionState ConnectionState { get; private set; }

		/// <summary>
		/// This is the <c>IPEndPoint</c> that this <c>HSMSConnection</c> is 
		/// connected to or is attempting to connect to.  On its own, this
		/// property is not useful for much use aside from providing debugging
		/// information.
		/// </summary>
		public IPEndPoint IPEndPoint { get; private set; }

		private const int StopEvent = 0;
		private const int  ReaderTerminatedEvent = 1;
		private const int WriterTerminatedEvent = 2;
		private EventWaitHandle[] waitHandles = new EventWaitHandle[] { new AutoResetEvent(false), new AutoResetEvent(false), new AutoResetEvent(false)};

		/// <summary>
		/// This is the <c>Thread</c> handle for the connection reader thread.
		/// </summary>
		private Thread? _connectionReaderThread = null;

		/// <summary>
		/// This is the <c>Thread</c> handle for the connection write thread.
		/// </summary>
		private Thread? _connectionWriterThread = null;

		/// <summary>
		/// This attribute is set and reset in the Supervisor
		/// and used in the ConnectionReader and ConnectionWriter.
		/// </summary>
        private NetworkStream? _ioStream;


		private Object _tcpClientLock = new Object();
		private TcpClient?		_tcpClient = null;
		/// <summary>
		/// This is set and used in the Supervisor and the <c>Stop</c>
		/// method in the event of a shutdown.
		/// </summary>
		private TcpClient? TcpClient
		{
			get
			{
				TcpClient? result;

				lock(_tcpClientLock)
				{
					result = _tcpClient;
				}

				return result;
			}
			set
			{
				lock(_tcpClientLock)
				{
					_tcpClient = value;
				}
			}
		}

		private Object _tcpListenerLock = new Object();
		private volatile TcpListener? 	_tcpListener = null;
		/// <summary>
		/// This property is not used unless this connection has a
		/// connection mode of &quot;Passive&quot;
		/// This is assigned started, listened on, stopped, and set to
		/// <c>null</c> in <c>ConnectAsPassive</c>.  It needs to be a class
		/// level attribute because in the <c>Stop</c> method if it
		/// is not <c>null</c> it means that the Supervisor thread is
		/// effectively waiting for <c>ConnectAsPassive</c> to return.  It 
		/// has not returned because it is waiting on <c>AcceptTcpClient()</c>
		/// to complete.  The only way to break it out of that is to have 
		/// the <c>Stop</c> method issue a <c>Stop</c> on the <c>TCPListener</c>.
		/// This will only occur if a shutdown is requested while attempting
		/// to connect as a <c>Passive</c> connection.
		/// </summary>
		private TcpListener? TcpListener
		{
			get
			{
				TcpListener? result;

				lock(_tcpListenerLock)
				{
					result = _tcpListener;
				}

				return result;
			}
			set
			{
				lock(_tcpListenerLock)
				{
					_tcpListener = value;
				}
			}
		}
		
		private enum SupervisorModeEnum
		{
			TryingToConnect,
			Connected
		}

		private Object _SupervisorModeLock = new Object();
		private SupervisorModeEnum _superVisorMode = SupervisorModeEnum.TryingToConnect;
		private SupervisorModeEnum SupervisorMode
		{
			get
			{
				SupervisorModeEnum result;

				lock(_SupervisorModeLock)
				{
					result = _superVisorMode;
				}

				return result;
			}
			set
			{
				lock(_SupervisorModeLock)
				{
					_superVisorMode = value;
				}
			}
		}


		/// <summary>
		/// This is the connection state state machine associated with this port.
		/// </summary>
		public HSMSConnectionSM HSMSConnectionSM { get; private set; }

		/// <summary>
		/// Create an HSMS connection object.
		/// </summary>
		/// <remarks>
		/// Use this form of the constructor in the event that the required configuration 
		/// information is contained in a <c>SECSConnectionConfigInfo</c> object.
		/// </remarks>
		public HSMSConnection(SECSConnectionConfigInfo configuration) : base(configuration.Name)
		{
			ExitNow = false;
			// EstablishConnection = true;

			T3 = configuration.T3;
			T5 = configuration.T5;
			T6 = configuration.T6;
			T7 = configuration.T7;
			T8 = configuration.T8;

			AddressFamily = configuration.AddressFamily;

			ConnectionMode = configuration.HSMSConnectionMode;

			IPAddress? ipAddress = null;
			IPAddress[] ipAddresses = Dns.GetHostAddresses(configuration.Address);

			if (string.Equals(configuration.AddressFamily, "IPV4"))
			{
				// It is IPV4
				AddressFamily = configuration.AddressFamily;

				foreach(IPAddress address in ipAddresses)
				{
					if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
					{
						ipAddress = address;
						// We are just grabbing the first one.
						break;
					}
				}
			}
			else
			{
				// If it is not IPV4 it is IPV6.
				AddressFamily = configuration.AddressFamily;

				foreach(IPAddress address in ipAddresses)
				{
					if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
					{
						ipAddress = address;
						// We are just grabbing the first one.
						break;
					}
				}
			}

			if (ipAddress == null)
			{
				Log.Fatal("Error trying to get IPAddress in family {0}", configuration.AddressFamily);
				Environment.Exit(-1);
			}

			IPEndPoint = new IPEndPoint(ipAddress, configuration.Port);

			Log.Debug("Connection {0} IP IPEndPoint {1}", ConnectionName, IPEndPoint.ToString());

			// Create the state machine...its initial state will be NoState
			HSMSConnectionSM = new HSMSConnectionSM();
			// Transition to state NotConnected
			HSMSConnectionSM.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);

			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationToken = _cancellationTokenSource.Token;
		}

		public HSMSConnection(string connectionName, IPEndPoint ipEndPoint, HSMSConnectionMode connectionMode) : base(connectionName)
		{
			ExitNow = false;
			// EstablishConnection = true;

			this.ConnectionMode = connectionMode;

			IPEndPoint = ipEndPoint;

			Log.Debug("Connection {0} IP IPEndPoint {1}", connectionName, IPEndPoint.ToString());

			// Create the state machine...its initial state will be NoState
			HSMSConnectionSM = new HSMSConnectionSM();
			// Transition to state NotConnected
			HSMSConnectionSM.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);

			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationToken = _cancellationTokenSource.Token;
		}

		/// <inheritdoc/>
		override public void Start()
		{
			Log.Verbose("{0} Start:Starting Supervisor Thread.", ConnectionName);
			SupervisorThread = new Thread(Supervisor);
			SupervisorThread.Name = "HSMSConnection:" + ConnectionName + ":Supervisor";
			SupervisorThread.Start();
			Log.Debug("{0} Start:Supervisor Thread started.", ConnectionName);
		} // End public void start()

		/// <inheritdoc/>
		override public void Stop()
		{
			Log.Debug("{0} Stop:Shutdown initiated", ConnectionName);

			if (SupervisorThread == null)
			{
				Log.Warning("{0} Stop:Supervisor was never started, nothing to stop.", ConnectionName);
				return;
			}

			// In order to shutdown properly this line needs to be before the rest.
			ExitNow = true;

			if (SupervisorMode == SupervisorModeEnum.TryingToConnect)
			{
				/*
					The Supervisor is in the Trying To Connect State
					so we have to take different steps to shut things down.
				*/
				if (ConnectionMode == HSMSConnectionMode.Active)
				{
					/*
						The connection mode is active so we have to "break out of"
						the ConnectAsActive logic.
					*/
					Log.Verbose("{0} Stop:Interrupting ConnectAsActive.", ConnectionName);
					_activeConnectionRetryTimer.Add(10);
					Log.Debug("{0} Stop:ConnectAsActive Interrupted.", ConnectionName);
				}
				else
				{
					/*
						The connection mode is Passive so we have to "break out of"
						the ConnectAsPassive logic.
					*/
					if (TcpListener != null)
					{
						Log.Verbose("{0} Stop:Interrupting ConnectAsPassive.", ConnectionName);
						TcpListener.Stop();
						TcpListener = null;
						Log.Debug("{0} Stop:ConnectAsPassive Interrupted.", ConnectionName);
					}
				}
			}
			

			Log.Verbose("{0} Stop:Signaling Supervisor for shutdown.", ConnectionName);
			waitHandles[StopEvent].Set();
			Log.Debug("{0} Stop:Signaled Supervisor for shutdown.", ConnectionName);

			// Do not return until the Supervisor has shutdown.
			Log.Verbose("{0} Stop:Waiting for supervisor to terminate", ConnectionName);
			SupervisorThread.Join();
			Log.Debug("{0} Stop:supervisor has terminated", ConnectionName);
		}

		/// <summary>
		/// This thread is responsible for awaiting a network connection request (Passive mode)
		/// or initiating a network connection (Active mode).  Once a connection is established
		/// this thread will start a connection reader thread and a connection writer
		/// thread,  These two additional threads will handle the business of receiving data
		/// from and sending data to the other end of the connection.  
		/// In addition, if the connection
		/// is broken this thread will also attempt to reestablish the connection (actually
		/// create a new one), unless it has been instructed to shut down.
		/// </summary>
		/// <remarks>
		/// This method can be thought of as having two states of operation.  The first
		/// state is trying to get a connection established.  The second &quot;State&quot;
		/// is basically a running state.
		/// <para>
		/// Until a connection is
		/// established only the <c>Supervisor</c> thread is running.  In this &quot;State&quot;
		/// a different method is needed in order to shut it down as opposed to while it is running.
		/// </para>
		///</remarks>
		override internal void Supervisor()
		{
			TcpClient = null;

			Log.Debug("{0} Started. Connection Mode is {1}.", Thread.CurrentThread.Name, ConnectionMode.ToString());
			do
			{
				SupervisorMode = SupervisorModeEnum.TryingToConnect;

				if (ConnectionMode == HSMSConnectionMode.Passive)
				{
					// The connection mode is Passive so we need
					// await a connection and get the TcpClient.
					Log.Verbose("{0} Attempting to connect as Passive.", Thread.CurrentThread.Name);
					TcpClient = ConnectAsPassive();
				}
				else
				{
					// The connection mode is Active so we need
					// attempt to connect and get the TcpClient.
					Log.Verbose("{0} Attempting to connect as Active.", Thread.CurrentThread.Name);
					TcpClient = ConnectAsActive();
				}

				if (TcpClient == null)
				{
					/*
						If TcpClient is null, but, _establishConnection != false
						and ExitNow != true we have a major issue.
					*/
					Log.Debug("{0} TcpClient is null and ExitNow is {1}...probably shutting down.", Thread.CurrentThread.Name, ExitNow);
					continue;
				}
				else
				{
					Log.Debug("{0} TCPClient created.", Thread.CurrentThread.Name);
				}

				SupervisorMode = SupervisorModeEnum.Connected;

				// Get the NetworkStream in preparation for IO
				_ioStream = TcpClient.GetStream();

				/*
					Start the read and writer threads.
				*/
				Log.Verbose("{0} Starting reader thread", Thread.CurrentThread.Name);
				_connectionReaderThread = new Thread(ConnectionReader);
				_connectionReaderThread.Name = "HSMSConnection:" + ConnectionName + ":ConnectionReader";
				_connectionReaderThread.Start();
				Log.Debug("{0} Started reader thread", Thread.CurrentThread.Name);
				
				Log.Verbose("{0} Starting writer thread", Thread.CurrentThread.Name);
				_connectionWriterThread = new Thread(ConnectionWriter);
				_connectionWriterThread.Name = "HSMSConnection:" + ConnectionName + ":ConnectionWriter";
				_connectionWriterThread.Start();
				Log.Debug("{0} Started writer thread", Thread.CurrentThread.Name);

				/*
					Wait for a notification of from the Stop method or
					the reader and or write thread to signal that it 
					terminated.
				*/
				Log.Debug("{0} Awaiting Stop, Reader Terminated, or Writer Terminated Event", Thread.CurrentThread.Name);
				int index = WaitHandle.WaitAny(waitHandles);

				Log.Debug("{0} Received {1} Event.", Thread.CurrentThread.Name, index == StopEvent ? "Stop" : index == ReaderTerminatedEvent ? "Reader Terminated" : "Writer Terminated");

				if (index == StopEvent)
				{
					// This is to cancel the writer
					Log.Verbose("{0} Stop Event:Cancelling the ConnectionWriter.", Thread.CurrentThread.Name);
					_cancellationTokenSource.Cancel();
					Log.Debug("{0} Stop Event:ConnectionWriter cancelled.", Thread.CurrentThread.Name);

					Log.Verbose("{0} Stop Event:Disposing _ioStream", Thread.CurrentThread.Name);
					_ioStream.Dispose();
					_ioStream = null;
					if (TcpClient != null)
					{
						Log.Verbose("{0} Stop Event:Closing TcpClient.", Thread.CurrentThread.Name);
						TcpClient.Close();
						TcpClient = null;
						Log.Debug("{0} Stop Event:TcpClient Closed.", Thread.CurrentThread.Name);
					}
				}
				else
				{
					// It was not a stop event so it must have been the reader or writer terminating.
					 if (index == ReaderTerminatedEvent)
					 {
						// The reader terminated so stop the writer.
						
						Log.Verbose("{0} ReaderTerminated Event:Cancelling the ConnectionWriter.", Thread.CurrentThread.Name);
						_cancellationTokenSource.Cancel();
						Log.Debug("{0} ReaderTerminated Event:ConnectionWriter cancelled.", Thread.CurrentThread.Name);

						Log.Verbose("{0} ReaderTerminated Event:Awaiting connection writer shutdown.", Thread.CurrentThread.Name);
						_connectionWriterThread.Join();
						Log.Debug("{0} ReaderTerminated Event:Connection writer terminated.", Thread.CurrentThread.Name);

						Log.Verbose("{0} ReaderTerminated Event:Disposing _ioStream", Thread.CurrentThread.Name);
						_ioStream.Dispose();
						_ioStream = null;
						if (TcpClient != null)
						{
							Log.Verbose("{0} WriterTerminated Event:Closing TcpClient.", Thread.CurrentThread.Name);
							TcpClient.Close();
							TcpClient = null;
							Log.Debug("{0} WriterTerminated Event:TcpClient Closed.", Thread.CurrentThread.Name);
						}
					 }
					 else
					 {
						// The write terminated so stop the reader.

						Log.Verbose("{0} WriterTerminated Event:Disposing _ioStream", Thread.CurrentThread.Name);
						_ioStream.Dispose();
						_ioStream = null;
						if (TcpClient != null)
						{
							Log.Verbose("{0} WriterTerminated Event:Closing TcpClient.", Thread.CurrentThread.Name);
							TcpClient.Close();
							TcpClient = null;
							Log.Debug("{0} WriterTerminated Event:TcpClient Closed.", Thread.CurrentThread.Name);
						}

						Log.Verbose("{0} WriterTerminated Event:Awaiting connection reader shutdown.", Thread.CurrentThread.Name);
						_connectionReaderThread.Join();
						Log.Debug("{0} WriterTerminated Event:Connection reader terminated.", Thread.CurrentThread.Name);
					 }

					// Reset the wait handles...Stop probably  does not need it.
					waitHandles[StopEvent].Reset();
					waitHandles[WriterTerminatedEvent].Reset();
					waitHandles[WriterTerminatedEvent].Reset();

					// Empty the message queues
					while(MessagesToSendQueue.TryTake(out _)){}
					while(MessagesReceivedQueue.TryTake(out _)){}

					_connectionReaderThread = null;
					_connectionWriterThread = null;

					// Reset cancellation token
					_cancellationTokenSource = new CancellationTokenSource();
					_cancellationToken = _cancellationTokenSource.Token;


				}
			} while(ExitNow == false);

			Log.Verbose("{0} Awaiting reader and writer shutdown.", Thread.CurrentThread.Name);
			/*
				_connectionWriterThread and / or _connectionWriterThread
				may be null in the case where a connection was not 
				established before the Stop method was called.
			*/
			if (_connectionWriterThread != null)
			{
				Log.Verbose("{0} Awaiting connection writer shutdown.", Thread.CurrentThread.Name);
				_connectionWriterThread.Join();
				Log.Debug("{0} Connection writer terminated.", Thread.CurrentThread.Name);
			}

			if (_connectionReaderThread != null)
			{
				Log.Verbose("{0} Awaiting connection reader shutdown.", Thread.CurrentThread.Name);
				_connectionReaderThread.Join();
				Log.Debug("{0} Connection reader terminated.", Thread.CurrentThread.Name);
			}
			
			Log.Debug("{0} Terminating", Thread.CurrentThread.Name);
		}
		
		/// <summary>
		/// Start a TcpListener and await a connection request from another
		/// entity.
		/// </summary>
		/// <returns>
		/// A <c>TcpClient</c> if successful, <c>null</c> if otherwise.
		/// </returns>
		private TcpClient? ConnectAsPassive()
		{
			TcpListener = null;
			TcpClient? tcpClient   = null;

			/*
				We are basically in server mode so setup to
				receive a connection from a client.
			*/
			Log.Verbose("{0} ConnectAsPassive:Attempting to established connection with client {1}.", Thread.CurrentThread.Name, IPEndPoint.ToString());
			TcpListener = new(IPEndPoint);
			TcpListener.Start();
			try
			{
				tcpClient = TcpListener.AcceptTcpClient();
			}
			catch(SocketException e)
			{
				if ((e.SocketErrorCode == SocketError.Interrupted))
				{
					Log.Verbose("{0} ConnectAsPassive:Passive mode accept terminated. {1} {2}", Thread.CurrentThread.Name, e.GetType().ToString(), e.Message);
					return null;
				}

				Log.Fatal("{0} ConnectAsPassive:Unhandled SocketException {1}", Thread.CurrentThread.Name, e.Message);
				Environment.Exit(-1);
			}
			catch(Exception e)
			{
				Log.Fatal("{0} ConnectAsPassive:Unhandled Exception {1}", Thread.CurrentThread.Name, e.Message);
				Environment.Exit(-1);
			}

//			_ioStream.ReadTimeout = _readTimeoutValue;

			/*
				We are stopping the TCPListener because once a connection is established
				we do not want to be accepting any others until we are finished
				with this one.
			*/
			TcpListener.Stop();
			TcpListener = null;

			Log.Debug("{0} ConnectAsPassive:TcpClient connection established to {1}", Thread.CurrentThread.Name, IPEndPoint.ToString());
			return tcpClient;

		}

		/// <summary>
		/// Establish a connection to another
		/// entity.
		/// </summary>
		/// <returns>
		/// A <c>TcpClient</c> if successful, <c>null</c> if otherwise.
		/// </returns>
		private TcpClient? ConnectAsActive()
		{
			/*
				We are in an Active mode so just connect to the
				"server" and get ready for work.
			*/

			TcpClient tcpClient = new TcpClient();

			do
			{
				try
				{
					/*
						Attempt to connect to the "server".  If we are able
					*/
					Log.Verbose("{0} ConnectAsActive:Attempting to connect with server {1}.", Thread.CurrentThread.Name, IPEndPoint.ToString());
					tcpClient.Connect(IPEndPoint);
					Log.Debug("{0} ConnectAsActive:Connected with server {1}", Thread.CurrentThread.Name, IPEndPoint.ToString());
					break;
				}
				catch(SocketException e)
				{
					if ((e.Message.IndexOf("Connection refused", StringComparison.OrdinalIgnoreCase) >= 0))
					{
						/*
						 	There does not seem to be anyone at the endpoint
							listening yet wait for retry timer and try again.
						*/
						int dummy;
						if (_activeConnectionRetryTimer.TryTake(out dummy, (int)T5 * 1000) == true)
						{
							Log.Debug("{0} ConnectAsActive:Cancelled.", Thread.CurrentThread.Name);
							tcpClient.Close();
							return null;
						}

						Log.Debug("{0} ConnectAsActive:ConnectAsActive:T5 timeout...retrying.", Thread.CurrentThread.Name);
						continue;
					}

					Log.Debug("{0} ConnectAsActive:Some other SocketException is {1} {2}", Thread.CurrentThread.Name, e.GetType().ToString(), e.Message);
					throw;
				}
				catch(Exception e)
				{
					Log.Warning("{0} ConnectAsActive: Another Exception {1}  {2}", Thread.CurrentThread.Name, e.Message, e.GetType().ToString());
					throw;
				}
			} while(ExitNow == false);

			return tcpClient;
		}

		override internal void ConnectionReader()
		{
			bool exitThread = false;

			Log.Debug("{0} Started", Thread.CurrentThread.Name);

			// Empty the message queue
			while(MessagesReceivedQueue.TryTake(out _)){}

			if (_ioStream == null)
			{
				Log.Fatal("{0} _ioStream is null terminating.", Thread.CurrentThread.Name);
				Environment.Exit(-1);
			}

			/*
				These two are always going to be required and their lengths
				will not change so we will declare them here so the GC
				does not have to worry about them until the end.
			*/
			byte[] messageLengthBytes = new byte[4];
			byte[] messageHeaderBytes = new byte[10];

			while(ExitNow == false && exitThread == false)
			{
				try
				{
					/*
						Read the length of the incoming message it is the
						first 4 bytes of the message.  It is Big Endian though
						so it needs to be converted into the "current" type of endian.
					*/
					int currentPosition = 0;
					// Attempt to read messageLengthBytes.Length (4) - currentPosition (0) ... 4 bytes
					while(currentPosition < messageLengthBytes.Length)
					{
						Log.Verbose("{0} Attempting to read the length bytes.", Thread.CurrentThread.Name);
						int numberOfBytesRead = _ioStream.Read(messageLengthBytes, currentPosition, messageLengthBytes.Length - currentPosition);
						if (numberOfBytesRead == 0)
						{
							// It appears that the connection may have been closed from the other side.
							Log.Debug("{0} The connection on the other end may have been terminated.", Thread.CurrentThread.Name);
							exitThread = true;

							// HSMSHeader tempHSMSHeader = new HSMSHeader();
							// tempHSMSHeader.SessionID = 65535;
							// tempHSMSHeader.SType = STypeValues.SeparateReq;

							// SECSMessage tempSECSMessage = new SECSMessage(tempHSMSHeader, new byte[0]);
							// MessagesReceivedQueue.Add(tempSECSMessage);

							break;
						}

						currentPosition += numberOfBytesRead;
					}

					if (exitThread)
					{
						break;
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
					Log.Verbose("{0} Incoming message length is {1} bytes, attempting to read them", Thread.CurrentThread.Name, messageLength);

					currentPosition = 0;
					// Attempt to read messageHeaderBytes.Length (10) - currentPosition (0) ... 10 bytes
					while(currentPosition < messageHeaderBytes.Length)
					{
						int numberOfBytesRead = _ioStream.Read(messageHeaderBytes, currentPosition, messageHeaderBytes.Length - currentPosition);
						currentPosition += numberOfBytesRead;
					}

					// We have successfully retrieved the message header
					HSMSHeader hsmsHeader = new HSMSHeader(messageHeaderBytes);

					byte[]? messageBody = null;
					if (messageLength > 10)
					{
						/*
							The message length is greater than 10...this means
							that the message is not "header only" so we need
							to read the rest of the message.
						*/
						messageBody = new byte[messageLength - 10];
						Log.Verbose("{0} Message length and header retrieved, {1} byte(s) remaining to be read", Thread.CurrentThread.Name, messageBody.Length);
						currentPosition = 0;
						while(currentPosition < messageBody.Length)
						{
							int numberOfBytesRead = _ioStream.Read(messageBody, currentPosition, messageBody.Length - currentPosition);
							currentPosition += numberOfBytesRead;
						}

						Log.Verbose("{0} {1} bytes successfully read (header + body message)", Thread.CurrentThread.Name, messageLength);
					}
					else
					{
						messageBody = new byte[0];
						Log.Verbose("{0} {1} bytes successfully read (header only message)", Thread.CurrentThread.Name, messageLength);
					}

					// Construct the SECS message from the remaining bytes received
					SECSMessage secsMessage = new SECSMessage(hsmsHeader, messageBody);

					/*
						Put the inbound message on a queue for another
						asynchronous thread.
					*/
					Log.Verbose("{0} Adding received message to MessagesReceivedQueue", Thread.CurrentThread.Name);
					MessagesReceivedQueue.Add(secsMessage);

					// Reset and go for another inbound message
					Array.Clear(messageLengthBytes);
					Array.Clear(messageHeaderBytes);
				}
				catch(IOException e)
				{
					if (e.InnerException != null &&
						e.InnerException.GetType() != null &&
					    e.InnerException.GetType() == typeof(System.Net.Sockets.SocketException) &&
						((System.Net.Sockets.SocketException)e.InnerException).SocketErrorCode == SocketError.TimedOut)
					{
						/*
							We have a read timeout.  This is "normal" and needed because we
							cannot seem to interrupt a read when a connection is broken so
							we need to poll and then check the exit status before trying again.
						*/
						continue;
					}

					Log.Debug("{0} Other IOException {1} {2} {3} {4}", Thread.CurrentThread.Name, e.Message, e.GetType().ToString(), e.InnerException != null ? e.InnerException.GetType().ToString() : "No Inner Exception", e.InnerException != null ? e.InnerException.Message : "No Inner Exception");
				}
				catch(ObjectDisposedException)
				{
					Log.Debug("{0} Disposed  exception. exit now is {1}", Thread.CurrentThread.Name, ExitNow);
				}
				catch(Exception e)
				{
					Log.Error("{0} other exception {1} {2}", Thread.CurrentThread.Name, e.Message, e.GetType().ToString());
					throw;
				}
			}

			Log.Debug("{0} Terminated", Thread.CurrentThread.Name);

			// Inform the supervisor the thread has terminated.
			waitHandles[ReaderTerminatedEvent].Set();
		}

		/// <summary>
		/// This is the thread that handles the output of messages to
		/// the other end of the connection.
		/// </summary>
		override internal void ConnectionWriter()
		{
			Log.Debug("{0} Started", Thread.CurrentThread.Name);

			if (_ioStream == null)
			{
				Log.Fatal("{0} _ioStream is null terminating.", Thread.CurrentThread.Name);
				Environment.Exit(-1);
			}

			// Empty the message queue
			while(MessagesToSendQueue.TryTake(out _)){}

			do
			{
				try
				{
					// Sit here and wait for work.
					SECSMessage secsMessage = MessagesToSendQueue.Take(_cancellationToken);

					byte[] binaryMessage = secsMessage.EncodeForTransport();

					UInt32 messageLength = (UInt32)binaryMessage.Length;
					byte[] messageLengthBytes = BitConverter.GetBytes(messageLength);

					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(messageLengthBytes);
					}
				
					if (messageLength == 10)
						Log.Debug("{0} Writing out header only message.", Thread.CurrentThread.Name);
					else
						Log.Debug("{0} Writing out header + body message.", Thread.CurrentThread.Name);

					_ioStream.Write(messageLengthBytes);
					_ioStream.Write(binaryMessage);
				}
				catch(OperationCanceledException)
				{
					// We were cancelled...ExitNow should be true and we will just terminate.
					Log.Debug("{0} Cancelled.", Thread.CurrentThread.Name);
					break;
				}
				catch(Exception e)
				{
					Log.Error("{0} Unexpected exception {1}", Thread.CurrentThread.Name, e.Message);
					break;
				}
			} while(ExitNow == false);

			Log.Debug("{0} Terminating", Thread.CurrentThread.Name);

			// Inform the supervisor the thread has terminated.
			waitHandles[WriterTerminatedEvent].Set();
		}
	} // End class HSMSConnection

} // End namespace CIMthetics.SECSUtilities
