using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Escape;

/// <inheritdoc cref="EmfRecordType.EMR_EXTESCAPE"/>
[PublicAPI]
public record EmrExtEscape : EnhancedMetafileRecord, IEmfParsable<EmrExtEscape>
{
    /// <summary>
    /// Specifies the printer driver escape to execute
    /// </summary>
    public MetafileEscapes IEscape { get; }

    /// <summary>
    /// Specifies the number of bytes to pass to the printer driver
    /// </summary>
    public uint CJIn { get; }

    /// <summary>
    /// The data to pass to the printer driver
    /// </summary>
    /// <remarks>
    /// There MUST be <see cref="CJIn"/> bytes available
    /// </remarks>
    public IReadOnlyList<byte> Data { get; }

    private EmrExtEscape(
        EmfRecordType recordType,
        uint size,
        MetafileEscapes iEscape,
        uint cjIn,
        IReadOnlyList<byte> data) : base(recordType, size)
    {
        IEscape = iEscape;
        CJIn = cjIn;
        Data = data;
    }

    public static EmrExtEscape Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var iEscape = stream.ReadEnum<MetafileEscapes>();
        var cjIn = stream.ReadUInt32();

        var data = new byte[cjIn];
        stream.ReadExactly(data);

        return new EmrExtEscape(recordType, size, iEscape, cjIn, data);
    }
}