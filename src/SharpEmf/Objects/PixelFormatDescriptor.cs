using JetBrains.Annotations;
using SharpEmf.Enums;

namespace SharpEmf.Objects;

/// <summary>
/// Specifies the pixel format of a drawing surface
/// </summary>
[PublicAPI]
public class PixelFormatDescriptor
{
    /// <summary>
    /// Specifies the size in bytes, of this data structure
    /// </summary>
    public ushort NSize { get; }

    /// <summary>
    /// MUST be set to 0x0001
    /// </summary>
    public const ushort NVersion = 0x0001;

    /// <summary>
    /// Properties of the pixel buffer that is used for output to the drawing surface
    /// </summary>
    public PixelFormatDescriptorFlags DwFlags { get; }

    // TODO: add missing properties
}