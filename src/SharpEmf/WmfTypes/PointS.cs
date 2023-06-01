using JetBrains.Annotations;
using SharpEmf.Extensions;

namespace SharpEmf.WmfTypes;

/// <summary>
/// Defines the x- and y-coordinates of a point
/// </summary>
[PublicAPI]
public readonly struct PointS
{
    /// <summary>
    /// Defines the horizontal (x) coordinate of the point
    /// </summary>
    public short X { get; }

    /// <summary>
    /// Defines the vertical (y) coordinate of the point
    /// </summary>
    public short Y { get; }

    private PointS(short x, short y)
    {
        X = x;
        Y = y;
    }

    public static PointS Parse(Stream stream) => new(
        x: stream.ReadInt16(),
        y: stream.ReadInt16());
}