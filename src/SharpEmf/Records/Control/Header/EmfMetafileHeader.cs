using System.Diagnostics;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Control.Header;

/// <inheritdoc cref="EmfRecordType.EMR_HEADER"/>
[PublicAPI]
public record EmfMetafileHeader : EnhancedMetafileRecord, IEmfParsable<EmfMetafileHeader>
{
    /// <summary>
    /// Fixed-size part of the header
    /// </summary>
    public const int FixedSize = 88;

    public override EmfRecordType Type => EmfRecordType.EMR_HEADER;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the rectangular inclusive-inclusive bounds in logical units of the smallest rectangle that can be drawn around the image stored in the metafile
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the rectangular inclusive-inclusive dimensions, in .01 millimeter units, of a rectangle that surrounds the image stored in the metafile
    /// </summary>
    public RectL Frame { get; }

    /// <summary>
    /// Record signature
    /// </summary>
    public FormatSignature Signature { get; } = FormatSignature.ENHMETA_SIGNATURE;

    /// <summary>
    /// Specifies the EMF version for interoperability. This MAY be 0x00010000
    /// </summary>
    public MetafileVersion Version { get; }

    /// <summary>
    /// Specifies the size of the metafile in bytes
    /// </summary>
    public uint Bytes { get; }

    /// <summary>
    /// Specifies the number of records in the metafile
    /// </summary>
    public uint Records { get; }

    /// <summary>
    /// Specifies the number of graphics objects that are used during the processing of the metafile
    /// </summary>
    public ushort Handles { get; }

    /// <summary>
    /// Specifies the number of UNICODE characters in the array that contains the description of the metafile's contents. This is zero if there is no description string
    /// </summary>
    public uint NDescription { get; }

    /// <summary>
    /// Specifies the offset from the beginning of this record to the array that contains the description of the metafile's contents
    /// </summary>
    public uint OffDescription { get; }

    /// <summary>
    /// Specifies the number of entries in the metafile palette. The location of the palette is specified in the EMR_EOF record
    /// </summary>
    public uint PalEntries { get; }

    /// <summary>
    /// Specifies the size of the reference device, in pixels
    /// </summary>
    public SizeL Device { get; }

    /// <summary>
    /// Specifies the size of the reference device, in millimeters
    /// </summary>
    public SizeL Millimeters { get; }

    /// <summary>
    /// Optional UNICODE description string
    /// </summary>
    public string? Description { get; private init; }

    protected EmfMetafileHeader(
        uint size,
        RectL bounds,
        RectL frame,
        MetafileVersion version,
        uint bytes,
        uint records,
        ushort handles,
        uint nDescription,
        uint offDescription,
        uint palEntries,
        SizeL device,
        SizeL millimeters,
        string? description)
    {
        Size = size;
        Bounds = bounds;
        Frame = frame;
        Version = version;
        Bytes = bytes;
        Records = records;
        Handles = handles;
        NDescription = nDescription;
        OffDescription = offDescription;
        PalEntries = palEntries;
        Device = device;
        Millimeters = millimeters;
        Description = description;
    }

    public static EmfMetafileHeader Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);
        var frame = RectL.Parse(stream);

        var signature = stream.ReadEnum<FormatSignature>();
        if (signature != FormatSignature.ENHMETA_SIGNATURE)
        {
            throw new Exception($"Invalid signature of EMF header: {signature}");
        }

        var version = stream.ReadEnum<MetafileVersion>();

        Debug.Assert(version == MetafileVersion.META_FORMAT_ENHANCED);

        var bytes = stream.ReadUInt32();
        var records = stream.ReadUInt32();
        var handles = stream.ReadUInt16();

        // Reserved (2 bytes): MUST be 0x0000 and MUST be ignored.
        var reserved = stream.ReadUInt16();
        if (reserved != 0)
        {
            throw new Exception($"Invalid value of reserved field in EMF header. Expected: 0, Actual: {reserved}");
        }

        var nDescription = stream.ReadUInt32();
        var offDescription = stream.ReadUInt32();

        var palEntries = stream.ReadUInt32();

        var device = new SizeL(
            cx: stream.ReadUInt32(),
            cy: stream.ReadUInt32());

        var millimeters = new SizeL(
            cx: stream.ReadUInt32(),
            cy: stream.ReadUInt32());

        var baseHeader = new EmfMetafileHeader(
            size: size,
            bounds: bounds,
            frame: frame,
            version: version,
            bytes: bytes,
            records: records,
            handles: handles,
            nDescription: nDescription,
            offDescription: offDescription,
            palEntries: palEntries,
            device: device,
            millimeters: millimeters,
            description: null);

        var resultSize = CalculateBaseHeaderSize(size, offDescription, nDescription);
        if (resultSize >= 100)
        {
            var headerExtension1 = EmfMetafileHeaderExtension1.Parse(stream, baseHeader);

            if (headerExtension1.OffPixelFormat >= 100
                && headerExtension1.OffPixelFormat + headerExtension1.CbPixelFormat <= baseHeader.Size
                && headerExtension1.OffPixelFormat < resultSize)
            {
                resultSize = headerExtension1.OffPixelFormat;
            }

            switch (resultSize)
            {
                case >= 108:
                {
                    var headerExtension2 = EmfMetafileHeaderExtension2.Parse(stream, headerExtension1);
                    var description = headerExtension2.ParseDescription(stream, offDescription, nDescription);
                    ReadAndSkipPixelFormatObject(stream, headerExtension2);

                    return headerExtension2 with
                    {
                        Description = description
                    };
                }
                case >= 100:
                {
                    var description = headerExtension1.ParseDescription(stream, offDescription, nDescription);
                    ReadAndSkipPixelFormatObject(stream, headerExtension1);

                    return headerExtension1 with
                    {
                        Description = description
                    };
                }
            }
        }

        return baseHeader with
        {
            Description = baseHeader.ParseDescription(stream, offDescription, nDescription)
        };
    }

    private static uint CalculateBaseHeaderSize(uint declaredSize, uint offDescription, uint nDescription)
    {
        uint resultSize = FixedSize;

        if (declaredSize < FixedSize)
        {
            return resultSize;
        }

        resultSize = declaredSize;
        if (offDescription >= FixedSize && offDescription + nDescription * 2 <= declaredSize)
        {
            resultSize = offDescription;
        }

        return resultSize;
    }

    private static void ReadAndSkipPixelFormatObject(Stream stream, EmfMetafileHeaderExtension1 extension)
    {
        if (extension.CbPixelFormat != 0 && extension.OffPixelFormat != 0)
        {
            Debug.Assert(stream.Position == extension.OffPixelFormat);
            // TODO: implement reading of PixelFormatDescriptor object if needed
            stream.Seek(extension.CbPixelFormat, SeekOrigin.Current);
        }
    }

    /// <summary>
    /// Tries to parse an optional description string from the specified stream. Stream MUST have position = <paramref name="offDescription"/>
    /// </summary>
    /// <param name="stream">Input EMF file stream</param>
    /// <param name="offDescription">Offset from the beginning of header record to the array that contains the description of the metafile's contents</param>
    /// <param name="nDescription">Number of UNICODE characters in the array that contains the description of the metafile's contents</param>
    /// <returns>If parse was successful - description string, otherwise - null</returns>
    private string? ParseDescription(Stream stream, uint offDescription, uint nDescription)
    {
        if (offDescription >= FixedSize && offDescription + nDescription * 2 <= Size)
        {
            Debug.Assert(stream.Position == offDescription);
            return stream.ReadUnicodeString((int)(nDescription * 2));
        }

        return null;
    }
}