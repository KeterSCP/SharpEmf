using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies the different possible brush types that can be used in graphics operations
/// </summary>
[PublicAPI]
public enum BrushStyle : uint
{
    BS_SOLID = 0x0000,
    BS_NULL = 0x0001,
    BS_HATCHED = 0x0002,
    BS_PATTERN = 0x0003,
    BS_INDEXED = 0x0004,
    BS_DIBPATTERN = 0x0005,
    BS_DIBPATTERNPT = 0x0006,
    BS_PATTERN8X8 = 0x0007,
    BS_DIBPATTERN8X8 = 0x0008,
    BS_MONOPATTERN = 0x0009
}