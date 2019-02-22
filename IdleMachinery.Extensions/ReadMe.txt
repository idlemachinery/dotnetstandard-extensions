-- .NET Standard --
https://docs.microsoft.com/en-us/dotnet/standard/net-standard

The .NET Standard is a formal specification of .NET APIs that are intended to be available on all .NET implementations. The motivation behind the .NET Standard is establishing greater uniformity in the .NET ecosystem. ECMA 335 continues to establish uniformity for .NET implementation behavior, but there's no similar spec for the .NET Base Class Libraries (BCL) for .NET library implementations.

The .NET Standard enables the following key scenarios:

- Defines uniform set of BCL APIs for all .NET implementations to implement, independent of workload.
- Enables developers to produce portable libraries that are usable across .NET implementations, using this same set of APIs.
- Reduces or even eliminates conditional compilation of shared source due to .NET APIs, only for OS APIs.

The various .NET implementations target specific versions of .NET Standard. Each .NET implementation version advertises the highest .NET Standard version it supports, a statement that means it also supports previous versions. For example, the .NET Framework 4.6 implements .NET Standard 1.3, which means that it exposes all APIs defined in .NET Standard versions 1.0 through 1.3. Similarly, the .NET Framework 4.6.1 implements .NET Standard 1.4, while .NET Core 1.0 implements .NET Standard 1.6.


-- .NET Implementation Support --
1.6 - https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.6.md
2.0 - https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md

.NET Standard				1.6			2.0
.NET Core					1.0			2.0
.NET Framework				4.6.1		4.6.1
Mono						4.6			5.4
Xamarin.iOS					10.0		10.14
Xamarin.Mac					3.0			3.8
Xamarin.Android				7.0			8.0
Universal Windows Platform	10.0.16299	10.0.16299
Windows						not supported				
Windows Phone				not supported					
Windows Phone Silverlight	not supported							
Unity						2018.1		2018.1


-- Which .NET Standard version to target --
When choosing a .NET Standard version, you should consider this trade-off:

- The higher the version, the more APIs are available to you.
- The lower the version, the more platforms implement it.
In general, we recommend you to target the lowest version of .NET Standard possible. So, after you find the highest .NET Standard version you can target, follow these steps:

1. Target the next lower version of .NET Standard and build your project.
2. If your project builds successfully, repeat step 1. Otherwise, retarget to the next higher version and that's the version you should use.
However, targeting lower .NET Standard versions introduces a number of support dependencies. If your project targets .NET Standard 1.x, we recommend that you also target .NET Standard 2.0. This simplifies the dependency graph for users of your library that run on .NET Standard 2.0 compatible frameworks, and it reduces the number of packages they need to download.