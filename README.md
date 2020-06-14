# Meraki.Cli

Nuget package for dotnet new Meraki

## Build

To build, from the root directory (and already having installed the latest version of nuget), type:
> nuget pack .\Meraki.Cli.nuspec

## Upload
You can then upload it to [https://www.nuget.org/packages/manage/upload](https://www.nuget.org/packages/manage/upload)

## Installation
To install the template, use:
> dotnet new -i Meraki.Cli

## Project creation
To create a new project using the template, use:
> dotnet new Meraki

or

> dotnet new Meraki --name MyProject.MyNameSpace

or

> dotnet new Meraki --name MyProject.MyNameSpace --apiKey YourApiKey

## Testing

You can test this using a Meraki Sandbox here:

- [Meraki Always On](https://devnetsandbox.cisco.com/RM/Diagram/Index/a9487767-deef-4855-b3e3-880e7f39eadc?diagramType=Topology)
- [Meraki Enterprise](https://devnetsandbox.cisco.com/RM/Diagram/Index/e7b3932b-0d47-408e-946e-c23a0c031bda?diagramType=Topology)
- [Meraki Small Business](https://devnetsandbox.cisco.com/RM/Diagram/Index/aa48e6e2-3e59-4b87-bfe5-7833c45f8db8?diagramType=Topology)

After signing in, look in the lower left hand side of the page for your API key.
