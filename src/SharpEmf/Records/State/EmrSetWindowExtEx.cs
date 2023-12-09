using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETWINDOWEXTEX"/>
[PublicAPI]
public record EmrSetWindowExtEx : EnhancedMetafileRecord, IEmfParsable<EmrSetWindowExtEx>
{
    public SizeL Extent { get; }

    private EmrSetWindowExtEx(EmfRecordType recordType, uint size, SizeL extent) : base(recordType, size)
    {
        Extent = extent;
    }

    public static EmrSetWindowExtEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var extent = SizeL.Parse(stream);
        return new EmrSetWindowExtEx(recordType, size, extent);
    }
}