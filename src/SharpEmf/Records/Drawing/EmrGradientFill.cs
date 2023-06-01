using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_GRADIENTFILL"/>
[PublicAPI]
public record EmrGradientFill : EnhancedMetafileRecord, IEmfParsable<EmrGradientFill>
{
    public override EmfRecordType Type => EmfRecordType.EMR_GRADIENTFILL;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the inclusive-inclusive bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of vertexes
    /// </summary>
    public uint NVer { get; }

    /// <summary>
    /// Specifies the number of rectangles or triangles to fill
    /// </summary>
    public uint NTri { get; }

    /// <summary>
    /// Specifies the gradient fill mode
    /// </summary>
    public GradientFill UlMode { get; }

    /// <summary>
    ///  An array of <see cref="NVer"/> <see cref="TriVertex"/> objects.
    /// Each object specifies the position and color of a vertex of either a rectangle or a triangle,
    /// depending on the value of the <see cref="UlMode"/> field
    /// </summary>
    public IReadOnlyList<TriVertex> VertexObjects { get; }

    /// <summary>
    /// An array of <see cref="NTri"/> <see cref="Objects.GradientRectangle"/> or <see cref="Objects.GradientTriangle"/> objects,
    /// depending on the value of the <see cref="UlMode"/> field. <para />
    /// Each object specifies indexes into the <see cref="VertexObjects"/> array
    /// </summary>
    public IReadOnlyList<IGradientShape> VertexIndexes { get; }

    /// <summary>
    /// An array of <see cref="NTri"/> times four bytes that MUST be present if
    /// the value of the <see cref="UlMode"/> field is <see cref="GradientFill.GRADIENT_FILL_RECT_H"/> or <see cref="GradientFill.GRADIENT_FILL_RECT_V"/>.
    /// If the value of the <see cref="UlMode"/> field is <see cref="GradientFill.GRADIENT_FILL_TRIANGLE"/>, no VertexPadding is present
    /// </summary>
    /// <remarks>
    /// This field MUST be ignored
    /// </remarks>
    public IReadOnlyList<byte>? VertexPadding { get; }

    private EmrGradientFill(
        uint size,
        RectL bounds,
        uint nVer,
        uint nTri,
        GradientFill ulMode,
        IReadOnlyList<TriVertex> vertexObjects,
        IReadOnlyList<IGradientShape> vertexIndexes,
        IReadOnlyList<byte>? vertexPadding)
    {
        Size = size;
        Bounds = bounds;
        NVer = nVer;
        NTri = nTri;
        UlMode = ulMode;
        VertexObjects = vertexObjects;
        VertexIndexes = vertexIndexes;
        VertexPadding = vertexPadding;
    }

    public static EmrGradientFill Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);

        var nVer = stream.ReadUInt32();
        var nTri = stream.ReadUInt32();
        var ulMode = stream.ReadEnum<GradientFill>();

        var vertexObjects = new TriVertex[nVer];
        for (var i = 0; i < nVer; i++)
        {
            vertexObjects[i] = TriVertex.Parse(stream);
        }

        var vertexIndexes = new IGradientShape[nTri];
        for (var i = 0; i < nTri; i++)
        {
            vertexIndexes[i] = ulMode switch
            {
                GradientFill.GRADIENT_FILL_RECT_H or GradientFill.GRADIENT_FILL_RECT_V => GradientRectangle.Parse(stream),
                GradientFill.GRADIENT_FILL_TRIANGLE => GradientTriangle.Parse(stream),
                _ => throw new SwitchExpressionException(ulMode)
            };
        }

        byte[]? vertexPadding = null;
        switch (ulMode)
        {
            case GradientFill.GRADIENT_FILL_RECT_H or GradientFill.GRADIENT_FILL_RECT_V:
            {
                var bytesAmount = (int)nTri * 4;
                vertexPadding = new byte[bytesAmount];
                stream.ReadExactly(vertexPadding);
                break;
            }
        }

        return new EmrGradientFill(size, bounds, nVer, nTri, ulMode, vertexObjects, vertexIndexes, vertexPadding);
    }
}