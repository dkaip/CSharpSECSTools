using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class SECS1Connection : SECSConnection
	{
		public UInt32 T1 { get; set; } // in ms
		public UInt32 T2 { get; set; }
		public UInt32 T4 { get; set; }
		public uint RetryCount { get; set; }
		public bool Master { get; set; }
		public string TTYPort { get; set; }

		public SECS1Connection(string ConnectionName, ref Queue<TransientMessage> MessagesReceived, ref EventWaitHandle MessageReceivedEF, string TTYPort, bool Master)
			: base(ConnectionName, ref MessagesReceived, ref MessageReceivedEF)
		{
			T1 = ConnectionDefaults.T1;
			T2 = ConnectionDefaults.T2;
			T3 = ConnectionDefaults.T3;
			T4 = ConnectionDefaults.T4;
			RetryCount = ConnectionDefaults.RetryCount;

			this.TTYPort = TTYPort;
			this.Master = Master;

		}

		override public void sendMessage(TransientMessage Message)
		{
		} // End override public void sendMessage(TransientMessage Message)

		override public void start()
		{
			Thread.CurrentThread.Name = ConnectionName + " Connection Thread(SECSI)";
		}
	} // End class SECSIConnection

} // End namespace CIMthetics.SECSUtilities
