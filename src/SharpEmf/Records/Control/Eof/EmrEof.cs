using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.Control.Eof;

[PublicAPI]
public record EmrEof : EnhancedMetafileRecord, IEmfParsable<EmrEof>
{
    public override EmfRecordType Type => EmfRecordType.EMR_EOF;
    public override uint Size { get; }

    private EmrEof(uint size)
    {
        Size = size;
    }

    public static EmrEof Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        // TODO: Implement parsing

        return new EmrEof(size);
    }
}