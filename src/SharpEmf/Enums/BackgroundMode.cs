using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Used to specify the background mode to be used with text, hatched brushes, and pen styles that are not solid.
/// The background mode determines how to combine the background with foreground text, hatched brushes, and pen styles that are not solid lines
/// </summary>
[PublicAPI]
public enum BackgroundMode : uint
{
    /// <summary>
    /// Background remains untouched
    /// </summary>
    TRANSPARENT = 0x0001,

    /// <summary>
    /// Background is filled with the current background color before the text, hatched brush, or pen is drawn
    /// </summary>
    OPAQUE = 0x0002
}