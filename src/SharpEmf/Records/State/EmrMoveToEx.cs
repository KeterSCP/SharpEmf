using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.WmfTypes;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_MOVETOEX"/>
[PublicAPI]
public record EmrMoveToEx : EnhancedMetafileRecord, IEmfParsable<EmrMoveToEx>
{
    /// <summary>
    /// Specifies coordinates of the new drawing position in logical units
    /// </summary>
    public PointL Offset { get; }

    private EmrMoveToEx(EmfRecordType Type, uint Size, PointL offset) : base(Type, Size)
    {
        Offset = offset;
    }

    public static EmrMoveToEx Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var offset = PointL.Parse(stream);

        return new EmrMoveToEx(recordType, size, offset);
    }
}