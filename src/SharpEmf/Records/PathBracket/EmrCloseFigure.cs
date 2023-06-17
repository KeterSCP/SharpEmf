using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.PathBracket;

/// <inheritdoc cref="EmfRecordType.EMR_CLOSEFIGURE"/>
[PublicAPI]
public record EmrCloseFigure : EnhancedMetafileRecord, IEmfParsable<EmrCloseFigure>
{
    private EmrCloseFigure(EmfRecordType Type, uint Size) : base(Type, Size)
    {
    }

    public static EmrCloseFigure Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        return new EmrCloseFigure(recordType, size);
    }
}