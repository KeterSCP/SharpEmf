﻿using JetBrains.Annotations;

namespace SharpEmf.Enums;

/// <summary>
/// Specifies ternary raster operation codes, which define how to combine the bits in a source bitmap with the bits in a destination bitmap.
/// <para>
/// Each ternary raster operation code represents a Boolean operation in which the values of the pixels in the source,
/// the selected brush, and the destination are combined
/// </para>
/// </summary>
[PublicAPI]
public enum TernaryRasterOperation : uint
{
    /// <summary>
    /// Fills the destination rectangle using the color associated with index 0 in the physical palette. (This color is black for the default physical palette)
    /// </summary>
    BLACKNESS = 0x00000042,
    DPSOON = 0x00010289,
    DPSONA = 0x00020C89,
    PSON = 0x000300AA,
    SDPONA = 0x00040C88,
    DPON = 0x000500A9,
    PDSXNON = 0x00060865,
    PDSAON = 0x000702C5,
    SDPNAA = 0x00080F08,
    PDSXON = 0x00090245,
    DPNA = 0x000A0329,
    SPNA = 0x000C0324,
    PDSNAON = 0x000D0B25,
    PDSONON = 0x000E08A5,
    PN = 0x000F0001,
    PDSONA = 0x00100C85,
    NOTSRCERASE = 0x001100A6,
    SDPXNON = 0x00120868,
    SDPAON = 0x001302C8,
    DPSXNON = 0x00140869,
    DPSAON = 0x001502C9,
    PSDPSANAXX = 0x00165CCA,
    SSPXDSXAXN = 0x00171D54,
    SPXPDXA = 0x00180D59,
    SDPSANAXN = 0x00191CC8,
    PDSPAOX = 0x001A06C5,
    SDPSXAXN = 0x001B0768,
    PSDPAOX = 0x001C06CA,
    DSPDXAXN = 0x001D0766,
    PDSOX = 0x001E01A5,
    PDSOAN = 0x001F0385,
    DPSNAA = 0x00200F09,
    SDPXON = 0x00210248,
    DSNA = 0x00220326,
    SPDNAON = 0x00230B24,
    SPXDSXA = 0x00240D55,
    PDSPANAXN = 0x00251CC5,
    SDPSAOX = 0x002606C8,
    SDPSXNOX = 0x00271868,
    DPSXA = 0x00280369,
    PSDPSAOXXN = 0x002916CA,
    DPSANA = 0x002A0CC9,
    SPDSOAX = 0x002C0784,
    PSDNOX = 0x002D060A,
    PSDPXOX = 0x002E064A,
    PSDNOAN = 0x002F0E2A,
    PSNA = 0x0030032A,
    SDPNAON = 0x00310B28,
    SDPSOOX = 0x00320688,
    NOTSRCCOPY = 0x00330008,
    SPDSAOX = 0x003406C4,
    SPDSXNOX = 0x00351864,
    SDPOX = 0x003601A8,
    SDPOAN = 0x00370388,
    PSDPOAX = 0x0038078A,
    SPDNOX = 0x0390604,
    SPDSXOX = 0x003A0644,
    SPDNOAN = 0x003B0E24,
    PSX = 0x003C004A,
    SPDSONOX = 0x003D18A4,
    SPDSNAOX = 0x003E1B24,
    PSAN = 0x003F00EA,
    PSDNAA = 0x00400F0A,
    DPSXON = 0x00410249,
    SDXPDXA = 0x00420D5D,
    SPDSANAXN = 0x00431CC4,
    SRCERASE = 0x00440328,
    DPSNAON = 0x00450B29,
    DSPDAOX = 0x004606C6,
    PSDPXAXN = 0x0047076A,
    SDPXA = 0x00480368,
    PDSPDAOXXN = 0x004916C5,
    DPSDOAX = 0x004A0789,
    SDPANA = 0x004C0CC8,
    SSPXDSXOXN = 0x004D1954,
    PDSPXOX = 0x004E0645,
    PDSNOAN = 0x004F0E25,
    PDNA = 0x00500325,
    DSPNAON = 0x00510B26,
    DPSDAOX = 0x005206C9,
    SPDSXAXN = 0x00530764,
    DPSONON = 0x005408A9,
    DSTINVERT = 0x00550009,
    DPSOX = 0x005601A9,
    DPSOAN = 0x000570389,
    PDSPOAX = 0x00580785,
    DPSNOX = 0x00590609,
    PATINVERT = 0x005A0049,
    DPSDONOX = 0x005B18A9,
    DPSDXOX = 0x005C0649,
    DPSNOAN = 0x005D0E29,
    DPSDNAOX = 0x005E1B29,
    DPAN = 0x005F00E9,
    PDSXA = 0x00600365,
    DSPDSAOXXN = 0x006116C6,
    DSPDOAX = 0x00620786,
    SDPNOX = 0x00630608,
    SDPSOAX = 0x00640788,
    DSPNOX = 0x00650606,
    SRCINVERT = 0x00660046,
    SDPSONOX = 0x006718A8,
    DSPDSONOXXN = 0x006858A6,
    PDSXXN = 0x00690145,
    DPSAX = 0x006A01E9,
    SDPAX = 0x006C01E8,
    PDSPDOAXXN = 0x006D1785,
    SDPSNOAX = 0x006E1E28,
    PDXNAN = 0x006F0C65,
    PDSANA = 0x00700CC5,
    SSDXPDXAXN = 0x00711D5C,
    SDPSXOX = 0x00720648,
    SDPNOAN = 0x00730E28,
    DSPDXOX = 0x00740646,
    DSPNOAN = 0x00750E26,
    SDPSNAOX = 0x00761B28,
    DSAN = 0x007700E6,
    PDSAX = 0x007801E5,
    DSPDSOAXXN = 0x00791786,
    DPSDNOAX = 0x007A1E29,
    SDPXNAN = 0x007B0C68,
    SPDSNOAX = 0x007C1E24,
    DPSXNAN = 0x007D0C69,
    SPXDSXO = 0x007E0955,
    DPSAAN = 0x007F03C9,
    DPSAA = 0x008003E9,
    SPXDSXON = 0x00810975,
    DPSXNA = 0x00820C49,
    SPDSNOAXN = 0x00831E04,
    SDPXNA = 0x00840C48,
    PDSPNOAXN = 0x00851E05,
    DSPDSOAXX = 0x008617A6,
    PDSAXN = 0x008701C5,
    SRCAND = 0x008800C6,
    SDPSNAOXN = 0x00891B08,
    DSPNOA = 0x008A0E06,
    SDPNOA = 0x008C0E08,
    SDPSXOXN = 0x008D0668,
    SSDXPDXAX = 0x008E1D7C,
    PDSANAN = 0x008F0CE5,
    PDSXNA = 0x00900C45,
    SDPSNOAXN = 0x00911E08,
    DPSDPOAXX = 0x009217A9,
    SPDAXN = 0x009301C4,
    PSDPSOAXX = 0x009417AA,
    DPSAXN = 0x009501C9,
    DPSXX = 0x00960169,
    PSDPSONOXX = 0x0097588A,
    SDPSONOXN = 0x00981888,
    DSXN = 0x00990066,
    DPSNAX = 0x009A0709,
    SDPSOAXN = 0x009B07A8,
    SPDNAX = 0x009C0704,
    DSPDOAXN = 0x009D07A6,
    DSPDSAOXX = 0x009E16E6,
    PDSXAN = 0x009F0345,
    DPA = 0x00A000C9,
    PDSPNAOXN = 0x00A11B05,
    DPSNOA = 0x00A20E09,
    DPSDXOXN = 0x00A30669,
    PDSPONOXN = 0x00A41885,
    PDXN = 0x00A50065,
    DSPNAX = 0x00A60706,
    PDSPOAXN = 0x00A707A5,
    DPSOA = 0x00A803A9,
    DPSOXN = 0x00A90189,
    D = 0x00AA0029,
    SPDSXAX = 0x00AC0744,
    DPSDAOXN = 0x00AD06E9,
    DSPNAO = 0x00AE0B06,
    DPNO = 0x00AF0229,
    PDSNOA = 0x00B00E05,
    PDSPXOXN = 0x00B10665,
    SSPXDSXOX = 0x00B21974,
    SDPANAN = 0x00B30CE8,
    PSDNAX = 0x00B4070A,
    DPSDOAXN = 0x00B507A9,
    DPSDPAOXX = 0x00B616E9,
    SDPXAN = 0x00B70348,
    PSDPXAX = 0x00B8074A,
    DSPDAOXN = 0x00B906E6,
    DPSNAO = 0x00BA0B09,
    MERGEPAINT = 0x00BB0226,
    SPDSANAX = 0x00BC1CE4,
    SDXPDXAN = 0x00BD0D7D,
    DPSXO = 0x00BE0269,
    DPSANO = 0x00BF08C9,
    MERGECOPY = 0x00C000CA,
    SPDSNAOXN = 0x00C11B04,
    SPDSONOXN = 0x00C21884,
    PSXN = 0x00C3006A,
    SPDNOA = 0x00C40E04,
    SPDSXOXN = 0x00C50664,
    SDPNAX = 0x00C60708,
    PSDPOAXN = 0x00C707AA,
    SDPOA = 0x00C803A8,
    SPDOXN = 0x00C90184,
    DPSDXAX = 0x00CA0749,
    SRCCOPY = 0x00CC0020,
    SDPONO = 0x00CD0888,
    SDPNAO = 0x00CE0B08,
    SPNO = 0x00CF0224,
    PSDPXOXN = 0x00D1066A,
    PDSNAX = 0x00D20705,
    SPDSOAXN = 0x00D307A4,
    SSPXPDXAX = 0x00D41D78,
    DPSANAN = 0x00D50CE9,
    PSDPSAOXX = 0x00D616EA,
    DPSXAN = 0x00D70349,
    PDSPXAX = 0x00D80745,
    SDPSAOXN = 0x00D906E8,
    DPSDANAX = 0x00DA1CE9,
    SPXDSXAN = 0x00DB0D75,
    SPDNAO = 0x00DC0B04,
    SDNO = 0x00DD0228,
    SDPXO = 0x00DE0268,
    SDPANO = 0x00DF08C8,
    PDSOA = 0x00E003A5,
    PDSOXN = 0x00E10185,
    DSPDXAX = 0x00E20746,
    PSDPAOXN = 0x00E306EA,
    SDPSXAX = 0x00E40748,
    PDSPAOXN = 0x00E506E5,
    SDPSANAX = 0x00E61CE8,
    SPXPDXAN = 0x00E70D79,
    SSPXDSXAX = 0x00E81D74,
    DSPDSANAXXN = 0x00E95CE6,
    DPSAO = 0x00EA02E9,
    SDPAO = 0x00EC02E8,
    SDPXNO = 0x00ED0848,
    SRCPAINT = 0x00EE0086,
    SDPNOO = 0x00EF0A08,
    /// <summary>
    /// Copies the specified pattern into the destination bitmap
    /// </summary>
    PATCOPY = 0x00F00021,
    PDSONO = 0x00F10885,
    PDSNAO = 0x00F20B05,
    PSNO = 0x00F3022A,
    PSDNAO = 0x00F40B0A,
    PDNO = 0x00F50225,
    PDSXO = 0x00F60265,
    PDSANO = 0x00F708C5,
    PDSAO = 0x00F802E5,
    PDSXNO = 0x00F90845,
    DPO = 0x00FA0089,
    PATPAINT = 0x00FB0A09,
    PSO = 0x00FC008A,
    PSDNOO = 0x00FD0A0A,
    DPSOO = 0x00FE02A9,

    /// <summary>
    /// Fills the destination rectangle using the color associated with index 1 in the physical palette. (This color is white for the default physical palette)
    /// </summary>
    WHITENESS = 0x00FF0062
}