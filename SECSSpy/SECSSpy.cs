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

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;
using com.CIMthetics.CSharpSECSTools.TextFormatter;
using com.CIMthetics.CSharpSECSTools.SECSItems;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSSpy
{
	internal class SECSSpy
	{
		ConfigConnectionPair[]? configConnectionPairs = null;

		public SECSFormatter Formatter { get; set; }
		
		internal SECSSpy(string configFileSpec)
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

			int numberOfLinks = connectionsPairsSection.GetChildren().Count();
			configConnectionPairs = new ConfigConnectionPair[numberOfLinks];

			for(int i = 0; i < numberOfLinks; i++)
			{
				configConnectionPairs[i] = new ConfigConnectionPair();

				// Grab a ConnectionPair element out of the ConnectionsPairs array. 
				var connectionsPair = configuration.GetSection("ConnectionPairs:" + i + ":ConnectionPair");

				int j = 0;
				foreach(IConfigurationSection connection in connectionsPair.GetChildren())
				{
					if (j > 1)
					{
						Console.WriteLine("There must only be two connections defined in a \"ConnectionPair\".");
						Environment.Exit(-1);
					}

					configConnectionPairs[i].ConnectionInfo[j] = GetConnectionInfo(connection);
					j++;
				}
			}


//			for(int i = 0; i < numberOfLinks; i++)
//			{
//				Console.WriteLine("Connection {0} side 1 {1}", i+1, configConnectionPairs[i].ConnectionInfo[0]);
//				Console.WriteLine("Connection {0} side 2 {1}", i+1, configConnectionPairs[i].ConnectionInfo[1]);
//			}

			TextFormatterConfig formatterConfiguration = configuration.GetSection("TextFormatterConfig").Get<TextFormatterConfig>();
//			Console.WriteLine(formatterConfiguration.ToString());
			Formatter = SECSFormatterFactory.CreateFormatter(formatterConfiguration);

		}

		/// <summary>
		/// Retrieve the information for a single element in a &quot;ConnectionPair&quot;
		/// read in from the json configuration file.
		/// 
		/// This is done in this manner as opposed to a more sophisticated manner because
		/// the individual array elements may have a different format(fields)
		/// depending on whether or not the connection type is HSMS or SECS-I.
		///
		/// This method returns a ConfigConnectionInfo which is a base class.
		/// The actual type may be a ConfigHSMSConnectionInfo or a
		/// ConfigSECSIConnectionInfo depending on whether or not the connection
		/// type is HSMS or SECS-I.
		/// </summary>
		private ConfigConnectionInfo GetConnectionInfo(IConfigurationSection connectionSection)
		{
			ConfigConnectionInfo? result = null;

			string connectionName = connectionSection.GetValue<string>("Name");
			if (String.IsNullOrEmpty(connectionName) == true)
			{
//				Log.Fatal("Could not retrieve \"Type\" element");
				Console.WriteLine("Could not retrieve \"Name\" element");
				Environment.Exit(-1);
			}

			string connectionType = connectionSection.GetValue<string>("Type");
			if (String.IsNullOrEmpty(connectionType) == true)
			{
//				Log.Fatal("Could not retrieve \"Type\" element");
				Console.WriteLine("Could not retrieve \"Type\" element");
				Environment.Exit(-1);
			}

			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == false &&
			    string.Equals(connectionType, "SECS-I", StringComparison.OrdinalIgnoreCase) == false)
			{
//				Log.Fatal("Connection type may only have the values of \"HSMS\" or \"SECS-I\".");
				Console.WriteLine("Connection type may only have the values of \"HSMS\" or \"SECS-I\".");
				Environment.Exit(-1);
			}

			if (string.Equals(connectionType, "HSMS", StringComparison.OrdinalIgnoreCase) == true)
			{
				// Look for HSMS connection information

				string connectionNetworkAddress = connectionSection.GetValue<string>("Address");
				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
				{
//					Log.Fatal("Could not retrieve \"Address\" element of Connection1.");
					Console.WriteLine("Could not retrieve \"Address\" element of Connection1.");
					Environment.Exit(-1);
				}

				string connectionNetworkAddressFamilyString = connectionSection.GetValue<string>("AddressFamily");
				if (String.IsNullOrEmpty(connectionNetworkAddress) == true)
				{
//					Log.Fatal("Could not retrieve \"AddressFamily\" element of Connection1.");
					Console.WriteLine("Could not retrieve \"AddressFamily\" element of Connection1.");
					Environment.Exit(-1);
				}

				UInt16 portNumber = connectionSection.GetValue<UInt16>("Port");
				if (portNumber <= 0)
				{
//					Log.Fatal("Error trying to retrieve \"Port\" element of Connection1.  It must be a number greater than 0.");
					Console.WriteLine("Error trying to retrieve \"Port\" element of Connection1.  It must be a number greater than 0.");
					Environment.Exit(-1);
				}

				string activeOrPassiveString = connectionSection.GetValue<string>("ConnectionMode");
				if (String.IsNullOrEmpty(activeOrPassiveString) == true)
				{
//					Log.Fatal("Could not retrieve \"ActiveOrPassive\" element of Connection1.");
					Console.WriteLine("Could not retrieve \"ActiveOrPassive\" element of Connection1.");
					Environment.Exit(-1);
				}
				
				result = new ConfigHSMSConnectionInfo(
					connectionName,
					connectionType,
					connectionNetworkAddress,
					connectionNetworkAddressFamilyString,
					portNumber,
					activeOrPassiveString);
			}
			else
			{
				// Look for SECS-I connection information

//					Log.Fatal("SECS-I connection information retrieval not implemented yet.");
					Console.WriteLine("SECS-I connection information retrieval not implemented yet.");
					Environment.Exit(-1);
			}

			return result;
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


			HSMSHeader hsmsHeader = new HSMSHeader();
			hsmsHeader.SessionID = 65534;
			hsmsHeader.Wbit = true;
			hsmsHeader.Stream = 1;
			hsmsHeader.Function = 13;
			hsmsHeader.PType = PTypeValues.SECSIIEncoding;
			hsmsHeader.SType = STypeValues.DataMessage;
			hsmsHeader.SystemBytes = 67305985;


            SECSItem testElement;
            LinkedList<SECSItem> expectedData1 = new LinkedList<SECSItem> ();
            LinkedList<SECSItem> expectedData2 = new LinkedList<SECSItem> ();
            testElement = new ASCIISECSItem ("ABC");
            testElement = new ASCIISECSItem ("Now is the time for all good men to come to the aid aid aid of their country." + Environment.NewLine + Environment.NewLine + "  But, you may ask why now?");
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BinarySECSItem (new byte [0]);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BinarySECSItem (new byte [] { 128 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BinarySECSItem (new byte [] { 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanArraySECSItem (new bool [] { true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, true, true, false, true, false, true, false, true, true, true, true, true, false, true, false, true, false, true, true });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanSECSItem (true);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4ArraySECSItem (new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4SECSItem (Single.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8ArraySECSItem (new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D, Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8SECSItem (Double.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1ArraySECSItem (new sbyte [] { -1, -128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2ArraySECSItem (new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4ArraySECSItem (new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8ArraySECSItem (new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L, -1, -9223372036854775808L, 0, 1, 9223372036854775807L, 1234L, 123456L, -1234567L, 12345678L, 123456789L, 1234567890L });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1ArraySECSItem (new byte [] { 255, 128, 0, 127,255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127, 255, 128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1SECSItem (255);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2ArraySECSItem (new UInt16 [] { 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767, 65535, 32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2SECSItem ((int)65535);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4ArraySECSItem (new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4SECSItem (0xFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8ArraySECSItem (new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);

            ListSECSItem innerList = new ListSECSItem (expectedData2);
            expectedData1.AddLast (innerList);
            expectedData1.AddLast (innerList);

            ListSECSItem secsItem = new ListSECSItem (expectedData1);

			// TODO fix SECSMessage for the case of header only message
			SECSMessage secsMessage = new SECSMessage(hsmsHeader, secsItem);

			string answer;
			answer = secsSpy.Formatter.GetSECSMessageAsText("ELP", "DAL", secsMessage);

			Console.WriteLine("Answer is:" + Environment.NewLine + answer);

            SECSItem secsItem2 = new BinarySECSItem (new byte [] { 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127, 128, 255, 0, 1, 127 });
			secsMessage = new SECSMessage(hsmsHeader, secsItem2);

			answer = secsSpy.Formatter.GetSECSMessageAsText("DAL", "ELP", secsMessage);

			Console.WriteLine(answer);
			Console.WriteLine(answer);
		}
	}
}
