using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_BEGINPATH"/>
[PublicAPI]
public record EmrBeginPath : EnhancedMetafileRecord, IEmfParsable<EmrBeginPath>
{
    private EmrBeginPath(EmfRecordType Type, uint Size) : base(Type, Size)
    {
    }

    public static EmrBeginPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrBeginPath(recordType, size);
    }
}