using System;
using System.Collections.Generic;

namespace IniSharpLite
{
    public class Configuration : IConfiguration
    {
        private readonly IParser _parser;
        private const char KeySeparator = ':';

        /// <param name="iniPath">Path to your .ini file.</param>
        /// <param name="useInMemory">toggle off ONLY IF you .ini file is ridiculously big, 
        /// or if the load time of this instance is a problem.</param>
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
        /// <summary>
        /// Get a value from given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Persist your changes to the .ini file.
        /// </summary>
        public void SaveChanges()
        {
            _parser.SaveAllChanges();
        }

        /// <summary>
        /// Set a value by given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, string value)
        {
            var (section, keyValue) = ParseKey(key);
            _parser.SetValue(section, keyValue, value);
        }

        /// <param name="sectionName">The section name within the .ini file.</param>
        /// <returns>Key value dictionary of contents of the section from the .ini file.</returns>
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
