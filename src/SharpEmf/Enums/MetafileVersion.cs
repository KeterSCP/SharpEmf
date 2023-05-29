using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines the interoperability version for EMF metafile
/// </summary>
[PublicAPI]
public enum MetafileVersion : uint
{
    /// <summary>
    /// Specifies EMF metafile interoperability
    /// </summary>
    META_FORMAT_ENHANCED = 0x00010000
}