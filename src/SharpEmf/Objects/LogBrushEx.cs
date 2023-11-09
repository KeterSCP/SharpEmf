using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;

namespace SharpEmf.Objects;

/// <summary>
/// Defines the style, color, and pattern of a device-independent brush
/// </summary>
/// <remarks>
/// The following table shows the relationship between the BrushStyle, Color, and BrushHatch fields:
/// <code>
/// | BrushStyle   | Color                                               | BrushHatch
/// |--------------|-----------------------------------------------------|-------------------------------------------------------------|
/// | BS_SOLID     | Specifies the color of the brush                    | Not used and SHOULD be ignored                              |
/// | BS_NULL      | Not used and SHOULD be ignored                      | Not used and SHOULD be ignored                              |
/// | BS_HATCHED   | Specifies the foreground color of the hatch pattern | Specifies the orientation of lines used to create the hatch |
/// </code>
/// </remarks>
[PublicAPI]
public readonly struct LogBrushEx
{
    /// <summary>
    /// Specifies the brush style.
    /// Values of this field MUST be <see cref="BrushStyle.BS_SOLID"/>, <see cref="BrushStyle.BS_HATCHED"/>, or <see cref="BrushStyle.BS_NULL"/>
    /// </summary>
    public BrushStyle BrushStyle { get; }

    /// <summary>
    /// Specifies a color
    /// </summary>
    public ColorRef Color { get; }

    /// <summary>
    /// Brush hatch data
    /// </summary>
    public HatchStyle HatchStyle { get; }

    private LogBrushEx(BrushStyle brushStyle, ColorRef color, HatchStyle hatchStyle)
    {
        BrushStyle = brushStyle;
        Color = color;
        HatchStyle = hatchStyle;
    }

    public static LogBrushEx Parse(Stream stream)
    {
        var brushStyle = stream.ReadEnum<BrushStyle>();

        if (brushStyle is not (BrushStyle.BS_SOLID or BrushStyle.BS_HATCHED or BrushStyle.BS_NULL))
        {
            throw new EmfParseException($"Invalid {nameof(BrushStyle)} value: {brushStyle}");
        }

        var color = ColorRef.Parse(stream);
        var hatchStyle = stream.ReadEnum<HatchStyle>();

        return new LogBrushEx(brushStyle, color, hatchStyle);
    }
}