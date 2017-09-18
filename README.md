# GiraffeSample

This sample app demonstrates F#, .NET Core, Giraffe, and using Sqlite. You can develop and run it cross-platform.

## Instructions

1. Download the [.NET Core 2.0 SDK](https://www.microsoft.com/net/core).
2. Install [Visual Studio Code and Ionide-FSharp.](https://docs.microsoft.com/en-us/dotnet/fsharp/get-started/get-started-vscode).  They're great for cross-platform F# development.
3. Run `dotnet restore` before opening the project in VSCode.
4. Use `dotnet watch run` for development.  It'll recompile and re-run the project when you make changes.

I recommend using [Postman](https://www.getpostman.com/) to play around with it.

## Deployment

This isn't really intended for production, but if you wish to deploy this somewhere, do the following:

1. Use `dotnet publish -o app -c release`.  This will copy a release version of the app into a new folder called `/app`, along with every other .dll dependency this app requires.
2. Put the contents of `/app` wherever you like.
3. Run it from there.
