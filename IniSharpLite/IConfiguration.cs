using System.Collections.Generic;

namespace IniSharpLite
{
    public interface IConfiguration
    {
        string GetValue(string key);
        string this[string key] { get; set; }
        void SaveChanges();
        void SetValue(string key, string value);
        IDictionary<string, string> GetSection(string sectionName);
        T GetSection<T>(string sectionName) where T : new();
    }

}
