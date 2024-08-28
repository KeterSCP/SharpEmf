using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETBRUSHORGEX"/>
[PublicAPI]
public record EmrSetBrushOrgEx : EnhancedMetafileRecord, IEmfParsable<EmrSetBrushOrgEx>
{
    public PointL Origin { get; }

    private EmrSetBrushOrgEx(EmfRecordType Type, uint Size, PointL origin) : base(Type, Size)
    {
        Origin = origin;
    }

    public static EmrSetBrushOrgEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var origin = PointL.Parse(stream);
        return new EmrSetBrushOrgEx(recordType, size, origin);
    }
}