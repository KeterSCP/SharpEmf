using JetBrains.Annotations;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Header;

/// <summary>
/// Defines the first extension to the EMF metafile header. It adds support
/// for a PixelFormatDescriptor object and OpenGL records
/// </summary>
[PublicAPI]
public record EmfMetafileHeaderExtension1 : EmfMetafileHeader, IEmfParsableExtension<EmfMetafileHeader, EmfMetafileHeaderExtension1>
{
    /// <summary>
    /// Specifies the size of the PixelFormatDescriptor object. This value is 0x00000000 if no pixel format is set
    /// </summary>
    public uint CbPixelFormat { get; }

    /// <summary>
    /// Specifies the offset to the PixelFormatDescriptor object. This value is 0x00000000 if no pixel format is set
    /// </summary>
    public uint OffPixelFormat { get; }

    /// <summary>
    /// Indicates whether OpenGL commands are present in the metafile.
    /// 0 - OpenGL commands are not present in the metafile. 1 - OpenGL commands are present in the metafile
    /// </summary>
    public uint BOpenGL { get; }

    protected EmfMetafileHeaderExtension1(EmfMetafileHeader emfMetafileHeader, uint cbPixelFormat, uint offPixelFormat, uint bOpenGL) : base(
        size: emfMetafileHeader.Size,
        version: emfMetafileHeader.Version,
        bounds: emfMetafileHeader.Bounds,
        frame: emfMetafileHeader.Frame,
        bytes: emfMetafileHeader.Bytes,
        records: emfMetafileHeader.Records,
        handles: emfMetafileHeader.Handles,
        nDescription: emfMetafileHeader.NDescription,
        offDescription: emfMetafileHeader.OffDescription,
        palEntries: emfMetafileHeader.PalEntries,
        device: emfMetafileHeader.Device,
        millimeters: emfMetafileHeader.Millimeters,
        description: emfMetafileHeader.Description)
    {
        CbPixelFormat = cbPixelFormat;
        OffPixelFormat = offPixelFormat;
        BOpenGL = bOpenGL;
    }

    public static EmfMetafileHeaderExtension1 Parse(Stream stream, EmfMetafileHeader baseRecord)
    {
        var cbPixelFormat = stream.ReadUInt32();
        var offPixelFormat = stream.ReadUInt32();
        var bOpenGL = stream.ReadUInt32();

        return new EmfMetafileHeaderExtension1(baseRecord, cbPixelFormat, offPixelFormat, bOpenGL);
    }
}