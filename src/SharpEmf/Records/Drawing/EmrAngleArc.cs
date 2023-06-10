using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.Drawing;

/// <inheritdoc cref="EmfRecordType.EMR_ANGLEARC"/>
[PublicAPI]
public record EmrAngleArc : EnhancedMetafileRecord, IEmfParsable<EmrAngleArc>
{
    /// <summary>
    /// Specifies the logical coordinates of the circle's center
    /// </summary>
    public PointL Center { get; }

    /// <summary>
    /// Specifies the circle's radius, in logical units
    /// </summary>
    public uint Radius { get; }

    /// <summary>
    /// Specifies the arc's start angle, in degrees
    /// </summary>
    public float StartAngle { get; }

    /// <summary>
    /// Specifies the arc's sweep angle, in degrees
    /// </summary>
    /// <remarks>
    /// If the sweep angle is greater than 360 degrees, the arc is swept multiple times
    /// </remarks>
    public float SweepAngle { get; }

    private EmrAngleArc(EmfRecordType recordType, uint size, PointL center, uint radius, float startAngle, float sweepAngle) : base(recordType, size)
    {
        Center = center;
        Radius = radius;
        StartAngle = startAngle;
        SweepAngle = sweepAngle;
    }

    public static EmrAngleArc Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var center = PointL.Parse(stream);
        var radius = stream.ReadUInt32();
        var startAngle = stream.ReadFloat32();
        var sweepAngle = stream.ReadFloat32();

        return new EmrAngleArc(recordType, size, center, radius, startAngle, sweepAngle);
    }
}