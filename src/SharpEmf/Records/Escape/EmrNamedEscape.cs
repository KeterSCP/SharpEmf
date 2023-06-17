using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Escape;

/// <inheritdoc cref="EmfRecordType.EMR_NAMEDESCAPE"/>
[PublicAPI]
public record EmrNamedEscape : EnhancedMetafileRecord, IEmfParsable<EmrNamedEscape>
{
    /// <summary>
    /// Specifies the printer driver escape to execute
    /// </summary>
    public MetafileEscapes IEscape { get; }

    /// <summary>
    /// Specifies the number of bytes in the <see cref="DriverName"/> field
    /// </summary>
    /// <remarks>
    /// This value MUST be an even number
    /// </remarks>
    public uint CJDriver { get; }

    /// <summary>
    /// Specifies the number of bytes to pass to the printer driver
    /// </summary>
    public uint CJIn { get; }

    /// <summary>
    /// String of UNICODE characters that specifies the name of the printer driver to receive data
    /// </summary>
    public string DriverName { get; }

    /// <summary>
    /// The data to pass to the printer driver
    /// </summary>
    /// <remarks>
    /// There MUST be <see cref="CJIn"/> bytes available
    /// </remarks>
    public IReadOnlyList<byte> Data { get; }

    private EmrNamedEscape(
        EmfRecordType recordType,
        uint size,
        MetafileEscapes iEscape,
        uint cjDriver,
        uint cjIn,
        string driverName,
        IReadOnlyList<byte> data) : base(recordType, size)
    {
        IEscape = iEscape;
        CJDriver = cjDriver;
        CJIn = cjIn;
        DriverName = driverName;
        Data = data;
    }

    public static EmrNamedEscape Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var iEscape = stream.ReadEnum<MetafileEscapes>();
        var cjDriver = stream.ReadUInt32();

        if (cjDriver % 2 != 0)
        {
            throw new EmfParseException("CJDriver MUST be an even number");
        }

        var cjIn = stream.ReadUInt32();

        var driverName = stream.ReadUnicodeString((int)cjDriver);
        var data = new byte[cjIn];
        stream.ReadExactly(data);

        return new EmrNamedEscape(recordType, size, iEscape, cjDriver, cjIn, driverName, data);
    }
}