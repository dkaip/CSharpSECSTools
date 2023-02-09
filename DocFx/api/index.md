# API Libraries Overview
This page contains overview level information for the API libraries available
in this project.  For more specific and complete information please click on
the section headers below.  This will take you to the detailed documentation
for each API.

If you just want to see the documentation for the methods, properties, etc.,
that are available for a specific API click on the links to the left.

## [SECSItems](SECSItems/index.md)

This API library allows a user to create and manipulate `SECSItem`s in the
programming language of C#.  A `SECSItem` is a base class for a collection
of classes that represent individual SECS-II data items.  Examples of these
classes include `ASCIISECSItem`, `BinarySECSItem`, `U4SECSItem`, etc.

Normally in the course of life SECS-II message are sent between communication
endpoints in a binary or &quot;transmission format&quot;.  This
&quot;transmission format&quot; while good for sending information between
the endpoints is not conducive for easy retrieval / manipulation in a
programming language without some form of helper API.  This API was created
to provide that help.

## [SECSCommUtils](SECSCommUtils/index.md)

## [TextFormatter](TextFormatter/index.md)

This API library allows a user to convert `SECSMessage`s, `SECSHeader`s, and
`SECSItem`s into a `string` that is in either [SML](#sml-output) or [XML](#xml-output).
The produced `string`s are suitable for output to a terminal and / or a file.

The `SECSMessage` and `SECSHeader` classes are located in the
[SECSCommUtils](#secscommutils) API library.  The `SECSItem` classes are
located in the [SECSItems](#secsitems) API library.

A primary use for features provided by this API are to generate output
for logging.

### SML Output

SML, or **S**ECS **M**essage **L**anguage, is a somewhat compact display
notation that many people who have been
in the industry for a while will be familiar with.  Humans can read SML
fairly well.  However, it can be a real pain to parse programmatically in
cases where the contents of a log file need to be used as input to another
program. i.e. Equipment or Host simulator scripts, data mining scripts, etc.

For more information on SML see
[SECS Message Language (SML)](https://www.peergroup.com/resources/secs-message-language/).

### XML Output

XML(eXtensible Markup Language) was designed to be a language for the
storage and transport of data.  This API is able to produce output in
this format.  XML can be quite human readable and it is much easier
to parse programmatically in the event that output in this format needs
to be used as input to another program. i.e. Equipment or Host simulator
scripts, data mining scripts, etc.
