using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;

namespace SharpEmf.Records.State;

/// <inheritdoc cref="EmfRecordType.EMR_RESTOREDC"/>
[PublicAPI]
public record EmrRestoreDC : EnhancedMetafileRecord, IEmfParsable<EmrRestoreDC>
{
    /// <summary>
    /// Specifies the saved state to restore relative to the current state
    /// </summary>
    /// <remarks>
    /// This value MUST be negative; –1 represents the state that was most recently saved on the stack, –2 the one before that, etc.
    /// <br/><br/>
    /// The stack can contain state information for multiple instances of the playback device context.
    /// When a state is restored, all state instances that were saved more recently MUST be discarded
    /// </remarks>
    public int SavedDC { get; }

    private EmrRestoreDC(EmfRecordType recordType, uint size, int savedDC) : base(recordType, size)
    {
        SavedDC = savedDC;
    }

    public static EmrRestoreDC Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var savedDC = stream.ReadInt32();
        return new EmrRestoreDC(recordType, size, savedDC);
    }
}