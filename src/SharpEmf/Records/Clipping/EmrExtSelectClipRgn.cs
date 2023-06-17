using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Clipping;

/// <inheritdoc cref="EmfRecordType.EMR_EXTSELECTCLIPRGN"/>
[PublicAPI]
public record EmrExtSelectClipRgn : EnhancedMetafileRecord, IEmfParsable<EmrExtSelectClipRgn>
{
    /// <summary>
    /// Specifies the size of region data in bytes
    /// </summary>
    public uint RgnDataSize { get; }

    /// <summary>
    /// Specifies the way to use the region
    /// </summary>
    public RegionMode RegionMode { get; }

    /// <summary>
    /// Specifies the <see cref="RegionData"/> object in logical units
    /// </summary>
    /// <remarks>
    /// If <see cref="RegionMode"/> is <see cref="RegionMode.RGN_COPY"/>,
    /// this data can be omitted and the clipping region SHOULD be set to the default clipping region
    /// </remarks>
    public RegionData? RgnData { get; }

    private EmrExtSelectClipRgn(EmfRecordType recordType, uint size, uint rgnDataSize, RegionMode regionMode, RegionData? rgnData) : base(recordType, size)
    {
        RgnDataSize = rgnDataSize;
        RegionMode = regionMode;
        RgnData = rgnData;
    }

    public static EmrExtSelectClipRgn Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var rgnDataSize = stream.ReadUInt32();
        var regionMode = stream.ReadEnum<RegionMode>();

        if (regionMode == RegionMode.RGN_COPY)
        {
            stream.Seek(rgnDataSize, SeekOrigin.Current);
            // Is this a correct way of handling copy mode?
            return new EmrExtSelectClipRgn(recordType, size, rgnDataSize, regionMode, null);
        }

        var rgnData = RegionData.Parse(stream);
        Debug.Assert(rgnDataSize == RegionDataHeader.Size + rgnData.Data.Count * Unsafe.SizeOf<RectL>());

        return new EmrExtSelectClipRgn(recordType, size, rgnDataSize, regionMode, rgnData);
    }
}