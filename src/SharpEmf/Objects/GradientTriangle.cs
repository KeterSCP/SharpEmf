using JetBrains.Annotations;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Objects;

/// <summary>
/// The GradientRectangle object defines a triangle using <see cref="TriVertex"/> objects
/// in an <see cref="Records.Drawing.EmrGradientFill"/> record
/// </summary>
[PublicAPI]
public readonly struct GradientTriangle : IGradientShape
{
    /// <summary>
    /// An index into an array of <see cref="TriVertex"/> objects that specifies the first vertex of a triangle.
    /// The index MUST be smaller than the size of the array, as defined by the <see cref="Records.Drawing.EmrGradientFill.NVer"/>
    /// </summary>
    public uint Vertex1 { get; }

    /// <summary>
    /// An index into an array of <see cref="TriVertex"/> objects that specifies the second vertex of a triangle.
    /// The index MUST be smaller than the size of the array, as defined by the <see cref="Records.Drawing.EmrGradientFill.NVer"/>
    /// </summary>
    public uint Vertex2 { get; }

    /// <summary>
    /// An index into an array of <see cref="TriVertex"/> objects that specifies the third vertex of a triangle.
    /// The index MUST be smaller than the size of the array, as defined by the <see cref="Records.Drawing.EmrGradientFill.NVer"/>
    /// </summary>
    public uint Vertex3 { get; }

    private GradientTriangle(uint vertex1, uint vertex2, uint vertex3)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Vertex3 = vertex3;
    }

    public static GradientTriangle Parse(Stream stream)
    {
        var vertex1 = stream.ReadUInt32();
        var vertex2 = stream.ReadUInt32();
        var vertex3 = stream.ReadUInt32();

        return new GradientTriangle(vertex1, vertex2, vertex3);
    }
}