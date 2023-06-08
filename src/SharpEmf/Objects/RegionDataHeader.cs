using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.WmfTypes;

namespace SharpEmf.Objects;

/// <summary>
/// Defines the properties of a <see cref="RegionData"/> object
/// </summary>
[PublicAPI]
public class RegionDataHeader
{
    /// <summary>
    /// Specifies the size of this object in bytes
    /// </summary>
    public const uint Size = 0x00000020;

    /// <summary>
    /// Specifies the region type
    /// </summary>
    public const uint Type = 0x00000001;

    /// <summary>
    /// Specifies the number of rectangles in this region
    /// </summary>
    public uint CountRects { get; }

    /// <summary>
    /// Specifies the size of the buffer of rectangles in bytes
    /// </summary>
    public uint RgnSize { get; }

    /// <summary>
    /// Specifies the bounds of the region
    /// </summary>
    public RectL Bounds { get; }

    private RegionDataHeader(uint countRects, uint rgnSize, RectL bounds)
    {
        CountRects = countRects;
        RgnSize = rgnSize;
        Bounds = bounds;
    }

    public static RegionDataHeader Parse(Stream stream)
    {
        var size = stream.ReadUInt32();
        if (size != Size)
        {
            throw new EmfParseException($"Expected size of {Size} bytes, but got {size}");
        }

        var type = stream.ReadUInt32();
        if (type != Type)
        {
            throw new EmfParseException($"Expected type of {Type} bytes, but got {type}");
        }

        var countRects = stream.ReadUInt32();
        var rgnSize = stream.ReadUInt32();
        var bounds = RectL.Parse(stream);

        Debug.Assert(rgnSize == countRects * Unsafe.SizeOf<RectL>());

        return new RegionDataHeader(countRects, rgnSize, bounds);
    }
}