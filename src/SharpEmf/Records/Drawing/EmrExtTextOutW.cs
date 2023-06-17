using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_EXTTEXTOUTW"/>
[PublicAPI]
public record EmrExtTextOutW : EnhancedMetafileRecord, IEmfParsable<EmrExtTextOutW>
{
    /// <summary>
    /// Not used and MUST be ignored on receipt
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the graphics mode
    /// </summary>
    public GraphicsMode IGraphicsMode { get; }

    /// <summary>
    /// Specifies the scale factor to apply along the X axis to convert from page space units to .01mm units
    /// </summary>
    /// <remarks>
    /// This SHOULD be used only if <see cref="IGraphicsMode"/> is <see cref="GraphicsMode.GM_COMPATIBLE"/>
    /// </remarks>
    public float EXScale { get; }

    /// <summary>
    /// Specifies the scale factor to apply along the T axis to convert from page space units to .01mm units
    /// </summary>
    /// <remarks>
    /// This SHOULD be used only if <see cref="IGraphicsMode"/> is <see cref="GraphicsMode.GM_COMPATIBLE"/>
    /// </remarks>
    public float EYScale { get; }

    /// <summary>
    /// Specifies the output string in UNICODE characters, with text attributes and spacing values
    /// </summary>
    public EmrText WEmrText { get; }

    private EmrExtTextOutW(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        GraphicsMode iGraphicsMode,
        float exScale,
        float eyScale,
        EmrText wEmrText) : base(recordType, size)
    {
        Bounds = bounds;
        IGraphicsMode = iGraphicsMode;
        EXScale = exScale;
        EYScale = eyScale;
        WEmrText = wEmrText;
    }

    public static EmrExtTextOutW Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var iGraphicsMode = stream.ReadEnum<GraphicsMode>();
        var exScale = stream.ReadFloat32();
        var eyScale = stream.ReadFloat32();

        var selfSizeWithoutTextBuffer =
            // Base record fields
            Unsafe.SizeOf<EmfRecordType>() +
            Unsafe.SizeOf<uint>() +
            // This record fields
            Unsafe.SizeOf<RectL>() +
            Unsafe.SizeOf<GraphicsMode>() +
            Unsafe.SizeOf<float>() +
            Unsafe.SizeOf<float>();

        var wEmrText = EmrText.Parse(stream, recordType, selfSizeWithoutTextBuffer);

        return new EmrExtTextOutW(recordType, size, bounds, iGraphicsMode, exScale, eyScale, wEmrText);
    }
}