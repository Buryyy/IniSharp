# IniSharp
IniSharp is a lightweight .NET library for reading and writing INI configuration files. It provides both file-based persistence and in-memory configuration, allowing flexibility in different application scenarios.

## Features
- Read/Write INI Files: Easily retrieve and update configuration values in the standard INI format.
- Strong Type Binding: Map INI sections directly to your C# classes for ease of use and type safety.
- Flexible Indexer: Access configuration values directly with a simple syntax, config["Section:Key"].
- Concurrency support - Multiple reads from different instances to config file is supported.
- Exclusive Writes: When an instance writes to the INI file, it does so exclusively. This ensures that during a write operation, no other instance can read or write, preserving data integrity. Once the write is complete, other instances can resume reading or initiate their own writes.
  
## Installation
You can install this package on [NuGet](https://www.nuget.org/packages/IniSharpLite), any additional setup is not required. 

## Quickstart

1. **Setting Up Configuration: You can initialize your configuration from a given INI file path:**
```cs
var config = new Configuration("path_to_your_config.ini");
```
2.  **Reading Values:**

Using a function:

```cs
var theme = config.GetValue("Settings:Theme");
```

Or using the indexer:
```cs
var theme = config["Settings:Theme"];
```

3. **Updating values**
```cs
config["Settings:Theme"] = "Light";
config.SaveChanges(); // Persist changes to the file
```
4. **Binding to Strong Types:**

If you have a C# class Profile:

```cs
public class Profile
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```
You can bind an INI section directly to this class:

```cs
var profile = config.GetSection<Profile>("Profile");
Console.WriteLine(profile.Name);
Console.WriteLine(profile.Age);
```
