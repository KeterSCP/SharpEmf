using SharpEmf.Records;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that uniquely identify records in an EMF metafile.
/// These values are specified in the <see cref="EnhancedMetafileRecord.Type"/> fields of EMF records
/// </summary>
public enum EmfRecordType : uint
{
    /// <summary>
    /// Defines the start of the metafile and specifies its characteristics; its
    /// contents, including the dimensions of the embedded image; the number of records in the metafile;
    /// and the resolution of the device on which the embedded image was created. These values make it
    /// possible for the metafile to be device-independent
    /// </summary>
    EMR_HEADER = 0x00000001,

    /// <summary>
    /// Defines a polygon consisting of two or more vertexes connected by straight lines.
    /// The polygon is outlined by using the current pen and filled by using the current brush and polygon fill mode.
    /// The polygon is closed automatically by drawing a line from the last vertex to the first
    /// </summary>
    EMR_POLYGON = 0x00000003,

    /// <summary>
    /// This record indicates the end of the metafile
    /// </summary>
    EMR_EOF = 0x0000000E,

    /// <summary>
    /// Defines the color of the pixel at the specified logical coordinates
    /// </summary>
    EMR_SETPIXELV = 0x0000000F,

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
}