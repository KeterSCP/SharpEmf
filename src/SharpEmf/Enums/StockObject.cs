using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies the indexes of predefined logical graphics objects that can be used in graphics operations
/// </summary>
[PublicAPI]
public enum StockObject : uint
{
    WHITE_BRUSH = 0x80000000,
    LTGRAY_BRUSH = 0x80000001,
    GRAY_BRUSH = 0x80000002,
    DKGRAY_BRUSH = 0x80000003,
    BLACK_BRUSH = 0x80000004,
    NULL_BRUSH = 0x80000005,
    WHITE_PEN = 0x80000006,
    BLACK_PEN = 0x80000007,
    NULL_PEN = 0x80000008,
    OEM_FIXED_FONT = 0x8000000A,
    ANSI_FIXED_FONT = 0x8000000B,
    ANSI_VAR_FONT = 0x8000000C,
    SYSTEM_FONT = 0x8000000D,
    DEVICE_DEFAULT_FONT = 0x8000000E,
    DEFAULT_PALETTE = 0x8000000F,
    SYSTEM_FIXED_FONT = 0x80000010,
    DEFAULT_GUI_FONT = 0x80000011,
    DC_BRUSH = 0x80000012,
    DC_PEN = 0x80000013
}