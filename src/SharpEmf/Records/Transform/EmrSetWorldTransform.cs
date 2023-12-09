using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Interfaces;
using SharpEmf.Objects;

namespace SharpEmf.Records.Transform;

/// <inheritdoc cref="EmfRecordType.EMR_SETWORLDTRANSFORM"/>
[PublicAPI]
public record EmrSetWorldTransform : EnhancedMetafileRecord, IEmfParsable<EmrSetWorldTransform>
{
    public XForm XForm { get; }

    private EmrSetWorldTransform(EmfRecordType Type, uint Size, XForm xForm) : base(Type, Size)
    {
        XForm = xForm;
    }

    public static EmrSetWorldTransform Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var xForm = XForm.Parse(stream);
        return new EmrSetWorldTransform(recordType, size, xForm);
    }
}