using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Clipping;

/// <inheritdoc cref="EmfRecordType.EMR_SELECTCLIPPATH"/>
[PublicAPI]
public record EmrSelectClipPath : EnhancedMetafileRecord, IEmfParsable<EmrSelectClipPath>
{
    /// <summary>
    /// Specifies how to combine the current clipping region with the current path bracket
    /// </summary>
    public RegionMode RegionMode { get; }

    private EmrSelectClipPath(EmfRecordType recordType, uint size, RegionMode regionMode) : base(recordType, size)
    {
        RegionMode = regionMode;
    }

    public static EmrSelectClipPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var regionMode = stream.ReadEnum<RegionMode>();
        return new EmrSelectClipPath(recordType, size, regionMode);
    }
}