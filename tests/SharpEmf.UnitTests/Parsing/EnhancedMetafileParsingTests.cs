using FluentAssertions;
using SharpEmf.Enums;
using SharpEmf.Exceptions;
using SharpEmf.Records;

namespace SharpEmf.UnitTests.Parsing;

public class EnhancedMetafileParsingTests
{
    [Theory(DisplayName = "Should correctly parse EMF file")]
    [MemberData(nameof(GetEmfFilePaths))]
    public void ShouldCorrectlyParseEmfFile(string filePath)
    {
        // Act
        var emf = EnhancedMetafile.LoadFromFile(filePath);

        // Assert
        emf.Should().NotBeNull();
        emf.Header.Should().NotBeNull();
        emf.Records.Should().NotBeNull();
        emf.Records.Should().HaveCount((int)emf.Header.Records - 2);
        emf.Eof.Should().NotBeNull();
    }

    [Theory(DisplayName = "All record types should have a defined parser")]
    [MemberData(nameof(GetRecordTypes))]
    public void AllRecordTypesShouldHaveAParserDefined(EmfRecordType recordType)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        const uint size = 1;

        memoryStream.Write(BitConverter.GetBytes((uint)recordType));
        memoryStream.Write(BitConverter.GetBytes(size));

        memoryStream.Seek(0, SeekOrigin.Begin);

        // Act

        // ReSharper disable once AccessToDisposedClosure
        var exception = Record.Exception(() => EnhancedMetafileRecord.Parse(memoryStream));

        // Assert
        exception?.Should().NotBeOfType<MissingEmfRecordParserException>();
    }

    public static IEnumerable<object[]> GetEmfFilePaths()
    {
        var files = Directory.EnumerateFiles("Samples");

        foreach (var file in files)
        {
            yield return new object[] { file };
        }
    }

    public static IEnumerable<object[]> GetRecordTypes()
    {
        var recordTypes = Enum.GetValues<EmfRecordType>();

        foreach (var recordType in recordTypes)
        {
            yield return new object[] { recordType };
        }
    }
}