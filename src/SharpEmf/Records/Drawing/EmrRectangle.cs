using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_RECTANGLE"/>
[PublicAPI]
public record EmrRectangle : EnhancedMetafileRecord, IEmfParsable<EmrRectangle>
{
    /// <summary>
    /// Specifies the inclusive-inclusive rectangle to draw
    /// </summary>
    public RectL Box { get; }

    private EmrRectangle(EmfRecordType recordType, uint size, RectL box) : base(recordType, size)
    {
        Box = box;
    }

    public static EmrRectangle Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var box = RectL.Parse(stream);

        return new EmrRectangle(recordType, size, box);
    }
}