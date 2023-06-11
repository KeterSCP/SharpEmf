using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies parameters that control various aspects of the output of text
/// </summary>
[PublicAPI]
[Flags]
public enum ExtTextOutOptions : uint
{
    /// <summary>
    /// Indicates that the current background color SHOULD be used to fill the rectangle
    /// </summary>
    ETO_OPAQUE = 0x00000002,

    /// <summary>
    /// Indicates that the text SHOULD be clipped to the rectangle
    /// </summary>
    ETO_CLIPPED = 0x00000004,

    /// <summary>
    /// Indicates that the codes for characters in an output text string are indexes of the character glyphs in a TrueType font
    /// </summary>
    /// <remarks>
    /// Glyph indexes are font-specific, so to display the correct characters on playback,
    /// the font that is used MUST be identical to the font used to generate the indexes
    /// </remarks>
    ETO_GLYPH_INDEX = 0x00000010,

    /// <summary>
    /// Indicates that the text MUST be laid out in right-to-left reading order, instead of the default left-to-right order
    /// </summary>
    /// <remarks>
    /// This SHOULD be applied only when the font selected into the playback device context is either Hebrew or Arabic
    /// </remarks>
    ETO_RTLREADING = 0x00000080,

    /// <summary>
    /// Indicates that the record does not specify a bounding rectangle for the text output
    /// </summary>
    ETO_NO_RECT = 0x00000100,

    /// <summary>
    /// Indicates that the codes for characters in an output text string are 8 bits,
    /// derived from the low bytes of Unicode UTF16-LE character codes, in which the high byte is assumed to be 0
    /// </summary>
    ETO_SMALL_CHARS = 0x00000200,

    /// <summary>
    /// Indicates that to display numbers, digits appropriate to the locale SHOULD be used
    /// </summary>
    ETO_NUMERICSLOCAL = 0x00000400,

    /// <summary>
    /// Indicates that to display numbers, European digits SHOULD be used
    /// </summary>
    ETO_NUMERICSLATIN = 0x00000800,

    /// <summary>
    /// Indicates that no special operating system processing for glyph placement is performed on right-to-left strings;
    /// that is, all glyph positioning SHOULD be taken care of by drawing and state records in the metafile
    /// </summary>
    ETO_IGNORELANGUAGE = 0x00001000,

    /// <summary>
    /// Indicates that both horizontal and vertical character displacement values SHOULD be provided
    /// </summary>
    ETO_PDY = 0x00002000,

    /// <summary>
    /// Reserved and SHOULD NOT be used
    /// </summary>
    ETO_REVERSE_INDEX_MAP = 0x00010000
}