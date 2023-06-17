using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.WmfTypes;

namespace SharpEmf.Objects;

/// <summary>
/// Contains values for text output
/// </summary>
[PublicAPI]
public record EmrText
{
    /// <summary>
    /// Specifies the coordinates of the reference point used to position the string
    /// </summary>
    public PointL Reference { get; }

    /// <summary>
    /// Specifies the number of characters in the string
    /// </summary>
    public uint Chars { get; }

    /// <summary>
    /// Specifies the offset to the output string in bytes, from the start of the record in which this object is contained
    /// </summary>
    /// <remarks>
    /// This value is 8- or 16-bit aligned, according to the character format
    /// </remarks>
    public uint OffString { get; }

    /// <summary>
    /// Specifies how to use the rectangle specified in the <see cref="Rectangle"/> field
    /// </summary>
    public ExtTextOutOptions Options { get; }

    /// <summary>
    /// Defines a clipping and/or opaquing rectangle in logical units.
    /// This rectangle is applied to the text output performed by the containing record
    /// </summary>
    public RectL? Rectangle { get; }

    /// <summary>
    /// Specifies the offset to an intercharacter spacing array in bytes, from the start of the record in which this object is contained
    /// </summary>
    /// <remarks>
    /// This value is 32-bit aligned
    /// </remarks>
    public uint OffDx { get; }

    /// <summary>
    /// The character string buffer
    /// </summary>
    /// <remarks>
    /// The size and encoding of the characters is determined by the type of record that
    /// contains this object, as follows:
    /// <para>
    /// - <see cref="EmfRecordType.EMR_EXTTEXTOUTA"/> and <see cref="EmfRecordType.EMR_POLYTEXTOUTA"/> records: 8-bit ASCII characters
    /// </para>
    /// <para>
    /// - <see cref="EmfRecordType.EMR_EXTTEXTOUTW"/> and <see cref="EmfRecordType.EMR_POLYTEXTOUTW"/> records: 16-bit UNICODE characters
    /// </para>
    /// </remarks>
    public string StringBuffer { get; }

    /// <summary>
    /// The character spacing buffer that specifies the output spacing between the origins of adjacent character cells in logical units
    /// </summary>
    /// <remarks>
    /// The location of this field is specified by the value of offDx in bytes from the start of this record.
    /// If spacing is defined, this field contains the same number of values as characters in the output string
    /// <para />
    /// If the <see cref="Options"/> field contains the <see cref="ExtTextOutOptions.ETO_PDY"/> flag, then this buffer contains twice as
    /// many values as there are characters in the output string, one horizontal and one vertical offset for each, in that order
    /// </remarks>
    public uint[] DxBuffer { get; }

    private EmrText(
        PointL reference,
        uint chars,
        uint offString,
        ExtTextOutOptions options,
        RectL? rectangle,
        uint offDx,
        string stringBuffer,
        uint[] dxBuffer)
    {
        Reference = reference;
        Chars = chars;
        OffString = offString;
        Options = options;
        Rectangle = rectangle;
        OffDx = offDx;
        StringBuffer = stringBuffer;
        DxBuffer = dxBuffer;
    }

    public static EmrText Parse(Stream stream, EmfRecordType parentRecordType,  int parentSizeWithoutTextBuffer)
    {
        var reference = PointL.Parse(stream);
        var chars = stream.ReadUInt32();
        var offString = stream.ReadUInt32();
        var options = stream.ReadEnum<ExtTextOutOptions>();

        RectL? rectangle = null;
        if (options.HasFlag(ExtTextOutOptions.ETO_NO_RECT))
        {
            stream.Seek(Unsafe.SizeOf<RectL>(), SeekOrigin.Current);
        }
        else
        {
            rectangle = RectL.Parse(stream);
        }
        var offDx = stream.ReadUInt32();

        var selfSize =
            Unsafe.SizeOf<PointL>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<uint>() +
            Unsafe.SizeOf<ExtTextOutOptions>() +
            Unsafe.SizeOf<RectL>() +
            Unsafe.SizeOf<uint>();

        var seekOffset = offString - (parentSizeWithoutTextBuffer + selfSize);
        stream.Seek(seekOffset, SeekOrigin.Current);

        var stringBytesCount = parentRecordType switch
        {
            EmfRecordType.EMR_EXTTEXTOUTA or EmfRecordType.EMR_POLYTEXTOUTA => (int)chars,
            EmfRecordType.EMR_EXTTEXTOUTW or EmfRecordType.EMR_POLYTEXTOUTW => (int)(chars * 2),
            _ => throw new EmfParseException("Unexpected parent record type")
        };

        var stringBuffer = parentRecordType switch
        {
            EmfRecordType.EMR_EXTTEXTOUTA or EmfRecordType.EMR_POLYTEXTOUTA => stream.ReadAsciiString(stringBytesCount),
            EmfRecordType.EMR_EXTTEXTOUTW or EmfRecordType.EMR_POLYTEXTOUTW => stream.ReadUnicodeString(stringBytesCount),
            _ => throw new EmfParseException("Unexpected parent record type")
        };

        seekOffset = offDx - (parentSizeWithoutTextBuffer + selfSize + seekOffset + stringBytesCount);
        stream.Seek(seekOffset, SeekOrigin.Current);

        var dxBufferSize = options.HasFlag(ExtTextOutOptions.ETO_PDY) ? chars * 2 : chars;
        var dxBuffer = stream.ReadUInt32Array((int)dxBufferSize);

        return new EmrText(reference, chars, offString, options, rectangle, offDx, stringBuffer, dxBuffer);
    }
}