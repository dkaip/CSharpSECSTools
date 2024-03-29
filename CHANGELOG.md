# Changelog

All notable changes to this project will be documented in this file.

23-Feb-2023

## Added

More documentation. Primarily for `SECSSpy`.

22-Feb-2023

## Changed

I modified `ListSECSItem` so that it now implements the `IList` interface.  This
will make working with it and its element much more natural in `C#`.

As part of getting `SECSSpy` operational. I performed a number of changes to the
`SECSCommUtils` API.  It is not finished, but, it should be in good enough shape
to experiment with...at least for communication using HSMS connections.

I modified the `SMLFormatter` so that header logging options now include adding
DisplayDeviceId, DisplaySystemBytes, and / or DisplayControlMessages.

## Added

Added an `AsDictionary` method to `ListSECSItem`.  This will return the list's
contents as a `Dictionary`.  The `SECSItem`s within the `Dictionary` that may
be accessed via their &quot;address&quot;  Refer to the documentation for more
information.

More documentation.

The utility `SECSSpy` now works.  It is not finished, but, it should be good enough
to experiment with (and hopefully provide some feedback on).

## Fixed

Some bugs.

8-Feb-2023

For the most part just added documentation.  The few minor changes I
did make will not break anything.

## Fixed

I fixed some display bugs in `SMLFormatter.cs`.  Since the API library containing
this file is not really released yet it should not create issues.

7-Feb-2023

The short answer is I recommend you to stay with the Baseline(v1.0.0)
for the moment.  I have made some changes to the `SECSItems` API which
will cause some rework.  If you do change to the latest at this point
you will have some rework, but, I am not expecting it to change too
much more other than adding more documentation.

## Deprecated

The `GetValue()` methods have been deprecated.  Please use the property
`Value` instead.

The `GetSECSItemFormatCode()` methods have been deprecated.  Please use
the property `ItemFormatCode` instead.

## Changed

`SECSItems` has been changed a bit and will cause some rework.

`SECSItemTests` have changed to handle the rework to `SECSItems`.

`SECSCommUtils` have changed a little due to functionality being
filled in.

## Added

`SECSSpy` application project.  This is for logging and is not suitable
for working with yet.

`SECSTee` application project.

`TextFormatter` API project.  This is used by `SECSSpy` in order to
generate text version of `SECSMessage`s, `SECSHeader`s, and`SECSItems`
that are suitable for logging. It can produce output in SML and XML.

A `DocFx` folder was added to contain the source for much of the documentation
that is being generated.  To generate the documentation as it currently exists
`cd` to the `DocFx` folder and enter the following `docfx docfx.json`.  This
will create the documentation and place it in the `docs` folder.

A `docs` folder has been added.  It contains the documentation generated by
`docfx`.

