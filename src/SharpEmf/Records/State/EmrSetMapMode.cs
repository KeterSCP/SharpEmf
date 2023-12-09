using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETMAPMODE"/>
[PublicAPI]
public record EmrSetMapMode : EnhancedMetafileRecord, IEmfParsable<EmrSetMapMode>
{
    public MapMode MapMode { get; }

    private EmrSetMapMode(EmfRecordType Type, uint Size, MapMode mapMode) : base(Type, Size)
    {
        MapMode = mapMode;
    }

    public static EmrSetMapMode Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var mapMode = stream.ReadEnum<MapMode>();
        return new EmrSetMapMode(recordType, size, mapMode);
    }
}