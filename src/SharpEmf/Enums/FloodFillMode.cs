using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies how to determine the area for a flood fill operation
/// </summary>
[PublicAPI]
public enum FloodFillMode : uint
{
    /// <summary>
    /// The fill area is bounded by a specific color
    /// </summary>
    FLOODFILLBORDER = 0x00000000,

    /// <summary>
    /// The fill area is defined by a specific color.
    /// Filling continues outward in all directions as long as the color is encountered
    /// </summary>
    /// <remarks>
    /// This style is useful for filling areas with multicolored boundaries
    /// </remarks>
    FLOODFILLSURFACE = 0x00000001
}