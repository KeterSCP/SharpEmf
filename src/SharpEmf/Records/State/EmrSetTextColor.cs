using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.Objects;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETTEXTCOLOR"/>
[PublicAPI]
public record EmrSetTextColor : EnhancedMetafileRecord, IEmfParsable<EmrSetTextColor>
{
    public ColorRef Color { get; }

    private EmrSetTextColor(EmfRecordType Type, uint Size, ColorRef color) : base(Type, Size)
    {
        Color = color;
    }

    public static EmrSetTextColor Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var color = ColorRef.Parse(stream);
        return new EmrSetTextColor(recordType, size, color);
    }
}