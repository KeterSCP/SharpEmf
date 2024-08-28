using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.ObjectManipulation;

/// <inheritdoc cref="EmfRecordType.EMR_DELETEOBJECT"/>
[PublicAPI]
public record EmrDeleteObject : EnhancedMetafileRecord, IEmfParsable<EmrDeleteObject>
{
    /// <summary>
    /// Specifies the index of a graphics object in the EMF object table
    /// </summary>
    /// <remarks>
    /// This value MUST NOT be 0, which is a reserved index that refers to the EMF metafile itself;
    /// and it MUST NOT be the index of a <see cref="StockObject"/>, which cannot be deleted
    /// </remarks>
    public uint IHObject { get; }

    private EmrDeleteObject(EmfRecordType Type, uint Size, uint ihObject) : base(Type, Size)
    {
        IHObject = ihObject;
    }

    public static EmrDeleteObject Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var ihObject = stream.ReadUInt32();

        if (ihObject == 0)
        {
            throw new EmfParseException("Object index MUST NOT be zero");
        }

        if (Enum.IsDefined(typeof(StockObject), ihObject))
        {
            throw new EmfParseException("Object index MUST NOT be a stock object");
        }

        return new EmrDeleteObject(recordType, size, ihObject);
    }
}