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
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

 using Microsoft.Extensions.Configuration;

using Serilog;

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;
using com.CIMthetics.CSharpSECSTools.TextFormatter;
using com.CIMthetics.CSharpSECSTools.SECSItems;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSSpy
{
	internal class SECSSpy
	{
		internal List<ConfigConnectionPair> configConnectionPairs = new List<ConfigConnectionPair>();

		public SECSFormatter Formatter { get; set; }
		
		internal SECSSpy(string? configFileSpec)
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
				The ConnectionPairs section in the json configuration file contains individual
				ConnectionPair sections(go figure).  Each of these ConnectionPair sections
				contain the information for each end of a connection that SECSSpy will sit in
				between.

			*/
			var connectionsPairsSection = configuration.GetSection("ConnectionPairs");
			if (connectionsPairsSection.GetChildren().LongCount() < 1)
			{
//				Log.Fatal("There must be a \"ConnectionPairs\" section within the  configuration file.");
				Console.WriteLine("There must be a \"ConnectionPairs\" section within the configuration file.");
				Environment.Exit(-1);
			}

			int numberOfConnectionPairs = connectionsPairsSection.GetChildren().Count();
//			configConnectionPairs = new ConfigConnectionPair[numberOfConnectionPairs];

			for(int i = 0; i < numberOfConnectionPairs; i++)
			{
//				configConnectionPairs[i] = new ConfigConnectionPair();

				// Grab a ConnectionPair element out of the ConnectionsPairs array. 
				var connectionsPair = configuration.GetSection("ConnectionPairs:" + i + ":ConnectionPair");

				SECSConnectionConfigInfo? endpoint1 = null;
				SECSConnectionConfigInfo? endpoint2 = null;
				int j = 0;
				foreach(IConfigurationSection connection in connectionsPair.GetChildren())
				{
					if (j > 1)
					{
						Console.WriteLine("There must only be two connections defined in a \"ConnectionPair\".");
						Environment.Exit(-1);
					}

					var individualConnection = connection.GetSection("SECSConnectionConfigInfo");

					SECSConnectionConfigInfo connectionInfo = individualConnection.Get<SECSConnectionConfigInfo>();

					if (j == 0)
					{
						endpoint1 = connectionInfo;
					}
					else
					{
						endpoint2 = connectionInfo;
					}

					j++;
				}

				if (endpoint1 == null || endpoint2 == null)
				{
					Log.Fatal("The \"ConnectionPair\" object appears to be malformed.");
					Environment.Exit(-1);
				}

				configConnectionPairs.Add(new ConfigConnectionPair(endpoint1, endpoint2));
			}


//			for(int i = 0; i < 1; i++)
//			{
//				Console.WriteLine("Connection {0} side 1 {1}", i+1, configConnectionPairs[i].ConnectionConfigurationInfo[0].ToString());
//				Console.WriteLine("Connection {0} side 2 {1}", i+1, configConnectionPairs[i].ConnectionConfigurationInfo[1].ToString());
//			}

			/*
				Now that we have completed the retrieval of the connection configuration
				we need to retrieve the configuration for the formatter.
			*/
			TextFormatterConfig formatterConfiguration = configuration.GetSection("TextFormatterConfig").Get<TextFormatterConfig>();

			// // Set up stuff for SML output config
			// HeaderOutputConfig headerOutputConfig = new HeaderOutputConfig();
			// headerOutputConfig.DisplayWBit = true;

			// BodyOutputConfig bodyOutputConfig = new BodyOutputConfig();
			// bodyOutputConfig.DisplayCount = true;
			// bodyOutputConfig.MaxOutputLineLength = 80;

			// SMLOutputConfig smlOutputConfig = new SMLOutputConfig();
			// smlOutputConfig.HeaderOutputConfig = headerOutputConfig;
			// smlOutputConfig.BodyOutputConfig = bodyOutputConfig;

			// // Set up stuff for XML output config
			// headerOutputConfig = new HeaderOutputConfig();
			// headerOutputConfig.DisplayAsType = DisplayAsType.Attributes;
			// headerOutputConfig.DisplayMessageIdAsSxFy = true;
			// headerOutputConfig.DisplayDeviceId = true;
			// headerOutputConfig.DisplaySystemBytes = true;
			// headerOutputConfig.DisplayWBit = true;
			// headerOutputConfig.DisplayControlMessages = true;

			// bodyOutputConfig = new BodyOutputConfig();
			// bodyOutputConfig.DisplayAsType = DisplayAsType.Attributes;
			// bodyOutputConfig.DisplayNumberOfLengthBytes = true;
			// bodyOutputConfig.DisplayLengthByteValue = true;
			// bodyOutputConfig.MaxOutputLineLength = 80;

			// XMLOutputConfig xmlOutputConfig = new XMLOutputConfig();
			// xmlOutputConfig.HeaderOutputConfig = headerOutputConfig;
			// xmlOutputConfig.BodyOutputConfig = bodyOutputConfig;

			// TextFormatterConfig formatterConfiguration = new TextFormatterConfig();
			// formatterConfiguration.AddTimestamp = true;
			// formatterConfiguration.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fff";
			// formatterConfiguration.AddDirection = true;
			// formatterConfiguration.IndentAmount = 2;
			// formatterConfiguration.MaxIndentionSpaces = 50;
			// formatterConfiguration.LoggingOutputFormat = "SML";
			// formatterConfiguration.SMLOutputConfig = smlOutputConfig;
			// formatterConfiguration.XMLOutputConfig = xmlOutputConfig;

			Formatter = SECSFormatterFactory.CreateFormatter(formatterConfiguration);

		}

// 	<summary>
// 	Retrieve the information for a single element in a &quot;ConnectionPair&quot;
// 	read in from the json configuration file.
// 	
// 	This is done in this manner as opposed to a more sophisticated manner because
// 	the individual array elements may have a different format(fields)
// 	depending on whether or not the connection type is HSMS or SECS-I.
// 		///
// 	This method returns a ConfigConnectionInfo which is a base class.
// 	The actual type may be a ConfigHSMSConnectionInfo or a
// 	ConfigSECSIConnectionInfo depending on whether or not the connection
// 	type is HSMS or SECS-I.
// 	</summary>
// 		private ConfigConnectionInfo GetConnectionInfo(IConfigurationSection connectionSection)
// 		{
// 			ConfigConnectionInfo? result = null;

// 			string connectionName = connectionSection.GetValue<string>("Name");
// 			if (String.IsNullOrEmpty(connectionName) == true)
// 			{
// //				Log.Fatal("Could not retrieve \"Type\" element");
// 				Console.WriteLine("Could not retrieve \"Name\" element");
// 				Environment.Exit(-1);
// 			}

// 			string connectionType = connectionSection.GetValue<string>("Type");
// 			if (String.IsNullOrEmpty(connectionType) == true)
// 			{
// //				Log.Fatal("Could not retrieve \"Type\" element");
// 				Console.WriteLine("Could not retrieve \"Type\" element");
// 				Environment.Exit(-1);
// 			}

// 			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == false &&
// 			    string.Equals(connectionType, "SECS-I", StringComparison.OrdinalIgnoreCase) == false)
// 			{
// //				Log.Fatal("Connection type may only have the values of \"HSMS\" or \"SECS-I\".");
// 				Console.WriteLine("Connection type may only have the values of \"HSMS\" or \"SECS-I\".");
// 				Environment.Exit(-1);
// 			}

// 			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == true)
// 			{
// 				// Look for HSMS connection information

// 				string connectionNetworkAddress = connectionSection.GetValue<string>("Address");
// 				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
// 				{
// //					Log.Fatal("Could not retrieve \"Address\" element of Connection1.");
// 					Console.WriteLine("Could not retrieve \"Address\" element of Connection1.");
// 					Environment.Exit(-1);
// 				}

// 				string connectionNetworkAddressFamilyString = connectionSection.GetValue<string>("AddressFamily");
// 				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
// 				{
// //					Log.Fatal("Could not retrieve \"AddressFamily\" element of Connection1.");
// 					Console.WriteLine("Could not retrieve \"AddressFamily\" element of Connection1.");
// 					Environment.Exit(-1);
// 				}

// 				UInt16 portNumber = connectionSection.GetValue<UInt16>("Port");
// 				if (portNumber <= 0)
// 				{
// //					Log.Fatal("Error trying to retrieve \"Port\" element of Connection1.  It must be a number greater than 0.");
// 					Console.WriteLine("Error trying to retrieve \"Port\" element of Connection1.  It must be a number greater than 0.");
// 					Environment.Exit(-1);
// 				}

// 				string activeOrPassiveString = connectionSection.GetValue<string>("ConnectionMode");
// 				if (String.IsNullOrEmpty(activeOrPassiveString) == true)
// 				{
// //					Log.Fatal("Could not retrieve \"ActiveOrPassive\" element of Connection1.");
// 					Console.WriteLine("Could not retrieve \"ActiveOrPassive\" element of Connection1.");
// 					Environment.Exit(-1);
// 				}
				
// 				result = new ConfigHSMSConnectionInfo(
// 					connectionName,
// 					connectionType,
// 					connectionNetworkAddress,
// 					connectionNetworkAddressFamilyString,
// 					portNumber,
// 					activeOrPassiveString);
// 			}
// 			else
// 			{
// 				// Look for SECS-I connection information

// //					Log.Fatal("SECS-I connection information retrieval not implemented yet.");
// 					Console.WriteLine("SECS-I connection information retrieval not implemented yet.");
// 					Environment.Exit(-1);
// 			}

// 			return result;
// 		}
		

	internal class Connection
	{
		private SECSConnection endpoint1;
		private SECSConnection endpoint2;
		private MessageProcessor endpoint1ToEndpoint2;
		private MessageProcessor endpoint2ToEndpoint1;

		private CancellationTokenSource cancellationTokenSource;
		private CancellationToken 	    messageProcessorCancellationToken;
		private Thread messageProcessorEndpoint1ToEndpoint2Thread;
		private Thread messageProcessorEndpoint2ToEndpoint1Thread;
		
		internal Connection(ConfigConnectionPair connectionPair, SECSFormatter formatter)
		{
			endpoint1 = new HSMSConnection(connectionPair.Endpoint1);
			endpoint2 = new HSMSConnection(connectionPair.Endpoint2);

			cancellationTokenSource = new CancellationTokenSource();
			messageProcessorCancellationToken = cancellationTokenSource.Token;

			endpoint1ToEndpoint2 = 
				new MessageProcessor(connectionPair.Endpoint1.Name,
									 connectionPair.Endpoint2.Name,
									endpoint1.MessagesReceivedQueue,
									endpoint2.MessagesToSendQueue,
									messageProcessorCancellationToken,
									formatter);

			messageProcessorEndpoint1ToEndpoint2Thread = new Thread(endpoint1ToEndpoint2.Run);

			endpoint2ToEndpoint1 = 
				new MessageProcessor(connectionPair.Endpoint2.Name,
									 connectionPair.Endpoint1.Name,
									endpoint2.MessagesReceivedQueue,
									endpoint1.MessagesToSendQueue,
									messageProcessorCancellationToken,
									formatter);

			messageProcessorEndpoint2ToEndpoint1Thread = new Thread(endpoint2ToEndpoint1.Run);
		}

		internal void Start()
		{
			messageProcessorEndpoint1ToEndpoint2Thread.Start();
			messageProcessorEndpoint2ToEndpoint1Thread.Start();

			endpoint1.Start();
			endpoint2.Start();
		}

		internal void Stop()
		{
			cancellationTokenSource.Cancel();
			endpoint1.Stop();
			endpoint2.Stop();
		}
	}

	/// <summary>
	/// Run this program with either no arguments or a single argument
	/// that is the file spec of the configuration file.  If no argument
	/// is provided the <c>appsettings.json</c> file in the directory
	/// where the <c>SECSSpy</c> program resides is used, otherwise, the <c>json</c>
	/// file specified as the command line argument will be used.
	/// </summary>
		public static void Main (string[] args)
		{
			SECSSpy? secsSpy = null;

			Console.WriteLine("Starting SECSSpy");
			
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
					Console.WriteLine("$ SECSSpy");
					Console.WriteLine("       or");
					Console.WriteLine("$ SECSSpy configFilespec");

					Environment.Exit(-1);
				}

				secsSpy = new SECSSpy(args[0]);
			}
			else
			{
				secsSpy = new SECSSpy(null);
			}

