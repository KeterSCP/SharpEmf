using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Bitmap;

/// <inheritdoc cref="EmfRecordType.EMR_BITBLT"/>
[PublicAPI]
public record EmrBitBlt : EnhancedMetafileRecord, IEmfParsable<EmrBitBlt>
{
    /// <summary>
    /// Specifies the destination bounding rectangle in logical coordinates
    /// </summary>
    /// <remarks>
    /// If the intersection of this rectangle with the current clipping regions in the playback device context is empty,
    /// this record has no effect
    /// </remarks>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the logical x-coordinate of the upper-left corner of the destination rectangle
    /// </summary>
    public int XDest { get; }

    /// <summary>
    /// Specifies the logical y-coordinate of the upper-left corner of the destination rectangle
    /// </summary>
    public int YDest { get; }

    /// <summary>
    /// Specifies the logical width of the source and destination rectangles
    /// </summary>
    public int CXDest { get; }

    /// <summary>
    /// Specifies the logical height of the source and destination rectangles
    /// </summary>
    public int CYDest { get; }

    /// <summary>
    /// Specifies the raster operation code
    /// </summary>
    /// <remarks>
    /// This code defines how the color data of the source rectangle is to be combined with the color data of
    /// the destination rectangle and optionally a brush pattern, to achieve the final color
    /// </remarks>
    public TernaryRasterOperation BitBltRasterOperation { get; }

    /// <summary>
    /// Specifies the logical x-coordinate of the upper-left corner of the source rectangle
    /// </summary>
    public int XSrc { get; }

    /// <summary>
    /// Specifies the logical y-coordinate of the upper-left corner of the source rectangle
    /// </summary>
    public int YSrc { get; }

    /// <summary>
    /// Specifies a world-space to page-space transform to apply to the source bitmap
    /// </summary>
    public XForm XFormSrc { get; }

    /// <summary>
    /// Specifies the background color of the source bitmap
    /// </summary>
    public ColorRef BkColorSrc { get; }

    /// <summary>
    /// Specifies how to interpret values in the color table in the source bitmap header
    /// </summary>
    public DIBColors UsageSrc { get; }

    /// <summary>
    /// Specifies the offset in bytes, from the start of this record to the source bitmap header
    /// </summary>
    public uint OffBmiSrc { get; }

    /// <summary>
    /// Specifies the size in bytes, of the source bitmap header
    /// </summary>
    public uint CbBmiSrc { get; }

    /// <summary>
    /// Specifies the offset in bytes, from the start of this record to the source bitmap bits
    /// </summary>
    public uint OffBitsSrc { get; }

    /// <summary>
    /// Specifies the size in bytes, of the source bitmap bits
    /// </summary>
    public uint CbBitsSrc { get; }

    /// <summary>
    /// The source bitmap header
    /// </summary>
    public IReadOnlyList<byte> BmiSrc { get; }

    /// <summary>
    /// The source bitmap bits
    /// </summary>
    public IReadOnlyList<byte> BitsSrc { get; }

    private EmrBitBlt(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        int xDest,
        int yDest,
        int cxDest,
        int cyDest,
        TernaryRasterOperation bitBltRasterOperation,
        int xSrc,
        int ySrc,
        XForm xFormSrc,
        ColorRef bkColorSrc,
        DIBColors usageSrc,
        uint offBmiSrc,
        uint cbBmiSrc,
        uint offBitsSrc,
        uint cbBitsSrc,
        IReadOnlyList<byte> bmiSrc,
        IReadOnlyList<byte> bitsSrc) : base(recordType, size)
    {
        Bounds = bounds;
        XDest = xDest;
        YDest = yDest;
        CXDest = cxDest;
        CYDest = cyDest;
        BitBltRasterOperation = bitBltRasterOperation;
        XSrc = xSrc;
        YSrc = ySrc;
        XFormSrc = xFormSrc;
        BkColorSrc = bkColorSrc;
        UsageSrc = usageSrc;
        OffBmiSrc = offBmiSrc;
        CbBmiSrc = cbBmiSrc;
        OffBitsSrc = offBitsSrc;
        CbBitsSrc = cbBitsSrc;
        BmiSrc = bmiSrc;
        BitsSrc = bitsSrc;
    }

    public static EmrBitBlt Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var xDest = stream.ReadInt32();
        var yDest = stream.ReadInt32();
        var cxDest = stream.ReadInt32();
        var cyDest = stream.ReadInt32();
        var bitBltRasterOperation = stream.ReadEnum<TernaryRasterOperation>();
        var xSrc = stream.ReadInt32();
        var ySrc = stream.ReadInt32();
        var xFormSrc = XForm.Parse(stream);
        var bkColorSrc = ColorRef.Parse(stream);
        var usageSrc = stream.ReadEnum<DIBColors>();
        var offBmiSrc = stream.ReadUInt32();
        var cbBmiSrc = stream.ReadUInt32();
        var offBitsSrc = stream.ReadUInt32();
        var cbBitsSrc = stream.ReadUInt32();

        var selfSizeWithoutBuffers =
            Unsafe.SizeOf<RectL>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<TernaryRasterOperation>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<XForm>() +
            Unsafe.SizeOf<ColorRef>() +
            Unsafe.SizeOf<DIBColors>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>();

        var seekOffset = offBmiSrc - selfSizeWithoutBuffers;
        stream.Seek(seekOffset, SeekOrigin.Current);

        var bmiSrc = stream.ReadByteArray((int)cbBmiSrc);

        seekOffset = offBitsSrc - (seekOffset + bmiSrc.Length);
        stream.Seek(seekOffset, SeekOrigin.Current);

        var bitsSrc = stream.ReadByteArray((int)cbBitsSrc);

        return new EmrBitBlt(
            recordType,
            size,
            bounds,
            xDest,
            yDest,
            cxDest,
            cyDest,
            bitBltRasterOperation,
            xSrc,
            ySrc,
            xFormSrc,
            bkColorSrc,
            usageSrc,
            offBmiSrc,
            cbBmiSrc,
            offBitsSrc,
            cbBitsSrc,
            bmiSrc,
            bitsSrc);
    }
}