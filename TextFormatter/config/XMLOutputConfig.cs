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
    public class XMLOutputConfig
    {
        public HeaderOutputConfig  HeaderOutputConfig { get; set; }
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
