# CSharpSECSTools

**CSharpSECSTools** is a project for those that are working primarily in the realm of shop floor automation in the semiconductor industry. 
If you do not work in that industry or you are not familiar with the [SEMI Standards](www.semi.org/en/standards) 
**E4**, **E5**, **E30**, **E37**, el al, this project is probably not for you. 

This project provides some tools / API libraries that will help 
in writing software for equipment interfaces and / or equipment simulators.

## Get the code
Use the <code>git clone</code> command to get the code. 

<code>git clone https://github.com/dkaip/CSharpSECSTools CSharpSECSTools</code> 

This will create a <code>CSharpSECSTools</code> directory in your current directory

## Building
Currently, this project may be built using <code>dotnet</code> version 6.0.108. The unit tests are run using NUnit.3.13.3.  I expect that any more recent NUnit (i.e. NUnit.3.13.3+) will work as well.

<code>dotnet build -configuration Debug</code> may be used to build debug versions of the files.

<code>dotnet build -configuration Release</code> may be used to build release versions of the files.

<code>dotnet clean -configuration Debug</code> may be used to clean debug versions of the files.

<code>dotnet clean -configuration Release</code> may be used to clean release versions of the files.

<code>dotnet test -configuration Debug</code> may be used to run the unit tests on debug versions of the files.

<code>dotnet test -configuration Release</code> may be used to run the unit tests on release versions of the files.

<code>dotnet build</code> or <code>dotnet clean</code> or <code>dotnet test</code> will produce, clean, or test debug versions.

## Documentation
I have added some documentation that so that IntelliSense capable editors/IDEs will be able to provide support for method completion, etc.

## Usage Notes
At the moment the SECSItems and SECSItemTests should work properly.  I have modified code 
in FixedBufferSimulatorConsole, SECSCommUtils, and EquipmentSimulatorSupportStuff so that
the project should at least compile (with some warnings) until I have the time to implement
and test those items.

## Miscellaneous
This version of this repository should be functionally the same as the Baseline version.  Since MonoDevelop seems to no longer be available for my platform I have switched to using Visual Studio Code for the editor and using the <code>dotnet</code> command from the command line.  I have also done modifications so that the build environment is for .NET 6.  As for code differences, I had to fix a couple of unit tests since <code>dotnet</code> seems to allow a little more precision for floating point numbers.  I removed the visibility of some &quot;internal&quot; classes and methods that should not have been public.  Some documentation was added as well.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[Apache 2.0](http://www.apache.org/licenses/LICENSE-2.0)
