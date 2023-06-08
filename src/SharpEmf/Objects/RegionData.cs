using JetBrains.Annotations;
using SharpEmf.WmfTypes;

namespace SharpEmf.Objects;

/// <summary>
/// Specifies data that defines a region, which is made of non-overlapping rectangles
/// </summary>
[PublicAPI]
public class RegionData
{
    /// <summary>
    /// Defines the contents of the <see cref="Data"/> field
    /// </summary>
    public RegionDataHeader RegionDataHeader { get; }

    /// <summary>
    /// An array of RectL that are merged to create the region
    /// </summary>
    public IReadOnlyList<RectL> Data { get; }

    private RegionData(RegionDataHeader regionDataHeader, IReadOnlyList<RectL> data)
    {
        RegionDataHeader = regionDataHeader;
        Data = data;
    }

    public static RegionData Parse(Stream stream)
    {
        var regionDataHeader = RegionDataHeader.Parse(stream);
        var data = new List<RectL>((int)regionDataHeader.CountRects);
        for (var i = 0; i < regionDataHeader.CountRects; i++)
        {
            data.Add(RectL.Parse(stream));
        }

        return new RegionData(regionDataHeader, data);
    }
}