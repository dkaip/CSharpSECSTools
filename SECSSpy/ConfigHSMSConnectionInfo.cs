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

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;

namespace com.CIMthetics.CSharpSECSTools.SECSSpy
{
    public class ConfigHSMSConnectionInfo : ConfigConnectionInfo
    {
        private string Type { get; }
        private string Address { get; }
        private HSMSConnectionNetworkFamily AddressFamily { get; }
        private UInt16 Port { get; }
        private HSMSConnectionMode ConnectionMode { get; }

        /// <summary>
        /// Create a config file version of HSMS connection information.
        /// <b>Note:</b> If the addressFamily argument is not &quot;IPV6&quot;
        /// it will be set to IPV4.  In addition if the activeOrPassive argument
        /// is not a case insensitive version of &quot;Active&quot; the connectionMode
        /// attribute will be set to Passive.
        /// </summary>
        internal ConfigHSMSConnectionInfo(  string name,
                                            string type,
                                            string address,
                                            string addressFamily,
                                            UInt16 port,
                                            string activeOrPassive) : base( name )
        {
            Type = type;
            Address = address;

            if (activeOrPassive.Equals("IPV6", StringComparison.OrdinalIgnoreCase))
                AddressFamily = HSMSConnectionNetworkFamily.IPV6;
            else
                AddressFamily = HSMSConnectionNetworkFamily.IPV4;

            Port = port;

            if (activeOrPassive.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase))
                ConnectionMode = HSMSConnectionMode.Active;
            else
                ConnectionMode = HSMSConnectionMode.Passive;
        }

        public override string ToString()
        {
            return "name:" + Name + " Type:" + Type + " Address:" + Address + " AddressFamily:" + AddressFamily + " Port:" + Port + " Mode:" + ConnectionMode;
        }
    }
}