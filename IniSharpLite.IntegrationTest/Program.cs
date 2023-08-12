using IniSharpLite;
using IniSharpLite.IntegrationTest;


// Assuming the INI file is named "config.ini" and is in the same directory as the executable.
string filePath = "C:\\test\\config.ini";

// Create or overwrite the INI file with sample data
System.IO.File.WriteAllLines(filePath, new string[]
{
                "[Settings]",
                "Theme=Dark",
                "AutoSave=True",
                "",
                "[Profile]",
                "Name=John",
                "Age=30"
});

IConfiguration config = new Configuration(filePath);

// 1. Reading values from the INI file
Console.WriteLine("Read using GetValue:");
Console.WriteLine($"Theme: {config.GetValue("Settings:Theme")}");
Console.WriteLine($"Name: {config.GetValue("Profile:Name")}");

// 2. Modify a value
config.SetValue("Profile:Name", "Jane");
Console.WriteLine("\nModified Name:");
Console.WriteLine($"Name: {config.GetValue("Profile:Name")}");

// 3. Use the indexer to get and set values
Console.WriteLine("\nUsing indexer:");
Console.WriteLine($"AutoSave: {config["Settings:AutoSave"]}");
config["Settings:AutoSave"] = "False";
Console.WriteLine($"Modified AutoSave: {config["Settings:AutoSave"]}");

// 4. Bind a section to a strong type
var profile = config.GetSection<Profile>("Profile");
Console.WriteLine("\nBind to strong type:");
Console.WriteLine($"Name: {profile.Name}");
Console.WriteLine($"Age: {profile.Age}");

// 5. Writing values and saving them.
config.SetValue("Profile:Name", "John");
config["Profile:Age"] = "35";
config.SaveChanges();  // This will save all changes at once.
Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();

/** Ini file output:
[Settings]
Theme=Dark
AutoSave=False

[Profile]
Name=John
Age=35
**/