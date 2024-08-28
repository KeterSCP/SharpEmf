using SharpEmf.Extensions;

namespace SharpEmf.WmfTypes.Bitmap;

/// <summary>
/// Contains information about the dimensions and color format of a device-independent bitmap (DIB)
/// </summary>
public class BitmapInfoHeader
{
    /// <summary>
    /// Defines the size of this object, in bytes
    /// </summary>
    public uint Size { get; }

    /// <summary>
    /// Defines the width of the DIB, in pixels. This value MUST be positive
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Defines the height of the DIB, in pixels. This value MUST NOT be zero
    /// </summary>
    /// <remarks>
    /// If this value is positive, the bitmap is a bottom-up DIB and its origin is the lower-left corner.
    /// This field SHOULD specify the height of the decompressed image file, if the Compression value specifies JPEG or PNG format
    /// <br/><br/>
    /// If this value is negative, the image is a top-down DIB and its origin is the upper-left corner.
    /// Top-down bitmaps do not support compression
    /// </remarks>
    public int Height { get; }

    /// <summary>
    /// Defines the number of planes for the target device. This value MUST be 0x0001
    /// </summary>
    public ushort Planes { get; }

    /// <summary>
    /// Defines the number of bits that define each pixel and the maximum number of colors in the DIB
    /// </summary>
    public BitCount BitCount { get; }

    /// <summary>
    /// Defines the compression mode of the DIB
    /// </summary>
    public Compression Compression { get; }

    /// <summary>
    /// Defines the size, in bytes, of the image
    /// </summary>
    /// <remarks>
    /// If the <see cref="Compression"/> value is <see cref="Compression.BI_RGB"/>, this value SHOULD be zero and MUST be ignored
    /// <br/><br/>
    /// If the <see cref="Compression"/>; value is <see cref="Compression.BI_JPEG"/> or <see cref="Compression.BI_PNG"/>, this value MUST specify the size of the JPEG or PNG image buffer, respectively
    /// </remarks>
    public uint SizeImage { get; }

    /// <summary>
    /// Defines the horizontal resolution, in pixels-per-meter, of the target device for the DIB
    /// </summary>
    public int XPelsPerMeter { get; }

    /// <summary>
    /// Defines the vertical resolution, in pixels-per-meter, of the target device for the DIB
    /// </summary>
    public int YPelsPerMeter { get; }

    /// <summary>
    /// Specifies the number of indexes in the color table used by the DIB, as follows:
    ///
    /// <list type="bullet">
    ///
    /// <item>
    /// <description>If this value is zero, the DIB uses the maximum number of colors that correspond to the <see cref="BitCount"/> value</description>
    /// </item>
    ///
    /// <item>
    /// <description>If this value is nonzero and the <see cref="BitCount"/> value is less than 16, this value specifies the number of colors used by the DIB</description>
    /// </item>
    ///
    /// <item>
    /// <description>If this value is nonzero and the <see cref="BitCount"/> value is 16 or greater,
    /// this value specifies the size of the color table used to optimize performance of the system palette</description>
    /// </item>
    ///
    /// </list>
    /// </summary>
    public uint ColorUsed { get; }

    /// <summary>
    /// Defines the number of color indexes that are required for displaying the DIB
    /// <remarks>
    /// If this value is zero, all color indexes are required
    /// </remarks>
    /// </summary>
    public uint ColorImportant { get; }

    private BitmapInfoHeader(
        uint size,
        int width,
        int height,
        ushort planes,
        BitCount bitCount,
        Compression compression,
        uint sizeImage,
        int xPelsPerMeter,
        int yPelsPerMeter,
        uint colorUsed,
        uint colorImportant)
    {
        Size = size;
        Width = width;
        Height = height;
        Planes = planes;
        BitCount = bitCount;
        Compression = compression;
        SizeImage = sizeImage;
        XPelsPerMeter = xPelsPerMeter;
        YPelsPerMeter = yPelsPerMeter;
        ColorUsed = colorUsed;
        ColorImportant = colorImportant;
    }

    public static BitmapInfoHeader Parse(Stream stream)
    {
        var size = stream.ReadUInt32();
        var width = stream.ReadInt32();
        var height = stream.ReadInt32();
        var planes = stream.ReadUInt16();
        var bitCount = stream.ReadEnum<BitCount>();
        var compression = stream.ReadEnum<Compression>();
        var sizeImage = stream.ReadUInt32();
        var xPelsPerMeter = stream.ReadInt32();
        var yPelsPerMeter = stream.ReadInt32();
        var colorUsed = stream.ReadUInt32();
        var colorImportant = stream.ReadUInt32();

        return new BitmapInfoHeader(
            size,
            width,
            height,
            planes,
            bitCount,
            compression,
            sizeImage,
            xPelsPerMeter,
            yPelsPerMeter,
            colorUsed,
            colorImportant);
    }
}