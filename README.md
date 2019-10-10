# CSharpSECSTools

**CSharpSECSTools** is a project for those that are working primarily in th realm of shop floor automation in the semiconductor industry. 
If you do not work in that industry or you are not familiar with the [SEMI Standards](www.semi.org/en/standards) 
**E4**, **E5**, **E30**, **E37**, el al, this project is probably not for you. 

This project provides some tools for problem identification and monitoring as well as API libraries that will help 
in writing software for equipment interfaces and / or equipment simulators.

## Get the code
Use the <code>git clone</code> command to get the code. 

<code>git clone https://github.com/dkaip/CSharpSECSTools CSharpSECSTools</code> 

This will create a <code>CSharpSECSTools</code> directory in your current directory

## Building
Currently, this project may be built using MonoDevelop 7.3.3.  It has been built 
and unit tests run using earlier versions as well.  The unit tests are run using
NUnit.3.12.0.  I expect that any more recent NUnit will work as well.

## Documentation
I have added some documentation that so that MonoDevelop is able to use IntelliSense 
for method completion, etc.

## Usage Notes
At the moment the SECSItems and SECSItemTests should work properly.  I have modified code 
in FixedBufferSimulatorConsole, SECSCommUtils, and EquipmentSimulatorSupportStuff so that
the project should at least compile (with some warnings) until I have the time to implement
and test those items.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[Apache 2.0](http://www.apache.org/licenses/LICENSE-2.0)
