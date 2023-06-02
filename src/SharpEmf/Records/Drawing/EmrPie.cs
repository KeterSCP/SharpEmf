using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_PIE"/>
[PublicAPI]
public record EmrPie : EnhancedMetafileRecord, IEmfParsable<EmrPie>
{
    public override EmfRecordType Type => EmfRecordType.EMR_PIE;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the endpoint of the first radial
    /// </summary>
    public PointL Start { get; }

    /// <summary>
    /// Specifies the coordinates, in logical units, of the endpoint of the second radial
    /// </summary>
    public PointL End { get; }

    private EmrPie(uint size, RectL box, PointL start, PointL end)
    {
        Size = size;
        Box = box;
        Start = start;
        End = end;
    }

    public static EmrPie Parse(Stream stream, uint size)
    {
        var box = RectL.Parse(stream);
        var start = PointL.Parse(stream);
        var end = PointL.Parse(stream);

        return new EmrPie(size, box, start, end);
    }
}