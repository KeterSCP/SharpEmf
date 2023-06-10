using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_FILLPATH"/>
[PublicAPI]
public record EmrFillPath : EnhancedMetafileRecord, IEmfParsable<EmrFillPath>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrFillPath(EmfRecordType recordType, uint size, RectL bounds) : base(recordType, size)
    {
        Bounds = bounds;
    }

    public static EmrFillPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);

        return new EmrFillPath(recordType, size, bounds);
    }
}