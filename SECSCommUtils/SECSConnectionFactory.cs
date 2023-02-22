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
 * See the License for the specific language governing private permissions and
 * limitations under the License.
 */

using System.Collections.Concurrent;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
    public static class SECSConnectionFactory
    {
        /// <summary>
        /// Create an appropriate <c>SECSConnection</c> based on the information
        /// contained in the <c>configuration</c> argument.
        /// </summary>
        /// <returns>
        /// Either an 
        /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.HSMSConnection">HSMSConnection</see>
        /// or a
        /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.SECSIConnection">SECSIConnection</see>
        /// depending
        /// on the information contained in the <c>configuration</c> argument.
        /// </returns>
        /// <param name="configuration">
        /// The <c>SECSConnectionConfigInfo</c> object the contains the information
        /// necessary to configure the created <c>SECSConnection</c>.
        /// </param>
        public static SECSConnection CreateConnection(SECSConnectionConfigInfo configuration)
        {
            if (String.Equals(configuration.Type, "HSMS"))
            {
                return new HSMSConnection(configuration);
            }

            return new SECSIConnection(configuration);
        }
    }
}