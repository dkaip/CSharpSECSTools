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
	/// A class to create a textual representation in XML of 
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.SECSMessage">SECSMessage</see>s,
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSCommUtils.SECSHeader">SECSHeader</see>s, and
    /// <see cref="com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem">SECSItem</see>s.
	/// </summary>
    public class XMLFormatter : SECSFormatter
    {
        /// <summary>
        /// Create an <c>XMLFormatter</c>.
        /// </summary>
        /// <param name="configurationData">
        /// A
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.TextFormatterConfig">TextFormatterConfig</see>
        /// object.
        /// </param>
        /// <remarks>
        /// </remarks>

        internal XMLFormatter(TextFormatterConfig configurationData) : base(configurationData) {}

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
                ((HSMSHeader)hdr).SType != 0)
            {
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
            } // End if it is an HSMS Control Message

            // If we made it here this is a normal SECS-II message
            if (configurationData.AddTimestamp || configurationData.AddDirection)
            {
                if (configurationData.AddTimestamp && configurationData.AddDirection)
                {
                    sb.Append("<SECSMessage Timestamp=\"");
                    sb.Append(DateTime.Now.ToString(configurationData.TimestampFormat));
                    sb.Append("\"");

                    sb.Append(" Src=\"");
                    sb.Append(source);
                    sb.Append("\" Dest=\"");
                    sb.Append(destination);
                    sb.AppendLine("\">");
                }
                else if (configurationData.AddTimestamp)
                {
                    sb.Append("<SECSMessage Timestamp=\"");
                    sb.Append(DateTime.Now.ToString(configurationData.TimestampFormat));
                    sb.AppendLine("\">");
                }
                else if (configurationData.AddDirection)
                {
                    sb.Append("<SECSMessage");
                    sb.Append(" Src=\"");
                    sb.Append(source);
                    sb.Append("\" Dest=\"");
                    sb.Append(destination);
                    sb.AppendLine("\">");
                }
            }
            else
            {
                sb.AppendLine("<SECSMessage>");
            }

            CurrentIndentLevel += configurationData.IndentAmount;

            GetHeaderAsText(sb, secsMessage.Header);

            if (secsMessage.IsHeaderOnly == false)
            {
                SECSItem? secsItem = SECSItemFactory.GenerateSECSItem(secsMessage.Body);
                GetSECSItemAsText(sb, secsItem);
            }

            CurrentIndentLevel -= configurationData.IndentAmount;

            sb.Append("</SECSMessage>");
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
                if (header.SType != 0)
                {
                    // This is an HSMS control message
                    return;
                }

                // This is a normal SECS-II message
                if (configurationData.HeaderOutputConfig.DisplayAsType == DisplayAsType.Elements)
                {
                    // output using elements

                    // output the header
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.AppendLine("<Header>");

                    CurrentIndentLevel += configurationData.IndentAmount;

                    if (configurationData.HeaderOutputConfig.DisplayDeviceId)
                    {
                        // Display the Session Id (DeviceId)
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<DeviceId>");
                        sb.Append(header.SessionID);
                        sb.AppendLine("</DeviceId>");
                    }

                    if (configurationData.HeaderOutputConfig.DisplayMessageIdAsSxFy == false)
                    {
                        // don't use SxFy format
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<Stream>");
                        sb.Append(header.Stream);
                        sb.AppendLine("</Stream>");

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<Function>");
                        sb.Append(header.Function);
                        sb.AppendLine("</Function>");
                    }
                    else
                    {
                        // use SxFy format
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<SxFy>");
                        sb.Append("S");
                        sb.Append(header.Stream);
                        sb.Append("F");
                        sb.Append(header.Function);
                        sb.AppendLine("</SxFy>");
                    }

                    if (configurationData.HeaderOutputConfig.DisplayWBit)
                    {
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<Wbit>");
                        sb.Append(header.Wbit);
                        sb.AppendLine("</Wbit>");
                    }

                    if (configurationData.HeaderOutputConfig.DisplaySystemBytes)
                    {
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.Append("<SystemBytes>");
                        sb.Append(header.SystemBytes);
                        sb.AppendLine("</SystemBytes>");
                    }

                    CurrentIndentLevel -= configurationData.IndentAmount;
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.AppendLine("</Header>");
                }
                else if (configurationData.HeaderOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                {
                    // output using attributes
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.Append("<Header");

                    if (configurationData.HeaderOutputConfig.DisplayDeviceId)
                    {
                        sb.Append(" DeviceId=\"");
                        sb.Append(header.SessionID);
                        sb.Append("\"");
                    }

                    if (configurationData.HeaderOutputConfig.DisplayMessageIdAsSxFy == false)
                    {
                        sb.Append(" Stream=\"");
                        sb.Append(header.Stream);
                        sb.Append("\"");

                        sb.Append(" Function=\"");
                        sb.Append(header.Function);
                        sb.Append("\"");
                    }
                    else
                    {
                        // use SxFy format
                        sb.Append(" SxFy=\"");
                        sb.Append("S");
                        sb.Append(header.Stream);
                        sb.Append("F");
                        sb.Append(header.Function);
                        sb.Append("\"");
                    }

                    if (configurationData.HeaderOutputConfig.DisplayWBit)
                    {
                        sb.Append(" Wbit=\"");
                        sb.Append(header.Wbit);
                        sb.Append("\"");
                    }

                    if (configurationData.HeaderOutputConfig.DisplaySystemBytes)
                    {
                        sb.Append(" SystemBytes=\"");
                        sb.Append(header.SystemBytes);
                        sb.Append("\"");
                    }
                    sb.AppendLine("/>");
                }
                else
                {
                    Console.Error.WriteLine("GetHeaderAsText:DisplayAsType of " + configurationData.HeaderOutputConfig.DisplayAsType.ToString() + " is unsupported.");
                    return;
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
            AddSECSItemsStuff(sb, secsItem.ItemFormatCode.ToString(), secsItem.NumberOfLengthBytes.ValueOf(), secsItem.LengthInBytes);

            if (secsItem.ItemFormatCode == SECSItemFormatCode.L)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                {
                    CurrentIndentLevel += configurationData.IndentAmount;
                }

                sb.Append(Whitespace[CurrentIndentLevel]);

                if (secsItem.LengthInBytes == 0)
                {
                    sb.AppendLine("<Value/>");
                }
                else
                {
                    sb.AppendLine("<Value>");

                    CurrentIndentLevel += configurationData.IndentAmount;

                    //Console.WriteLine("List count is {0}.", ((ListSECSItem)secsItem).Value.Count());
                    foreach (SECSItem listEntry in ((ListSECSItem)secsItem))
                    {
                        //                                Console.WriteLine("Current indent level = {0}.", CurrentIndentLevel);
                        GetSECSItemAsText(sb, listEntry);
                    }

                    CurrentIndentLevel -= configurationData.IndentAmount;

                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.AppendLine("</Value>");
                }
            } // End if List type
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.B)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);

                if (secsItem.LengthInBytes == 0)
                {
                    // Its an empty SECSItem
                    sb.AppendLine("<Value/>");
                }
                else if (secsItem.LengthInBytes == 1)
                {
                    // It has only one element.
                    sb.Append("<Value>");

                    sb.Append("0x");
                    sb.Append(((BinarySECSItem)secsItem).Value[0].ToString("X2"));

                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It has more than one element.
                    sb.AppendLine("<Value>");

                    CurrentIndentLevel += configurationData.IndentAmount;
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    int lineLength = CurrentIndentLevel;

                    int currentArrayElement = 0;
                    int arrayLength = ((BinarySECSItem)secsItem).Value.Count();
                    foreach (byte item in ((BinarySECSItem)secsItem).Value)
                    {
                        if (lineLength + 4 >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.AppendLine("");
                            sb.Append(Whitespace[CurrentIndentLevel]);
                            lineLength = CurrentIndentLevel;
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

                    sb.AppendLine("");

                    CurrentIndentLevel -= configurationData.IndentAmount;

                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.AppendLine("</Value>");
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.BO)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.LengthInBytes == 0)
                {
                    // Its an empty SECSItem
                    sb.AppendLine("<Value/>");
                }
                else if (secsItem.LengthInBytes == 1)
                {
                    // It is not an array.

                    sb.Append("<Value>");

                    if (((BooleanSECSItem)secsItem).Value == true)
                        sb.Append("True");
                    else
                        sb.Append("False");

                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array.

                    sb.AppendLine("<Value>");

                    CurrentIndentLevel += configurationData.IndentAmount;
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    int lineLength = CurrentIndentLevel;

                    int currentArrayElement = 0;
                    int arrayLength = ((BooleanArraySECSItem)secsItem).Value.Count();
                    foreach (bool item in ((BooleanArraySECSItem)secsItem).Value)
                    {
                        if (lineLength + (item ? 4 : 5) >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.AppendLine("");
                            sb.Append(Whitespace[CurrentIndentLevel]);
                            lineLength = CurrentIndentLevel;
                        }

                        if (item)
                        {
                            sb.Append("True");
                            lineLength += 4;
                        }
                        else
                        {
                            sb.Append("False");
                            lineLength += 5;
                        }

                        if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                        {
                            sb.Append(" ");
                            lineLength++;
                        }
                    }

                    sb.AppendLine("");

                    CurrentIndentLevel -= configurationData.IndentAmount;

                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.AppendLine("</Value>");
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.A)
            {
                // TODO deal with max line length, but, don't forget preserve formatting(whitespace)
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.LengthInBytes == 0)
                {
                    sb.AppendLine("<Value/>");
                }
                else
                {
                    sb.Append("<Value>");
                    sb.Append(((ASCIISECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.J8)
            {
                // TODO deal with max line length
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                sb.Append("<Value>");
                sb.Append("Not Implemented Yet");
                sb.AppendLine("</Value>");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.C2)
            {
                // TODO deal with max line length
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                sb.Append("<Value>");
                sb.Append("Not Implemented Yet");
                sb.AppendLine("</Value>");
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I8)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(I8SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((I8SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((I8ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((I8ArraySECSItem)secsItem).Value.Count();
                        foreach (Int64 item in ((I8ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength(item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I1)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(I1SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((I1SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 1)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((I1ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        // It is an array

                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((I1ArraySECSItem)secsItem).Value.Count();
                        foreach (sbyte item in ((I1ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength((Int64)item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I2)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(I2SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((I2SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 2)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((I2ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((I2ArraySECSItem)secsItem).Value.Count();
                        foreach (Int16 item in ((I2ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength((Int64)item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.I4)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(I4SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((I4SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 4)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((I4ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((I4ArraySECSItem)secsItem).Value.Count();
                        foreach (Int32 item in ((I4ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetSignedItemLength((Int64)item);

                            lineLength += itemLength;

                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.F8)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(F8SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((F8SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((F8ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

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
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(itemText);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.F4)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(F4SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((F4SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 4)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((F4ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

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
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(itemText);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U8)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(U8SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((U8SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((U8ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((U8ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt64 item in ((U8ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength((UInt64)item);

                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U1)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(U1SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((U1SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((U1ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((U1ArraySECSItem)secsItem).Value.Count();
                        foreach (byte item in ((U1ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength((UInt64)item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U2)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(U2SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((U2SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array
                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((U2ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((U2ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt16 item in ((U2ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength((UInt64)item);
                            if (lineLength + itemLength >= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else if (secsItem.ItemFormatCode == SECSItemFormatCode.U4)
            {
                if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
                    CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                
                if (secsItem.GetType() == typeof(U4SECSItem))
                {
                    // It is not an array.
                    sb.Append("<Value>");
                    sb.Append(((U4SECSItem)secsItem).Value);
                    sb.AppendLine("</Value>");
                }
                else
                {
                    // It is an array

                    if (secsItem.LengthInBytes == 0)
                    {
                        // Its an empty SECSItem
                        sb.AppendLine("<Value/>");
                    }
                    else if (secsItem.LengthInBytes == 8)
                    {
                        // It is an array, but, it has only one element.
                        sb.Append("<Value>");

                        sb.Append(((U4ArraySECSItem)secsItem).Value[0]);

                        sb.AppendLine("</Value>");
                    }
                    else
                    {
                        sb.AppendLine("<Value>");

                        CurrentIndentLevel += configurationData.IndentAmount;
                        sb.Append(Whitespace[CurrentIndentLevel]);
                        int lineLength = CurrentIndentLevel;

                        int currentArrayElement = 0;
                        int arrayLength = ((U4ArraySECSItem)secsItem).Value.Count();
                        foreach (UInt32 item in ((U4ArraySECSItem)secsItem).Value)
                        {
                            int itemLength = GetUnsignedItemLength((UInt64)item);
                            if (lineLength + itemLength>= configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.AppendLine("");
                                sb.Append(Whitespace[CurrentIndentLevel]);
                                lineLength = CurrentIndentLevel;
                            }

                            sb.Append(item);

                            lineLength += itemLength;

                            if (++currentArrayElement < arrayLength && lineLength < configurationData.BodyOutputConfig.MaxOutputLineLength)
                            {
                                sb.Append(" ");
                                lineLength++;
                            }
                        }

                        sb.AppendLine("");

                        CurrentIndentLevel -= configurationData.IndentAmount;

                        sb.Append(Whitespace[CurrentIndentLevel]);
                        sb.AppendLine("</Value>");
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("Unhandled format code of {0}.", secsItem.ItemFormatCode);
                return;
            }

            CurrentIndentLevel -= configurationData.IndentAmount;

            sb.Append(Whitespace[CurrentIndentLevel]);
            sb.AppendLine("</SECSItem>");

            //            CurrentIndentLevel -= IndentAmount;

            return;
        }

        private void AddSECSItemsStuff(StringBuilder sb, string formatCode, int numLengthBytes, int lengthByteValue)
        {
            if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Elements)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);
                sb.AppendLine("<SECSItem>");

                CurrentIndentLevel += configurationData.IndentAmount;

                sb.Append(Whitespace[CurrentIndentLevel]);
                sb.Append("<Type>");
                sb.Append(formatCode);
                sb.AppendLine("</Type>");

                if (configurationData.BodyOutputConfig.DisplayNumberOfLengthBytes)
                {
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.Append("<NumLengthBytes>");
                    sb.Append(numLengthBytes);
                    sb.AppendLine("</NumLengthBytes>");
                }

                if (configurationData.BodyOutputConfig.DisplayLengthByteValue)
                {
                    sb.Append(Whitespace[CurrentIndentLevel]);
                    sb.Append("<LengthByteValue>");
                    sb.Append(lengthByteValue);
                    sb.AppendLine("</LengthByteValue>");
                }
            }
            else if (configurationData.BodyOutputConfig.DisplayAsType == DisplayAsType.Attributes)
            {
                sb.Append(Whitespace[CurrentIndentLevel]);
                sb.Append("<SECSItem type=\"");
                sb.Append(formatCode);
                sb.Append("\"");

                if (configurationData.BodyOutputConfig.DisplayNumberOfLengthBytes)
                {
                    sb.Append(" NumLengthBytes=\"");
                    sb.Append(numLengthBytes);
                    sb.Append("\"");
                }

                if (configurationData.BodyOutputConfig.DisplayLengthByteValue)
                {
                    sb.Append(" LengthByteValue=\"");
                    sb.Append(lengthByteValue);
                    sb.Append("\"");
                }

                sb.AppendLine(">");
            }
            else
            {
                Console.Error.WriteLine("AddSECSItemsStuff:DisplayAsType of " + configurationData.BodyOutputConfig.DisplayAsType.ToString() + " is unsupported.");
                return;
            }
        }
    }
}