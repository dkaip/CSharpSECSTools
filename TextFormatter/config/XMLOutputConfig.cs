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

using System.Text;

namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
    /// <summary>
    /// This class contains the information for a formatter to use when producing
    /// output.
    /// </summary>
    public class XMLOutputConfig
    {
        /// <summary>
        /// This property contains the information a formatter needs to generate output
        /// when producing output for a <c>SECSHeader</c> class.
        /// </summary>
        public HeaderOutputConfig  HeaderOutputConfig { get; set; }

        /// <summary>
        /// This property contains the information a formatter needs to generate output
        /// when producing output for a <c>SECSItem</c> class.
        /// </summary>
        public BodyOutputConfig    BodyOutputConfig { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HeaderOutputConfig.ToString());
            sb.AppendLine(BodyOutputConfig.ToString());

            return sb.ToString();
        }
    }
}
