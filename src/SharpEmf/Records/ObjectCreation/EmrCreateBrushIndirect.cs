using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;

namespace SharpEmf.Records.ObjectCreation;

/// <inheritdoc cref="EmfRecordType.EMR_CREATEBRUSHINDIRECT"/>
[PublicAPI]
public record EmrCreateBrushIndirect : EnhancedMetafileRecord, IEmfParsable<EmrCreateBrushIndirect>
{
    /// <summary>
    /// Specifies the index of the logical brush object in the EMF object table
    /// </summary>
    /// <remarks>
    /// This index is used to refer to the object, so it can be reused or modified
    /// </remarks>
    public uint IHBrush { get; }

    /// <summary>
    /// Specifies the style, color, and pattern of the logical brush
    /// </summary>
    public LogBrushEx LogBrush { get; }

    private EmrCreateBrushIndirect(EmfRecordType Type, uint Size, uint ihBrush, LogBrushEx logBrush) : base(Type, Size)
    {
        IHBrush = ihBrush;
        LogBrush = logBrush;
    }

    public static EmrCreateBrushIndirect Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var ihBrush = stream.ReadUInt32();
        var logBrush = LogBrushEx.Parse(stream);

        return new EmrCreateBrushIndirect(recordType, size, ihBrush, logBrush);
    }
}