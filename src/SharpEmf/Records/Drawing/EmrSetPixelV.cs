using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_SETPIXELV"/>
[PublicAPI]
public record EmrSetPixelV : EnhancedMetafileRecord, IEmfParsable<EmrSetPixelV>
{
    public override EmfRecordType Type => EmfRecordType.EMR_SETPIXELV;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the logical coordinates for the pixel
    /// </summary>
    public PointL Pixel { get; }

    /// <summary>
    /// Specifies the <see cref="Pixel"/> color
    /// </summary>
    public ColorRef Color { get; }

    private EmrSetPixelV(uint size, PointL pixel, ColorRef color)
    {
        Size = size;
        Pixel = pixel;
        Color = color;
    }

    public static EmrSetPixelV Parse(Stream stream, uint size)
    {
        var pixel = PointL.Parse(stream);

        var color = ColorRef.Parse(stream);

        return new EmrSetPixelV(size, pixel, color);
    }
}