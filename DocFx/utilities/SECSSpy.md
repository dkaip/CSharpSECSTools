# SECSSpy
This utility program is used to log the SECS message traffic between two endpoints of a &quot;SECS connection&quot; to a file.

The output format of the resulting log file may be configured to be either [SECS Message Language(SML)](https://www.peergroup.com/resources/secs-message-language/) or [XML(Extensible Markup Language)](https://www.w3.org/standards/xml/core).  When the output format is selected there are a number of customization options that may be applied depending on the output format selected.  Refer to the [SML Output](#sml-output) or [XML Output](#xml-output) sections of this document for more information on these options.

Why are there two output formats available?  SML has been around for many years now and is quite familiar to those who have been working in this part of this industry for a long time.  When looking at the log files with your own eyes it is very adequate.  However, it can be a real pain to write a parser for, hence the ability to write out log files in XML.  Why would you ever need to parse a log file?

There are a number of reasons why being able to easily parse a log file is a good and useful thing.
- Equipment Interface(EI) Development/Enhancment/Debugging - It can be a great aide in the development and or debugging cycle to be able to generate an equipment simulator.  With this simulator you are able to run your EI against a simulator that is simulating a genuine equipment operation cycle complete with all of the data events and their respective reports, perhaps megabytes worth of data.  I will mention here that parsing the log file and producing code segments for [sc_open](https://github.com/dkaip/sc_open) can be invaluable in EI development, maintenance, debugging, etc.
- Audit Trail - depending on your software integration / automation stack it may be much easier to generate audit trail information from log files generated from the communication between the MES software and the equipment than it is to generate the information from other sources.

There are plenty of additional uses for a log files that may be parsed easily by a program.

**Note:** At this time this program is only able to log SECS messages between two endpoints of an HSMS connection.

## Configuration
The configuration file, unless specified, is `appsettings.json`.

## SML Output

### SML Header Output Format Options

### SML Body Output Format Options

## XML Output
When outputting the log information in the XML format be warned that an XML document is **NOT** produced.  Only well-formed XML is output.  If you want to be able to parse the XML with any of the standard XML parser APIs available you will need to create an XML document.  Doing this is an easy task.  You must add an XML declaration to your file in addition to a root element of your choice.  Don't forget to close the root element.

```
<?xml version="1.0"?>
<MyLogFileRootElement>
```
Paste the contents of the log file you are interested in parsing here.
```
</MyLogFileRootElement>
```

Make sure when you are doing your copying and pasting that you &quot;grab&quot; a complete XML element(s).

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
```
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
</SECSMessage>
```

Example output with `DisplayAsElementsOrAttributes` set to `Attributes`
```
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