using JetBrains.Annotations;
using SharpEmf.Records;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that uniquely identify records in an EMF metafile.
/// These values are specified in the <see cref="EnhancedMetafileRecord.Type"/> fields of EMF records
/// </summary>
[PublicAPI]
public enum EmfRecordType : uint
{
    /// <summary>
    /// Defines the start of the metafile and specifies its characteristics; its
    /// contents, including the dimensions of the embedded image; the number of records in the metafile;
    /// and the resolution of the device on which the embedded image was created
    /// </summary>
    EMR_HEADER = 0x00000001,

    /// <summary>
    /// Defines one or more Bezier curves.
    /// </summary>
    /// <remarks>
    /// Cubic Bezier curves are defined using specified endpoints and control points, and are stroked with the current pen
    /// </remarks>
    EMR_POLYBEZIER = 0x00000002,

    /// <summary>
    /// Defines a polygon consisting of two or more vertexes connected by straight lines.
    /// The polygon is outlined by using the current pen and filled by using the current brush and polygon fill mode.
    /// The polygon is closed automatically by drawing a line from the last vertex to the first
    /// </summary>
    EMR_POLYGON = 0x00000003,

    /// <summary>
    /// Defines a series of line segments by connecting the points in the specified array
    /// </summary>
    EMR_POLYLINE = 0x00000004,

    /// <summary>
    ///  This record defines one or more straight lines based upon the current drawing position
    /// </summary>
    /// <remarks>
    /// A line is drawn from the current drawing position to the first point specified by the points
    /// field by using the current pen. For each additional line, drawing is performed from the ending
    /// point of the previous line to the next point specified by points
    /// </remarks>
    EMR_POLYLINETO = 0x00000006,

    /// <summary>
    /// This record indicates the end of the metafile
    /// </summary>
    EMR_EOF = 0x0000000E,

    /// <summary>
    /// Defines the color of the pixel at the specified logical coordinates
    /// </summary>
    EMR_SETPIXELV = 0x0000000F,

    /// <summary>
    /// Defines a line segment of an arc
    /// </summary>
    /// <remarks>
    /// The line segment is drawn from the current drawing position to the beginning of the arc.
    /// The arc is drawn along the perimeter of a circle with the given radius and center.
    /// The length of the arc is defined by the given start and sweep angles
    /// </remarks>
    EMR_ANGLEARC = 0x00000029,

    /// <summary>
    /// Defines an ellipse. The center of the ellipse is the center of the specified bounding rectangle.
    /// The ellipse is outlined by using the current pen and is filled by using the current brush
    /// </summary>
    EMR_ELLIPSE = 0x0000002A,

    /// <summary>
    /// Defines a rectangle. The rectangle is outlined by using the current pen and filled by using the current brush <para />
    /// The current drawing position is neither used nor updated by this record. <para />
    /// If a PS_NULL pen is used, the dimensions of the rectangle are 1 pixel less in height and 1 pixel less in width
    /// </summary>
    EMR_RECTANGLE = 0x0000002B,

    /// <summary>
    /// Defines a rectangle with rounded corners. The rectangle is outlined by using the current pen and filled by using the current brush
    /// </summary>
    EMR_ROUNDRECT = 0x0000002C,

    /// <summary>
    /// Defines an elliptical arc
    /// </summary>
    EMR_ARC = 0x0000002D,

    /// <summary>
    /// Defines a line from the current drawing position up to, but not including,
    /// the specified point. It resets the current drawing position to the specified point
    /// </summary>
    EMR_LINETO = 0x00000036,

    /// <summary>
    /// Defines a set of line segments and Bezier curves
    /// </summary>
    EMR_POLYDRAW = 0x00000038,

    /// <summary>
    /// Closes any open figures in the current path bracket and fills its interior by using the current brush and polygon-filling mode
    /// </summary>
    EMR_FILLPATH = 0x0000003E,

    /// <summary>
    /// Renders the specified path by using the current pen
    /// </summary>
    EMR_STROKEPATH = 0x00000040,

    /// <summary>
    /// Defines one or more Bezier curves based on the current position
    /// </summary>
    EMR_POLYBEZIERTO16 = 0x00000058,

    /// <summary>
    /// Specifies filling rectangles or triangles with gradients of
    /// </summary>
    EMR_GRADIENTFILL = 0x00000076,
}