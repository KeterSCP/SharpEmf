using JetBrains.Annotations;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Control.Header;

/// <summary>
/// Defines the second extension to the EMF metafile header
/// </summary>
[PublicAPI]
public record EmfMetafileHeaderExtension2 : EmfMetafileHeaderExtension1, IEmfParsableExtension<EmfMetafileHeaderExtension1, EmfMetafileHeaderExtension2>
{
    /// <summary>
    /// Horizontal size of the display device for which the metafile image was generated, in micrometers
    /// </summary>
    public uint MicrometersX { get; }

    /// <summary>
    /// Vertical size of the display device for which the metafile image was generated, in micrometers
    /// </summary>
    public uint MicrometersY { get; }

    internal EmfMetafileHeaderExtension2(EmfMetafileHeaderExtension1 emfMetafileHeaderExtension1, uint micrometersX, uint micrometersY) : base(
        emfMetafileHeader: emfMetafileHeaderExtension1,
        cbPixelFormat: emfMetafileHeaderExtension1.CbPixelFormat,
        offPixelFormat: emfMetafileHeaderExtension1.OffPixelFormat,
        bOpenGL: emfMetafileHeaderExtension1.BOpenGL)
    {
        MicrometersX = micrometersX;
        MicrometersY = micrometersY;
    }

    public static EmfMetafileHeaderExtension2 Parse(Stream stream, EmfMetafileHeaderExtension1 baseRecord)
    {
        var micrometersX = stream.ReadUInt32();
        var micrometersY = stream.ReadUInt32();

        return new EmfMetafileHeaderExtension2(baseRecord, micrometersX, micrometersY);
    }
}