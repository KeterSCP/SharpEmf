using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETWINDOWORGEX"/>
[PublicAPI]
public record EmrSetWindowOrgEx : EnhancedMetafileRecord, IEmfParsable<EmrSetWindowOrgEx>
{
    public PointL Origin { get; }

    private EmrSetWindowOrgEx(EmfRecordType recordType, uint size, PointL origin) : base(recordType, size)
    {
        Origin = origin;
    }

    public static EmrSetWindowOrgEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var origin = PointL.Parse(stream);
        return new EmrSetWindowOrgEx(recordType, size, origin);
    }
}