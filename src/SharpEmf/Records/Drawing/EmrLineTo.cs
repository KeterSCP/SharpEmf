using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_LINETO"/>
[PublicAPI]
public record EmrLineTo : EnhancedMetafileRecord, IEmfParsable<EmrLineTo>
{
    /// <summary>
    /// Specifies the coordinates of the line's endpoint
    /// </summary>
    public PointL Point { get; }

    private EmrLineTo(EmfRecordType recordType, uint size, PointL point) : base(recordType, size)
    {
        Point = point;
    }

    public static EmrLineTo Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var point = PointL.Parse(stream);

        return new EmrLineTo(recordType, size, point);
    }
}