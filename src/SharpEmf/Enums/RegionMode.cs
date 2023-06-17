using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that are used with <see cref="EmfRecordType.EMR_SELECTCLIPPATH"/> and <see cref="EmfRecordType.EMR_EXTSELECTCLIPRGN"/>,
/// specifying the current path bracket or a new region that is being combined with the current clipping region
/// </summary>
[PublicAPI]
public enum RegionMode : uint
{
    /// <summary>
    /// The new clipping region includes the intersection (overlapping areas) of the current
    /// clipping region and the current path bracket (or new region)
    /// </summary>
    RGN_AND = 0x01,

    /// <summary>
    /// The new clipping region includes the union (combined areas) of the current clipping region
    /// and the current path bracket (or new region)
    /// </summary>
    RGN_OR = 0x02,

    /// <summary>
    /// The new clipping region includes the union of the current clipping region and the current
    /// path bracket (or new region) but without the overlapping areas
    /// </summary>
    RGN_XOR = 0x03,

    /// <summary>
    /// The new clipping region includes the areas of the current clipping region with those of the
    /// current path bracket (or new region) excluded
    /// </summary>
    RGN_DIFF = 0x04,

    /// <summary>
    /// The new clipping region is the current path bracket (or the new region)
    /// </summary>
    RGN_COPY = 0x05
}