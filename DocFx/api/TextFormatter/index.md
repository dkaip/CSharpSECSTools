# TextFormatter


### SML Output

SML is a somewhat compact display notation that many people who have been
in the industry for a while will be familiar with.  Humans can read SML
fairly well.  However, it can be a real pain to parse programmatically in
cases where the contents of a log file need to be used as input to another
program. i.e. Equipment or Host simulator scripts, data mining scripts, etc.

Given the nature of SML there are only a small number of configuration options
available for controlling the resulting output.  These options may be found....

For more information on SML see
[SECS Message Language (SML)](https://www.peergroup.com/resources/secs-message-language/).

### XML Output

XML(eXtensible Markup Language) was designed to be a language for the
storage and transport of data.  This API is able to produce output in
this format.  This format is quite human readable and it is much easier
to parse programmatically in the event that output in this format needs
to be used as input to another program. i.e. Equipment or Host simulator
scripts, data mining scripts, etc.

Since output in XML can be a bit verbose there are a number of configuration
options available for controlling the resulting output.  These options
may be found....

**Note:**  This API, when producing XML output, does NOT output an XML
Document.  It outputs well-formed XML elements.  If you desire to take
the XML output produced by this program and parse it with many of the
available XML tool kits you will need to create an XML Document.  This
is easy to do.

1. First create an empty file and add the line (or a line like it
depending on the version number) to the top `<?xml version="1.0"?>`.
2. After the previous line add a root element.  For example
`<MyRootElement>`.
3. Insert the desired XML output produced by this API after the root element.
4. Add the closure to the root element added previously. In this case `</MyRootElement>`.
5. Save the file and read it with whatever utility you prefer to use
to parse XML.

Additional formatting and or indention is unnecessary, **however**, when you
perform your copy / paste operations make sure and get complete XML elements.

