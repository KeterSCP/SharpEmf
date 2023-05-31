using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Used to specify how a point is to be used in a drawing call
/// </summary>
[PublicAPI]
public enum Point : byte
{
    /// <summary>
    /// A <see cref="PT_LINETO"/> or <see cref="PT_BEZIERTO"/> type can be combined with this value by using
    /// the bitwise operator OR to indicate that the corresponding point is the last point in a figure and the figure is closed
    /// </summary>
    PT_CLOSEFIGURE = 0x01,

    /// <summary>
    /// Specifies that a line is to be drawn from the current position to this point, which then becomes the new current position
    /// </summary>
    PT_LINETO = 0x02,

    /// <summary>
    /// Specifies that this point is a control point or ending point for a Bezier curve
    /// </summary>
    /// <remarks>
    /// PT_BEZIERTO types always occur in sets of three.
    /// The current position defines the starting point for the Bezier curve.
    /// The first two PT_BEZIERTO points are the control points, and the third PT_BEZIERTO point is the ending point.
    /// The ending point becomes the new current position. If there are not three consecutive PT_BEZIERTO points, an error results
    /// </remarks>
    PT_BEZIERTO = 0x04,

    /// <summary>
    /// Specifies that this point starts a disjoint figure. This point becomes the new current position
    /// </summary>
    PT_MOVETO = 0x06
}