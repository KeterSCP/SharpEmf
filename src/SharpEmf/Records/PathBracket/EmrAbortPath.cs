using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_ABORTPATH"/>
[PublicAPI]
public record EmrAbortPath : EnhancedMetafileRecord, IEmfParsable<EmrAbortPath>
{
    private EmrAbortPath(EmfRecordType recordType, uint size) : base(recordType, size)
    {
    }

    public static EmrAbortPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrAbortPath(recordType, size);
    }
}