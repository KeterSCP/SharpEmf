using SharpEmf.Enums;

namespace SharpEmf.Interfaces;

/// <summary>
/// Provides static abstract method for parsing EMF record
/// </summary>
/// <typeparam name="T">Type of EMF record to be parsed</typeparam>
internal interface IEmfParsable<out T> where T : IEmfParsable<T>
{
    internal static abstract T Parse(Stream stream, EmfRecordType recordType, uint size);
}