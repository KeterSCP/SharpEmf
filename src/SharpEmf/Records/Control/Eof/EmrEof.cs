using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Control.Eof;

/// <inheritdoc cref="EmfRecordType.EMR_EOF"/>
[PublicAPI]
public record EmrEof : EnhancedMetafileRecord, IEmfParsable<EmrEof>
{
    private EmrEof(EmfRecordType recordType, uint size) : base(recordType, size)
    {
    }

    public static EmrEof Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        // TODO: Implement parsing

        return new EmrEof(recordType, size);
    }
}