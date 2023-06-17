using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Extensions;
using SharpEmf.Records.Clipping;
using SharpEmf.Records.Control.Eof;
using SharpEmf.Records.Control.Header;
using SharpEmf.Records.Drawing;
using SharpEmf.Records.Escape;

namespace SharpEmf.Records;

[PublicAPI]
public abstract record EnhancedMetafileRecord(EmfRecordType Type, uint Size)
{
    /// <summary>
    /// Record type
    /// </summary>
    public EmfRecordType Type { get; } = Type;

    /// <summary>
    /// Record size in bytes
    /// </summary>
    public uint Size { get; } = Size;

    public static EnhancedMetafileRecord Parse(Stream stream)
    {
        var type = stream.ReadEnum<EmfRecordType>();
        var size = stream.ReadUInt32();

        if (size % 4 != 0)
        {
            throw new EmfParseException($"Record size is not a multiple of 4: {size} bytes");
        }

        Func<Stream, EmfRecordType, uint, EnhancedMetafileRecord> parseFunc = type switch
        {
            EmfRecordType.EMR_HEADER => EmfMetafileHeader.Parse,
            EmfRecordType.EMR_EOF => EmrEof.Parse,

            EmfRecordType.EMR_POLYBEZIER => EmrPolyBezier.Parse,
            EmfRecordType.EMR_POLYGON => EmrPolygon.Parse,
            EmfRecordType.EMR_POLYLINE => EmrPolyLine.Parse,
            EmfRecordType.EMR_POLYBEZIERTO => EmrPolyBezierTo.Parse,
            EmfRecordType.EMR_POLYLINETO => EmrPolyLineTo.Parse,
            EmfRecordType.EMR_POLYPOLYLINE => EmrPolyPolyline.Parse,
            EmfRecordType.EMR_POLYPOLYGON => EmrPolyPolygon.Parse,
            EmfRecordType.EMR_SETPIXELV => EmrSetPixelV.Parse,
            EmfRecordType.EMR_OFFSETCLIPRGN => EmrOffsetClipRgn.Parse,
            EmfRecordType.EMR_EXCLUDECLIPRECT => EmrExcludeClipRect.Parse,
            EmfRecordType.EMR_INTERSECTCLIPRECT => EmrIntersectClipRect.Parse,
            EmfRecordType.EMR_ANGLEARC => EmrAngleArc.Parse,
            EmfRecordType.EMR_ELLIPSE => EmrEllipse.Parse,
            EmfRecordType.EMR_RECTANGLE => EmrRectangle.Parse,
            EmfRecordType.EMR_ROUNDRECT => EmrRoundRect.Parse,
            EmfRecordType.EMR_ARC => EmrArc.Parse,
            EmfRecordType.EMR_CHORD => EmrChord.Parse,
            EmfRecordType.EMR_PIE => EmrPie.Parse,
            EmfRecordType.EMR_EXTFLOODFILL => EmrExtFloodFill.Parse,
            EmfRecordType.EMR_LINETO => EmrLineTo.Parse,
            EmfRecordType.EMR_ARCTO => EmrArcTo.Parse,
            EmfRecordType.EMR_POLYDRAW => EmrPolyDraw.Parse,
            EmfRecordType.EMR_FILLPATH => EmrFillPath.Parse,
            EmfRecordType.EMR_STROKEANDFILLPATH => EmrStrokeAndFillPath.Parse,
            EmfRecordType.EMR_STROKEPATH => EmrStrokePath.Parse,
            EmfRecordType.EMR_SELECTCLIPPATH => EmrSelectClipPath.Parse,
            EmfRecordType.EMR_FILLRGN => EmrFillRgn.Parse,
            EmfRecordType.EMR_FRAMERGN => EmrFrameRgn.Parse,
            EmfRecordType.EMR_PAINTRGN => EmrPaintRgn.Parse,
            EmfRecordType.EMR_EXTSELECTCLIPRGN => EmrExtSelectClipRgn.Parse,
            EmfRecordType.EMR_EXTTEXTOUTA => EmrExtTextOutA.Parse,
            EmfRecordType.EMR_EXTTEXTOUTW => EmrExtTextOutW.Parse,
            EmfRecordType.EMR_POLYBEZIER16 => EmrPolyBezier16.Parse,
            EmfRecordType.EMR_POLYGON16 => EmrPolygon16.Parse,
            EmfRecordType.EMR_POLYLINE16 => EmrPolyline16.Parse,
            EmfRecordType.EMR_POLYBEZIERTO16 => EmrPolyBezierTo16.Parse,
            EmfRecordType.EMR_POLYLINETO16 => EmrPolylineTo16.Parse,
            EmfRecordType.EMR_POLYPOLYLINE16 => EmrPolyPolyline16.Parse,
            EmfRecordType.EMR_POLYPOLYGON16 => EmrPolyPolygon16.Parse,
            EmfRecordType.EMR_POLYDRAW16 => EmrPolyDraw16.Parse,
            EmfRecordType.EMR_POLYTEXTOUTA => EmrPolyTextOutA.Parse,
            EmfRecordType.EMR_POLYTEXTOUTW => EmrPolyTextOutW.Parse,
            EmfRecordType.EMR_DRAWESCAPE => EmrDrawEscape.Parse,
            EmfRecordType.EMR_EXTESCAPE => EmrExtEscape.Parse,
            EmfRecordType.EMR_SMALLTEXTOUT => EmrSmallTextOut.Parse,
            EmfRecordType.EMR_NAMEDESCAPE => EmrNamedEscape.Parse,
            EmfRecordType.EMR_GRADIENTFILL => EmrGradientFill.Parse,

            _ => SkipRecord
        };
        return parseFunc(stream, type, size);
    }

    private static EnhancedMetafileRecord SkipRecord(Stream stream, EmfRecordType recordType, uint size)
    {
        if (Enum.IsDefined(recordType))
        {
            throw new MissingEmfRecordParserException(recordType);
        }

        stream.Seek(size - 8, SeekOrigin.Current);
        return null!;
    }
}