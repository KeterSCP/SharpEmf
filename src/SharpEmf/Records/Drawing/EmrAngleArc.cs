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
    public override EmfRecordType Type => EmfRecordType.EMR_ANGLEARC;
    public override uint Size { get; }

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

    private EmrAngleArc(uint size, PointL center, uint radius, float startAngle, float sweepAngle)
    {
        Size = size;
        Center = center;
        Radius = radius;
        StartAngle = startAngle;
        SweepAngle = sweepAngle;
    }

    public static EmrAngleArc Parse(Stream stream, uint size)
    {
        var center = PointL.Parse(stream);
        var radius = stream.ReadUInt32();
        var startAngle = stream.ReadFloat32();
        var sweepAngle = stream.ReadFloat32();

        return new EmrAngleArc(size, center, radius, startAngle, sweepAngle);
    }
}