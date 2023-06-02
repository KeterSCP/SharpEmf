using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_STROKEANDFILLPATH"/>
[PublicAPI]
public record EmrStrokeAndFillPath : EnhancedMetafileRecord, IEmfParsable<EmrStrokeAndFillPath>
{
    public override EmfRecordType Type => EmfRecordType.EMR_STROKEANDFILLPATH;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrStrokeAndFillPath(uint size, RectL bounds)
    {
        Size = size;
        Bounds = bounds;
    }

    public static EmrStrokeAndFillPath Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);

        return new EmrStrokeAndFillPath(size, bounds);
    }
}