using JetBrains.Annotations;

namespace SharpEmf.Interfaces;

/// <summary>
/// Represents a shape that can be used in a gradient fill.
/// </summary>
/// <remarks>
/// There are only two implementations of this interface: <see cref="Objects.GradientRectangle"/> and <see cref="Objects.GradientTriangle"/>.
/// </remarks>
[PublicAPI]
public interface IGradientShape
{
}