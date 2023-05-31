using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
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
        var bounds = new RectL(
            left: stream.ReadInt32(),
            top: stream.ReadInt32(),
            right: stream.ReadInt32(),
            bottom: stream.ReadInt32());

        return new EmrFillpath(size, bounds);
    }
}