using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies printer driver functionality
/// </summary>
[PublicAPI]
public enum MetafileEscapes : uint
{
    /// <summary>
    /// Notifies the printer driver that the application has finished writing to a page
    /// </summary>
    NEWFRAME = 0x0001,

    /// <summary>
    /// Stops processing the current document
    /// </summary>
    ABORTDOC = 0x0002,

    /// <summary>
    /// Notifies the printer driver that the application has finished writing to a band
    /// </summary>
    NEXTBAND = 0x0003,

    /// <summary>
    /// Sets color table values
    /// </summary>
    SETCOLORTABLE = 0x0004,

    /// <summary>
    /// Gets color table values
    /// </summary>
    GETCOLORTABLE = 0x0005,

    /// <summary>
    /// Causes all pending output to be flushed to the output device
    /// </summary>
    FLUSHOUT = 0x0006,

    /// <summary>
    /// Indicates that the printer driver SHOULD print text only, and no graphics
    /// </summary>
    DRAFTMODE = 0x0007,

    /// <summary>
    /// Queries a printer driver to determine whether a specific escape function is supported on the output device it drives
    /// </summary>
    QUERYESCSUPPORT = 0x0008,

    /// <summary>
    /// Sets the application-defined function that allows a print job to be canceled during printing
    /// </summary>
    SETABORTPROC = 0x0009,

    /// <summary>
    /// Notifies the printer driver that a new print job is starting
    /// </summary>
    STARTDOC = 0x000A,

    /// <summary>
    /// Notifies the printer driver that the current print job is ending
    /// </summary>
    ENDDOC = 0x000B,

    /// <summary>
    /// Retrieves the physical page size currently selected on an output device
    /// </summary>
    GETPHYSPAGESIZE = 0x000C,

    /// <summary>
    /// Retrieves the offset from the upper-left corner of the physical page where the actual printing or drawing begins
    /// </summary>
    GETPRINTINGOFFSET = 0x000D,

    /// <summary>
    /// Retrieves the scaling factors for the x-axis and the y-axis of a printer
    /// </summary>
    GETSCALINGFACTOR = 0x000E,

    /// <summary>
    /// Used to embed an enhanced metafile format (EMF) metafile within a WMF metafile
    /// </summary>
    META_ESCAPE_ENHANCED_METAFILE = 0x000F,

    /// <summary>
    /// Sets the width of a pen in pixels
    /// </summary>
    SETPENWIDTH = 0x0010,

    /// <summary>
    /// Sets the number of copies
    /// </summary>
    SETCOPYCOUNT = 0x0011,

    /// <summary>
    /// Sets the source, such as a particular paper tray or bin on a printer, for output forms
    /// </summary>
    SETPAPERSOURCE = 0x0012,

    /// <summary>
    /// This record passes through arbitrary data
    /// </summary>
    PASSTHROUGH = 0x0013,

    /// <summary>
    /// Gets information concerning graphics technology that is supported on a device
    /// </summary>
    GETTECHNOLOGY = 0x0014,

    /// <summary>
    /// Specifies the line-drawing mode to use in output to a device
    /// </summary>
    SETLINECAP = 0x0015,

    /// <summary>
    /// Specifies the line-joining mode to use in output to a device
    /// </summary>
    SETLINEJOIN = 0x0016,

    /// <summary>
    /// Sets the limit for the length of miter joins to use in output to a device
    /// </summary>
    SETMITERLIMIT = 0x0017,

    /// <summary>
    /// Retrieves or specifies settings concerning banding on a device, such as the number of bands
    /// </summary>
    BANDINFO = 0x0018,

    /// <summary>
    /// Draws a rectangle with a defined pattern
    /// </summary>
    DRAWPATTERNRECT = 0x0019,

    /// <summary>
    /// Retrieves the physical pen size currently defined on a device
    /// </summary>
    GETVECTORPENSIZE = 0x001A,

    /// <summary>
    /// Retrieves the physical brush size currently defined on a device
    /// </summary>
    GETVECTORBRUSHSIZE = 0x001B,

    /// <summary>
    /// Enables or disables double-sided (duplex) printing on a device
    /// </summary>
    ENABLEDUPLEX = 0x001C,

    /// <summary>
    /// Retrieves or specifies the source of output forms on a device
    /// </summary>
    GETSETPAPERBINS = 0x001D,

    /// <summary>
    ///  Retrieves or specifies the paper orientation on a device
    /// </summary>
    GETSETPRINTORIENT = 0x001E,

    /// <summary>
    /// Retrieves information concerning the sources of different forms on an output device
    /// </summary>
    ENUMPAPERBINS = 0x001F,

    /// <summary>
    /// Specifies the scaling of device-independent bitmaps (DIBs)
    /// </summary>
    SETDIBSCALING = 0x0020,

