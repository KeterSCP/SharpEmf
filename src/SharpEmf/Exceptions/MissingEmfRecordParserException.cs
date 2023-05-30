using SharpEmf.Enums;

namespace SharpEmf.Exceptions;

/// <summary>
/// Thrown when a record parser is missing
/// </summary>
internal class MissingEmfRecordParserException : Exception
{
    public MissingEmfRecordParserException(EmfRecordType recordType) : base($"Missing parser for record type {recordType}")
    {
    }
}