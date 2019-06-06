using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class HSMSConnection : SECSConnection
	{
		private Thread CurrentThread;
		private TcpClient tcpclnt = null;
		private TcpListener tcpListener = null;
		private NetworkStream IOStream = null;
		private HSMSReader HSMSReader;
		private Thread ReaderThread;
		private HSMSWriter HSMSWriter;
		private Thread WriterThread;
		public TCPState TCPState { get; set; }


		public UInt32 T5 { get; set; }
		public UInt32 T6 { get; set; }
		public UInt32 T7 { get; set; }
		public UInt32 T8 { get; set; }
		public HSMSConnectionMode ConnectionMode { get; set; }

		public string IPAddress { get; set; }
		public string HostName { get; set; }

		public UInt16 PortNumber { get; set; }

		public HSMSConnectionState ConnectionState { get; private set; }

		public HSMSConnection(string ConnectionName, ref Queue<TransientMessage> MessagesReceived, ref EventWaitHandle MessageReceivedWaitHandle, string IPAddress, HSMSConnectionMode ConnectionMode) : base( ConnectionName, ref MessagesReceived, ref MessageReceivedWaitHandle )
		{
			T3 = ConnectionDefaults.T3;
			T5 = ConnectionDefaults.T5;
			T6 = ConnectionDefaults.T6;
			T7 = ConnectionDefaults.T7;
			T8 = ConnectionDefaults.T8;

			PortNumber = ConnectionDefaults.Port;

			ConnectionState = HSMSConnectionState.NotConnected;

			this.IPAddress = IPAddress;
			this.ConnectionMode = ConnectionMode;
			TCPState = TCPState.Inactive;


			ReceivedSECSMessages = new Queue<SECSMessage>();
			ReceivedSECSMessagesWH = new EventWaitHandle(false, EventResetMode.AutoReset);
		}

		public HSMSConnection(string ConnectionName, ref Queue<TransientMessage> MessagesReceived, ref EventWaitHandle MessageReceivedEF, string IPAddress, UInt16 Port, HSMSConnectionMode ConnectionMode)
			: base(ConnectionName, ref MessagesReceived, ref MessageReceivedEF)
		{
			T3 = ConnectionDefaults.T3;
			T5 = ConnectionDefaults.T5;
			T6 = ConnectionDefaults.T6;
			T7 = ConnectionDefaults.T7;
			T8 = ConnectionDefaults.T8;

			PortNumber = Port;

			ConnectionState = HSMSConnectionState.NotConnected;

			this.IPAddress = IPAddress;
			this.ConnectionMode = ConnectionMode;
			TCPState = TCPState.Inactive;

			ReceivedSECSMessages = new Queue<SECSMessage>();
			ReceivedSECSMessagesWH = new EventWaitHandle(false, EventResetMode.AutoReset);
		}

		override public void sendMessage(TransientMessage Message)
		{
			if (TCPState != TCPState.Active)
			{
				// We do not have an active connection so take the outgoing message
				// and put it on the Received messages queue with and error and
				// let the connection instanciator deal with it.

				Message.MessageStatus = TransientMessageStatus.NoConnection;

				lock (MessagesReceived)
				{
					MessagesReceived.Enqueue(Message);
				}

				MessageReceivedWaitHandle.Set();
			}



			// Put the outbound message on the queue
			lock (MessagesToSend)
			{
				MessagesToSend.Enqueue(Message);
			}

			// Wake up the connect to do some work
			MessageToSendWaitHandle.Set();

			Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Message set to writer");

		} // End public override void sendMessage(TransientMessage Message)

		override public void start()
		{
			CurrentThread = Thread.CurrentThread;
			Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Starting");

			while (true)
			{
				/*
                            try
                            {
                                IPHostEntry hostInfo;

                                // Attempt to resolve DNS for given host or address
                                hostInfo = Dns.GetHostEntry(IPAddress);

                                // Display the primary host name
                                Console.WriteLine(CurrentThread.Name + "\tCanonical Name: " + hostInfo.HostName);

                                HostName = hostInfo.HostName;

                                // Display list of IP addresses for this host
                                Console.WriteLine(CurrentThread.Name + "\tIP Addresses:   ");
                                foreach (IPAddress ipaddr in hostInfo.AddressList)
                                {
                                    Console.WriteLine(CurrentThread.Name + "\t\t" +ipaddr.ToString());
                                }
                                Console.WriteLine(CurrentThread.Name);

                                // Display list of alias names for this host
                                Console.WriteLine(CurrentThread.Name + "\tAliases:        ");
                                foreach (String alias in hostInfo.Aliases)
                                {
                                    Console.WriteLine(CurrentThread.Name + "\t\t" + alias + " ");

                                }
                                Console.WriteLine(CurrentThread.Name + "\n");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine( CurrentThread.Name + "\tUnable to resolve host: " + this.IPAddress.ToString() + " " + e.ToString() + "\n" + e.StackTrace  + "\n");
                            }
                */

				if (ConnectionMode == HSMSConnectionMode.Active ||
					ConnectionMode == HSMSConnectionMode.ActivePassthru)
				{
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Attempting to connect");

					tcpclnt = new TcpClient();

					try
					{
						tcpclnt.Connect("192.168.1.65", 5000); // use the ipaddress as in the server program
					}
					catch (Exception e)
					{
						Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "\tUnable to connect to 192.168.1.65 port 5000: " + this.IPAddress.ToString() + " " + e.ToString() + "\n" + e.StackTrace + "\n");
					}
					IOStream = tcpclnt.GetStream();

					HSMSReader = new HSMSReader(IOStream, ref ReceivedSECSMessages, ref ReceivedSECSMessagesWH);
					ReaderThread = new Thread(new ThreadStart(HSMSReader.start));
					ReaderThread.Name = CurrentThread.Name + "HSMSReader:";

					ReaderThread.Start();

					HSMSWriter = new HSMSWriter(IOStream, MessagesToSend, MessageToSendWaitHandle);
					WriterThread = new Thread(new ThreadStart(HSMSWriter.start));
					WriterThread.Name = CurrentThread.Name + "HSMSWriter:";

					WriterThread.Start();

					TCPState = TCPState.Active;
				}
				else
				{
					tcpListener = new TcpListener(System.Net.IPAddress.Parse("192.168.1.24"), 5000);
					tcpListener.Start();

					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Accepting Client");
					tcpclnt = tcpListener.AcceptTcpClient();
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Accepted Client");
					IOStream = tcpclnt.GetStream();

					HSMSReader = new HSMSReader(IOStream, ref ReceivedSECSMessages, ref ReceivedSECSMessagesWH);
					ReaderThread = new Thread(new ThreadStart(HSMSReader.start));
					ReaderThread.Name = CurrentThread.Name + "HSMSReader:";

					ReaderThread.Start();
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Finished Starting Reader");

					HSMSWriter = new HSMSWriter(IOStream, MessagesToSend, MessageToSendWaitHandle);
					WriterThread = new Thread(new ThreadStart(HSMSWriter.start));
					WriterThread.Name = CurrentThread.Name + "HSMSWriter:";

					WriterThread.Start();
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Finished Starting Writer");

					TCPState = TCPState.Active;
				}
				/*
                            try
                            {
                
                                Console.WriteLine("Connecting.....");

                //                tcpclnt.Connect("192.168.1.65", 5000); // use the ipaddress as in the server program

                                Console.WriteLine("Connected");
                                Console.Write("Enter the string to be transmitted : ");

                                String str = Console.ReadLine();
                

                                HSMSHeader header = new HSMSHeader();

                                header.SessionID = 1;
                                header.SType = (Byte)STypeValues.SelectReq;

                                byte[] message = new byte[14];
                                message[0] = 0;
                                message[1] = 0;
                                message[2] = 0;
                                message[3] = 10;
                                byte[] ba = header.Encode();

                                message[4] = ba[0];
                                message[5] = ba[1];
                                message[6] = ba[2];
                                message[7] = ba[3];
                                message[8] = ba[4];
                                message[9] = ba[5];
                                message[10] = ba[6];
                                message[11] = ba[7];
                                message[12] = ba[8];
                                message[13] = ba[9];

                                Console.WriteLine("Transmitting.....");

                //                stm.Write(message, 0, message.Length);

                //                byte[] bb = new byte[100];
                //                int k = stm.Read(bb, 0, 100);
                //stm.                string hex = BitConverter.ToString(bb);

                //                Console.WriteLine( "Returned " + k +" Bytes :" + hex );
                //                for (int i = 0; i < k; i++)
                //                    Console.Write(Convert..ToChar(bb[i]));

                                tcpclnt.Close();
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine( CurrentThread.Name + " Error..... " + e.StackTrace);
                            }

                 */
				//                Thread.Sleep(10000);
				while (true)
				{
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Blocking for message from Reader Thread");
					ReceivedSECSMessagesWH.WaitOne();
					SECSMessage ReceivedMessage = null;

					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Received Inbound message from Reader Thread");
					lock (ReceivedSECSMessages)
					{
						ReceivedMessage = ReceivedSECSMessages.Dequeue();
					}

					if (ReceivedMessage.IsValidMessage == false)
					{
						Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Inbound message Indicates I/O has link failed");

						TransientMessage TM1 = new TransientMessage();

						TM1.MessageStatus = TransientMessageStatus.NoConnection;

						lock (MessagesReceived)
						{
							MessagesReceived.Enqueue(TM1);
						}

						MessageReceivedWaitHandle.Set();
						Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Instructing Writer Thread to terminate.");
						break;
					}
					TransientMessage TM = new TransientMessage();

					TM.SECSData = ReceivedMessage;
					TM.MessageStatus = TransientMessageStatus.IncommingMessage;
					TM.ReceivedFrom = ConnectionName;

					lock (MessagesReceived)
					{
						MessagesReceived.Enqueue(TM);
					}

					MessageReceivedWaitHandle.Set();
					Console.WriteLine(DateTime.Now.ToString() + " " + CurrentThread.Name + "Inbound message passed to connection owner");

				} // End while (true)

				IOStream.Close();

				if ( tcpListener != null )
					tcpListener.Stop();

				tcpListener = null;
				IOStream = null;
				HSMSReader = null;
				ReaderThread = null;
				WriterThread = null;
				HSMSWriter = null;

			} // End // End while (true)

		} // End public void start()

	} // End class HSMSConnection

} // End namespace CIMthetics.SECSUtilities
