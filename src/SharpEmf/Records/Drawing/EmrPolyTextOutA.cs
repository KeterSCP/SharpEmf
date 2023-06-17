using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYTEXTOUTA"/>
[PublicAPI]
public record EmrPolyTextOutA : EnhancedMetafileRecord, IEmfParsable<EmrPolyTextOutA>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
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
    /// Specifies the number of objects in the <see cref="AEmrTexts"/> array
    /// </summary>
    public uint CStrings { get; }

    /// <summary>
    /// Specifies the output strings in 8-bit ASCII characters, with text attributes, and spacing values
    /// </summary>
    /// <remarks>
    /// The number of objects is specified by <see cref="CStrings"/>
    /// </remarks>
    public IReadOnlyList<EmrText> AEmrTexts { get; }

    private EmrPolyTextOutA(
        EmfRecordType recordType,
        uint size,
        RectL bounds,
        GraphicsMode iGraphicsMode,
        float exScale,
        float eyScale,
        uint cStrings,
        IReadOnlyList<EmrText> aEmrTexts) : base(recordType, size)
    {
        Bounds = bounds;
        IGraphicsMode = iGraphicsMode;
        EXScale = exScale;
        EYScale = eyScale;
        CStrings = cStrings;
        AEmrTexts = aEmrTexts;
    }

    public static EmrPolyTextOutA Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        var iGraphicsMode = stream.ReadEnum<GraphicsMode>();
        var exScale = stream.ReadFloat32();
        var eyScale = stream.ReadFloat32();
        var cStrings = stream.ReadUInt32();

        var selfSizeWithoutTextBuffers =
            // Base record fields
            Unsafe.SizeOf<EmfRecordType>() +
            Unsafe.SizeOf<uint>() +
            // This record fields
            Unsafe.SizeOf<RectL>() +
            Unsafe.SizeOf<GraphicsMode>() +
            Unsafe.SizeOf<float>() +
            Unsafe.SizeOf<float>() +
            Unsafe.SizeOf<uint>();

        var aEmrTexts = new List<EmrText>((int)cStrings);

        for (var i = 0; i < cStrings; i++)
        {
            aEmrTexts.Add(EmrText.Parse(stream, recordType, selfSizeWithoutTextBuffers));
        }

        return new EmrPolyTextOutA(
            recordType,
            size,
            bounds,
            iGraphicsMode,
            exScale,
            eyScale,
            cStrings,
            aEmrTexts
        );
    }
}