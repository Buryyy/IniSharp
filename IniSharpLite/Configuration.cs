using System;
using System.Collections.Generic;

namespace IniSharpLite
{
    public class Configuration : IConfiguration
    {
        private readonly IParser _parser;
        private const char KeySeparator = ':';

        public Configuration(string iniPath, bool useInMemory = true)
        {
            _parser = new Parser(iniPath, useInMemory);
        }

        private (string section, string key) ParseKey(string compositeKey)
        {
            var segments = compositeKey.Split(KeySeparator);
            if (segments.Length != 2 || string.IsNullOrWhiteSpace(segments[0]) || string.IsNullOrWhiteSpace(segments[1]))
            {
                throw new ArgumentException($"Key must be in the format 'Section{KeySeparator}Key' and neither section nor key should be empty.", nameof(compositeKey));
            }

            return (segments[0].Trim(), segments[1].Trim());
        }

        public string GetValue(string key)
        {
            var (section, keyValue) = ParseKey(key);
            return _parser.GetValue(section, keyValue);
        }

        public string this[string key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        public void SaveChanges()
        {
            _parser.SaveAllChanges();
        }

        public void SetValue(string key, string value)
        {
            var (section, keyValue) = ParseKey(key);
            _parser.SetValue(section, keyValue, value);
        }

        public IDictionary<string, string> GetSection(string sectionName)
        {
            return _parser.GetSection(sectionName);
        }

        public T GetSection<T>(string sectionName) where T : new()
        {
            var sectionData = GetSection(sectionName);
            var result = new T();

            foreach (var property in typeof(T).GetProperties())
            {
                if (sectionData.ContainsKey(property.Name))
                {
                    try
                    {
                        property.SetValue(result, Convert.ChangeType(sectionData[property.Name], property.PropertyType));
                    }
                    catch (InvalidCastException)
                    {
                        throw new InvalidOperationException($"Failed to convert the value '{sectionData[property.Name]}' in section '{sectionName}' for property '{property.Name}' of type '{property.PropertyType.Name}'.");
                    }
                }
            }

            return result;
        }
    }
}
