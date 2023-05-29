using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Set of bit flags that specify properties of the pixel buffer that is used for output to the drawing surface
/// </summary>
[PublicAPI]
[Flags]
public enum PixelFormatDescriptorFlags : uint
{
    NONE = 0,

    /// TODO: add crefs
    /// <summary>
    /// The buffer uses RGBA pixels on a palette-managed device. A LogPalette
    /// object is required to achieve the best results for this pixel
    /// type. Colors in the palette SHOULD be specified according to the values
    /// of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBlueBits, and
    /// cBlueShift fields
    /// </summary>
    PFD_NEED_PALETTE = 1 << 0,

    /// <summary>
    /// The pixel format is natively supported by the operating system; this is
    /// known as the "generic" implementation.
    /// If clear, the pixel format is supported by a device driver or hardware
    /// </summary>
    PFD_GENERIC_FORMAT = 1 << 1,

    /// <summary>
    /// The pixel buffer supports OpenGL drawing
    /// </summary>
    PFD_SUPPORT_OPENGL = 1 << 2,

    /// <summary>
    /// This flag SHOULD be clear, but it MAY be set.
    /// This flag and <see cref="PixelFormatDescriptorFlags.PFD_DOUBLEBUFFER"/> MUST NOT both be set
    /// </summary>
    PFD_SUPPORT_GDI = 1 << 3,

    /// <summary>
    /// The pixel buffer can draw to a memory bitmap
    /// </summary>
    PFD_DRAW_TO_BITMAP = 1 << 4,

    /// <summary>
    /// The pixel buffer can draw to a window or device surface
    /// </summary>
    PFD_DRAW_TO_WINDOW = 1 << 5,

    /// <summary>
    /// The pixel buffer MAY be stereoscopic; that is, it MAY specify a color
    /// plane that is used to create the illusion of depth in an image
    /// </summary>
    PFD_STEREO = 1 << 6,

    /// <summary>
    /// The pixel buffer is double-buffered.
    /// This flag and <see cref="PixelFormatDescriptorFlags.PFD_SUPPORT_GDI"/> MUST NOT both be set
    /// </summary>
    PFD_DOUBLEBUFFER = 1 << 7,

    /// <summary>
    /// The pixel buffer supports compositing, which indicates that source pixels
    /// MAY overwrite or be combined with background pixels
    /// </summary>
    PFD_SUPPORT_COMPOSITION = 1 << 8,

    /// <summary>
    /// The pixel buffer supports Direct3D drawing, which accelerated rendering in three dimensions
    /// </summary>
    PFD_DIRECT3D_ACCELERATED = 1 << 9,

    /// <summary>
    /// The pixel buffer supports DirectDraw drawing, which allows applications to
    /// have low-level control of the output drawing surface
    /// </summary>
    PFD_SUPPORT_DIRECTDRAW = 1 << 10,

    /// <summary>
    /// The pixel format is supported by a device driver that accelerates the
    /// generic implementation. If this flag is clear and the
    /// <see cref="PixelFormatDescriptorFlags.PFD_GENERIC_FORMAT"/> flag is set, the pixel format is supported by the
    /// generic implementation only
    /// </summary>
    PFD_GENERIC_ACCELERATED = 1 << 11,

    /// <summary>
    /// A device can swap individual color planes with pixel formats that include
    /// double-buffered overlay or underlay color planes. Otherwise all color
    /// planes are swapped together as a group
    /// </summary>
    PFD_SWAP_LAYER_BUFFERS = 1 << 12,

    /// <summary>
    /// The contents of the back buffer have been copied to the front buffer in a
    /// double-buffered color plane. The contents of the back buffer have not
    /// been affected
    /// </summary>
    PFD_SWAP_COPY = 1 << 13,

    /// <summary>
    /// The contents of the back buffer have been exchanged with the contents of
    /// the front buffer in a double-buffered color plane
    /// </summary>
    PFD_SWAP_EXCHANGE = 1 << 14,

    /// <summary>
    /// The output device supports one hardware palette in 256-color mode only.
    /// For such systems to use hardware acceleration, the hardware palette
    /// MUST be in a fixed order (for example, 3-3-2) when in RGBA mode, or
    /// MUST match the LogPalette object when in color table mode
    /// </summary>
    PFD_NEED_SYSTEM_PALETTE = 1 << 15,

    /// <summary>
    /// The pixel buffer MAY be either monoscopic or stereoscopic
    /// </summary>
    PFD_STEREO_DONTCARE = 1 << 28,

    /// <summary>
    /// The pixel buffer can be either single or double buffered
    /// </summary>
    PFD_DOUBLEBUFFER_DONTCARE = 1 << 29,

    /// <summary>
    /// The pixel buffer is not required to include a color plane for depth effects
    /// </summary>
    PFD_DEPTH_DONTCARE = 1 << 30,
}