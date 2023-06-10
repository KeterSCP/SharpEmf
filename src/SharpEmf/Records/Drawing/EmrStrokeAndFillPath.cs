using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_STROKEANDFILLPATH"/>
[PublicAPI]
public record EmrStrokeAndFillPath : EnhancedMetafileRecord, IEmfParsable<EmrStrokeAndFillPath>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrStrokeAndFillPath(EmfRecordType recordType, uint size, RectL bounds) : base(recordType, size)
    {
        Bounds = bounds;
    }

    public static EmrStrokeAndFillPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);

        return new EmrStrokeAndFillPath(recordType, size, bounds);
    }
}