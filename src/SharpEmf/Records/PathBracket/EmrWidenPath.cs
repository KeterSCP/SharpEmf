using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_WIDENPATH"/>
[PublicAPI]
public record EmrWidenPath : EnhancedMetafileRecord, IEmfParsable<EmrWidenPath>
{
    private EmrWidenPath(EmfRecordType Type, uint Size) : base(Type, Size)
    {
    }

    public static EmrWidenPath Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrWidenPath(recordType, size);
    }
}