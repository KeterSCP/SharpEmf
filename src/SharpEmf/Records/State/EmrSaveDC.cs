using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SAVEDC"/>
[PublicAPI]
public record EmrSaveDC : EnhancedMetafileRecord, IEmfParsable<EmrSaveDC>
{
    private EmrSaveDC(EmfRecordType recordType, uint size) : base(recordType, size)
    {
    }

    public static EmrSaveDC Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrSaveDC(recordType, size);
    }
}