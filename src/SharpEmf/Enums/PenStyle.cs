using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines the attributes of pens that can be used in graphics operations.
/// A pen style is a combination of pen type, line style, line cap, and line join
/// </summary>
[PublicAPI]
[Flags]
public enum PenStyle : uint
{
    /// <summary>
    /// A pen type that specifies a line with a width of one logical unit and a style that is a solid color
    /// </summary>
    PS_COSMETIC = 0x00000000,

    /// <summary>
    /// A line cap that specifies round ends
    /// </summary>
    PS_ENDCAP_ROUND = 0x00000000,

    /// <summary>
    /// A line join that specifies round joins
    /// </summary>
    PS_JOIN_ROUND = 0x00000000,

    /// <summary>
    /// A line style that is a solid color
    /// </summary>
    PS_SOLID = 0x00000000,

    /// <summary>
    /// A line style that is dashed
    /// </summary>
    PS_DASH = 0x00000001,

    /// <summary>
    /// A line style that is dotted
    /// </summary>
    PS_DOT = 0x00000002,

    /// <summary>
    ///  A line style that consists of alternating dashes and dots
    /// </summary>
    PS_DASHDOT = 0x00000003,

    /// <summary>
    /// A line style that consists of dashes and double dots
    /// </summary>
    PS_DASHDOTDOT = 0x00000004,

    /// <summary>
    /// A line style that is invisible
    /// </summary>
    PS_NULL = 0x00000005,

    /// <summary>
    /// A line style that is a solid color
    /// </summary>
    /// <remarks>
    /// When this style is specified in a drawing record that takes a bounding rectangle,
    /// the dimensions of the figure are shrunk so that it fits entirely in the bounding rectangle, considering the width of the pen
    /// </remarks>
    PS_INSIDEFRAME = 0x00000006,

    /// <summary>
    ///  A line style that is defined by a styling array, which specifies the lengths of dashes and gaps in the line
    /// </summary>
    PS_USERSTYLE = 0x00000007,

    /// <summary>
    /// A line style in which every other pixel is set. This style is applicable only to a pen type of <see cref="PS_COSMETIC"/>
    /// </summary>
    PS_ALTERNATE = 0x00000008,

    /// <summary>
    /// A line cap that specifies square ends
    /// </summary>
    PS_ENDCAP_SQUARE = 0x00000100,

    /// <summary>
    /// A line cap that specifies flat ends
    /// </summary>
    PS_ENDCAP_FLAT = 0x00000200,

    /// <summary>
    /// A line join that specifies beveled joins
    /// </summary>
    PS_JOIN_BEVEL = 0x00001000,

    // TODO: add cref
    /// <summary>
    /// A line join that specifies mitered joins when the lengths of the joins are within the current miter length limit.
    /// If the lengths of the joins exceed the miter limit, beveled joins are specified
    /// </summary>
    /// <remarks>
    /// The miter length limit is a metafile state property that is set by the EMR_SETMITERLIMIT record
    /// </remarks>
    PS_JOIN_MITER = 0x00002000,

    /// <summary>
    /// A pen type that specifies a line with a width that is measured in logical units and a style that can contain any of the attributes of a brush
    /// </summary>
    PS_GEOMETRIC = 0x00010000
}