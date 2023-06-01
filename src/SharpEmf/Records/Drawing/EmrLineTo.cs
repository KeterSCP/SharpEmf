using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_LINETO"/>
[PublicAPI]
public record EmrLineTo : EnhancedMetafileRecord, IEmfParsable<EmrLineTo>
{
    public override EmfRecordType Type => EmfRecordType.EMR_LINETO;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the coordinates of the line's endpoint
    /// </summary>
    public PointL Point { get; }

    private EmrLineTo(uint size, PointL point)
    {
        Size = size;
        Point = point;
    }

    public static EmrLineTo Parse(Stream stream, uint size)
    {
        var point = PointL.Parse(stream);

        return new EmrLineTo(size, point);
    }
}