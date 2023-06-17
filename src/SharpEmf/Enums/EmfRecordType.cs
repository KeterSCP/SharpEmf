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
    /// Defines a polygon consisting of two or more vertexes connected by straight lines
    /// </summary>
    /// <remarks>
    /// The polygon is outlined by using the current pen and filled by using the current brush and polygon fill mode.
    /// The polygon is closed automatically by drawing a line from the last vertex to the first
    /// </remarks>
    EMR_POLYGON = 0x00000003,

    /// <summary>
    /// Defines a series of line segments by connecting the points in the specified array
    /// </summary>
    EMR_POLYLINE = 0x00000004,

    /// <summary>
    /// Defines one or more Bezier curves based upon the current drawing position
    /// </summary>
    /// <remarks>
    /// The Bezier curves SHOULD be drawn using the current pen
    /// </remarks>
    EMR_POLYBEZIERTO = 0x00000005,

    /// <summary>
    /// Defines one or more straight lines based upon the current drawing position
    /// </summary>
    /// <remarks>
    /// A line is drawn from the current drawing position to the first point specified by the points field by using the current pen <para />
    /// For each additional line, drawing is performed from the ending point of the previous line to the next point specified by points
    /// </remarks>
    EMR_POLYLINETO = 0x00000006,

    /// <summary>
    /// Defines multiple series of connected line segments
    /// </summary>
    /// <remarks>
    /// The line segments are drawn by using the current pen <para />
    /// The figures formed by the segments are not filled <para />
    /// The current position is neither used nor updated by this record
    /// </remarks>
    EMR_POLYPOLYLINE = 0x00000007,

    /// <summary>
    /// Defines a series of closed polygons
    /// </summary>
    /// <remarks>
    /// Each polygon is outlined by using the current pen and filled by using the current brush and polygon fill mode <para />
    /// The polygons defined by this record can overlap
    /// </remarks>
    EMR_POLYPOLYGON = 0x00000008,

    /// <summary>
    /// Indicates the end of the metafile
    /// </summary>
    EMR_EOF = 0x0000000E,

    /// <summary>
    /// Defines the color of the pixel at the specified logical coordinates
    /// </summary>
    EMR_SETPIXELV = 0x0000000F,

    /// <summary>
    /// Redefines the current clipping region by the specified offsets
    /// </summary>
    EMR_OFFSETCLIPRGN = 0x0000001A,

    /// <summary>
    /// Defines a new clipping region that consists of the current clipping region intersected with the specified rectangle
    /// </summary>
    EMR_EXCLUDECLIPRECT = 0x0000001D,

    /// <summary>
    /// Defines a new clipping region from the intersection of the current clipping region and the specified rectangle
    /// </summary>
    EMR_INTERSECTCLIPRECT = 0x0000001E,

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
    /// Defines an ellipse. The center of the ellipse is the center of the specified bounding rectangle
    /// </summary>
    /// <remarks>
    /// The ellipse is outlined by using the current pen and is filled by using the current brush
    /// </remarks>
    EMR_ELLIPSE = 0x0000002A,

    /// <summary>
    /// Defines a rectangle
    /// </summary>
    /// <remarks>
    /// The rectangle is outlined by using the current pen and filled by using the current brush <para />
    /// The current drawing position is neither used nor updated by this record. <para />
    /// If a PS_NULL pen is used, the dimensions of the rectangle are 1 pixel less in height and 1 pixel less in width
    /// </remarks>
    EMR_RECTANGLE = 0x0000002B,

    /// <summary>
    /// Defines a rectangle with rounded corners
    /// </summary>
    /// <remarks>
    /// The rectangle is outlined by using the current pen and filled by using the current brush
    /// </remarks>
    EMR_ROUNDRECT = 0x0000002C,

    /// <summary>
    /// Defines an elliptical arc
    /// </summary>
    EMR_ARC = 0x0000002D,

    /// <summary>
    /// Defines a chord, which is a region bounded by the intersection of an ellipse and a line segment, called a secant
    /// </summary>
    /// <remarks>
    /// The chord is outlined by using the current pen and filled by using the current brush <para />
    /// If the starting point and ending point of the curve are the same, a complete ellipse is drawn <para />
    /// The current drawing position is neither used nor updated by processing this record
    /// </remarks>
    EMR_CHORD = 0x0000002E,

    /// <summary>
    /// Defines a pie-shaped wedge bounded by the intersection of an ellipse and two radials
    /// </summary>
    /// <remarks>
    /// The pie is outlined by using the current pen and filled by using the current brush
    /// <para />
    /// The curve of the pie is defined by an ellipse that fits the specified bounding rectangle.
    /// The curve begins at the point where the ellipse intersects the first radial and extends counterclockwise to the
    /// point where the ellipse intersects the second radial
    /// <para />
    /// The current drawing position is neither used nor updated by this record
    /// </remarks>
    EMR_PIE = 0x0000002F,

    /// <summary>
    /// Fills an area of the display surface with the current brush
    /// </summary>
    EMR_EXTFLOODFILL = 0x00000035,

    /// <summary>
    /// Defines a line from the current drawing position up to, but not including,
    /// the specified point. It resets the current drawing position to the specified point
    /// </summary>
    EMR_LINETO = 0x00000036,

    /// <summary>
    /// Defines an elliptical arc
    /// </summary>
    /// <remarks>
    /// It resets the current position to the endpoint of the arc
    /// </remarks>
    EMR_ARCTO = 0x00000037,

    /// <summary>
    /// Defines a set of line segments and Bezier curves
    /// </summary>
    EMR_POLYDRAW = 0x00000038,

    /// <summary>
    /// Closes any open figures in the current path bracket and fills its interior by using the current brush and polygon-filling mode
    /// </summary>
    EMR_FILLPATH = 0x0000003E,

    /// <summary>
    /// Closes any open figures in a path, strokes the outline of the path by using the current pen,
    /// and fills its interior by using the current brush
    /// </summary>
    EMR_STROKEANDFILLPATH = 0x0000003F,

    /// <summary>
    /// Renders the specified path by using the current pen
    /// </summary>
    EMR_STROKEPATH = 0x00000040,

    /// <summary>
    /// Specifies a clipping region as the current clipping region combined with the current path bracket, using the specified mode
    /// </summary>
    EMR_SELECTCLIPPATH = 0x00000043,

    /// <summary>
    /// Fills the specified region by using the specified brush
    /// </summary>
    /// <remarks>
    /// The current clipping regions used by this record are maintained in a Regions state element in the playback device context
    /// </remarks>
    EMR_FILLRGN = 0x00000047,

    /// <summary>
    /// Draws a border around the specified region using the specified brush
    /// </summary>
    /// <remarks>
    /// The current clipping regions used by this record are maintained in a Regions state element in the playback device context
    /// </remarks>
    EMR_FRAMERGN = 0x00000048,

    /// <summary>
    /// Paints the specified region by using the current brush
    /// </summary>
    /// <remarks>
    /// The current clipping regions used by this record are maintained in a Regions state element in the playback device context
    /// </remarks>
    EMR_PAINTRGN = 0x0000004A,

    /// <summary>
    /// Combines the specified region with the current clipping region, using the specified mode
    /// </summary>
    EMR_EXTSELECTCLIPRGN = 0x0000004B,

    /// <summary>
    /// Draws an ASCII text string using the current font and text colors
    /// </summary>
    EMR_EXTTEXTOUTA = 0x00000053,

    /// <summary>
    /// Draws a Unicode text string using the current font and text colors
    /// </summary>
    EMR_EXTTEXTOUTW = 0x00000054,

    /// <inheritdoc cref="EMR_POLYBEZIER"/>
    EMR_POLYBEZIER16 = 0x00000055,

    /// <inheritdoc cref="EMR_POLYGON"/>
    EMR_POLYGON16 = 0x00000056,

    /// <inheritdoc cref="EMR_POLYLINE"/>
    EMR_POLYLINE16 = 0x00000057,

    /// <inheritdoc cref="EMR_POLYBEZIER"/>
    EMR_POLYBEZIERTO16 = 0x00000058,

    /// <inheritdoc cref="EMR_POLYLINETO"/>
    EMR_POLYLINETO16 = 0x00000059,

    /// <inheritdoc cref="EMR_POLYPOLYLINE"/>
    EMR_POLYPOLYLINE16 = 0x0000005A,

    /// <inheritdoc cref="EMR_POLYPOLYGON"/>
    EMR_POLYPOLYGON16 = 0x0000005B,

    /// <inheritdoc cref="EMR_POLYDRAW"/>
    EMR_POLYDRAW16 = 0x0000005C,

    /// <summary>
    /// Draws one or more ASCII text strings using the current font and text colors
    /// </summary>
    EMR_POLYTEXTOUTA = 0x00000060,

    /// <summary>
    /// Draws one or more Unicode text strings using the current font and text colors
    /// </summary>
    EMR_POLYTEXTOUTW = 0x00000061,

    /// <summary>
    /// Passes arbitrary information to the driver. The intent is that the information results in drawing being done
    /// </summary>
    EMR_DRAWESCAPE = 0x00000069,

    /// <summary>
    /// Passes arbitrary information to the driver. The intent is that the information does not result in drawing being done
    /// </summary>
    EMR_EXTESCAPE = 0x0000006A,

    /// <summary>
    /// Outputs a string
    /// </summary>
    EMR_SMALLTEXTOUT = 0x0000006C,

    /// <summary>
    /// Passes arbitrary information to the given named driver
    /// </summary>
    EMR_NAMEDESCAPE = 0x0000006E,

    /// <summary>
    /// Specifies filling rectangles or triangles with gradients of
    /// </summary>
    EMR_GRADIENTFILL = 0x00000076,
}