using JetBrains.Annotations;
using SharpEmf.Extensions;

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

    private PointL(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static PointL Parse(Stream stream) => new(
        x: stream.ReadInt32(),
        y: stream.ReadInt32());
}