using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <summary>
/// Specifies a line from the current drawing position up to, but not including, the specified point.
/// It resets the current position to the specified point
/// </summary>
[PublicAPI]
public record EmrLineto : EnhancedMetafileRecord, IEmfParsable<EmrLineto>
{
    public override EmfRecordType Type => EmfRecordType.EMR_LINETO;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the coordinates of the line's endpoint
    /// </summary>
    public PointL Point { get; }

    private EmrLineto(uint size, PointL point)
    {
        Size = size;
        Point = point;
    }

    public static EmrLineto Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var point = new PointL(
            x: stream.ReadInt32(),
            y: stream.ReadInt32());

        return new EmrLineto(size, point);
    }
}