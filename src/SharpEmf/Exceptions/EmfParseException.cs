using JetBrains.Annotations;

namespace SharpEmf.Exceptions;

[PublicAPI]
public class EmfParseException : Exception
{
    public EmfParseException(string message) : base(message)
    {
    }

    public EmfParseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}