using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETVIEWPORTORGEX"/>
[PublicAPI]
public record EmrSetViewportOrgEx : EnhancedMetafileRecord, IEmfParsable<EmrSetViewportOrgEx>
{
    public PointL Origin { get; }

    private EmrSetViewportOrgEx(EmfRecordType recordType, uint size, PointL origin) : base(recordType, size)
    {
        Origin = origin;
    }

    public static EmrSetViewportOrgEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var origin = PointL.Parse(stream);
        return new EmrSetViewportOrgEx(recordType, size, origin);
    }
}