/*
			HSMSHeader hsmsHeader = new HSMSHeader();
			hsmsHeader.SessionID = 65534;
			hsmsHeader.Wbit = true;
			hsmsHeader.Stream = 1;
			hsmsHeader.Function = 13;
			hsmsHeader.PType = PTypeValues.SECSIIEncoding;
			hsmsHeader.SType = STypeValues.DataMessage;
			hsmsHeader.SystemBytes = 67305985;


            SECSItem testElement;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
//            testElement = new ASCIISECSItem("ABC");
            testElement = new ASCIISECSItem("Now is the time for all good men to come to the aid aid aid of their country." + Environment.NewLine + Environment.NewLine + "  But, you may ask why now?");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [0]);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, true, true, true, true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L, -1, -9223372036854775808L, 0, 1, 9223372036854775807L, 1234L, 123456L, -1234567L, 12345678L, 123456789L, 1234567890L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127,255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

//Console.WriteLine("List has {0} elements.", expectedData1.Count);
            ListSECSItem secsItem = new ListSECSItem(expectedData1);

			// TODO fix SECSMessage for the case of header only message
			SECSMessage secsMessage = new SECSMessage(hsmsHeader, secsItem);

			string answer;
			answer = secsSpy.Formatter.GetSECSMessageAsText("ELP", "DAL", secsMessage);


//			Console.WriteLine("Answer is:" + Environment.NewLine + answer);

            SECSItem secsItem2 = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127 });
			secsMessage = new SECSMessage(hsmsHeader, secsItem2);

//			answer = secsSpy.Formatter.GetSECSMessageAsText("DAL", "ELP", secsMessage);

//			Console.WriteLine(answer);
//			Console.WriteLine("");

			hsmsHeader = new HSMSHeader();
			hsmsHeader.SessionID = 1234;
			hsmsHeader.Wbit = true;
			hsmsHeader.Stream = 6;
			hsmsHeader.Function = 11;
			hsmsHeader.PType = PTypeValues.SECSIIEncoding;
			hsmsHeader.SType = STypeValues.DataMessage;
			hsmsHeader.SystemBytes = 14;


            ListSECSItem eventReportData = new ListSECSItem();
			eventReportData.Add(new ASCIISECSItem("DATAID"));
			eventReportData.Add(new ASCIISECSItem("CEID"));

            ListSECSItem reports = new ListSECSItem();

			/////////////////////////////////////////////////////////////////////////////
            ListSECSItem report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID1"));

            ListSECSItem vidList = new ListSECSItem();

			vidList.Add(new U2SECSItem(1));
			vidList.Add(new U2ArraySECSItem(new UInt16[]{ 2, 3, 4, 5}));
			vidList.Add(new BooleanSECSItem(true));
			report.Add(vidList);

			reports.Add(report);

			/////////////////////////////////////////////////////////////////////////////
            report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID2"));

            vidList = new ListSECSItem();

			vidList.Add(new ASCIISECSItem());
			vidList.Add(new BinarySECSItem(new byte[]{ 0, 1, 127, 255, 0, 100}));
			vidList.Add(new F8SECSItem(3.141593D));
			vidList.Add(new BooleanArraySECSItem(new bool[]{ true, false, true, true, false}));
			vidList.Add(new I4SECSItem(0x7FFFFFFF));
			report.Add(vidList);

			reports.Add(report);

			/////////////////////////////////////////////////////////////////////////////
            report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID3"));

            vidList = new ListSECSItem();

			vidList.Add(new ListSECSItem());
			vidList.Add(new ASCIISECSItem());
			vidList.Add(new BinarySECSItem());
			vidList.Add(new BooleanArraySECSItem());
			vidList.Add(new I8ArraySECSItem());
			vidList.Add(new I1ArraySECSItem());
			vidList.Add(new I2ArraySECSItem());
			vidList.Add(new I4ArraySECSItem());
			vidList.Add(new F8ArraySECSItem());
			vidList.Add(new F4ArraySECSItem());
			vidList.Add(new U8ArraySECSItem());
			vidList.Add(new U1ArraySECSItem());
			vidList.Add(new U2ArraySECSItem());
			vidList.Add(new U4ArraySECSItem());
			report.Add(vidList);

			reports.Add(report);

			eventReportData.Add(reports);

			// foreach(SECSItem secsitem in eventReportData.Value)
			// {
			// 	Console.WriteLine("SECSItem format = {0} LIB {1}", secsitem.ItemFormatCode, secsitem.LengthInBytes);
			// }

			secsMessage = new SECSMessage(hsmsHeader, eventReportData);

			answer = secsSpy.Formatter.GetSECSMessageAsText("E", "EQ", secsMessage);

//			Console.WriteLine(answer);
*/
//			SECSConnection endpoint1 = new HSMSConnection(secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[0]);

