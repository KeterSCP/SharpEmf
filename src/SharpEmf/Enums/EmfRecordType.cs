﻿using SharpEmf.Records;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that uniquely identify records in an EMF metafile. These values are specified in the <see cref="EnhancedMetafileRecord.Type"/> fields of EMF records
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
    /// Defines an ellipse. The center of the ellipse is the center of the specified bounding rectangle.
    /// The ellipse is outlined by using the current pen and is filled by using the current brush
    /// </summary>
    EMR_ELLIPSE = 0x0000002A,

    /// <summary>
    /// Defines a line from the current drawing position up to, but not including,
    /// the specified point. It resets the current drawing position to the specified point
    /// </summary>
    EMR_LINETO = 0x00000036,
}