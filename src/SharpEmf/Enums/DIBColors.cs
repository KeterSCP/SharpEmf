using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines how to interpret the values in the color table of a DIB (device-independent bitmap)
/// </summary>
[PublicAPI]
public enum DIBColors : uint
{
    /// <summary>
    /// The color table contains literal RGB values
    /// </summary>
    DIB_RGB_COLORS = 0x00,
    /// <summary>
    /// The color table consists of an array of 16-bit indexes into the LogPalette object that is currently defined in the playback device context
    /// </summary>
    DIB_PAL_COLORS = 0x01,
    /// <summary>
    /// No color table exists. The pixels in the DIB are indices into the current logical palette in the playback device context
    /// </summary>
    DIB_PAL_INDICES = 0x02
}