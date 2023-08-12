using System.Collections.Generic;

namespace IniSharp
{
    public interface IParser
    {
        string GetValue(string section, string key);
        IDictionary<string, string> GetSection(string section);
        void SetValue(string section, string key, string value);
        void SaveAllChanges();
    }
}
