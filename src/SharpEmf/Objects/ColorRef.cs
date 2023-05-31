using JetBrains.Annotations;

namespace SharpEmf.Objects;

/// <summary>
/// Defines the RGB color
/// </summary>
[PublicAPI]
public readonly struct ColorRef
{
    /// <summary>
    /// Defines the relative intensity of red
    /// </summary>
    public byte Red { get; }

    /// <summary>
    /// Defines the relative intensity of green
    /// </summary>
    public byte Green { get; }

    /// <summary>
    /// Defines the relative intensity of blue
    /// </summary>
    public byte Blue { get; }

    private ColorRef(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public static ColorRef Parse(Stream stream)
    {
        var red = stream.ReadByte();
        var green = stream.ReadByte();
        var blue = stream.ReadByte();
        var reserved = stream.ReadByte();

        if (reserved != 0x00)
        {
            throw new Exception($"Reserved byte must be 0x00, but was {reserved}");
        }

        return new ColorRef((byte)red, (byte)green, (byte)blue);
    }
}