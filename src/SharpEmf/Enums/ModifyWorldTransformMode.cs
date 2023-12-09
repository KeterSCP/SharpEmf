using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines modes for changing the world-space to page-space transform that is currently defined in the playback device context
/// </summary>
[PublicAPI]
public enum ModifyWorldTransformMode : uint
{
    /// <summary>
    /// Reset the current transform using the identity matrix. In this mode, the specified transform data is ignored
    /// </summary>
    MWT_IDENTITY = 0x01,

    /// <summary>
    /// Multiply the current transform. In this mode, the specified transform data is the left multiplicand, and the current transform is the right multiplicand
    /// </summary>
    MWT_LEFTMULTIPLY = 0x02,

    /// <summary>
    /// Multiply the current transform. In this mode, the specified transform data is the right multiplicand, and the current transform is the left multiplicand
    /// </summary>
    MWT_RIGHTMULTIPLY = 0x03,

    /// <summary>
    /// Set the current transform to the specified transform data
    /// </summary>
    MWT_SET = 0x04
}