using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Bitmap;

/// <inheritdoc cref="EmfRecordType.EMR_STRETCHDIBITS"/>
[PublicAPI]
public record EmrStretchDiBits : EnhancedMetafileRecord
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
    /// Specifies the logical x-coordinate of the upper-left corner of the source rectangle
    /// </summary>
    public int XSrc { get; }

    /// <summary>
    /// Specifies the logical y-coordinate of the upper-left corner of the source rectangle
    /// </summary>
    public int YSrc { get; }

    /// <summary>
    /// Specifies the width in pixels of the source rectangle
    /// </summary>
    public int CXSrc { get; }

    /// <summary>
    /// Specifies the height in pixels of the source rectangle
    /// </summary>
    public int CYSrc { get; }

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
    /// Specifies how to interpret values in the color table in the source bitmap header
    /// </summary>
    public DIBColors UsageSrc { get; }

    /// <summary>
    /// Specifies the raster operation code
    /// </summary>
    /// <remarks>
    /// This code defines how the color data of the source rectangle is to be combined with the color data of
    /// the destination rectangle and optionally a brush pattern, to achieve the final color
    /// </remarks>
    public TernaryRasterOperation BitBltRasterOperation { get; }

    /// <summary>
    /// Specifies the logical width of the destination rectangle
    /// </summary>
    public int CXDest { get; }

    /// <summary>
    /// Specifies the logical height of the destination rectangle
    /// </summary>
    public int CYDest { get; }

    /// <summary>
    /// The source bitmap header
    /// </summary>
    public IReadOnlyList<byte> BmiSrc { get; }

    /// <summary>
    /// The source bitmap bits
    /// </summary>
    public IReadOnlyList<byte> BitsSrc { get; }

    private EmrStretchDiBits(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        int xDest,
        int yDest,
        int xSrc,
        int ySrc,
        int cxSrc,
        int cySrc,
        uint offBmiSrc,
        uint cbBmiSrc,
        uint offBitsSrc,
        uint cbBitsSrc,
        DIBColors usageSrc,
        TernaryRasterOperation bitBltRasterOperation,
        int cxDest,
        int cyDest,
        IReadOnlyList<byte> bmiSrc,
        IReadOnlyList<byte> bitsSrc) : base(recordType, size)
    {
        Bounds = bounds;
        XDest = xDest;
        YDest = yDest;
        XSrc = xSrc;
        YSrc = ySrc;
        CXSrc = cxSrc;
        CYSrc = cySrc;
        OffBmiSrc = offBmiSrc;
        CbBmiSrc = cbBmiSrc;
        OffBitsSrc = offBitsSrc;
        CbBitsSrc = cbBitsSrc;
        UsageSrc = usageSrc;
        BitBltRasterOperation = bitBltRasterOperation;
        CXDest = cxDest;
        CYDest = cyDest;
        BmiSrc = bmiSrc;
        BitsSrc = bitsSrc;
    }

    public static EmrStretchDiBits Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var xDest = stream.ReadInt32();
        var yDest = stream.ReadInt32();
        var xSrc = stream.ReadInt32();
        var ySrc = stream.ReadInt32();
        var cxSrc = stream.ReadInt32();
        var cySrc = stream.ReadInt32();
        var offBmiSrc = stream.ReadUInt32();
        var cbBmiSrc = stream.ReadUInt32();
        var offBitsSrc = stream.ReadUInt32();
        var cbBitsSrc = stream.ReadUInt32();
        var usageSrc = stream.ReadEnum<DIBColors>();
        var bitBltRasterOperation = stream.ReadEnum<TernaryRasterOperation>();
        var cxDest = stream.ReadInt32();
        var cyDest = stream.ReadInt32();

        var selfSizeWithoutBuffers =
            // Base record fields
            Unsafe.SizeOf<EmfRecordType>() +
            Unsafe.SizeOf<uint>() +
            // Self fields
            Unsafe.SizeOf<RectL>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<DIBColors>() +
            Unsafe.SizeOf<TernaryRasterOperation>() +
            Unsafe.SizeOf<int>() +
            Unsafe.SizeOf<int>();

        long seekOffset = 0;
        if (offBmiSrc != 0)
        {
            seekOffset = offBmiSrc - selfSizeWithoutBuffers;
            stream.Seek(seekOffset, SeekOrigin.Current);
        }

        var bmiSrc = stream.ReadByteArray((int)cbBmiSrc);

        if (offBitsSrc != 0)
        {
            seekOffset = offBitsSrc - (seekOffset + bmiSrc.Length + selfSizeWithoutBuffers);
            stream.Seek(seekOffset, SeekOrigin.Current);
        }

        var bitsSrc = stream.ReadByteArray((int)cbBitsSrc);

        return new EmrStretchDiBits(
            recordType,
            size,
            bounds,
            xDest,
            yDest,
            xSrc,
            ySrc,
            cxSrc,
            cySrc,
            offBmiSrc,
            cbBmiSrc,
            offBitsSrc,
            cbBitsSrc,
            usageSrc,
            bitBltRasterOperation,
            cxDest,
            cyDest,
            bmiSrc,
            bitsSrc);
    }
}