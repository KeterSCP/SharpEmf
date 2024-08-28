using JetBrains.Annotations;
using SharpEmf.Extensions;

namespace SharpEmf.WmfTypes;

/// <summary>
/// Defines the x- and y-extents of a rectangle
/// </summary>
[PublicAPI]
public readonly struct SizeL
{
    /// <summary>
    /// Defines the x-coordinate of the point
    /// </summary>
    public uint Cx { get; }

    /// <summary>
    /// Defines the y-coordinate of the point
    /// </summary>
    public uint Cy { get; }

    private SizeL(uint cx, uint cy)
    {
        Cx = cx;
        Cy = cy;
    }

    // TODO: read this as Int64 and reinterpret as SizeL via Unsafe.BitCast to reduce the number of stream reads
    public static SizeL Parse(Stream stream) => new(
        cx: stream.ReadUInt32(),
        cy: stream.ReadUInt32());
}