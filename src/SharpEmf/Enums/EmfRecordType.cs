using SharpEmf.Records;

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
    /// This record indicates the end of the metafile
    /// </summary>
    EMR_EOF = 0x0000000E,

    /// <summary>
    /// Defines a line from the current drawing position up to, but not including,
    /// the specified point. It resets the current drawing position to the specified point
    /// </summary>
    EMR_LINETO = 0x00000036,
}