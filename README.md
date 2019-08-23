# Bener.DebuggerVisualizers
Debugger visualizer for .net projects. 
Supported types:
* List<>
* Dictionary<,>
* DataRow

## Installation
Build the project and copy output DLL to the one of the following locations:
* My Documents\\*VisualStudioVersion*\Visualizers
* *VisualStudioInstallPath*\Common7\Packages\Debugger\Visualizers

For more information:<br/>
https://docs.microsoft.com/en-us/visualstudio/debugger/how-to-install-a-visualizer?view=vs-2019

### .Net Core projects
Also copy the DLL's to the following folders to use in .net core projects:
* My Documents\\*VisualStudioVersion*\Visualizers\netstandard2.0
* *VisualStudioInstallPath*\Common7\Packages\Debugger\Visualizers\netstandard2.0

## Usage
Debug your project, set breakpoint, drag mouse to a List<> or Dictionary<,> object variable, click the magnifying glass.

![alt text](https://raw.githubusercontent.com/ibener/Bener.DebuggerVisualizers/master/screenshot.png)

## Limitations
Editing the values in the visualizer not (yet) supported.
