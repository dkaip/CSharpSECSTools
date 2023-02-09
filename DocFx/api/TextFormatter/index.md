# TextFormatter

## Introduction

Logging of SECS message traffic (information flow) between the two endpoints
of a SECS &quot;connection&quot; is an indispensable ability in the automation
of semiconductor manufacturing.  This logging is used for debugging purposes
during initial development of **E**quipment **I**nterfaces, etc.  In addition logging
can be critical in the forensic analysis of scrap events and other unexpected
and abnormal processing scenarios.

This API provides the functionality to convert `SECSMessage`s, `SECSHeader`s, and
`SECSItem`s into a `string` that may be displayed on a terminal and / or output
to a file.

**Note:** The `SECSMessage` and `SECSHeader` classes are located in the
[SECSCommUtils](../SECSCommUtils/index.md) API library and the `SECSItem`
classes are located in the [SECSItems](../SECSItems/index.md) API library.

## Configuring a Formatter

### Using a `json` file

If you are already reading and processing an `appsettings.json` file during your
application's startup the easiest
way to create the configuration for the formatter is to add a `TextFormatterConfig`
section to the `json` file. Then use the following code to extract the configuration
information:

```csharp
    TextFormatterConfig formatterConfiguration = configuration.GetSection("TextFormatterConfig").Get<TextFormatterConfig>();
```

The `TextFormatterConfig` section should look like this (with the appropriate changes for your environment):

```json
    "TextFormatterConfig":
    {
        "AddTimestamp": true,
        "TimestampFormat": "yyyy-MM-ddTHH:mm:ss.fff",
        "AddDirection": true,
        "IndentAmount": 2,
        "MaxIndentionSpaces": 50,
        "LoggingOutputFormat": "SML",
        "XMLOutputConfig":
        {
            "HeaderOutputConfig":
            {
                "DisplayAsElementsOrAttributes": "Attributes",
                "DisplayMessageIdAsSxFy": true,
                "DisplayDeviceId": true,
                "DisplaySystemBytes": true,
                "DisplayWBit": true,
                "DisplayControlMessages": true
            },
            "BodyOutputConfig":
            {
                "DisplayAsElementsOrAttributes": "Attributes",
                "DisplayNumberOfLengthBytes": true,
                "DisplayLengthByteValue": true,
                "MaxOutputLineLength": 80
            }
        },
        "SMLOutputConfig":
        {
            "HeaderOutputConfig":
            {
                "DisplayWBit": true
            },
            "BodyOutputConfig":
            {
                "DisplayCount": false,
                "MaxOutputLineLength": 80
            }
        }
    }
```

If the value of `LoggingOutputFormat` is SML the formatter created will use the information in the
`SMLOutputConfig` section.  If its value is XML the formatter created will use the information in
the `XMLOutputConfig` section.  Since both `SMLOutputConfig` and `XMLOutputConfig`  are present
all the user has to do is changed the value of `LoggingOutputFormat` between &quot;SML&quot; and
&quot;XML&quot; to switch between the two output formats.

### Not using a `json` file

If you do not want to use a `json` file to create the `TextFormatterConfig` object you will need
to create it manually.

Examine the `json` text above.  You will notice that there are simple line items and compound
line items.  You will need to go from inside to outside and create the `TextFormatterConfig` object.
Each of the &quot;compound&quot; objects has a class associated with it.  Here is a code snippet
that gives a rough idea of what needs to be done.  This should produce the same results as the
above `json` configuration will.

