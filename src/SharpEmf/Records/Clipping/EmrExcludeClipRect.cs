using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Clipping;

/// <inheritdoc cref="EmfRecordType.EMR_EXCLUDECLIPRECT"/>
[PublicAPI]
public record EmrExcludeClipRect : EnhancedMetafileRecord, IEmfParsable<EmrExcludeClipRect>
{
    /// <summary>
    /// Specifies a rectangle in logical units
    /// </summary>
    public RectL Clip { get; }

    private EmrExcludeClipRect(EmfRecordType recordType, uint size, RectL clip) : base(recordType, size)
    {
        Clip = clip;
    }

    public static EmrExcludeClipRect Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var clip = RectL.Parse(stream);
        return new EmrExcludeClipRect(recordType, size, clip);
    }
}