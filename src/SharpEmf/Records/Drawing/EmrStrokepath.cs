using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_STROKEPATH"/>
[PublicAPI]
public record EmrStrokepath : EnhancedMetafileRecord, IEmfParsable<EmrStrokepath>
{
    public override EmfRecordType Type => EmfRecordType.EMR_STROKEPATH;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    private EmrStrokepath(uint size, RectL bounds)
    {
        Size = size;
        Bounds = bounds;
    }

    public static EmrStrokepath Parse(Stream stream, uint size)
    {
        var bounds = new RectL(
            left: stream.ReadInt32(),
            top: stream.ReadInt32(),
            right: stream.ReadInt32(),
            bottom: stream.ReadInt32());

        return new EmrStrokepath(size, bounds);
    }
}