using System.Runtime.CompilerServices;
using System.Text;
using FluentAssertions;
using SharpEmf.Enums;
using SharpEmf.Objects;
using SharpEmf.WmfTypes;

namespace SharpEmf.UnitTests.Parsing;

public class EmrTextParsingTests
{
    [Theory(DisplayName = "Should correctly parse EmrExtTextOutA record")]
    [MemberData(nameof(GetTestArguments))]
    public void ShouldCorrectlyParseStringBuffer(ExtTextOutOptions options, EmfRecordType parentRecordType)
    {
        // Arrange
        const int undefinedStringBufferSpace = 32;
        const string stringBuffer = "Hello, World!";

        const int undefinedDxSpace = 16;
        var dxBufferSize = options.HasFlag(ExtTextOutOptions.ETO_PDY) ? stringBuffer.Length * 2 : stringBuffer.Length;
        var dxBuffer = Enumerable.Range(0, dxBufferSize).Select(_ => unchecked((uint)Random.Shared.Next())).ToArray();

        using var memoryStream = new MemoryStream();

        RectL? rectangle = options.HasFlag(ExtTextOutOptions.ETO_NO_RECT) ? null : new RectL(
            left: Random.Shared.Next(),
            top: Random.Shared.Next(),
            right: Random.Shared.Next(),
            bottom: Random.Shared.Next());

        var parentSizeWithoutTextBuffer =
            // Base record fields
            Unsafe.SizeOf<EmfRecordType>() +
            Unsafe.SizeOf<uint>() +
            // EmrExtTextOutA.Bounds
            Unsafe.SizeOf<RectL>() +
            // EmrExtTextOutA.IGraphicsMode
            Unsafe.SizeOf<GraphicsMode>() +
            // EmrExtTextOutA.EXScale
            Unsafe.SizeOf<float>() +
            // EmrExtTextOutA.EYScale
            Unsafe.SizeOf<float>();

        var emrTextSizeWithoutBuffers =
            // EmrText.Reference
            Unsafe.SizeOf<PointL>() +
            // EmrText.Chars
            Unsafe.SizeOf<uint>() +
            // EmrText.OffString
            Unsafe.SizeOf<uint>() +
            // EmrText.Options
            Unsafe.SizeOf<ExtTextOutOptions>() +
            // EmrText.Rectangle
            Unsafe.SizeOf<RectL>() +
            // EmrText.OffDx
            Unsafe.SizeOf<uint>();

        // EmrText.Reference
        memoryStream.Write(stackalloc byte[Unsafe.SizeOf<PointL>()]);
        // EmrText.Chars
        memoryStream.Write(BitConverter.GetBytes((uint)stringBuffer.Length));
        // EmrText.OffString
        memoryStream.Write(BitConverter.GetBytes(parentSizeWithoutTextBuffer + emrTextSizeWithoutBuffers + undefinedStringBufferSpace));
        // EmrText.Options
        memoryStream.Write(BitConverter.GetBytes((uint)options));
        // EmrText.Rectangle
        memoryStream.Write(BitConverter.GetBytes(rectangle?.Left ?? 0));
        memoryStream.Write(BitConverter.GetBytes(rectangle?.Top ?? 0));
        memoryStream.Write(BitConverter.GetBytes(rectangle?.Right ?? 0));
        memoryStream.Write(BitConverter.GetBytes(rectangle?.Bottom ?? 0));
        // EmrText.OffDx
        memoryStream.Write(BitConverter.GetBytes(
            parentSizeWithoutTextBuffer +
            emrTextSizeWithoutBuffers +
            undefinedStringBufferSpace +
            parentRecordType switch
            {
                EmfRecordType.EMR_EXTTEXTOUTA or EmfRecordType.EMR_POLYTEXTOUTA => stringBuffer.Length,
                EmfRecordType.EMR_EXTTEXTOUTW or EmfRecordType.EMR_POLYTEXTOUTW => stringBuffer.Length * 2,
                _ => throw new SwitchExpressionException("Unexpected parent record type")
            } +
            undefinedDxSpace));

        // EmrText.StringBuffer
        memoryStream.Write(stackalloc byte[undefinedStringBufferSpace]);

        var encoding = parentRecordType switch
        {
            EmfRecordType.EMR_EXTTEXTOUTA or EmfRecordType.EMR_POLYTEXTOUTA => Encoding.ASCII,
            EmfRecordType.EMR_EXTTEXTOUTW or EmfRecordType.EMR_POLYTEXTOUTW => Encoding.Unicode,
            _ => throw new SwitchExpressionException("Unexpected parent record type")
        };
        memoryStream.Write(encoding.GetBytes(stringBuffer));

        // EmrText.DxBuffer
        memoryStream.Write(stackalloc byte[undefinedDxSpace]);
        memoryStream.Write(dxBuffer.SelectMany(BitConverter.GetBytes).ToArray());

        // Act
        memoryStream.Seek(0, SeekOrigin.Begin);
        var emrText = EmrText.Parse(memoryStream, parentRecordType, parentSizeWithoutTextBuffer);

        // Assert
        if (rectangle is null)
        {
            emrText.Rectangle.Should().BeNull();
        }
        else
        {
            var emrTextRectangle = emrText.Rectangle!.Value;
            emrTextRectangle.Left.Should().Be(rectangle.Value.Left);
            emrTextRectangle.Top.Should().Be(rectangle.Value.Top);
            emrTextRectangle.Right.Should().Be(rectangle.Value.Right);
            emrTextRectangle.Bottom.Should().Be(rectangle.Value.Bottom);
        }
        emrText.StringBuffer.Should().Be(stringBuffer);
        emrText.DxBuffer.Should().BeEquivalentTo(dxBuffer);
    }

    public static IEnumerable<object[]> GetTestArguments()
    {
        var textOutOptions = Enum.GetValues<ExtTextOutOptions>();
        var parentRecordTypes = new[]
        {
            EmfRecordType.EMR_EXTTEXTOUTA,
            EmfRecordType.EMR_EXTTEXTOUTW,
            EmfRecordType.EMR_POLYTEXTOUTA,
            EmfRecordType.EMR_POLYTEXTOUTW
        };

        foreach (var textOutOption in textOutOptions)
        {
            foreach (var parentRecordType in parentRecordTypes)
            {
                yield return new object[] { textOutOption, parentRecordType };
            }
        }
    }
}