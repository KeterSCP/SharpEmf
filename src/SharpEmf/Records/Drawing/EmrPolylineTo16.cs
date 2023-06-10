using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_POLYLINETO16"/>
[PublicAPI]
public record EmrPolylineTo16 : EnhancedMetafileRecord, IEmfParsable<EmrPolylineTo16>
{
    /// <summary>
    /// Specifies the bounding rectangle in logical units
    /// </summary>
    public RectL Bounds { get; }

    /// <summary>
    /// Specifies the number of points in the <see cref="APoints"/> array
    /// </summary>
    public uint Count { get; }

    /// <summary>
    /// Specifies the point data, in logical units
    /// </summary>
    public IReadOnlyList<PointS> APoints { get; }

    private EmrPolylineTo16(EmfRecordType recordType, uint size, RectL bounds, uint count, IReadOnlyList<PointS> aPoints) : base(recordType, size)
    {
        Bounds = bounds;
        Count = count;
        APoints = aPoints;
    }

    public static EmrPolylineTo16 Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var bounds = RectL.Parse(stream);
        // TODO: according to the documentation, number of maximum points allowed depends on line width and on the fact if device supports wideline
        var count = stream.ReadUInt32();

        var points = new PointS[(int)count];
        for (var i = 0; i < count; i++)
        {
            points[i] = PointS.Parse(stream);
        }

        return new EmrPolylineTo16(recordType, size, bounds, count, points);
    }
}