```csharp
    // Set up stuff for SML output config
    HeaderOutputConfig headerOutputConfig = new HeaderOutputConfig();
    headerOutputConfig.DisplayWBit = true;

    BodyOutputConfig bodyOutputConfig = new BodyOutputConfig();
    bodyOutputConfig.DisplayCount = true;
    bodyOutputConfig.MaxOutputLineLength = 80;

    SMLOutputConfig smlOutputConfig = new SMLOutputConfig();
    smlOutputConfig.HeaderOutputConfig = headerOutputConfig;
    smlOutputConfig.BodyOutputConfig = bodyOutputConfig;

    // Set up stuff for XML output config
    headerOutputConfig = new HeaderOutputConfig();
    headerOutputConfig.DisplayAsType = DisplayAsType.Attributes;
    headerOutputConfig.DisplayMessageIdAsSxFy = true;
    headerOutputConfig.DisplayDeviceId = true;
    headerOutputConfig.DisplaySystemBytes = true;
    headerOutputConfig.DisplayWBit = true;
    headerOutputConfig.DisplayControlMessages = true;

    bodyOutputConfig = new BodyOutputConfig();
    bodyOutputConfig.DisplayAsType = DisplayAsType.Attributes;
    bodyOutputConfig.DisplayNumberOfLengthBytes = true;
    bodyOutputConfig.DisplayLengthByteValue = true;
    bodyOutputConfig.MaxOutputLineLength = 80;

    XMLOutputConfig xmlOutputConfig = new XMLOutputConfig();
    xmlOutputConfig.HeaderOutputConfig = headerOutputConfig;
    xmlOutputConfig.BodyOutputConfig = bodyOutputConfig;

    TextFormatterConfig formatterConfiguration = new TextFormatterConfig();
    formatterConfiguration.AddTimestamp = true;
    formatterConfiguration.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fff";
    formatterConfiguration.AddDirection = true;
    formatterConfiguration.IndentAmount = 2;
    formatterConfiguration.MaxIndentionSpaces = 50;
    formatterConfiguration.LoggingOutputFormat = "SML";
    formatterConfiguration.SMLOutputConfig = smlOutputConfig;
    formatterConfiguration.XMLOutputConfig = xmlOutputConfig;

```

