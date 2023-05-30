using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ELLIPSE"/>
[PublicAPI]
public record EmrEllipse : EnhancedMetafileRecord, IEmfParsable<EmrEllipse>
{
    public override EmfRecordType Type => EmfRecordType.EMR_ELLIPSE;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    private EmrEllipse(uint size, RectL box)
    {
        Size = size;
        Box = box;
    }

    public static EmrEllipse Parse(Stream stream, uint size)
    {
        var box = new RectL(
            left: stream.ReadInt32(),
            top: stream.ReadInt32(),
            right: stream.ReadInt32(),
            bottom: stream.ReadInt32());

        return new EmrEllipse(size, box);
    }
}