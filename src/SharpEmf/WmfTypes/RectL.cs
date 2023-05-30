﻿using JetBrains.Annotations;

namespace SharpEmf.WmfTypes;

/// <summary>
/// Defines a rectangle
/// </summary>
[PublicAPI]
public readonly struct RectL
{
    /// <summary>
    /// Defines the x coordinate, in logical coordinates, of the upper-left corner of the rectangle
    /// </summary>
    public int Left { get; }

    /// <summary>
    /// Defines the y coordinate, in logical coordinates, of the upper-left corner of the rectangle
    /// </summary>
    public int Top { get; }

    /// <summary>
    /// Defines the x coordinate, in logical coordinates, of the lower-right corner of the rectangle
    /// </summary>
    public int Right { get; }

    /// <summary>
    /// Defines y coordinate, in logical coordinates, of the lower-right corner of the rectangle
    /// </summary>
    public int Bottom { get; }

    public RectL(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}