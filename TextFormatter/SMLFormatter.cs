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

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;
using com.CIMthetics.CSharpSECSTools.SECSItems;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
	/// <summary>
	/// A class to create a textual representation in SML of 
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.SECSMessage">SECSMessage</see>s,
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.SECSHeader">SECSHeader</see>s, and
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem">SECSItem</see>s.
	/// </summary>
    public class SMLFormatter : SECSFormatter
    {
        internal SMLFormatter(TextFormatterConfig configurationData) : base(configurationData) {}

        /// <inheritdoc/>
        public override string GetSECSMessageAsText(string source, string destination, SECSMessage secsMessage)
        {
            StringBuilder sb = new StringBuilder();

            GetSECSMessageAsText(sb, source, destination, secsMessage);

            return sb.ToString();
        }

        /// <inheritdoc/>
        public override void GetSECSMessageAsText(StringBuilder sb, string source, string destination, SECSMessage secsMessage)
        {
            SECSHeader hdr = secsMessage.Header;
            if (hdr.GetType() == typeof(HSMSHeader) &&
                ((HSMSHeader)hdr).PType != 0)
            {
                // This is an HSMS Control message

                // TODO figure out what to do with control messages for SML
                HSMSHeader header = (HSMSHeader)hdr;
                // This is an HSMS control message
                if (configurationData.HeaderOutputConfig.DisplayControlMessages == false)
                {
                    // We are not interested in these so just return...
                    // sb should be empty still.
                    return;
                }

                if (configurationData.AddTimestamp)
                {
                    // Add timestamp if requested
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.Append("<HSMSControlMessage Timestamp=\"");
                    sb.Append(DateTime.Now.ToString(configurationData.TimestampFormat));
                    sb.Append("\"");
                }
                else
                {
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.Append("<HSMSControlMessage");
                }

                sb.Append(" Src=\"");
                sb.Append(source);
                sb.Append("\" Dest=\"");
                sb.Append(destination);
                sb.Append("\"");

                if (configurationData.HeaderOutputConfig.DisplayDeviceId)
                {
                    // Display the Session Id (DeviceId)
                    sb.Append(" DeviceId=\"");
                    sb.Append(header.SessionID);
                    sb.Append("\"");
                }

                sb.Append(" HeaderByte2=\"");
                sb.Append(header.HeaderByte2);
                sb.Append("\"");

                sb.Append(" HeaderByte3=\"");
                sb.Append(header.HeaderByte3);
                sb.Append("\"");

                sb.Append(" PType=\"");
                sb.Append(((PTypeValues)header.PType).ToString());
                sb.Append("\"");

                sb.Append(" SType=\"");
                sb.Append(((STypeValues)header.SType).ToString());
                sb.Append("\"");

                if (configurationData.HeaderOutputConfig.DisplaySystemBytes)
                {
                    sb.Append(" SystemBytes=\"");
                    sb.Append(header.SystemBytes);
                    sb.Append("\"");
                }

                sb.AppendLine("/>");

                // We finished with the control message so just return it.
                return;
            }

            // If we made it here this is a normal SECS-II message
            if (configurationData.AddTimestamp)
            {
                // Add timestamp if requested
                sb.Append("#Timestamp:");
                sb.AppendLine(DateTime.Now.ToString(configurationData.TimestampFormat));
            }

            if (configurationData.AddDirection)
            {
                // Add timestamp if requested
                sb.Append("#Direction Src:");
                sb.Append(source);
                sb.Append(" Dest:");
                sb.AppendLine(destination);
            }

            GetHeaderAsText(sb, secsMessage.Header);

            bool headerOnly = false;
            if (headerOnly == false)
            {
                sb.AppendLine("");
                GetSECSItemAsText(sb, secsMessage.GetBodyAsSECSItem());
            }

            sb.Append(".");

            return;
        }
        
        /// <inheritdoc/>
        public override string GetHeaderAsText(SECSHeader secsHeader)
        {
            StringBuilder sb = new StringBuilder();

            GetHeaderAsText(sb, secsHeader);

            return sb.ToString();
        }

        /// <inheritdoc/>
        public override void GetHeaderAsText(StringBuilder sb, SECSHeader secsHeader)
        {
            SECSHeader hdr = secsHeader;
            if (hdr.GetType() == typeof(HSMSHeader))
            {
                HSMSHeader header = (HSMSHeader)hdr;
                if (header.PType != 0)
                {
                    // This is an HSMS control message
                    return;
                }

                sb.Append("S");
                sb.Append(header.Stream);
                sb.Append("F");
                sb.Append(header.Function);

                if (configurationData.HeaderOutputConfig.DisplayWBit)
                {
                    if (header.Wbit)
                    {
                        sb.Append(" W");
                    }
                }
            }
            else
            {
                // It must be a SECS-I header
                Console.Error.WriteLine("SECS-I header not implemented yet.");
            }

            return;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// JIS-8 and C2 are not implemented yet.
        /// </remarks>
        public override string GetSECSItemAsText(SECSItem secsItem)
        {
            StringBuilder sb = new StringBuilder();

            GetSECSItemAsText(sb, secsItem);

            return sb.ToString();
        }

        /// <inheritdoc/>
        /// <remarks>
        /// JIS-8 and C2 are not implemented yet.
        /// </remarks>
        public override void GetSECSItemAsText(StringBuilder sb, SECSItem secsItem)
        {
            if (secsItem.ItemFormatCode == SECSItemFormatCode.L)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                if (configurationData.BodyOutputConfig.DisplayCount)
                {
                    sb.Append("<L [");
                    sb.Append(((ListSECSItem)secsItem).Value.Count());
                    if (((ListSECSItem)secsItem).Value.Count() == 0)
                    {
                        sb.Append("]>");
                        return;
                    }

                    sb.AppendLine("]");
                }
                else
                {
                    if (((ListSECSItem)secsItem).Value.Count() == 0)
                    {
                        sb.Append("<L>");
                        return;
                    }

                    sb.AppendLine("<L");
                }

                CurrentIndentLevel += configurationData.IndentAmount;

                int arrayLength = ((ListSECSItem)secsItem).Value.Count();
                foreach (SECSItem listEntry in ((ListSECSItem)secsItem).Value)
                {
                    GetSECSItemAsText(sb, listEntry);

                    sb.AppendLine("");
                }

                CurrentIndentLevel -= configurationData.IndentAmount;
                sb.Append(Whitespace[CurrentIndentLevel]);
                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.B)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "B", secsItem.LengthInBytes, out offsetToData);
                if (secsItem.LengthInBytes == 0)
                {
                    ;
                }
                else if (secsItem.LengthInBytes == 1)
                {
                    sb.Append("0x");
                    sb.Append((((BinarySECSItem)secsItem).Value[0]).ToString("X2"));
                }
                else
                {
                    // It is an array.

                    int lineLength = CurrentIndentLevel + offsetToData;

                    int currentArrayElement = 0;
                    int arrayLength = ((BinarySECSItem)secsItem).Value.Count();
                    foreach (byte item in ((BinarySECSItem)secsItem).Value)
                    {
                        if (lineLength + 4 >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.AppendLine("");
                            sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                            lineLength = CurrentIndentLevel + offsetToData;
                        }

                        sb.Append("0x");
                        sb.Append(item.ToString("X2"));
                        lineLength += 4;

                        if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.Append(" ");
                            lineLength++;
                        }
                    }

                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.BO)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "BOOLEAN", secsItem.LengthInBytes, out offsetToData);
                if (secsItem.LengthInBytes == 0)
                {
                   ;
                }
                else if (secsItem.LengthInBytes == 1)
                {
                    if (((BooleanSECSItem)secsItem).Value == true)
                        sb.Append("T");
                    else
                        sb.Append("F");
                }
                else
                {
                    // It is an array.

                    int lineLength = CurrentIndentLevel + offsetToData;

                    int currentArrayElement = 0;
                    int arrayLength = ((BooleanArraySECSItem)secsItem).Value.Count();
                    foreach (bool item in ((BooleanArraySECSItem)secsItem).Value)
                    {
                        if (lineLength + 1 >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.AppendLine("");
                            sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                            lineLength = CurrentIndentLevel + offsetToData;
                        }

                        if (item)
                        {
                            sb.Append("T");
                            lineLength++;
                        }
                        else
                        {
                            sb.Append("F");
                            lineLength++;
                        }

                        if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.Append(" ");
                            lineLength++;
                        }
                    }

                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.A)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                if (secsItem.LengthInBytes == 0)
                {
                    if (configurationData.BodyOutputConfig.DisplayCount)
                        sb.Append("<A [0]>");
                    else
                        sb.Append("<A>");
                }
                else
                {
                    int offsetToData;
                    AddPreText(sb, "A", secsItem.LengthInBytes, out offsetToData);

                    int lineLength = CurrentIndentLevel + offsetToData;

                    bool withinQuotes = false;
                    int arrayLength = ((ASCIISECSItem)secsItem).Value.Length;
                    foreach (Char item in ((ASCIISECSItem)secsItem).Value)
                    {
                        int itemLength;
                        if (Char.IsLetterOrDigit(item) || Char.IsPunctuation(item) || Char.IsSymbol(item) || (item==' '))
                        {
                            itemLength = 1;
                        }
                        else
                        {
                            itemLength = 8;
                        }

                        if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.AppendLine("\"");
                            sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                            lineLength = CurrentIndentLevel + offsetToData;
                            withinQuotes = false;
                        }

                        if (itemLength == 1)
                        {
                            if (withinQuotes == false)
                            {
                                sb.Append("\"");
                                withinQuotes = true;
                            }
                            sb.Append(item);
                        }
                        else
                        {
                            byte value = Convert.ToByte(item);

                            if (withinQuotes)
                            {
                                sb.Append("\" ");
                                withinQuotes = false;
                            }
                            else
                                sb.Append(" ");
                            sb.Append("0x");
                            sb.Append(value.ToString("X2"));
                            sb.Append(" ");
                        }

                        lineLength += itemLength;
                    }

                    sb.Append("\">");
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.J8)
            {
                sb.Append("<J \"Not Implemented Yet\">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.C2)
            {
                sb.Append("<C \"Not Implemented Yet\">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I8)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "I8", secsItem.LengthInBytes/sizeof(Int64), out offsetToData);
                if (secsItem.GetType() == typeof(I8SECSItem))
                {
                    sb.Append(((I8SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((I8ArraySECSItem)secsItem).Value.Count();
                        foreach (Int64 item in ((I8ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I1)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "I1", secsItem.LengthInBytes/sizeof(sbyte), out offsetToData);
                if (secsItem.GetType() == typeof(I1SECSItem))
                {
                    sb.Append(((I1SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((I1ArraySECSItem)secsItem).Value.Count();
                        foreach (sbyte item in ((I1ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I2)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "I2", secsItem.LengthInBytes/sizeof(Int16), out offsetToData);
                if (secsItem.GetType() == typeof(I2SECSItem))
                {
                    sb.Append(((I2SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((I2ArraySECSItem)secsItem).Value.Count();
                        foreach (Int16 item in ((I2ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I4)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "I4", secsItem.LengthInBytes/sizeof(Int32), out offsetToData);
                if (secsItem.GetType() == typeof(I4SECSItem))
                {
                    sb.Append(((I4SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((I4ArraySECSItem)secsItem).Value.Count();
                        foreach (Int32 item in ((I4ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.F8)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "F8", secsItem.LengthInBytes/sizeof(double), out offsetToData);
                if (secsItem.GetType() == typeof(F8SECSItem))
                {
                    sb.Append(((F8SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((F8ArraySECSItem)secsItem).Value.Count();
                        foreach (double item in ((F8ArraySECSItem)secsItem).Value)
                        {
                            /*
                                This is not a good way to get the length of the item
                                in text form.  It will produce a lot more garbage
                                than the method used for the integer types.  However,
                                At this point I cannot think of a better way to do 
                                it without spending a lot of time on it.
                            */
                            string itemText = item.ToString();
                            int itemLength = itemText.Length;

                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(itemText);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.F4)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "F4", secsItem.LengthInBytes/sizeof(float), out offsetToData);
                if (secsItem.GetType() == typeof(F4SECSItem))
                {
                    sb.Append(((F4SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((F4ArraySECSItem)secsItem).Value.Count();
                        foreach (float item in ((F4ArraySECSItem)secsItem).Value)
                        {
                            /*
                                This is not a good way to get the length of the item
                                in text form.  It will produce a lot more garbage
                                than the method used for the integer types.  However,
                                At this point I cannot think of a better way to do 
                                it without spending a lot of time on it.
                            */
                            string itemText = item.ToString();
                            int itemLength = itemText.Length;

                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(itemText);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }
                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U8)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "U8", secsItem.LengthInBytes/sizeof(UInt64), out offsetToData);
                if (secsItem.GetType() == typeof(U8SECSItem))
                {
                    sb.Append(((U8SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((U8ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt64 item in ((U8ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U1)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "U1", secsItem.LengthInBytes/sizeof(byte), out offsetToData);
                if (secsItem.GetType() == typeof(U1SECSItem))
                {
                    sb.Append(((U1SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((U1ArraySECSItem)secsItem).Value.Count();
                        foreach (byte item in ((U1ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U2)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "U2", secsItem.LengthInBytes/sizeof(UInt16), out offsetToData);
                if (secsItem.GetType() == typeof(U2SECSItem))
                {
                    sb.Append(((U2SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((U2ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt16 item in ((U2ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U4)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);

                int offsetToData;
                AddPreText(sb, "U4", secsItem.LengthInBytes/sizeof(UInt32), out offsetToData);
                if (secsItem.GetType() == typeof(U4SECSItem))
                {
                    sb.Append(((U4SECSItem)secsItem).Value);
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        ;
                    }
                    else
                    {
                        // It is an array.

                        int lineLength = CurrentIndentLevel + offsetToData;

                        int currentArrayElement = 0;
                        int arrayLength = ((U4ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt32 item in ((U4ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel + offsetToData]);
                                lineLength = CurrentIndentLevel + offsetToData;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                    }
                }

                sb.Append(">");
            }
            else
            {
                Console.Error.WriteLine("Unhandled format code of {0}.", secsItem.ItemFormatCode);
                return;
            }
        }

        private void AddPreText(StringBuilder sb, string elementType, int elementCount, out int outputLength)
        {
            StringBuilder tempSb = new StringBuilder();

            if (configurationData.BodyOutputConfig.DisplayCount)
            {
                if (elementCount == 0)
                {
                    // No ending space if count is 0
                    tempSb.Append("<");
                    tempSb.Append(elementType);
                    tempSb.Append(" [0]");
                }
                else
                {
                    tempSb.Append("<");
                    tempSb.Append(elementType);
                    tempSb.Append(" [");
                    tempSb.Append(elementCount);
                    tempSb.Append("] ");
                }

                sb.Append(tempSb.ToString());
                outputLength = tempSb.Length;
            }
            else
            {
                if (elementCount == 0)
                {
                    // No ending space if count is 0
                    tempSb.Append("<");
                    tempSb.Append(elementType);
                }
                else
                {
                    tempSb.Append("<");
                    tempSb.Append(elementType);
                    tempSb.Append(" ");
                }

                sb.Append(tempSb.ToString());
                outputLength = tempSb.Length;
            }
        }
     }
}