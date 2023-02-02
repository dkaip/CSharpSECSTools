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


namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
    /// <summary>
    /// A factory class used to create a <c>SECSFormatter</c> of the appropriate
    /// type.
    /// </summary>
    public static class SECSFormatterFactory
    {
        /// <summary>
        /// Create an appropriate <c>SECSFormatter</c> based on the information
        /// contained in the <c>configuration</c> argument.
        /// </summary>
        /// <returns>
        /// Either an 
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.SMLFormatter">SMLFormatter</see>
        /// or an
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.XMLFormatter">XMLFormatter</see>
        /// depending
        /// on the information contained in the <c>configuration</c> argument.
        /// </returns>
        /// <param name="configuration">
        /// The <c>TextFormatterConfig</c> object the contains the information
        /// necessary to configure the created <c>SECSFormatter</c>.
        /// </param>
        public static SECSFormatter CreateFormatter(TextFormatterConfig configuration)
        {
            if (String.Equals(configuration.LoggingOutputFormat, "SML"))
            {
                return new SMLFormatter(configuration);
            }

            return new XMLFormatter(configuration);
        }
    }
}