//			Console.WriteLine("RK1");
//			Console.ReadKey();

//			SECSConnection endpoint2 = new HSMSConnection(secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[1]);

			// CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			// CancellationToken 	    messageProcessorCancellationToken = cancellationTokenSource.Token;

			// MessageProcessor endpoint1ToEndpoint2 = 
			// 	new MessageProcessor(secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[0].Name,
			// 						 secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[1].Name,
			// 						endpoint1.MessagesReceivedQueue,
			// 						endpoint2.MessagesToSendQueue,
			// 						messageProcessorCancellationToken,
			// 						secsSpy.Formatter);

			// Thread cp1Thread = new Thread(endpoint1ToEndpoint2.Run);

			// cp1Thread.Start();

			// MessageProcessor endpoint2ToEndpoint1 = 
			// 	new MessageProcessor(secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[1].Name,
			// 						 secsSpy.configConnectionPairs[0].ConnectionConfigurationInfo[0].Name,
			// 						endpoint2.MessagesReceivedQueue,
			// 						endpoint1.MessagesToSendQueue,
			// 						messageProcessorCancellationToken,
			// 						secsSpy.Formatter);

			// Thread cp2Thread = new Thread(endpoint2ToEndpoint1.Run);

			// cp2Thread.Start();

//			Console.WriteLine("RK1");
//			Console.ReadKey();

			// endpoint1.Start();
			// endpoint2.Start();

			List<Connection> connections = new List<Connection>();
			foreach(ConfigConnectionPair pairConfiguration in secsSpy.configConnectionPairs)
			{
				connections.Add(new Connection(pairConfiguration, secsSpy.Formatter));
			}

			foreach(Connection connection in connections)
			{
				connection.Start();
			}

			Log.Debug("SECSSpy:Ready to go.");

			Console.CancelKeyPress += delegate
			{
				Log.Information("SECSSpy: User pressed ^C, shutting down.");

				foreach(Connection connection in connections)
				{
					connection.Stop();
				}
				
				// Give the other thread a moment to die
				Thread.Sleep(2000);
				
				Log.Debug("SECSSpy:All threads have shut down...exiting.");

				Log.CloseAndFlush();
			};

		}
	}
}
