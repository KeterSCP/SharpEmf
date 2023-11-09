using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that specify how to calculate the region of a polygon that is to be filled
/// </summary>
[PublicAPI]
public enum PolygonFillMode
{
    /// <summary>
    /// Selects alternate mode (fills the area between odd-numbered and even-numbered polygon sides on each scan line)
    /// </summary>
    ALTERNATE = 0x01,

    /// <summary>
    /// Selects winding mode (fills any region with a nonzero winding value)
    /// </summary>
    WINDING = 0x02
}