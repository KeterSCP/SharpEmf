using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_STROKEPATH"/>
[PublicAPI]
public record EmrStrokePath : EnhancedMetafileRecord, IEmfParsable<EmrStrokePath>
{
    public override EmfRecordType Type => EmfRecordType.EMR_STROKEPATH;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrStrokePath(uint size, RectL bounds)
    {
        Size = size;
        Bounds = bounds;
    }

    public static EmrStrokePath Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);

        return new EmrStrokePath(size, bounds);
    }
}