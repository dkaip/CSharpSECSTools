# SECSSpy

This utility program is used to log the SECS message traffic between two endpoints of a &quot;SECS connection&quot; to a terminal  or a file.

The output format of the resulting log file may be configured to be either [SECS Message Language(SML)](https://www.peergroup.com/resources/secs-message-language/) or [XML(Extensible Markup Language)](https://www.w3.org/standards/xml/core).  When the output format is selected there are a number of customization options that may be applied depending on the output format selected.  Refer to the [SML Output](#smloutput) or [XML Output](#xmloutput) sections of this document for more information on these options.

Why are there two output formats available?  SML has been around for many years now and is quite familiar to those who have been working in this part of this industry for a long time.  When looking at the log files with your own eyes it is very adequate.  However, it can be a real pain to write a parser for, hence the ability to write out log files in XML.  Why would you ever need to parse a log file?

There are a number of reasons why being able to easily parse a log file is a good and useful thing.

- Equipment Interface(EI) Development/Enhancment/Debugging - It can be a great aide in the development and or debugging cycle to be able to generate an equipment simulator.  With this simulator you are able to run your EI against a simulator that is simulating a genuine equipment operation cycle complete with all of the data events and their respective reports, perhaps megabytes worth of data.  I will mention here that parsing the log file and producing code segments for [sc_open](https://github.com/dkaip/sc_open) can be invaluable in EI development, maintenance, debugging, etc.
- Audit Trail - depending on your software integration / automation stack it may be much easier to generate audit trail information from log files generated from the communication between the MES software and the equipment than it is to generate the information from other sources.

There are plenty of additional uses for a log files that may be parsed easily by a program.

**Note:**

- At this time this program is only able to log SECS messages between two endpoints of an HSMS connection.
- The IP Address Family of the HSMS connection may be either IPV4 or IPV6. 

## Configuration File

The configuration file, unless specified on the command line, is `appsettings.json`.  This file contains several elements that are needed to configure `SECSSpy` for operation.  Following is documentation pertaining to various elements.

### &quot;ConfigurationPairs&quot;

This section contains an array of `ConfigurationPair` sections.  A configuration pair represents a &quot;SECS connection&quot; or link between two &quot;entities&quot; that are communicating to each other using the SECS protocol either using HSMS or SECS-I as the transport layer.

In the normal case only one `ConfigurationPair` section is configured.  This will allow the logging of the SECS-II traffic (and potentially HSMS control messages) between one &quot;entities&quot; and different &quot;entities&quot;.  This is not done via magic however, you will need &quot;point&quot; the two entities to `SECSSpy` and configure `SECSSpy` to point to the two entities.

An example may help, consider an HSMS connect/link betweem an EI and a piece of equipment.  You might configure the piece of equipment with a connection mode of `Passive` and listening on `Port` 50000.  In this case the EI would be configured to be `Active` and given port 50000 as the port it should attempt to connect to.  (Yes, the network address needs to be specified as well.)  In order to use `SECSSpy` to log the traffic between them you would configure your EI to use `Port` 50001 instead of `Port` 50000.  You would then configure one of the elements in the `ConfigurationPair` section `SECSSpy`'s configuration file to be `Passive` and listen on `Port` 50001 and the other element in the pair to be `Active` on port 50000.  This will effectively put `SECSSpy` in the middle and allow it to log all SECS-II traffic.

It is possible to have multiple `ConfigurationPair` sections within the `ConfigurationPairs` section in order to allow for the logging of the traffic on multiple connections/links to the same log file.  This is not how `SECSSpy` would typically be used.   However, it might prove valuable in the case of a Litho cluster.  In this situation the messages are NOT logged in the order they are received, although it will probably be close enough.  Each message is received in its own thread.  Once it is passed on to its destination it will be dumped onto the FIFO queue that feeds the log output &quot;engine&quot; and from there it will be in FIFO order.

### &quot;ConfigurationPair&quot;

This section is a `json` array that contains one entry for each end of a SECS connection.  There may only be two entries in this array.

### &quot;LoggingOutputFormat&quot;

This instructs `SECSSpy` to product logging output in either SML or XML.  Valid values for this element are &quotSML&quot; or
&quot;XML&quot;.

## &quot;SMLOutput&quot;

### SML Header Output Format Options

### SML Body Output Format Options

## &quot;XMLOutput&quot;

When outputting the log information in the XML format be warned that an XML document is **NOT** produced.  Only well-formed XML is output.  If you want to be able to parse the XML with any of the standard XML parser APIs available you will need to create an XML document.  Doing this is an easy task.  You must add an XML declaration to your file in addition to a root element of your choice.  Don't forget to close the root element.

```C#
<?xml version="1.0"?>
<MyLogFileRootElement>
```

Paste the contents of the log file you are interested in parsing here.

```C#
</MyLogFileRootElement>
```

Make sure when you are doing your copying and pasting that you &quot;grab&quot; complete XML element(s).

When XML output is chosen there are some customization that may be applied.

### XML Header Output Format Options

|Header Config Element  | Description|
| --- | --- |
|DisplayAsElementsOrAttributes| `Elements` or `Attributes` Display the individual header parts using XML elements or as XML Attributes in a single XML element|
|DisplayMessageIdAsSxFy| `true` if you want the message Id displayed like `SxFy` false if otherwise|
|DisplayDeviceId| `true` if you want the device Id displayed, `false` if otherwise|
|DisplaySystemBytes| `true` if you want the System Bytes displayed, `false` if otherwise|
|DisplayWBit|`true` if you want the W-bit displayed, `false` if otherwise|
|DisplayControlMessages|`true` if you want control messages displayed, `false` if otherwise|

Example output with `DisplayAsElementsOrAttributes` set to `Elements`

```C#
<SECSMessage>
  <Direction>
    <Source>MES</Source>
    <Destination>Equipment</Destination>
  </Direction>
  <Header>
    <DeviceId>0</DeviceId>

    <Stream>1</Stream>
    <Function>1</Function>
            or
    <SxFy>S1F1</SxFy>

    <WBit>1</WBit>
    <SystemBytes>40127</SystemBytes>
  </Header>
  <Body>
  </Body>
     or 
  <Body/>
</SECSMessage>
```

Example output with `DisplayAsElementsOrAttributes` set to `Attributes`

```C#
<SECSMessage>
  <Direction src="MES" Dest="Equipment"/>

  <Header DeviceId="0" Stream="1" Function="1" WBit="1" SystemBytes="40127"/>
                             or
  <Header DeviceId="0" SxFy="S1F1" WBit="1" SystemBytes="40127"/>
  <Body>
  </Body>
</SECSMessage>
```

### XML Body Output Format Options

Example body

```C#
<SECSItem>
  <Type>U4</Type>
  <NumberOfLengthBytes>1<NumberOfLengthBytes>
  <LengthByteValue>4</LengthByteValue>
  <Value>1234567</Value>
</SECSItem>
```

example 2

```XML
<SECSItem>
  <Type>L</Type>
  <NumberOfLengthBytes>1<NumberOfLengthBytes>
  <LengthByteValue>3</LengthByteValue>
  <SECSItems>
    <SECSItem>
      <Type>U4</Type>
      <NumberOfLengthBytes>1<NumberOfLengthBytes>
      <LengthByteValue>4</LengthByteValue>
      <Value>1234567</Value>
    </SECSItem>
    <SECSItem>
      <Type>U2</Type>
      <NumberOfLengthBytes>1<NumberOfLengthBytes>
      <LengthByteValue>6</LengthByteValue>
      <Value>65535 32767 0</Value>
    </SECSItem>
    <SECSItem>
      <Type>I1</Type>
      <NumberOfLengthBytes>1<NumberOfLengthBytes>
      <LengthByteValue>1</LengthByteValue>
      <Value>-128</Value>
    </SECSItem>
  </SECSItems>
</SECSItem>
```
