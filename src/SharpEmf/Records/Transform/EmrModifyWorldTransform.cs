using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Interfaces;
using SharpEmf.Objects;

namespace SharpEmf.Records.Transform;

/// <inheritdoc cref="EmfRecordType.EMR_MODIFYWORLDTRANSFORM"/>
[PublicAPI]
public record EmrModifyWorldTransform : EnhancedMetafileRecord, IEmfParsable<EmrModifyWorldTransform>
{
    /// <summary>
    /// Used according to the <see cref="ModifyWorldTransformMode"/> to define a new value for the world-space to page-space transform in the playback device context
    /// </summary>
    public XForm XForm { get; }

    /// <summary>
    /// Specifies how the transform specified in <see cref="XForm"/> is used
    /// </summary>
    public ModifyWorldTransformMode ModifyWorldTransformMode { get; }

    private EmrModifyWorldTransform(EmfRecordType Type, uint Size, XForm xForm, ModifyWorldTransformMode modifyWorldTransformMode) : base(Type, Size)
    {
        XForm = xForm;
        ModifyWorldTransformMode = modifyWorldTransformMode;
    }

    public static EmrModifyWorldTransform Parse(Stream stream, EmfRecordType recordType, uint size)
    {
        var xForm = XForm.Parse(stream);
        var modifyWorldTransformMode = stream.ReadEnum<ModifyWorldTransformMode>();

        return new EmrModifyWorldTransform(recordType, size, xForm, modifyWorldTransformMode);
    }
}