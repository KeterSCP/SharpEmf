using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines the modes for gradient fill operations
/// </summary>
[PublicAPI]
public enum GradientFill : uint
{
    /// <summary>
    /// Color interpolation along a gradient from the left to the right edges of a rectangle
    /// </summary>
    GRADIENT_FILL_RECT_H = 0x00000000,

    /// <summary>
    /// Color interpolation along a gradient from the top to the bottom edges of a rectangle
    /// </summary>
    GRADIENT_FILL_RECT_V = 0x00000001,

    /// <summary>
    /// Color interpolation between vertexes of a triangle
    /// </summary>
    GRADIENT_FILL_TRIANGLE = 0x00000002
}