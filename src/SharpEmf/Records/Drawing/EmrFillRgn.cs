using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_FILLRGN"/>
[PublicAPI]
public record EmrFillRgn : EnhancedMetafileRecord, IEmfParsable<EmrFillRgn>
{
    public override EmfRecordType Type => EmfRecordType.EMR_FILLRGN;
    public override uint Size { get; }

    /// <summary>
    /// Specifies the destination bounding rectangle in logical units
    /// </summary>
    /// <remarks>
    /// If the intersection of this rectangle with the current clipping region is empty, this record has no effect
    /// </remarks>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the size of region data in bytes
    /// </summary>
    public uint RgnDataSize { get; }

    /// <summary>
    /// Specifies the index of the brush in the EMF object table for filling the region
    /// </summary>
    public uint IhBrush { get; }

    /// <summary>
    /// Specifies the output region in a <see cref="RegionData"/> object
    /// </summary>
    /// <remarks>
    /// The bounds specified by the <see cref="RegionDataHeader"/> field of this object be used as the bounding region when this record is processed
    /// </remarks>
    public RegionData RgnData { get; }

    private EmrFillRgn(uint size, RectL bounds, uint rgnDataSize, uint ihBrush, RegionData rgnData)
    {
        Size = size;
        Bounds = bounds;
        RgnDataSize = rgnDataSize;
        IhBrush = ihBrush;
        RgnData = rgnData;
    }

    public static EmrFillRgn Parse(Stream stream, uint size)
    {
        var bounds = RectL.Parse(stream);
        var rgnDataSize = stream.ReadUInt32();
        var ihBrush = stream.ReadUInt32();
        var rgnData = RegionData.Parse(stream);

        Debug.Assert(rgnDataSize == RegionDataHeader.Size + rgnData.Data.Count * Unsafe.SizeOf<RectL>());

        return new EmrFillRgn(size, bounds, rgnDataSize, ihBrush, rgnData);
    }
}