using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_ENDPATH"/>
[PublicAPI]
public record EmrEndPath : EnhancedMetafileRecord, IEmfParsable<EmrEndPath>
{
    private EmrEndPath(EmfRecordType Type, uint Size) : base(Type, Size)
    {
    }

    public static EmrEndPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrEndPath(recordType, size);
    }
}