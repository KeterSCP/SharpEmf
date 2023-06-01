using JetBrains.Annotations;
using SharpEmf.Extensions;

namespace SharpEmf.Objects;

/// <summary>
/// Specifies color and position information for the definition of a rectangle or triangle vertex
/// </summary>
[PublicAPI]
public readonly struct TriVertex
{
    /// <summary>
    /// Specifies the horizontal position, in logical units
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Specifies the vertical position, in logical units
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Specifies the red color value for the point
    /// </summary>
    public ushort Red { get; }

    /// <summary>
    /// Specifies the green color value for the point
    /// </summary>
    public ushort Green { get; }

    /// <summary>
    /// Specifies the blue color value for the point
    /// </summary>
    public ushort Blue { get; }

    /// <summary>
    /// Specifies the alpha transparency value for the point
    /// </summary>
    public ushort Alpha { get; }

    private TriVertex(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
    {
        X = x;
        Y = y;
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public static TriVertex Parse(Stream stream)
    {
        var x = stream.ReadInt32();
        var y = stream.ReadInt32();
        var red = stream.ReadUInt16();
        var green = stream.ReadUInt16();
        var blue = stream.ReadUInt16();
        var alpha = stream.ReadUInt16();

        return new TriVertex(x, y, red, green, blue, alpha);
    }
}