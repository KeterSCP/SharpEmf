using JetBrains.Annotations;

namespace SharpEmf.WmfTypes;

/// <summary>
/// Defines a rectangle
/// </summary>
[PublicAPI]
public readonly struct RectL
{
    /// <summary>
    /// Defines the x coordinate, in logical coordinates, of the upper-left corner of the rectangle
    /// </summary>
    public uint Left { get; }

    /// <summary>
    /// Defines the y coordinate, in logical coordinates, of the upper-left corner of the rectangle
    /// </summary>
    public uint Top { get; }

    /// <summary>
    /// Defines the x coordinate, in logical coordinates, of the lower-right corner of the rectangle
    /// </summary>
    public uint Right { get; }

    /// <summary>
    /// Defines y coordinate, in logical coordinates, of the lower-right corner of the rectangle
    /// </summary>
    public uint Bottom { get; }

    public RectL(uint left, uint top, uint right, uint bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}