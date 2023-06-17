using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Clipping;

/// <inheritdoc cref="EmfRecordType.EMR_INTERSECTCLIPRECT"/>
[PublicAPI]
public record EmrIntersectClipRect : EnhancedMetafileRecord, IEmfParsable<EmrIntersectClipRect>
{
    /// <summary>
    /// Specifies the rectangle in logical units
    /// </summary>
    /// <remarks>
    /// The lower and right edges of the specified rectangle are excluded from the clipping region
    /// </remarks>
    public RectL Clip { get; }

    private EmrIntersectClipRect(EmfRecordType recordType, uint size, RectL clip) : base(recordType, size)
    {
        Clip = clip;
    }

    public static EmrIntersectClipRect Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var clip = RectL.Parse(stream);
        return new EmrIntersectClipRect(recordType, size, clip);
    }
}