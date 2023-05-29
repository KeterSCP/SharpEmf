using JetBrains.Annotations;

namespace SharpEmf.WmfTypes;

/// <summary>
/// Defines the coordinates of a point
/// </summary>
[PublicAPI]
public readonly struct PointL
{
    /// <summary>
    /// Defines the horizontal (x) coordinate of the point
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Defines the vertical (y) coordinate of the point
    /// </summary>
    public int Y { get; }

    public PointL(int x, int y)
    {
        X = x;
        Y = y;
    }
}