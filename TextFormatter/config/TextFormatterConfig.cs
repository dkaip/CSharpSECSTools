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
    public class TextFormatterConfig
    {
        public bool         AddTimestamp { get; set; } = true;
        public string       TimestampFormat { get; set; } = "yyyy-MM-ddTHH:mm:ss.fff";
        public bool         AddDirection { get; set; } = true;
        public int          IndentAmount { get; set; } = 2;
        private string      _loggingOutputFormat = "XML";
        public string       LoggingOutputFormat
        {
            get { return _loggingOutputFormat; }
            set
            {
                if (string.Compare(value, "XML", true) == 0 || string.Compare(value, "SML", true) == 0)
                {
                    _loggingOutputFormat = value.ToUpper();
                }
                else
                {
                    throw new ArgumentException("LoggingOutputFormat may only have a value of either \"SML\" or \"XML\"");
                }
            }
        }

        public SMLOutputConfig SMLOutputConfig { get; set; }
        public XMLOutputConfig XMLOutputConfig { get; set; }

        public HeaderOutputConfig HeaderOutputConfig
        {
            get
            {
                if (string.Equals(LoggingOutputFormat, "SML"))
                {
                    return SMLOutputConfig.HeaderOutputConfig;
                }
                else
                {
                    return XMLOutputConfig.HeaderOutputConfig;
                }
            }
        }
        public BodyOutputConfig   BodyOutputConfig
        {
            get
            {
                if (string.Equals(LoggingOutputFormat, "SML"))
                {
                    return SMLOutputConfig.BodyOutputConfig;
                }
                else
                {
                    return XMLOutputConfig.BodyOutputConfig;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("AddTimestamp:");
            sb.AppendLine(AddTimestamp.ToString());

            sb.Append("TimestampFormat:");
            sb.AppendLine(TimestampFormat);

            sb.Append("AddDirection:");
            sb.AppendLine(AddDirection.ToString());

            sb.Append("LoggingOutputFormat:");
            sb.AppendLine(LoggingOutputFormat);

            sb.AppendLine("SMLOutputConfig:");
            sb.AppendLine(SMLOutputConfig.ToString());

            sb.AppendLine("XMLOutputConfig:");
            sb.AppendLine(XMLOutputConfig.ToString());

            return sb.ToString();
        }
    }
}
