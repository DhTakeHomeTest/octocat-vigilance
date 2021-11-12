# octocat-vigilance - Main Branch Protection WebService For GitHub

octocat-vigilance is a web a simple web service that listens for organization events to know when a repository has been created. When a new repository is created the main branch is automatically protected. A new issue is also added to the repository mentioning the account owner of the new protections added. Interactivity with GitHub API is using [Octokit](https://github.com/octokit/octokit.net/).

## Usage examples

Get public info on a specific user.

```c#
var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
var user = await github.User.Get("half-ogre");
Console.WriteLine(user.Followers + " folks love the half ogre!");
```

## Supported Platforms

* .NET 4.6 (Desktop / Server) or greater
* [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) or greater

## Getting Started

Octokit is a GitHub API client library for .NET and is [available on NuGet](https://www.nuget.org/packages/Octokit/):

```
dotnet add package Octokit
```

There is also an IObservable based GitHub API client library for .NET using Reactive Extensions:

```
dotnet add package Octokit.Reactive
```



## Build

Octokit is a single assembly designed to be easy to deploy anywhere.

To clone and build it locally click the "Clone in Desktop" button above or run the
following git commands.

```
git clone git@github.com:octokit/Octokit.net.git Octokit
cd Octokit
```

To build the libraries, run the following command:

Windows: `.\build.ps1`

Linux/OSX: `./build.sh`

## Contribute

Visit the [Contributor Guidelines](https://github.com/octokit/octokit.net/blob/main/CONTRIBUTING.md)
for more details. All contributors are expected to follow our
[Code of Conduct](https://github.com/octokit/octokit.net/blob/main/CODE_OF_CONDUCT.md).

## Problems?

If you find an issue with our library, please visit the [issue tracker](https://github.com/octokit/octokit.net/issues)
and report the issue.

Please be kind and search to see if the issue is already logged before creating
a new one. If you're pressed for time, log it anyways.

When creating an issue, clearly explain

* What you were trying to do.
* What you expected to happen.
* What actually happened.
* Steps to reproduce the problem.

Also include any other information you think is relevant to reproduce the
problem.

## Related Projects

 - [ScriptCs.OctoKit](https://github.com/hnrkndrssn/ScriptCs.OctoKit) - a [script pack](https://github.com/scriptcs/scriptcs/wiki/Script-Packs) to use Octokit in scriptcs
 - [ScriptCs.OctokitLibrary](https://github.com/ryanrousseau/ScriptCs.OctokitLibrary) - a [script library](https://github.com/scriptcs/scriptcs/wiki/Script-Libraries) to use Octokit in scriptcs

## Copyright and License

Copyright 2017 GitHub, Inc.

Licensed under the [MIT License](https://github.com/octokit/octokit.net/blob/main/LICENSE.txt)