---
id: 37
number: 37
title: "Working with Files, Streams, and Serialization"
description: Master file system operations, stream-based I/O, text encoding, and object serialization in .NET 10.
category: DataAccess
topics:
  - File system management
  - Reading and writing files
  - Streams and buffers
  - Text encoding
  - JSON serialization
  - XML serialization
---

## Step 1: Managing the File System
**Type: Read**

Navigate and manipulate files and directories:

```csharp
using System.IO;

// Working with paths
string path = Path.Combine("folder", "subfolder", "file.txt");
string fileName = Path.GetFileName(path);           // "file.txt"
string extension = Path.GetExtension(path);         // ".txt"
string directory = Path.GetDirectoryName(path);     // "folder/subfolder"
string fullPath = Path.GetFullPath(path);           // Absolute path

// Special folders
string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string temp = Path.GetTempPath();

// Directory operations
Directory.CreateDirectory("MyFolder");
bool exists = Directory.Exists("MyFolder");
string[] files = Directory.GetFiles(".", "*.txt");
string[] directories = Directory.GetDirectories(".");

// Enumerate files (memory efficient for large directories)
foreach (string file in Directory.EnumerateFiles(".", "*.cs", SearchOption.AllDirectories))
{
    Console.WriteLine(file);
}

// File operations
File.Copy("source.txt", "destination.txt");
File.Move("old.txt", "new.txt");
File.Delete("unwanted.txt");
bool fileExists = File.Exists("myfile.txt");

// File information
var fileInfo = new FileInfo("myfile.txt");
Console.WriteLine($"Size: {fileInfo.Length} bytes");
Console.WriteLine($"Created: {fileInfo.CreationTime}");
Console.WriteLine($"Modified: {fileInfo.LastWriteTime}");
```

---

## Step 2: Reading and Writing Files
**Type: Action**

Simple file I/O operations:

```csharp
// Write all text at once
string content = "Hello, World!";
await File.WriteAllTextAsync("hello.txt", content);

// Read all text at once
string text = await File.ReadAllTextAsync("hello.txt");

// Write all lines
string[] lines = ["Line 1", "Line 2", "Line 3"];
await File.WriteAllLinesAsync("lines.txt", lines);

// Read all lines
string[] readLines = await File.ReadAllLinesAsync("lines.txt");

// Append to file
await File.AppendAllTextAsync("log.txt", $"{DateTime.Now}: Event occurred\n");

// Read bytes (binary files)
byte[] bytes = await File.ReadAllBytesAsync("image.png");
await File.WriteAllBytesAsync("copy.png", bytes);

// Read lines efficiently with IAsyncEnumerable
await foreach (string line in File.ReadLinesAsync("large-file.txt"))
{
    Console.WriteLine(line);
    // Process line by line - doesn't load entire file into memory
}
```

---

## Step 3: Streams
**Type: Read**

Use streams for efficient I/O:

```csharp
// FileStream for low-level control
await using var fs = new FileStream("data.bin", FileMode.Create, FileAccess.Write);
byte[] data = [0x48, 0x65, 0x6C, 0x6C, 0x6F];  // "Hello" in bytes
await fs.WriteAsync(data);

// StreamWriter for text
await using var writer = new StreamWriter("output.txt");
await writer.WriteLineAsync("First line");
await writer.WriteLineAsync("Second line");

// StreamReader for reading text
using var reader = new StreamReader("output.txt");
while (!reader.EndOfStream)
{
    string? line = await reader.ReadLineAsync();
    Console.WriteLine(line);
}

// MemoryStream for in-memory operations
using var memory = new MemoryStream();
await memory.WriteAsync(data);
memory.Position = 0;  // Reset to beginning
byte[] buffer = new byte[memory.Length];
await memory.ReadAsync(buffer);

// Copying streams
await using var source = File.OpenRead("source.txt");
await using var dest = File.Create("dest.txt");
await source.CopyToAsync(dest);

// BufferedStream for performance
await using var buffered = new BufferedStream(
    new FileStream("large.dat", FileMode.Open), 
    bufferSize: 4096);
```

---

## Step 4: Text Encoding
**Type: Read**

Handle different text encodings:

