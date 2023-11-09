using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies the hatch pattern
/// </summary>
[PublicAPI]
public enum HatchStyle : uint
{
    HS_SOLIDCLR = 0x0006,
    HS_DITHEREDCLR = 0x0007,
    HS_SOLIDTEXTCLR = 0x0008,
    HS_DITHEREDTEXTCLR = 0x0009,
    HS_SOLIDBKCLR = 0x000A,
    HS_DITHEREDBKCLR = 0x000B
}