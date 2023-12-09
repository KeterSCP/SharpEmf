using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Used to define the unit of measure for transforming page space units into device space units and for defining the orientation of the drawing axes
/// </summary>
[PublicAPI]
public enum MapMode : uint
{
    /// <summary>
    /// Each logical unit is mapped to one device pixel. Positive x is to the right; positive y is down
    /// </summary>
    MM_TEXT = 0x01,
    /// <summary>
    /// Each logical unit is mapped to 0.1 millimeter. Positive x is to the right; positive y is up
    /// </summary>
    MM_LOMETRIC = 0x02,
    /// <summary>
    ///  Each logical unit is mapped to 0.01 millimeter. Positive x is to the right; positive y is up
    /// </summary>
    MM_HIMETRIC = 0x03,
    /// <summary>
    /// Each logical unit is mapped to 0.01 inch. Positive x is to the right; positive y is up
    /// </summary>
    MM_LOENGLISH = 0x04,
    /// <summary>
    ///Each logical unit is mapped to 0.001 inch. Positive x is to the right; positive y is up
    /// </summary>
    MM_HIENGLISH = 0x05,
    /// <summary>
    /// Each logical unit is mapped to one-twentieth of a printer's point (1/1440 inch, also called a "twip"). Positive x is to the right; positive y is up
    /// </summary>
    MM_TWIPS = 0x06,
    /// <summary>
    /// Logical units are isotropic; that is, they are mapped to arbitrary units with equally scaled axes.
    /// Thus, one unit along the x-axis is equal to one unit along the y-axis
    /// </summary>
    MM_ISOTROPIC = 0x07,
    /// <summary>
    ///  Logical units are anisotropic; that is, they are mapped to arbitrary units with arbitrarily scaled axes
    /// </summary>
    MM_ANISOTROPIC = 0x08
}