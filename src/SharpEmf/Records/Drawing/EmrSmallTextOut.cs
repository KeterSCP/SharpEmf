using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_SMALLTEXTOUT"/>
[PublicAPI]
public record EmrSmallTextOut : EnhancedMetafileRecord, IEmfParsable<EmrSmallTextOut>
{
    /// <summary>
    /// Specifies the x-coordinate of where to place the string
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Specifies the y-coordinate of where to place the string
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Specifies the number of 16-bit characters in the string
    /// </summary>
    /// <remarks>
    /// The string is NOT null-terminated
    /// </remarks>
    public uint CChars { get; }

    /// <summary>
    /// Specifies the text output options to use
    /// </summary>
    public ExtTextOutOptions FUOptions { get; }

    /// <summary>
    /// Specifies the graphics mode
    /// </summary>
    public GraphicsMode IGraphicsMode { get; }

    /// <summary>
    /// Specifies how much to scale the text in the x-direction
    /// </summary>
    public float EXScale { get; }

    /// <summary>
    /// Specifies how much to scale the text in the y-direction
    /// </summary>
    public float EYScale { get; }

    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    /// <remarks>
    /// If <see cref="ExtTextOutOptions.ETO_NO_RECT"/> is set in the <see cref="FUOptions"/> field, this field is not included in the record
    /// </remarks>
    public RectL? Bounds { get; }

    /// <summary>
    /// A string that contains the text string to draw, in either 8-bit or 16-bit character codes,
    /// according to the value of the <see cref="FUOptions"/> field
    /// </summary>
    /// <remarks>
    /// If <see cref="ExtTextOutOptions.ETO_SMALL_CHARS"/> is set in the <see cref="FUOptions"/> field, <see cref="TextString"/> contains 8-bit codes for characters,
    /// derived from the low bytes of Unicode UTF16-LE character codes, in which the high byte is assumed to be 0
    /// </remarks>
    public string TextString { get; }

    private EmrSmallTextOut(
        EmfRecordType recordType,
        uint size,
        int x,
        int y,
        uint cChars,
        ExtTextOutOptions fuOptions,
        GraphicsMode iGraphicsMode,
        float exScale,
        float eyScale,
        RectL? bounds,
        string textString) : base(recordType, size)
    {
        X = x;
        Y = y;
        CChars = cChars;
        FUOptions = fuOptions;
        IGraphicsMode = iGraphicsMode;
        EXScale = exScale;
        EYScale = eyScale;
        Bounds = bounds;
        TextString = textString;
    }


    public static EmrSmallTextOut Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var x = stream.ReadInt32();
        var y = stream.ReadInt32();

        var cChars = stream.ReadUInt32();
        var fuOptions = stream.ReadEnum<ExtTextOutOptions>();
        var iGraphicsMode = stream.ReadEnum<GraphicsMode>();

        var exScale = stream.ReadFloat32();
        var eyScale = stream.ReadFloat32();

        RectL? bounds = fuOptions.HasFlag(ExtTextOutOptions.ETO_NO_RECT) ? null : RectL.Parse(stream);
        var textString = stream.ReadUnicodeString((int)cChars);

        return new EmrSmallTextOut(recordType, size, x, y, cChars, fuOptions, iGraphicsMode, exScale, eyScale, bounds, textString);
    }
}