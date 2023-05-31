using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_RECTANGLE"/>
[PublicAPI]
public record EmrRectangle : EnhancedMetafileRecord, IEmfParsable<EmrRectangle>
{
    public override EmfRecordType Type => EmfRecordType.EMR_RECTANGLE;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive rectangle to draw
    /// </summary>
    public RectL Box { get; }

    private EmrRectangle(uint size, RectL box)
    {
        Size = size;
        Box = box;
    }

    public static EmrRectangle Parse(Stream stream, uint size)
    {
        var box = new RectL(
            left: stream.ReadInt32(),
            top: stream.ReadInt32(),
            right: stream.ReadInt32(),
            bottom: stream.ReadInt32());

        return new EmrRectangle(size, box);
    }
}