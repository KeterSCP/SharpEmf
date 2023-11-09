using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETPOLYFILLMODE"/>
[PublicAPI]
public record EmrSetPolyfillMode : EnhancedMetafileRecord, IEmfParsable<EmrSetPolyfillMode>
{
    /// <summary>
    /// Specifies the polygon fill mode
    /// </summary>
    public PolygonFillMode PolygonFillMode { get; }

    private EmrSetPolyfillMode(EmfRecordType Type, uint Size, PolygonFillMode polygonFillMode) : base(Type, Size)
    {
        PolygonFillMode = polygonFillMode;
    }

    public static EmrSetPolyfillMode Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var polygonFillMode = stream.ReadEnum<PolygonFillMode>();

        return new EmrSetPolyfillMode(recordType, size, polygonFillMode);
    }
}