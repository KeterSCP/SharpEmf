using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Records.Control.Eof;
using SharpEmf.Records.Control.Header;
using SharpEmf.Records.Drawing;

namespace SharpEmf.Records;

[PublicAPI]
public abstract record EnhancedMetafileRecord
{
    /// <summary>
    /// Record type
    /// </summary>
    public abstract EmfRecordType Type { get; }

    /// <summary>
    /// Record size in bytes
    /// </summary>
    public abstract uint Size { get; }

    public static EnhancedMetafileRecord Parse(Stream stream)
    {
        var type = stream.ReadEnum<EmfRecordType>();
        var size = stream.ReadUInt32();

        return type switch
        {
            EmfRecordType.EMR_HEADER => EmfMetafileHeader.Parse(stream, size),
            EmfRecordType.EMR_EOF => EmrEof.Parse(stream, size),

            EmfRecordType.EMR_POLYBEZIER => EmrPolyBezier.Parse(stream, size),
            EmfRecordType.EMR_POLYGON => EmrPolygon.Parse(stream, size),
            EmfRecordType.EMR_POLYLINE => EmrPolyLine.Parse(stream, size),
            EmfRecordType.EMR_POLYLINETO => EmrPolyLineTo.Parse(stream, size),
            EmfRecordType.EMR_SETPIXELV => EmrSetPixelV.Parse(stream, size),
            EmfRecordType.EMR_ANGLEARC => EmrAngleArc.Parse(stream, size),
            EmfRecordType.EMR_ELLIPSE => EmrEllipse.Parse(stream, size),
            EmfRecordType.EMR_RECTANGLE => EmrRectangle.Parse(stream, size),
            EmfRecordType.EMR_ROUNDRECT => EmrRoundRect.Parse(stream, size),
            EmfRecordType.EMR_ARC => EmrArc.Parse(stream, size),
            EmfRecordType.EMR_CHORD => EmrChord.Parse(stream, size),
            EmfRecordType.EMR_PIE => EmrPie.Parse(stream, size),
            EmfRecordType.EMR_LINETO => EmrLineTo.Parse(stream, size),
            EmfRecordType.EMR_ARCTO => EmrArcTo.Parse(stream, size),
            EmfRecordType.EMR_POLYDRAW => EmrPolyDraw.Parse(stream, size),
            EmfRecordType.EMR_FILLPATH => EmrFillPath.Parse(stream, size),
            EmfRecordType.EMR_STROKEPATH => EmrStrokePath.Parse(stream, size),
            EmfRecordType.EMR_POLYBEZIERTO16 => EmrPolyBezierTo16.Parse(stream, size),
            EmfRecordType.EMR_GRADIENTFILL => EmrGradientFill.Parse(stream, size),

            _ => SkipRecord(stream, type, size)
        };
    }

    private static EnhancedMetafileRecord SkipRecord(Stream stream, EmfRecordType type, uint size)
    {
        if (Enum.IsDefined(type))
        {
            throw new MissingEmfRecordParserException(type);
        }

        stream.Seek(size - 8, SeekOrigin.Current);
        return null!;
    }
}