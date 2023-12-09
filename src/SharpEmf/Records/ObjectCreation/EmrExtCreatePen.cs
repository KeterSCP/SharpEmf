using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;

namespace SharpEmf.Records.ObjectCreation;

/// <inheritdoc cref="EmfRecordType.EMR_EXTCREATEPEN"/>
[PublicAPI]
public record EmrExtCreatePen : EnhancedMetafileRecord, IEmfParsable<EmrExtCreatePen>
{
    /// <summary>
    /// Specifies the index of the extended logical pen object in the EMF object table
    /// </summary>
    /// <remarks>
    /// This index MUST be saved so that this object can be reused or modified
    /// </remarks>
    public uint IhPen { get; }

    /// <summary>
    /// Specifies the offset from the start of this record to the DIB header if the record contains a DIB
    /// </summary>
    public uint OffBmi { get; }

    /// <summary>
    /// Specifies the size of the DIB header if the record contains a DIB
    /// </summary>
    public uint CbBmi { get; }

    /// <summary>
    /// Specifies the offset from the start of this record to the DIB bits if the record contains a DIB
    /// </summary>
    public uint OffBits { get; }

    /// <summary>
    /// Specifies the size of the DIB bits if the record contains a DIB
    /// </summary>
    public uint CbBits { get; }

    /// <summary>
    /// Specifies an extended logical pen with attributes including an optional line style array
    /// </summary>
    public LogPenEx Elp { get; }

    /// <summary>
    /// The Device Independent Bitmap header
    /// </summary>
    public IReadOnlyList<byte> BmiSrc { get; }

    /// <summary>
    /// The Device Independent Bitmap bits
    /// </summary>
    public IReadOnlyList<byte> BitsSrc { get; }

    private EmrExtCreatePen(
        EmfRecordType recordType,
        uint size,
        uint ihPen,
        uint offBmi,
        uint cbBmi,
        uint offBits,
        uint cbBits,
        LogPenEx elp,
        IReadOnlyList<byte> bmiSrc,
        IReadOnlyList<byte> bitsSrc) : base(recordType, size)
    {
        IhPen = ihPen;
        OffBmi = offBmi;
        CbBmi = cbBmi;
        OffBits = offBits;
        CbBits = cbBits;
        Elp = elp;
        BmiSrc = bmiSrc;
        BitsSrc = bitsSrc;
    }

    public static EmrExtCreatePen Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var positionBeforeParsing =
            stream.Position -
            // Base record fields
            (Unsafe.SizeOf<EmfRecordType>() + Unsafe.SizeOf<uint>());

        var ihPen = stream.ReadUInt32();
        var offBmi = stream.ReadUInt32();
        var cbBmi = stream.ReadUInt32();
        var offBits = stream.ReadUInt32();
        var cbBits = stream.ReadUInt32();
        var elp = LogPenEx.Parse(stream);

        var positionAfterParsing = stream.Position;
        var selfSizeWithoutBuffers = positionAfterParsing - positionBeforeParsing;

        long seekOffset = 0;
        // Is the second condition correct?
        if (offBmi != 0 && offBmi > selfSizeWithoutBuffers)
        {
            seekOffset = offBmi - selfSizeWithoutBuffers;
            stream.Seek(seekOffset, SeekOrigin.Current);
        }

        var bmiSrc = stream.ReadByteArray((int)cbBmi);
        // Same as above
        if (offBits != 0 && offBits > seekOffset + bmiSrc.Length + selfSizeWithoutBuffers)
        {
            seekOffset = offBits - (seekOffset + bmiSrc.Length + selfSizeWithoutBuffers);
            stream.Seek(seekOffset, SeekOrigin.Current);
        }

        var bitsSrc = stream.ReadByteArray((int)cbBits);

        return new EmrExtCreatePen(recordType, size, ihPen, offBmi, cbBmi, offBits, cbBits, elp, bmiSrc, bitsSrc);
    }
}