using JetBrains.Annotations;
using SharpEmf.Extensions;

namespace SharpEmf.Objects;

/// <summary>
/// Defines a two-dimensional, linear transform matrix
/// </summary>
/// <remarks>
/// The following equations specify how the matrix values are used to transform a point (X,Y) to a new point (X',Y'):
/// <code>
/// X' = M11 * X + M21 * Y + Dx
/// Y' = M12 * X + M22 * Y + Dy
/// </code>
/// <code>
/// | Operation  | M11                             | M12                                 | M21                               | M22                           |
/// |------------|---------------------------------|-------------------------------------|-----------------------------------|-------------------------------|
/// | Rotation   | Cosine                          | Sine                                | Negative sine                     | Cosine                        |
/// | Scaling    | Horizontal scaling component    | Not used                            | Not used                          | Vertical Scaling Component    |
/// | Shear      | Not used                        | Horizontal Proportionality Constant | Vertical Proportionality Constant | Not used                      |
/// | Reflection | Horizontal Reflection Component | Not used                            | Not used                          | Vertical Reflection Component |
/// </code>
/// </remarks>
[PublicAPI]
public readonly struct XForm
{
    public float M11 { get; }
    public float M12 { get; }
    public float M21 { get; }
    public float M22 { get; }

    /// <summary>
    /// The horizontal translation component, in logical units
    /// </summary>
    public float Dx { get; }

    /// <summary>
    /// The vertical translation component, in logical units
    /// </summary>
    public float Dy { get; }

    private XForm(float m11, float m12, float m21, float m22, float dx, float dy)
    {
        M11 = m11;
        M12 = m12;
        M21 = m21;
        M22 = m22;
        Dx = dx;
        Dy = dy;
    }

    public static XForm Parse(Stream stream)
    {
        var m11 = stream.ReadFloat32();
        var m12 = stream.ReadFloat32();
        var m21 = stream.ReadFloat32();
        var m22 = stream.ReadFloat32();
        var dx = stream.ReadFloat32();
        var dy = stream.ReadFloat32();

        return new XForm(m11, m12, m21, m22, dx, dy);
    }
}