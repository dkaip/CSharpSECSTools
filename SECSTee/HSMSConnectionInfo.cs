/*
 * Copyright 2022 Douglas Kaip
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

using System.Text;
using com.CIMthetics.CSharpSECSTools.SECSCommUtils;

namespace com.CIMthetics.CSharpSECSTools.SECSTee
{
    internal class HSMSConnectionInfo : ConnectionInfo
    {
        internal NetworkFamily      NetworkFamily { get; }
        internal string             IPAddress { get; }
        internal UInt16             Port { get; }
        internal HSMSConnectionMode ConnectionMode { get; }

        internal HSMSConnectionInfo(NetworkFamily networkFamily, string address,  UInt16 port, HSMSConnectionMode connectionMode)
        {
            this.NetworkFamily = networkFamily;
            this.IPAddress = address;
            this.Port = port;
            this.ConnectionMode = connectionMode;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ConnectionConfig" + Environment.NewLine);
            sb.Append("    NetworkFamily:" + NetworkFamily.ToString() + Environment.NewLine);
            sb.Append("    Address:" + IPAddress + Environment.NewLine);
            sb.Append("    Port:" + Port + Environment.NewLine);
            sb.Append("    ConnetionMode:" + ConnectionMode.ToString() + Environment.NewLine);

            return sb.ToString();
        }
    }
}