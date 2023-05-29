using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Defines values that are used to identify the format of embedded data in EMF metafiles
/// </summary>
[PublicAPI]
public enum FormatSignature : uint
{
    /// <summary>
    /// The sequence of ASCII characters "FME ", which denotes EMF data. The reverse of the string is " EMF"
    /// </summary>
    ENHMETA_SIGNATURE = 0x464D4520,

    /// <summary>
    /// The sequence of ASCII characters "FSPE", which denotes encapsulated PostScript (EPS) data. The reverse of the string is "EPSF"
    /// </summary>
    EPS_SIGNATURE = 0x46535045,
}