```csharp
using System.Text;

// Common encodings
Encoding utf8 = Encoding.UTF8;        // Most common
Encoding ascii = Encoding.ASCII;      // 7-bit ASCII
Encoding unicode = Encoding.Unicode;  // UTF-16 (Windows default)

// Convert string to bytes
string text = "Hello, 世界! 🌍";
byte[] utf8Bytes = Encoding.UTF8.GetBytes(text);
byte[] utf16Bytes = Encoding.Unicode.GetBytes(text);

Console.WriteLine($"UTF-8: {utf8Bytes.Length} bytes");    // 18 bytes
Console.WriteLine($"UTF-16: {utf16Bytes.Length} bytes");  // 22 bytes

// Convert bytes to string
string decoded = Encoding.UTF8.GetString(utf8Bytes);

// Read file with specific encoding
string content = await File.ReadAllTextAsync("file.txt", Encoding.UTF8);

// Write with specific encoding
await File.WriteAllTextAsync("file.txt", content, Encoding.UTF8);

// Detect encoding with BOM
await using var stream = File.OpenRead("unknown.txt");
using var reader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true);
string text2 = await reader.ReadToEndAsync();
Console.WriteLine($"Detected: {reader.CurrentEncoding.WebName}");
```

---

## Step 5: JSON Serialization
**Type: Action**

Serialize objects to/from JSON:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

// Define a model
public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public List<string> Hobbies { get; set; } = [];
    
    [JsonIgnore]
    public string Secret { get; set; } = "";
    
    [JsonPropertyName("email_address")]
    public string Email { get; set; } = "";
}

// Serialize to JSON
var person = new Person 
{ 
    Name = "Alice", 
    Age = 30, 
    Hobbies = ["Reading", "Gaming"],
    Email = "alice@example.com"
};

string json = JsonSerializer.Serialize(person, new JsonSerializerOptions 
{ 
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});
Console.WriteLine(json);

// Deserialize from JSON
Person? loaded = JsonSerializer.Deserialize<Person>(json);

// Save to file
await File.WriteAllTextAsync("person.json", json);

// Load from file
string fileJson = await File.ReadAllTextAsync("person.json");
Person? fromFile = JsonSerializer.Deserialize<Person>(fileJson);

// Source generators for AOT (faster, no reflection)
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(List<Person>))]
public partial class AppJsonContext : JsonSerializerContext { }

// Usage with source generator
json = JsonSerializer.Serialize(person, AppJsonContext.Default.Person);
```

---

## Step 6: Working with Different Formats
**Type: Read**

Handle XML and other formats:

```csharp
using System.Xml.Serialization;
using System.Xml.Linq;

// XML Serialization
[XmlRoot("Person")]
public class XmlPerson
{
    [XmlElement("FullName")]
    public string Name { get; set; } = "";
    
    [XmlAttribute("age")]
    public int Age { get; set; }
    
    [XmlArray("Hobbies")]
    [XmlArrayItem("Hobby")]
    public List<string> Hobbies { get; set; } = [];
}

// Serialize to XML
var xmlPerson = new XmlPerson { Name = "Bob", Age = 25, Hobbies = ["Music"] };
var serializer = new XmlSerializer(typeof(XmlPerson));

await using var xmlStream = new MemoryStream();
serializer.Serialize(xmlStream, xmlPerson);
string xml = Encoding.UTF8.GetString(xmlStream.ToArray());

// LINQ to XML (more flexible)
var doc = new XDocument(
    new XElement("People",
        new XElement("Person",
            new XAttribute("id", 1),
            new XElement("Name", "Alice"),
            new XElement("Age", 30)
        ),
        new XElement("Person",
            new XAttribute("id", 2),
            new XElement("Name", "Bob"),
            new XElement("Age", 25)
        )
    )
);

doc.Save("people.xml");

// Query XML
var people = doc.Descendants("Person")
    .Select(p => new 
    {
        Id = (int)p.Attribute("id")!,
        Name = (string)p.Element("Name")!,
        Age = (int)p.Element("Age")!
    });

foreach (var p in people)
{
    Console.WriteLine($"{p.Name} (ID: {p.Id}) is {p.Age}");
}
```

---

## Quiz

### Question 1
Which method reads an entire file without loading it all into memory?
- File.ReadAllText
- File.ReadAllLines
- File.ReadLines (or ReadLinesAsync)
- File.ReadAllBytes

**Correct: 2**
**Explanation:** File.ReadLines returns an IEnumerable that yields lines one at a time, not loading the entire file into memory.

### Question 2
What encoding is most commonly used for text files?
- ASCII
- UTF-8
- UTF-16
- Latin-1

**Correct: 1**
**Explanation:** UTF-8 is the most widely used encoding because it's compact for ASCII text while supporting all Unicode characters.

### Question 3
Which attribute prevents a property from being serialized to JSON?
- [JsonExclude]
- [JsonIgnore]
- [NotSerialized]
- [SkipJson]

**Correct: 1**
**Explanation:** The [JsonIgnore] attribute tells the serializer to skip that property when converting to/from JSON.
