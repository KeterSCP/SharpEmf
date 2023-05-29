using JetBrains.Annotations;

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

    public SizeL(uint cx, uint cy)
    {
        Cx = cx;
        Cy = cy;
    }
}