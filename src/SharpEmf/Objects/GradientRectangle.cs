using JetBrains.Annotations;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Records.Drawing;

namespace SharpEmf.Objects;

/// <summary>
/// Defines a rectangle using <see cref="TriVertex"/> objects
/// in an <see cref="Records.Drawing.EmrGradientFill"/> record
/// </summary>
[PublicAPI]
public readonly struct GradientRectangle : IGradientShape
{
    /// <summary>
    /// An index into an array of <see cref="TriVertex"/> objects that specifies the upper-left vertex of a rectangle.
    /// The index MUST be smaller than the size of the array, as defined by the <see cref="EmrGradientFill.NVer"/>
    /// </summary>
    public uint UpperLeft { get; }

    /// <summary>
    /// An index into an array of <see cref="TriVertex"/> objects that specifies the lower-right vertex of a rectangle.
    /// The index MUST be smaller than the size of the array, as defined by the <see cref="EmrGradientFill.NVer"/>
    /// </summary>
    public uint LowerRight { get; }

    private GradientRectangle(uint upperLeft, uint lowerRight)
    {
        UpperLeft = upperLeft;
        LowerRight = lowerRight;
    }

    public static GradientRectangle Parse(Stream stream)
    {
        var upperLeft = stream.ReadUInt32();
        var lowerRight = stream.ReadUInt32();

        return new GradientRectangle(upperLeft, lowerRight);
    }
}