Now that you have the `TextFormatterConfig` object you are able to create
a formatter.  See [Creating a Formatter](#creating-a-formatter) or continue
reading to get an understanding of how the various configuration options
alter the output.

### Output Examples

This sections contains some examples of the type of output produced depending
on the value of the configuration parameters.  If the value of `LoggingOutputFormat`
is &quot;SML&quot; output will be in SML so look at the [SML Output Examples](#sml-output-examples).
If the value of `LoggingOutputFormat`
is &quot;XML&quot; output will be in XML so look at the [XML Output Examples](#xml-output-examples).

#### SML Output Examples

Given the nature of SML there are only a small number of configuration options
available for controlling the resulting output.  These options and their effects
are described and illustrated in the following text.

Here are some examples of output in SML.  This first example has `AddTimestamp` and
`AddDirection` set to `true`.  In addition `DisplayCount` and `DisplayWBit` are
set to `true`.

```csharp
#Timestamp:2023-02-08T19:33:31.043
#Direction Src:EQ Dest:EI
S6F11 W
<L [3]
  <A [6] "DATAID">
  <A [4] "CEID">
  <L [2]
    <L [2]
      <A [6] "RPTID1">
      <L [3]
        <U2 [1] 1>
        <U2 [4] 2 3 4 5>
        <BOOLEAN [1] T>
      >
    >
    <L [2]
      <A [6] "RPTID2">
      <L [4]
        <A [0]>
        <B [6] 0x00 0x00 0x00 0x00 0x00 0x00>
        <F8 [1] 3.141593>
        <BOOLEAN [5] T F T T F>
        <I4 [1] 2147483647>
      >
    >
  >
>.
```

This second example has `AddTimestamp` and
`AddDirection` set to `false`.  In addition `DisplayCount` and `DisplayWBit` are
set to `false` as well.

```csharp
S6F11
<L
  <A "DATAID">
  <A "CEID">
  <L
    <L
      <A "RPTID1">
      <L
        <U2 1>
        <U2 2 3 4 5>
        <BOOLEAN T>
      >
    >
    <L
      <A "RPTID2">
      <L
        <A>
        <B 0x00 0x00 0x00 0x00 0x00 0x00>
        <F8 3.141593>
        <BOOLEAN T F T T F>
        <I4 2147483647>
      >
    >
  >
>.
```

#### XML Output Examples

##### **An Important Reminder**

Before we get started we need to discuss XML output a little bit.
This API, when producing XML output, does NOT output an XML
Document.  It outputs well-formed XML elements.  If you desire to take
the XML output produced by this program and parse it with many of the
available XML tool kits, available in many languages, you will need to
create an XML Document.  This is easy to do.

1. First create an empty file and add the line `<?xml version="1.0"?>`
(or a line like it depending on the version number) as the first line of the file.
1. After the previous line add a root element line as the second line of the file.
For example `<MyRootElement>`.
1. Insert the desired XML output produced by this API after the root element.
1. Add a closure line to the root element added previously at the end of the file.
In this case `</MyRootElement>`.
1. Save the file and read it with whatever utility you prefer to use
to parse XML.

Additional formatting and or indention is unnecessary, **however**, when you
perform your copy / paste operations make sure and get complete XML elements.

##### **On With the Examples**

Since output in XML can be a bit verbose there are a number of configuration
options available for controlling the resulting output. In the following text
these options will be explored.

For brevity's sake we are going to look at the individual options and how they
effect the output produced.

| AddTimestamp | AddDirection | Result |
| ------------ | ------------ | ------ |
|       T      |      T       |    1   |
|       F      |      T       |    2   |
|       T      |      F       |    3   |
|       F      |      F       |    4   |

```xml
1. <SECSMessage Timestamp="2023-02-09T09:31:01.194" Src="EI" Dest="EQ">
2. <SECSMessage Src="EI" Dest="EQ">
3. <SECSMessage Timestamp="2023-02-09T09:37:04.993">
4. <SECSMessage>
```

##### **HeaderOutputConfig:DisplayAsElementsOrAttributes is Elements**

Now for the header where `DisplayAsElementsOrAttributes` is &quot;Elements&quot;.

###### **HSMS Control Messages**

###### **SECS Messages**

A message's Stream and Function are always displayed. `DisplayMessageIdAsSxFy`
controls what the output looks like. If `DisplayMessageIdAsSxFy` is `true`
the header might look like (depending on the other options):

```xml
  <Header>
    <SxFy>S6F11</SxFy>
  </Header>
```

If `DisplayMessageIdAsSxFy` is `false` the header might look like (depending on the other options):

```xml
  <Header>
    <Stream>6</Stream>
    <Function>11</Function>
  </Header>
```

As for most of the rest of the header options.

| DisplayDeviceId | DisplaySystemBytes | DisplayWBit | Result |
| --------------- | ------------------ | ----------- | ------ |
|        T        |          T         |      T      |    1   |
|        T        |          T         |      F      |        |
|        T        |          F         |      T      |        |
|        T        |          F         |      F      |        |
|        F        |          T         |      T      |        |
|        F        |          T         |      F      |        |
|        F        |          F         |      T      |    2   |
|        F        |          F         |      F      |    3   |

```xml
1. <Header>
    <DeviceId>1234</DeviceId>
    <SxFy>S6F11</SxFy>
    <Wbit>True</Wbit>
    <SystemBytes>14</SystemBytes>
  </Header>

2. <Header>
    <SxFy>S6F11</SxFy>
    <Wbit>True</Wbit>
  </Header>

3. <Header>
    <SxFy>S6F11</SxFy>
  </Header>
```

As you can see, examples for all of the combinations are not displayed, but,
you get the idea.

##### **HeaderOutputConfig:DisplayAsElementsOrAttributes is Attributes**

Now for the header where `DisplayAsElementsOrAttributes` is &quot;Attributes&quot;.

###### **HSMS Control Messages**

###### **SECS Messages**

A message's Stream and Function are always displayed. `DisplayMessageIdAsSxFy`
controls what the output looks like. If `DisplayMessageIdAsSxFy` is `true`
the header might look like (depending on the other options):

```xml
  <Header SxFy="S6F11"/>
```

If `DisplayMessageIdAsSxFy` is `false` the header might look like (depending on the other options):

```xml
  <Header Stream="6" Function="11"/>
```

As for most of the rest of the header options.

| DisplayDeviceId | DisplaySystemBytes | DisplayWBit | Result |
| --------------- | ------------------ | ----------- | ------ |
|        T        |          T         |      T      |    1   |
|        T        |          T         |      F      |        |
|        T        |          F         |      T      |        |
|        T        |          F         |      F      |        |
|        F        |          T         |      T      |        |
|        F        |          T         |      F      |        |
|        F        |          F         |      T      |    2   |
|        F        |          F         |      F      |    3   |

```xml
1. <Header DeviceId="1234" Stream="6" Function="11" Wbit="True" SystemBytes="14"/>

2. <Header SxFy="S6F11" Wbit="True"/>

3. <Header Stream="6" Function="11"/>
```

As you can see, examples for all of the combinations are not displayed, but,
you get the idea.

##### **BodyOutputConfig:DisplayAsElementsOrAttributes is Elements**

Now for the body where `DisplayAsElementsOrAttributes` is &quot;Elements&quot;.

| DisplayNumberOfLengthBytes | DisplayLengthByteValue | Result |
| -------------------------- | ---------------------- | ------ |
|             T              |           T            |    1   |
|             T              |           F            |    2   |
|             F              |           T            |    3   |
|             F              |           F            |    4   |

```xml
1. <SECSItem>
     <Type>A</Type>
     <NumLengthBytes>1</NumLengthBytes>
     <LengthByteValue>6</LengthByteValue>
     <Value>DATAID</Value>
   </SECSItem>

2. <SECSItem>
     <Type>A</Type>
     <NumLengthBytes>1</NumLengthBytes>
     <Value>DATAID</Value>
   </SECSItem>

3. <SECSItem>
     <Type>A</Type>
     <LengthByteValue>6</LengthByteValue>
     <Value>DATAID</Value>
   </SECSItem>

4. <SECSItem>
     <Type>A</Type>
     <Value>DATAID</Value>
   </SECSItem>
```

##### **BodyOutputConfig:DisplayAsElementsOrAttributes is Attributes**

Now for the body where `DisplayAsElementsOrAttributes` is &quot;Attributes&quot;.

| DisplayNumberOfLengthBytes | DisplayLengthByteValue | Result |
| -------------------------- | ---------------------- | ------ |
|             T              |           T            |    1   |
|             T              |           F            |    2   |
|             F              |           T            |    3   |
|             F              |           F            |    4   |

```xml
1. <SECSItem type="A" NumLengthBytes="1" LengthByteValue="6">
     <Value>DATAID</Value>
   </SECSItem>

2. <SECSItem type="A" NumLengthBytes="1">
     <Value>DATAID</Value>
   </SECSItem>

3. <SECSItem type="A" LengthByteValue="6">
     <Value>DATAID</Value>
   </SECSItem>

4. <SECSItem type="A">
     <Value>DATAID</Value>
   </SECSItem>
```

Following is pretty much the same message as formatted above in SML.
In this case it is formatted in XML.  As you will notice it is considerably
more verbose, but, as mentioned before it is much easier to parse into a
machine readable format.

```xml
<SECSMessage Timestamp="2023-02-09T14:18:14.088" Src="EQ" Dest="EI">
  <Header DeviceId="1234" Stream="6" Function="11" Wbit="True" SystemBytes="14"/>
  <SECSItem type="L">
    <Value>
      <SECSItem type="A">
        <Value>DATAID</Value>
      </SECSItem>
      <SECSItem type="A">
        <Value>CEID</Value>
      </SECSItem>
      <SECSItem type="L">
        <Value>
          <SECSItem type="L">
            <Value>
              <SECSItem type="A">
                <Value>RPTID1</Value>
              </SECSItem>
              <SECSItem type="L">
                <Value>
                  <SECSItem type="U2">
                    <Value>1</Value>
                  </SECSItem>
                  <SECSItem type="U2">
                    <Value>2</Value>
                  </SECSItem>
                  <SECSItem type="BO">
                    <Value>True</Value>
                  </SECSItem>
                </Value>
              </SECSItem>
            </Value>
          </SECSItem>
          <SECSItem type="L">
            <Value>
              <SECSItem type="A">
                <Value>RPTID2</Value>
              </SECSItem>
              <SECSItem type="L">
                <Value>
                  <SECSItem type="A">
                    <Value/>
                  </SECSItem>
                  <SECSItem type="B">
                    <Value>
                      0x00 0x01 0x7F 0xFF 0x00 0x64
                    </Value>
                  </SECSItem>
                  <SECSItem type="F8">
                    <Value>3.141593</Value>
                  </SECSItem>
                  <SECSItem type="BO">
                    <Value>
                      True False True True False
                    </Value>
                  </SECSItem>
                  <SECSItem type="I4">
                    <Value>2147483647</Value>
                  </SECSItem>
                </Value>
              </SECSItem>
            </Value>
          </SECSItem>
        </Value>
      </SECSItem>
    </Value>
  </SECSItem>
</SECSMessage>
```

## Creating a Formatter

In order to create a formatter use the factory method shown below:

```csharp
    SECSFormatter formatter = SECSFormatterFactory.CreateFormatter(formatterConfiguration);
```

The information found in `formatterConfiguration` will determine what the created
formatter will be.  It will be either be an `SMLFormatter` or an `XMLFormatter`
depending on the value of `LoggingOutputFormat`.  In either case the methods
available are the same.

## Using a Formatter

Using a `SECSFormatter` is easy.  You just need to use the appropriate methods
to format objects of type [`SECSMessage`](#for-secsmessage-objects),
[`SECSHeader`](#for-secsheader-objects), or [`SECSItem`](#for-secsitem-objects).

### For `SECSMessage` objects

If you have a `SECSMessage` you wish to log just
produce some code like the following:

```csharp
    string outputString = formatter.GetSECSMessageAsText("TheSource",
                                                         "TheDestination",
                                                         secsMessage);
```

`outputString` will end up with a formatted version of the `secsMessage` argument.
This string can be displayed on the console or written to a file.

One thing to note, a SECS message by itself really has no concept of a source or a
destination.  The source and destination arguments are present so that the
application, which probably does know the message's source and destination, can
assign some meaningful name to each to help in the reading of the resulting text.
An example of a source could be something like &quot;Implanter1 EI&quot;
and the destination could be &quot;Equipment&quot;.  In the cases where multiple
connections are being logged to the same file this could help a lot. Think of
a litho cell where there are both a scanner and track to communicate with.

There is another form of the `GetSECSMessageAsText` method that includes a
`StringBuilder` object as the first argument.  Using this method will
cause the formatted output to be appended to the `StringBuilder` object
provided.

```csharp
    StringBuilder sb = new StringBuilder();

    string outputString = formatter.GetSECSMessageAsText(sb,
                                                         "TheSource",
                                                         "TheDestination",
                                                         secsMessage);
```

### For `SECSHeader` objects

If you have a `SECSHeader` object and desire to format it on its own,
as opposed to it being in a `SECSMessage`,
there are two methods that are appropriate.  They are:

```csharp
    public abstract string GetHeaderAsText(SECSHeader secsHeader);
    public abstract void GetHeaderAsText(StringBuilder sb, SECSHeader secsHeader);
```

The one you should choose will depend on whether or not you want to get
the result as a `string` or desire the result to be appended to a
`StringBuilder` that you create and supply.

### For `SECSItem` objects

If you have a `SECSItem` object and desire to format it on its own,
as opposed to it being in a `SECSMessage`,
there are two methods that are appropriate.  They are:

```csharp
    public abstract string GetSECSItemAsText(SECSItem secsItem);
    public abstract void GetSECSItemAsText(StringBuilder sb, SECSItem secsItem);
```

The one you should choose will depend on whether or not you want to get
the result as a `string` or desire the result to be appended to a
`StringBuilder` that you create and supply.
