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
    public class HeaderOutputConfig
    {
        public DisplayAsType    DisplayAsType;
        public string   DisplayAsElementsOrAttributes
        {
            get { return DisplayAsType.ToString(); }
            set
            {
                if (string.Equals(value, "Elements", StringComparison.OrdinalIgnoreCase) == true)
                    DisplayAsType = DisplayAsType.Elements;
                else if (string.Equals(value, "Attributes", StringComparison.OrdinalIgnoreCase) == true)
                    DisplayAsType = DisplayAsType.Attributes;
                else
                {
                    throw new ArgumentException("DisplayAsElementsOrAttributes must be either \"Elements\" or \"Attributes\".");
                }
            }
        }
        public bool     DisplayMessageIdAsSxFy { get; set; }
        public bool     DisplayDeviceId { get; set; }
        public bool     DisplaySystemBytes { get; set; }
        public bool     DisplayWBit { get; set; }
        public bool     DisplayControlMessages { get; set; }

        public override string ToString()
        {
            return  "DisplayAsElementsOrAttributes:" + DisplayAsElementsOrAttributes +
                    " DisplayMessageIdAsSxFy:" + DisplayMessageIdAsSxFy +
                    " DisplayDeviceId:" + DisplayDeviceId +
                    " DisplaySystemBytes:" + DisplaySystemBytes +
                    " DisplayWBit:" + DisplayWBit +
                    " DisplayControlMessages:" + DisplayControlMessages;
        }
    }
}
