using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies how to interpret shape data such as rectangle coordinates
/// </summary>
[PublicAPI]
public enum GraphicsMode
{
    /// <summary>
    /// TrueType text MUST be written from left to right and right side up, even if the
    /// rest of the graphics are rotated about the x-axis or y-axis because of the current world-to-device
    /// transform. Only the height of the text SHOULD be scaled
    /// </summary>
    /// <remarks>
    /// Arcs MUST be drawn using the current arc direction, but they MUST NOT reflect the current world-to-device transform,
    /// which might require a rotation along the x-axis or y-axis
    /// <para />
    /// In <see cref="GM_COMPATIBLE"/> graphics mode, bottom and rightmost edges MUST be excluded when rectangles are drawn
    /// </remarks>
    GM_COMPATIBLE = 0x00000001,

    /// <summary>
    ///  TrueType text output SHOULD fully conform to the current world-to-device transform
    /// </summary>
    /// <remarks>
    /// Arcs MUST be drawn in the counterclockwise direction in world space; however, both arc control
    /// points and the arcs themselves MUST reflect the current world-to-device transform
    /// <para />
    /// In GM_ADVANCED graphics mode, bottom and rightmost edges MUST be included when rectangles are drawn
    /// </remarks>
    GM_ADVANCED = 0x00000002
}