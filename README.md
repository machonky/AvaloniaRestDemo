# AvaloniaRestDemo
Show how to use RestSharp &amp; Avalonia.

In the demo we demonstrate how to deal with CORS issues for Browser Cross-Platform issues. Check CORSDemo/Extensions and Program.cs

Also the App class uses some .NET Standard tooling for DI as well as some techniques to present facilities to the .Net IOC Container (App.axaml.cs) - ConfigureServices()

There is also some tricks on how to use appsettings.json in a cross platform way.

RestSharp is employed in the ApiClient - which is in turn used by a DataService which abstracts away the web stuff, so theoretically we can have mock data services for design time...
