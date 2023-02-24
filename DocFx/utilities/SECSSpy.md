# SECSSpy

This utility program is used to log the SECS message traffic between two endpoints of a &quot;SECS connection&quot;.  Since this utility
uses `Serilog` the log output may be configured to end up on a terminal, in a file, some other data sink, or some combination thereof.

The output format of the resulting log file may be configured to be either [SECS Message Language(SML)](https://www.peergroup.com/resources/secs-message-language/) or [XML(Extensible Markup Language)](https://www.w3.org/standards/xml/core).  When the output format is selected there are a number of customization options that may be applied depending on the output format selected.  Refer to the [`SMLOutputConfig`](#smloutputconfig) or [`XMLOutputConfig`](#xmloutputconfig) sections of this document for more information on these options.

Why are there two output formats available?  SML has been around for many years now and is quite familiar to those who have been working in this part of this industry for a long time.  When looking at the log files with your own eyes it is very adequate.  However, it can be a real pain to write a parser for, hence the ability to write out log files in XML.  Why would you ever need to parse a log file?

There are a number of reasons why being able to easily parse a log file is a good and useful thing. Here are two of them:

- Equipment Interface(EI) Development/Enhancement/Debugging - It can be a great aide in the development and or debugging cycle to be able to generate an equipment simulator.  With this simulator you are able to run your EI against a simulator that is simulating a genuine equipment operation cycle complete with all of the data events and their respective reports, perhaps megabytes worth of data.  I will mention here that parsing the log file and producing code segments for [sc_open](https://github.com/dkaip/sc_open) can be invaluable in EI development, maintenance, debugging, etc.
- Audit Trail - depending on your software integration / automation stack it may be much easier to generate audit trail information from log files generated from the communication between the MES software and the equipment than it is to generate the information from other sources.

There are plenty of additional uses for a log files that may be parsed easily by a program.

**Note:**

- At this time this program is only able to log SECS messages between two endpoints of an HSMS connection.
- The IP Address Family of the HSMS connection may be either IPV4 or IPV6.

## Configuring SECSSpy

`SECSSpy` is a command line application that requires a configuration file.
The configuration file, unless specified on the command line, will be `appsettings.json`.  This file contains a number of `json` elements that are needed to configure `SECSSpy` for operation.  Following is documentation pertaining to the various elements or sections of the required configuration file.

### &quot;Serilog&quot;

`SECSSpy` uses the `Serilog` logging library API.  This section is used to provide configuration information
for `Serilog`.  For more information, please refer to the widely available documentation for using and configuring `Serilog`.

One item of note though, the configuration item `MinimumLevel` should normally be set to a value of &quot;Information&quot;.  In this case the logging output should
pretty much just contain the log output of the SECS-II messages passed between the endpoints of a connection.  If `MinimumLevel` is set to &quot;Debug&quot; or
&quot;Verbose&quot; the log output will be polluted with a lot of lower level debug information that will just clutter up the output.

### &quot;ConfigurationPairs&quot;

This section contains an array of `ConfigurationPair` sections.  A configuration pair represents a &quot;SECS connection&quot; or link between two &quot;entities&quot; that are communicating to each other using the SECS protocol either using HSMS as the transport layer.

In the normal case only one `ConfigurationPair` section is configured.  This will allow the logging of the SECS-II traffic (and optionally HSMS control messages) between on endpoint of a connection and the other endpoint.  This is not accomplished magically however, you will need &quot;point&quot; the two endpoint to `SECSSpy` and configure `SECSSpy` to point to the two endpoints.

An example may help, consider an HSMS connect/link between an EI and a piece of equipment.  You might configure the piece of equipment with a connection mode of `Passive` and listening on `Port` 50000.  In this case the EI would be configured to be `Active` and given port 50000 as the port it should attempt to connect to.  (Yes, the network address needs to be specified as well.)  In order to use `SECSSpy` to log the traffic between them you would configure your EI to use `Port` 50001 instead of `Port` 50000.  You would then configure one of the endpoints in the `ConfigurationPair` section of `SECSSpy`'s configuration file to be `Passive` and listen on `Port` 50001 and the other endpoint in the pair to be `Active` on port 50000.  This will effectively put `SECSSpy` in the middle and allow it to log all SECS-II traffic between the two endpoints.

It is possible to have multiple `ConfigurationPair` sections within the `ConfigurationPairs` section in order to allow for the logging of the traffic on multiple connections to the same log file.  This is not how `SECSSpy` would typically be used.   However, it might prove valuable in the case of something like a Litho cluster.  In this situation the messages are NOT logged in the order they are received, although it will probably be close enough.  Each message is received in its own thread.  Once it is passed on to its destination it will be dumped onto the FIFO queue that feeds the log output &quot;engine&quot; and from there it will be in FIFO order.

### &quot;ConfigurationPair&quot;

This section is a `json` array that contains one entry for each end of a SECS connection.  There may only be two `SECSConnectionConfigInfo` entries in this array.

### &quot;SECSConnectionConfigInfo&quot;

This section contains the configuration information required for each endpoint in the HSMS connection.

| Item Name | Description |
| -------------- | --------------- |
| Name | This is the &quot;name&quot; of the connection endpoint.  This name will appear in the output logging to aid in documenting the message direction. |
| Type | This is the type of connection to be used.  The value should be &quot;HSMS&quot; since &quot;SECSI&quot; is not supported at this time. |
| Address | This is the IP Address or host name to be used for the connection. |
| AddressFamily | For an HSMS connection this value must be either &quot;IPV4&quot; or &quot;IPV6&quot;. |
| Port | This is the IP Port to use for the connection. |
| ConnectionMode | This is the connection mode to be used for the connection.  There are only two valid values, they are &quot;active&quot; and &quot;passive&quot;. |

### &quot;TextFormatterConfig&quot;

This section contains the information for configuring the formatted output of the SECS-II messages
that are to be logged.

| Item Name | Description |
| --------- | ----------- |
| AddTimestamp | Controls whether or not a timestamp is included in the logging output. Valid values are `true` or `false`.
| TimestampFormat | This is a format string that controls the format of the timestamp that is output, if one is desired.  Refer to documentation on `C#` time and date format strings for more information. |
| AddDirection | Controls whether or not &quot;from/to&quot; information for the messages are displayed. |
| IndentAmount | This is the number of spaces to indent items when indention is necessary. |
| MaxIndentionSpaces | This is the maximum number of spaces available for indentation.  With an `IndentAmount` of 2 this allows for 25 levels of indentation which should be way more than enough. In general 10 levels of indentation should be more than enough. |
| LoggingOutputFormat | Controls the format of the logging output.  Its value should be &quot;SML&quot; or &quot;XML&quot;. |
| [SMLOutputConfig](#smloutputconfig) | If `LoggingOutputFormat` has a value of &quot;[SML](#smloutputconfig)&quot; this section's configuration information will apply to the output produced. |
| [XMLOutputConfig](#xmloutputconfig) | If `LoggingOutputFormat` has a value of &quot;[XML](#xmloutputconfig)&quot; this section's configuration information will apply to the output produced. |

### &quot;SMLOutputConfig&quot;

This section of the configuration file contains information that will be used to determine what the resulting
output will look like when the SECS-II messages are display in an SML format.

#### SML &quot;HeaderOutputConfig&quot;

This section of the configuration file contains items that have an effect on how the `HSMSHeader` is formatted for output.

| Item Name | Description |
| --- | --- |
|DisplayDeviceId| `true` if you want the device Id displayed, `false` if otherwise|
|DisplaySystemBytes| `true` if you want the System Bytes displayed, `false` if otherwise|
|DisplayWBit|`true` if you want the W-bit displayed, `false` if otherwise|
|DisplayControlMessages|`true` if you want control messages displayed, `false` if otherwise|

If `DisplayControlMessages` is `true` HSMS Control Messages will be displayed.   If `DisplayControlMessages`
is `false` HSMS Control Messages will not appear in the output.  The following is
an example where `DisplayControlMessages` is `true`:

```C#
#Timestamp:2023-02-23T19:00:11.108
#Direction Src:TheHost Dest:TheEquipment
#HSMS Control Message: SelectReq SessionId:65535 System Bytes:1
#Timestamp:2023-02-23T19:00:11.108
#Direction Src:TheEquipment Dest:TheHost
#HSMS Control Message: SelectRsp SessionId:65535 Select Status:0 System Bytes:1
```

If `AddTimestamp` was `false` you would not have seen the `#Timestamp:...` lines.

If `AddDirection` was `false` you would not have seen the `#Direction ...` lines.

Here is an example where `DisplayDeviceId`, `DisplaySystemBytes`, and `DisplayWBit` are set to `true`.

```C#
S1F1 W DevId:0 SysBytes:2.
```

If only `DisplayWBit` is set to `true` the output would resemble:

```C#
S1F1 W.
```

#### SML &quot;BodyOutputConfig&quot;

This section of the configuration file contains items that have an effect on how the body (if present)
of the `SECSMessage` is formatted for output.

| Item Name | Description |
| --- | --- |
| DisplayCount | Controls whether or not an item's count value is displayed.  Valid values are `true` or `false`. |
| MaxOutputLineLength | Set the maximum output line length of the generated log output. |

If `DisplayCount` is `false` the output will resemble:

```C#
#Timestamp:2023-02-23T18:37:56.021
#Direction Src:TheEquipment Dest:TheHost
S1F2
<L
  <A "EqModel">
  <A "1.2.14">
>.
```

If `DisplayCount` is `true` the output will resemble:

```C#
#Timestamp:2023-02-23T19:00:14.838
#Direction Src:TheEquipment Dest:TheHost
S1F2
<L [2]
  <A [7] "EqModel">
  <A [6] "1.2.14">
>.
```

### &quot;XMLOutputConfig&quot;

This section of the configuration file contains information that will be used to determine what the resulting
output will look like when the SECS-II messages are display in an XML format.

**Note:**

When outputting the log information in the XML format be warned that an XML document is **NOT** produced.  Only well-formed XML is output.  If you want to be able to parse the XML with any of the standard XML parser APIs available you will need to create an XML document.  Doing this is an easy task.  You must add an XML declaration to your file in addition to a root element of your choice.  Don't forget to close the root element.

```xml
<?xml version="1.0"?>
<MyLogFileRootElement>
```

Paste the contents of the log file you are interested in parsing here.

```xml
</MyLogFileRootElement>
```

Make sure when you are doing your copying and pasting that you &quot;grab&quot; complete XML element(s).

Following are the customization options that may be used when `LoggingOutputFormat`
is set to XML.

#### XML &quot;HeaderOutputConfig&quot;

This section of the configuration file contains items that have an effect on how the `HSMSHeader` is formatted for output.

| Item | Description|
| --- | --- |
|DisplayAsElementsOrAttributes| `Elements` or `Attributes` Display the individual header parts using XML elements or as XML Attributes in a single XML element|
|DisplayMessageIdAsSxFy| `true` if you want the message Id displayed like `SxFy` false if otherwise|
|DisplayDeviceId| `true` if you want the device Id displayed, `false` if otherwise|
|DisplaySystemBytes| `true` if you want the System Bytes displayed, `false` if otherwise|
|DisplayWBit|`true` if you want the W-bit displayed, `false` if otherwise|
|DisplayControlMessages|`true` if you want control messages displayed, `false` if otherwise|

Example output with `DisplayAsElementsOrAttributes` set to `Elements`

```xml
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

```xml
<SECSMessage>
  <Direction src="MES" Dest="Equipment"/>

  <Header DeviceId="0" Stream="1" Function="1" WBit="1" SystemBytes="40127"/>
                             or
  <Header DeviceId="0" SxFy="S1F1" WBit="1" SystemBytes="40127"/>
  <Body>
  </Body>
</SECSMessage>
```

#### XML &quot;BodyOutputConfig&quot;

This section of the configuration file contains items that have an effect on how the body (if present)
of the `SECSMessage` is formatted for output.

| Item | Description |
| --- | --- |
| DisplayAsElementsOrAttributes| `Elements` or `Attributes` Display the individual header parts using XML elements or as XML Attributes in a single XML element|
| DisplayNumberOfLengthBytes | Controls whether or not the `SECSItem`'s number of length bytes is displayed.  Valid values are `true` or `false`. |
| DisplayLengthByteValue | Controls whether or not the `SECSItem`''s length in bytes is displayed. Valid values are `true` or `false`. Remember, for a **L**ist item its length in bytes is actually the number of elements it contains. |
| MaxOutputLineLength | Set the maximum output line length of the generated log output. |

Example body

```xml
<SECSItem>
  <Type>U4</Type>
  <NumberOfLengthBytes>1<NumberOfLengthBytes>
  <LengthByteValue>4</LengthByteValue>
  <Value>1234567</Value>
</SECSItem>
```

example 2

```xml
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

## Running SECSSpy

`SECSSpy` is a command line application.  There are two ways to start it.

1. `SECSSpy` If started using this method the application will start and
attempt to open an `appsettings.json` file in order to read its configuration
information.
2. `SECSSpy myConfigFile.json` If started using this method the application will
start and attempt to open the `myConfigFile.json` file and read its configuration
information from there.

## Terminating SECSSpy

When run from the command line in a terminal `SECSSpy` may be terminated by pressing ^C (ctrl-c).  This
will cause `SECSSpy` to perform a graceful shutdown. On Linux `SECSSpy` may also be terminated by using the
`kill -SIGINT` or `kill -2` commands from another terminal window or script.
