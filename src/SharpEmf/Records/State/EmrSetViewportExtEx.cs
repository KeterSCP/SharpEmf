using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETVIEWPORTEXTEX"/>
[PublicAPI]
public record EmrSetViewportExtEx : EnhancedMetafileRecord, IEmfParsable<EmrSetViewportExtEx>
{
    public SizeL Extent { get; }

    private EmrSetViewportExtEx(EmfRecordType recordType, uint size, SizeL extent) : base(recordType, size)
    {
        Extent = extent;
    }

    public static EmrSetViewportExtEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var extent = SizeL.Parse(stream);
        return new EmrSetViewportExtEx(recordType, size, extent);
    }
}