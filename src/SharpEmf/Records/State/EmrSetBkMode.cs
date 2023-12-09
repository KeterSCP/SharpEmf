using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_SETBKMODE"/>
[PublicAPI]
public record EmrSetBkMode : EnhancedMetafileRecord, IEmfParsable<EmrSetBkMode>
{
    public BackgroundMode BackgroundMode { get; }

    private EmrSetBkMode(EmfRecordType Type, uint Size, BackgroundMode backgroundMode) : base(Type, Size)
    {
        BackgroundMode = backgroundMode;
    }

    public static EmrSetBkMode Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var backgroundMode = stream.ReadEnum<BackgroundMode>();
        return new EmrSetBkMode(recordType, size, backgroundMode);
    }
}