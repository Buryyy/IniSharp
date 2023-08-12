using System;

namespace IniSharp
{
    public class IniParseException : Exception
    {
        public IniParseException(int lineNum, string message)
           : base($"Error at line {lineNum}: {message}") { }
    }

    public class MissingSectionHeaderException : IniParseException
    {
        public MissingSectionHeaderException(int lineNum)
            : base(lineNum, "Missing section header.") { }
    }

    public class InvalidSectionHeaderException : IniParseException
    {
        public InvalidSectionHeaderException(int lineNum)
            : base(lineNum, "Invalid section header format.") { }
    }

    public class InvalidKeyValuePairException : IniParseException
    {
        public InvalidKeyValuePairException(int lineNum)
            : base(lineNum, "Invalid key-value pair format.") { }
    }
}
