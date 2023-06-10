using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ELLIPSE"/>
[PublicAPI]
public record EmrEllipse : EnhancedMetafileRecord, IEmfParsable<EmrEllipse>
{
    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Box { get; }

    private EmrEllipse(EmfRecordType recordType, uint size, RectL box) : base(recordType, size)
    {
        Box = box;
    }

    public static EmrEllipse Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var box = RectL.Parse(stream);

        return new EmrEllipse(recordType, size, box);
    }
}