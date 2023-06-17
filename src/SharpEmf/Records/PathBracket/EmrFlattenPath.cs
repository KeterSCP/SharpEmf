using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_FLATTENPATH"/>
[PublicAPI]
public record EmrFlattenPath : EnhancedMetafileRecord, IEmfParsable<EmrFlattenPath>
{
    private EmrFlattenPath(EmfRecordType Type, uint Size) : base(Type, Size)
    {
    }

    public static EmrFlattenPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrFlattenPath(recordType, size);
    }
}