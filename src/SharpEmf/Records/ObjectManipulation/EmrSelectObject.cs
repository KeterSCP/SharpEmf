using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.ObjectManipulation;

/// <inheritdoc cref="EmfRecordType.EMR_SELECTOBJECT"/>
[PublicAPI]
public record EmrSelectObject : EnhancedMetafileRecord, IEmfParsable<EmrSelectObject>
{
    /// <summary>
    /// Specifies either the index of a graphics object in the EMF object table or the index of a stock object in the <see cref="StockObject"/> enumeration
    /// </summary>
    /// <remarks>
    /// The object index MUST NOT be zero, which is reserved and refers to the EMF metafile itself. <para />
    /// The object specified by this record MUST be used in subsequent EMF drawing operations, until another EMR_SELECTOBJECT record
    /// changes the object of that type or the object is deleted
    /// </remarks>
    public uint IHObject { get; }

    private EmrSelectObject(EmfRecordType Type, uint Size, uint ihObject) : base(Type, Size)
    {
        IHObject = ihObject;
    }

    public static EmrSelectObject Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var ihObject = stream.ReadUInt32();

        if (ihObject == 0)
        {
            throw new EmfParseException("Object index MUST NOT be zero");
        }

        return new EmrSelectObject(recordType, size, ihObject);
    }
}