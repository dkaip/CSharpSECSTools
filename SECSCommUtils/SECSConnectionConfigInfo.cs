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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
    public class SECSConnectionConfigInfo
    {
		public UInt32 T1  { get; set; } = ConnectionDefaults.T1;
		public UInt32 T2 { get; set; } = ConnectionDefaults.T2;
		public UInt32 T3 { get; set; } = ConnectionDefaults.T3;
		public UInt32 T4 { get; set; } = ConnectionDefaults.T4;
		public UInt32 T5 { get; set; } = ConnectionDefaults.T5;
		public UInt32 T6 { get; set; } = ConnectionDefaults.T6;
		public UInt32 T7 { get; set; } = ConnectionDefaults.T7;
		public UInt32 T8 { get; set; } = ConnectionDefaults.T8;

		public UInt32 RetryCount { get; set; } = ConnectionDefaults.RetryCount;

		public UInt16 Port { get; set; } = ConnectionDefaults.Port;
		public string TTYPort { get; set; } = "";

		public UInt32 BaudRate = ConnectionDefaults.BaudRate;
		public byte	Bits = ConnectionDefaults.Bits;
		public byte	StopBits = ConnectionDefaults.StopBits;
		public string Parity = ConnectionDefaults.Parity;

		public string Name { get; set; } = "Not Set";
		private SECSConnectionType ConnectionType = SECSConnectionType.HSMS;
		public string Type
		{
			get
			{
				return ConnectionType.ToString();
			}
			set
			{
                if (string.Compare(value, "HSMS", true) == 0)
                {
                    ConnectionType = SECSConnectionType.HSMS;
                }
                else if (string.Compare(value, "SECSI", true) == 0)
                {
                    ConnectionType = SECSConnectionType.SECSI;
                }
                else
                {
                    throw new ArgumentException("The connection Type may only have a value of either \"HSMS\" or \"SECSI\"");
                }
			}
		}

		public string Address { get; set; } = "Not Set";

		private string _addressFamily = "IPV4";
		public string AddressFamily
		{
			get
			{
				return _addressFamily;
			}
			set
			{
                if (string.Compare(value, "IPV4", true) == 0)
                {
                    _addressFamily = "IPV4";
                }
                else if (string.Compare(value, "IPV6", true) == 0)
                {
                    _addressFamily = "IPV6";
                }
                else
                {
                    throw new ArgumentException("The Address Family may only have a value of either \"IPV4\" or \"IPV6\"");
                }
			}
		}


		public HSMSConnectionMode HSMSConnectionMode = HSMSConnectionMode.Active;
		public string ConnectionMode
		{
			get
			{
				return HSMSConnectionMode.ToString();
			}
			set
			{
                if (string.Compare(value, "active", true) == 0)
                {
                    HSMSConnectionMode = HSMSConnectionMode.Active;
                }
                else if (string.Compare(value, "passive", true) == 0)
                {
                    HSMSConnectionMode = HSMSConnectionMode.Passive;
                }
                else
                {
                    throw new ArgumentException("The Connection Mode may only have a value of either \"active\" or \"passive\"");
                }
			}
		}

        public override string ToString()
        {
            return ConnectionMode;
        }
    }
}