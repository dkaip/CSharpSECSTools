using Microsoft.Extensions.Configuration;
using Serilog;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;
using com.CIMthetics.CSharpSECSTools.SECSStateMachines;
using com.CIMthetics.CSharpSECSTools.SECSStateMachines.HSMSConnectionSM;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSTee
{
	/// <summary>
	/// The purpose of this program is to provide a Tee functionality.
	/// In short, you are able to &quot;insert&quot; this program in
	/// between a piece of equipment and the host that it is connected
	/// to and cause a copy of the data stream to be sent to another
	/// recipient.
	/// </summary
	public class SECSTee
	{
		private 	SECSConnection	_connection1;
		private		BlockingCollection<SECSMessage> _connection1ReceivedMessagesQueue;
		private		SECSConnection  _connection2;
		private		BlockingCollection<SECSMessage> _connection2ReceivedMessagesQueue;


		internal SECSTee(string? configFileSpec)
		{
			IConfiguration? configuration = null;

			// Load the configuration file contents.
			if (String.IsNullOrEmpty(configFileSpec) == true)
			{
				/*
					The config file spec was not specified on the command
					line so we will attempt to load the default.
				*/
                configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
			}
			else
			{
				/*
				    The config file spec was specified on the command line
					so attempt to load it.
				*/
				configuration = new ConfigurationBuilder().AddJsonFile(configFileSpec).Build();
			}

			// Set up a Serilog logger using the information in the config file.
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();

			/*
				Now that we have the configuration file loaded and a logger
				created we need to get on to the job of extracting the
				information needed for operation.
			*/

			// Grab the section of the config file for connection 1
			IConfigurationSection connectionSection = configuration.GetSection("SECSTeeConfig:Connection1");
			if (connectionSection == null)
			{
				/*
				    Note: For some reason this does not work when Connection1 is
					missing from the configuration file.
				*/
				Log.Fatal("Configuration element \"Connection1\" is missing it must be present.");
				Environment.Exit(-1);
			}

			ConnectionInfo connection1ConfigInfo = GetConnectionInfo(connectionSection);


			// Grab the section of the config file for connection 2
			connectionSection = configuration.GetSection("SECSTeeConfig:Connection2");
			if (connectionSection == null)
			{
				/*
				    Note: For some reason this does not work when Connection1 is
					missing from the configuration file.
				*/
				Log.Fatal("Configuration element \"Connection2\" is missing it must be present.");
				Environment.Exit(-1);
			}

			ConnectionInfo connection2ConfigInfo = GetConnectionInfo(connectionSection);

			/*
			    Now we need to exract the information for the Tee(s)
			*/
			var teeConnectionSection = configuration.GetSection("SECSTeeConfig:TeeConnections");
			if (teeConnectionSection.GetChildren().LongCount() < 1)
			{
				Log.Fatal("There must be at least 1 \"TeeConnection\" element within the \"TeeConnections\" section of the configuration file.");
				Environment.Exit(-1);
			}

			List<TeeConnectionInfo> teeConnectionInfos = GetTeeConnectionInfo(teeConnectionSection);

			/*
			    Create the queues that will receive the messages from 
				connection 1 and connection 2.
			*/
			_connection1ReceivedMessagesQueue = new BlockingCollection<SECSMessage>();
			_connection2ReceivedMessagesQueue = new BlockingCollection<SECSMessage>();

			if (connection1ConfigInfo is HSMSConnectionInfo)
			{
				Log.Verbose("Connection 1 type is HSMS");
				HSMSConnectionInfo configInfo = (HSMSConnectionInfo)connection1ConfigInfo;
			Log.Verbose("Connection1 config info");
			Log.Verbose(configInfo.ToString());

				IPAddress? ipAddress = GetIPAddress(configInfo);

				if (ipAddress == null)
				{
					Log.Fatal("Address {0} did not resolve to and address.", configInfo.IPAddress);
					Environment.Exit(-1);
				}

				_connection1 = new HSMSConnection("Connection1", _connection1ReceivedMessagesQueue, ipAddress, configInfo.Port, configInfo.ConnectionMode);
			}
			else if (connection1ConfigInfo is SECSIConnectionInfo)
			{
				Log.Verbose("Connection 1 type is SECS-I");
				Log.Error("Not implemented yet.");
				_connection1 = new SECSIConnection("Connection1", _connection1ReceivedMessagesQueue, "tty port", false);
			}
			else
			{
				Log.Fatal("Unexpected ConnectionInfo object type of {0}.", connection1ConfigInfo.GetType().Name);
				Environment.Exit(-1);
			}

			if (connection2ConfigInfo is HSMSConnectionInfo)
			{
				Log.Verbose("Connection 2 type is HSMS");

				HSMSConnectionInfo configInfo = (HSMSConnectionInfo)connection2ConfigInfo;
			Log.Verbose("Connection1 config info");
			Log.Verbose(configInfo.ToString());

				IPAddress? ipAddress = GetIPAddress(configInfo);

				if (ipAddress == null)
				{
					Log.Fatal("Address {0} did not resolve to and address.", configInfo.IPAddress);
					Environment.Exit(-1);
				}
				_connection2 = new HSMSConnection("Connection2", _connection2ReceivedMessagesQueue, ipAddress, configInfo.Port, configInfo.ConnectionMode);
			}
			else if (connection1ConfigInfo is SECSIConnectionInfo)
			{
				Log.Verbose("Connection 2 type is SECS-I");
				Log.Error("Not implemented yet.");
				_connection2 = new SECSIConnection("Connection2", _connection2ReceivedMessagesQueue, "tty port", false);
			}
			else
			{
				Log.Fatal("Unexpected ConnectionInfo object type of {0}.", connection2ConfigInfo.GetType().Name);
				Environment.Exit(-1);
			}
		}

		private IPAddress? GetIPAddress(HSMSConnectionInfo connectionInfo)
		{
			Log.Verbose("Hostname to get IP address from is {0}", connectionInfo.IPAddress);
			IPAddress? ipAddress = null;
			IPAddress[] ipAddresses = Dns.GetHostAddresses(connectionInfo.IPAddress);

			if (connectionInfo.NetworkFamily == NetworkFamily.IPV4)
			{
				foreach(IPAddress address in ipAddresses)
				{
					if (address.AddressFamily == AddressFamily.InterNetwork)
					{
						ipAddress = address;
						// We are just grabbing the first one.
						break;
					}
				}
			}
			else if (connectionInfo.NetworkFamily == NetworkFamily.IPV6)
			{
				foreach(IPAddress address in ipAddresses)
				{
					if (address.AddressFamily == AddressFamily.InterNetworkV6)
					{
						ipAddress = address;
						// We are just grabbing the first one.
						break;
					}
				}
			}

			return ipAddress;
		}

		private ConnectionInfo GetConnectionInfo(IConfigurationSection connectionSection)
		{
			ConnectionInfo? result = null;

			string connectionType = connectionSection.GetValue<string>("Type");
			if (String.IsNullOrEmpty(connectionType) == true)
			{
				Log.Fatal("Could not retrieve \"Type\" element");
				Environment.Exit(-1);
			}

			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == false &&
			    string.Equals(connectionType, "SECS-I", StringComparison.OrdinalIgnoreCase) == false)
			{
				Log.Fatal("Connection type may only have the values of \"HSMS\" or \"SECS-I\".");
				Environment.Exit(-1);
			}

			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == true)
			{
				// Look for HSMS connection information

				string connectionNetworkAddress = connectionSection.GetValue<string>("Address");
				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
				{
					Log.Fatal("Could not retrieve \"Address\" element of Connection1.");
					Environment.Exit(-1);
				}

				string connectionNetworkAddressFamilyString = connectionSection.GetValue<string>("AddressFamily");
				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
				{
					Log.Fatal("Could not retrieve \"AddressFamily\" element of Connection1.");
					Environment.Exit(-1);
				}

				NetworkFamily connectionNetworkAddressFamily;
				if (String.Equals(connectionNetworkAddressFamilyString, "ipv4", StringComparison.OrdinalIgnoreCase) == true)
				{
					connectionNetworkAddressFamily = NetworkFamily.IPV4;
				}
				else
				{
					connectionNetworkAddressFamily = NetworkFamily.IPV6;
				}

				UInt16 portNumber = connectionSection.GetValue<UInt16>("Port");
				if (portNumber <= 0)
				{
					Log.Fatal("Error trying to retrieve \"Port\" element of Connection1.  It must be a number greater than 0.");
					Environment.Exit(-1);
				}

				HSMSConnectionMode activeOrPassive;
				string activeOrPassiveString = connectionSection.GetValue<string>("ActiveOrPassive");
				if (String.IsNullOrEmpty(activeOrPassiveString) == true)
				{
					Log.Fatal("Could not retrieve \"ActiveOrPassive\" element of Connection1.");
					Environment.Exit(-1);
				}
				
				if (String.Equals(activeOrPassiveString, "active", StringComparison.OrdinalIgnoreCase) == true)
				{
					activeOrPassive = HSMSConnectionMode.Active;
				}
				else
				{
					activeOrPassive = HSMSConnectionMode.Passive;
				}

				result = new HSMSConnectionInfo(connectionNetworkAddressFamily, connectionNetworkAddress, portNumber, activeOrPassive);
			}
			else
			{
				// Look for SECS-I connection information

					Log.Fatal("SECS-I connection information retrieval not implemented yet.");
					Environment.Exit(-1);
			}

			return result;
		}
		
		private List<TeeConnectionInfo> GetTeeConnectionInfo(IConfigurationSection teeConnectionSection)
		{
			Log.Verbose("TeeConnections contains " + teeConnectionSection.GetChildren().LongCount().ToString() + " elements.");

			List<TeeConnectionInfo> result = new List<TeeConnectionInfo>();
			foreach(IConfigurationSection section in teeConnectionSection.GetChildren())
			{
				/*
					Part of a TeeConnection is identical to the previous connection
					information so just use that method to retrieve that part of
					the information.

					Note: At this point "path" for section is SECSTeeConfig:TeeConnections:0.
					For the second element it will be SECSTeeConfig:TeeConnections:1 and so
					on.  We still need to do the GetSection for TeeConnection to get the 
					individual sections.
				*/
				ConnectionInfo connectionConfigInfo = GetConnectionInfo(section.GetSection("TeeConnection"));

				var connectionsToForward = section.GetSection("TeeConnection:ConnectionsToForward").Get<string[]>();
				if (connectionsToForward.Length < 1 || connectionsToForward.Length > 2)
				{
					Log.Fatal("ConnectionsToForward must be [\"Connection1\"], [\"Connection2\"], [\"Connection1\", \"Connection2\"], or [\"Connection2\", \"Connection1\"].");
					Environment.Exit(-1);
				}

				TeeConnectionInfo teeConnectionInfo = new TeeConnectionInfo(connectionConfigInfo, connectionsToForward);
			}

			return result;
		}

		/// <summary>
		/// Run this program with either no arguments or a single argument
		/// that is the file spec of the configuration file.  If no argument
		/// is provided the <c>appsettings.json</c> file in the directory
		/// where the <c>SECSTee</c> program resides is used, otherwise, the <c>json</c>
		/// file specified as the command line argument will be used.
		/// </summary>
		public static void Main (string[] args)
		{
			SECSTee? secsTee = null;

			// Load the configuration file.
			if (args.Length > 0)
			{
				/*
				    If a command line argument was specified there must only
					be one and it will be used as the configuration file spec.
				*/
				if (args.Length != 1)
				{
					Console.WriteLine("Usage is:");
					Console.WriteLine("$ SECSTee");
					Console.WriteLine("       or");
					Console.WriteLine("$ SECSTee configFilespec");

					Environment.Exit(-1);
				}

				// Load the configuration file contents.
				Console.WriteLine("args is " + args[0]);

				secsTee = new SECSTee(args[0]);
			}
			else
			{
				secsTee = new SECSTee(null);
			}

			Log.Warning("SM b4 constructor");
			HSMSConnectionSM sm = new HSMSConnectionSM();
			List<State> states = sm.GetStates();
			// Log.Warning("Number of states is {0}", states.Count());
			// foreach(State state in states)
			// {
			// 	Log.Debug("in SECSTee State {0}:{1}", state.StateName, state.StateID);
			// }
			Log.Warning("SM af constructor");
			Console.ReadKey();
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition1);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition1);
			Log.Warning("A state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition2);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition2);
			Log.Warning("B state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition4);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
			Log.Warning("C state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition5);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition5);
			Log.Warning("D state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition4);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
			Log.Warning("E state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition5);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition5);
			Log.Warning("F state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition6);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition6);
			Log.Warning("G state is {0}", sm.CurrentState.StateName);
			Log.Debug("SECSTee attempting {0}", HSMSConnectionSMTransitions.Transition4);
			sm.PerformTransition((int)HSMSConnectionSMTransitions.Transition4);
			Log.Warning("H state is {0}", sm.CurrentState.StateName);
//			sm.GoToState(HSMSConnectionStateMachine.State_NotSelected);
//			sm.GoToState(HSMSConnectionStateMachine.State_Selected);
//			sm.GoToState(HSMSConnectionStateMachine.State_NotSelected);
//			sm.GoToState(HSMSConnectionStateMachine.State_NotConnected);

			Console.ReadKey();


			CancellationTokenSource _cancellationTokenSource  = new CancellationTokenSource();
			CancellationToken 		_cancellationToken = _cancellationTokenSource.Token;

			SECSMessageProcessor connection1ToConnection2 = new SECSMessageProcessor("Connection1", "Connection2", secsTee._connection1ReceivedMessagesQueue, secsTee._connection2.MessagesToSendQueue, _cancellationToken);
			SECSMessageProcessor connection2ToConnection1 = new SECSMessageProcessor("Connection2", "Connection1", secsTee._connection2ReceivedMessagesQueue, secsTee._connection1.MessagesToSendQueue, _cancellationToken);

			Thread cp1Thread = new Thread(connection1ToConnection2.Run);
			Thread cp2Thread = new Thread(connection2ToConnection1.Run);

			cp1Thread.Start();
			cp2Thread.Start();
			
			Log.Information("SECSTee startup");
			Log.Verbose("starting connection 1");
			secsTee._connection1.Start();
			Log.Verbose("starting connection 2");
			secsTee._connection2.Start();


			Console.CancelKeyPress += delegate
			{
				Log.Information("SECSTee shutting down due to ^c");

				secsTee._connection2.Stop();
				secsTee._connection1.Stop();

				_cancellationTokenSource.Cancel();

//				Log.Debug("Joining connection 1 thread.");
//				secsTee._connection1.GetThread().Join();
//				Log.Debug("Joining connection 2 thread.");
//				secsTee._connection2.GetThread().Join();

				// Give the other thread a moment to die
				Thread.Sleep(1000);
				Log.Debug("all threads have shut down...exiting.");

				Log.CloseAndFlush();
			};
		}
	}
}