    /// <summary>
    /// Indicates the start and end of an encapsulated PostScript (EPS) section
    /// </summary>
    EPSPRINTING = 0x0021,

    /// <summary>
    /// Queries a printer driver for paper dimensions and other forms data
    /// </summary>
    ENUMPAPERMETRICS = 0x0022,

    /// <summary>
    /// Retrieves or specifies paper dimensions and other forms data on an output device
    /// </summary>
    GETSETPAPERMETRICS = 0x0023,

    /// <summary>
    /// Sends arbitrary PostScript data to an output device
    /// </summary>
    POSTSCRIPT_DATA = 0x0025,

    /// <summary>
    /// Notifies an output device to ignore PostScript data
    /// </summary>
    POSTSCRIPT_IGNORE = 0x0026,

    /// <summary>
    /// Gets the device units currently configured on an output device
    /// </summary>
    GETDEVICEUNITS = 0x002A,

    /// <summary>
    /// Gets extended text metrics currently configured on an output device
    /// </summary>
    GETEXTENDEDTEXTMETRICS = 0x0100,

    /// <summary>
    /// Gets the font kern table currently defined on an output device
    /// </summary>
    GETPAIRKERNTABLE = 0x0102,

    /// <summary>
    /// Draws text using the currently selected font, background color, and text color
    /// </summary>
    EXTTEXTOUT = 0x0200,

    /// <summary>
    /// Gets the font face name currently configured on a device
    /// </summary>
    GETFACENAME = 0x0201,

    /// <summary>
    /// Sets the font face name on a device
    /// </summary>
    DOWNLOADFACE = 0x0202,

    /// <summary>
    /// Queries a printer driver about the support for metafiles on an output device
    /// </summary>
    METAFILE_DRIVER = 0x0801,

    /// <summary>
    /// Queries the printer driver about its support for DIBs on an output device
    /// </summary>
    QUERYDIBSUPPORT = 0x0C01,

    /// <summary>
    /// Opens a path
    /// </summary>
    BEGIN_PATH = 0x1000,

    /// <summary>
    /// Defines a clip region that is bounded by a path
    /// </summary>
    /// <remarks>
    /// The input MUST be a 16-bit quantity that defines the action to take
    /// </remarks>
    CLIP_TO_PATH = 0x1001,

    /// <summary>
    /// Ends a path
    /// </summary>
    END_PATH = 0x1002,

    /// <summary>
    /// The same as <see cref="STARTDOC"/> specified with a NULL document and output filename, data in raw mode, and a type of zero
    /// </summary>
    OPENCHANNEL = 0x100E,

    /// <summary>
    /// Instructs the printer driver to download sets of PostScript procedures
    /// </summary>
    DOWNLOADHEADER = 0x100F,

    /// <summary>
    /// The same as <see cref="ENDDOC"/>. See <see cref="OPENCHANNEL"/>
    /// </summary>
    CLOSECHANNEL = 0x1010,

    /// <summary>
    /// Sends arbitrary data directly to a printer driver, which is expected to process this data only when in PostScript mode.
    /// See <see cref="POSTSCRIPT_IDENTIFY"/>
    /// </summary>
    POSTSCRIPT_PASSTHROUGH = 0x1013,

    /// <summary>
    /// Sends arbitrary data directly to the printer driver
    /// </summary>
    ENCAPSULATED_POSTSCRIPT = 0x1014,

    /// <summary>
    /// Sets the printer driver to either PostScript or GDI mode
    /// </summary>
    POSTSCRIPT_IDENTIFY = 0x1015,

    /// <summary>
    /// Inserts a block of raw data into a PostScript stream
    /// </summary>
    /// <remarks>
    /// The input MUST be a 32-bit quantity specifying the number of bytes to inject, a 16-bit quantity specifying the
    /// injection point, and a 16-bit quantity specifying the page number, followed by the bytes to inject
    /// </remarks>
    POSTSCRIPT_INJECTION = 0x1016,

    /// <summary>
    /// Checks whether the printer supports a JPEG image
    /// </summary>
    CHECKJPEGFORMAT = 0x1017,

    /// <summary>
    /// Checks whether the printer supports a PNG image
    /// </summary>
    CHECKPNGFORMAT = 0x1018,

    /// <summary>
    /// Gets information on a specified feature setting for a PostScript printer driver
    /// </summary>
    GET_PS_FEATURESETTING = 0x1019,

    /// <summary>
    /// Enables applications to write documents to a file or to a printer in XML Paper Specification (XPS) format
    /// </summary>
    MXDC_ESCAPE = 0x101A,

    /// <summary>
    ///  Enables applications to include private procedures and other arbitrary data in documents
    /// </summary>
    SPCLPASSTHROUGH2 = 0x11D8
}