using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Clipping;

/// <inheritdoc cref="EmfRecordType.EMR_OFFSETCLIPRGN"/>
[PublicAPI]
public record EmrOffsetClipRgn : EnhancedMetafileRecord, IEmfParsable<EmrOffsetClipRgn>
{
    /// <summary>
    /// Specifies the horizontal and vertical offsets in logical units
    /// </summary>
    public PointL Offset { get; }

    private EmrOffsetClipRgn(EmfRecordType recordType, uint size, PointL offset) : base(recordType, size)
    {
        Offset = offset;
    }

    public static EmrOffsetClipRgn Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var offset = PointL.Parse(stream);
        return new EmrOffsetClipRgn(recordType, size, offset);
    }
}