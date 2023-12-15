using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;

namespace SharpEmf.Objects;

/// <summary>
/// Specifies the style, width, and color of an extended logical pen
/// </summary>
/// <remarks>
/// The following table shows the relationship between the <see cref="BrushStyle"/>, <see cref="ColorRef"/>, and <see cref="BrushHatch"/> fields in this object:
/// <code>
/// | BrushStyle      | ColorRef                              | BrushHatch                                                   |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_SOLID        | Specifies the color of lines drawn by | Not used and is ignored                                      |
/// |                 | the pen                               |                                                              |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_NULL         | Not used and is ignored               | Not used and is ignored                                      |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_HATCHED      | Specifies the foreground color of     | Specifies the orientation of lines used to create the hatch  |
/// |                 | the hatch pattern                     | If PS_GEOMETRIC is not set in the PenStyle field, this field |
/// |                 |                                       | MUST be either HS_SOLIDTEXTCLR or HS_SOLIDBKCLR              |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_PATTERN      | The low-order 16-bits is a value from | Not used and is ignored                                      |
/// |                 | the ColorUsage enum                   |                                                              |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_DIBPATTERN   | The low-order 16 bits is a value from | Not used and is ignored                                      |
/// |                 | the ColorUsage enum                   |                                                              |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// | BS_DIBPATTERNPT | The low-order 16 bits is a value from | Not used and is ignored                                      |
/// |                 | the ColorUsage enum                   |                                                              |
/// |-----------------|---------------------------------------|--------------------------------------------------------------|
/// </code>
/// </remarks>
[PublicAPI]
public readonly struct LogPenEx
{
    /// <summary>
    /// Specifies the pen style
    /// </summary>
    public PenStyle PenStyle { get; }

    /// <summary>
    /// Specifies the width of the line drawn by the pen
    /// </summary>
    /// <remarks>
    /// If the pen type in the <see cref="PenStyle"/> field is <see cref="PenStyle.PS_COSMETIC"/>, this value is the width in logical units;
    /// otherwise, the width is specified in device units. If the pen type in the <see cref="PenStyle"/> field is
    /// <see cref="PenStyle.PS_COSMETIC"/>, this value MUST be 0x00000001
    /// </remarks>
    public uint Width { get; }

    /// <summary>
    /// Specifies a brush style for the pen
    /// </summary>
    /// <remarks>
    /// If the pen type in the <see cref="PenStyle"/> field is <see cref="PenStyle.PS_GEOMETRIC"/>, this value is either <see cref="BrushStyle.BS_SOLID"/> or
    /// <see cref="BrushStyle.BS_HATCHED"/>. The value of this field can be <see cref="BrushStyle.BS_NULL"/>, but only if the line style specified in
    /// <see cref="PenStyle"/> is <see cref="Enums.PenStyle.PS_NULL"/>. The <see cref="BrushStyle.BS_NULL"/> style SHOULD be used to specify a brush that has no effect
    /// </remarks>
    public BrushStyle BrushStyle { get; }

    /// <summary>
    /// Specifies the color of the pen
    /// </summary>
    public ColorRef ColorRef { get; }

    /// <summary>
    /// The brush hatch pattern
    /// </summary>
    public HatchStyle BrushHatch { get; }

    /// <summary>
    /// The number of elements in the array specified in the StyleEntry field.
    /// </summary>
    /// <remarks>
    /// This value SHOULD be zero if <see cref="PenStyle"/> does not specify <see cref="PenStyle.PS_USERSTYLE"/>
    /// </remarks>
    public uint NumStyleEntries { get; }

    /// <summary>
    /// Defines the lengths of dashes and gaps in the line drawn by this pen when the value of <see cref="PenStyle"/> is <see cref="PenStyle.PS_USERSTYLE"/>.
    /// The array contains the number of entries specified by <see cref="NumStyleEntries"/>, but it is used as if it repeated indefinitely
    /// </summary>
    /// <remarks>
    /// The first entry in the array specifies the length of the first dash. The second entry specifies the length of the first gap.
    /// Thereafter, lengths of dashes and gaps alternate. If the pen type in the <see cref="PenStyle"/> field is <see cref="PenStyle.PS_GEOMETRIC"/>,
    /// lengths are specified in logical units; otherwise, they are specified in device units.
    /// </remarks>
    public IReadOnlyList<PenStyle> StyleEntry { get; }

    public LogPenEx(PenStyle penStyle, ColorRef color)
    {
        PenStyle = penStyle;
        ColorRef = color;
        Width = 1;
        BrushStyle = BrushStyle.BS_SOLID;
    }

    private LogPenEx(PenStyle penStyle, uint width, BrushStyle brushStyle, ColorRef colorRef, HatchStyle brushHatch, uint numStyleEntries, IReadOnlyList<PenStyle> styleEntry)
    {
        PenStyle = penStyle;
        Width = width;
        BrushStyle = brushStyle;
        ColorRef = colorRef;
        BrushHatch = brushHatch;
        NumStyleEntries = numStyleEntries;
        StyleEntry = styleEntry;
    }

    public static LogPenEx Null { get; } = new(PenStyle.PS_NULL, color: default);

    public static LogPenEx Parse(Stream stream)
    {
        var penStyle = stream.ReadEnum<PenStyle>();
        var width = stream.ReadUInt32();
        var brushStyle = stream.ReadEnum<BrushStyle>();
        var colorRef = ColorRef.Parse(stream);
        var brushHatch = stream.ReadEnum<HatchStyle>();
        var numStyleEntries = stream.ReadUInt32();

        var styleEntry = numStyleEntries == 0 ? Array.Empty<PenStyle>() : new PenStyle[numStyleEntries];
        for (int i = 0; i < numStyleEntries; i++)
        {
            styleEntry[i] = stream.ReadEnum<PenStyle>();
        }

        return new LogPenEx(penStyle, width, brushStyle, colorRef, brushHatch, numStyleEntries, styleEntry);
    }
}