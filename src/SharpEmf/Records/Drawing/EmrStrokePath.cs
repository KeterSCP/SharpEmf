using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_STROKEPATH"/>
[PublicAPI]
public record EmrStrokePath : EnhancedMetafileRecord, IEmfParsable<EmrStrokePath>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrStrokePath(EmfRecordType recordType, uint size, RectL bounds) : base(recordType, size)
    {
        Bounds = bounds;
    }

    public static EmrStrokePath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        return new EmrStrokePath(recordType, size, bounds);
    }
}