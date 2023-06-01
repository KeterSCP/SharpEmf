using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_FILLPATH"/>
[PublicAPI]
public record EmrFillpath : EnhancedMetafileRecord, IEmfParsable<EmrFillpath>
{
    public override EmfRecordType Type => EmfRecordType.EMR_FILLPATH;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrFillpath(uint size, RectL bounds)
    {
        Size = size;
        Bounds = bounds;
    }

    public static EmrFillpath Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);

        return new EmrFillpath(size, bounds);
    }
}