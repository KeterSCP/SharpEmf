using System.Diagnostics;
using JetBrains.Annotations;
using SharpEmf.Exceptions;
using SharpEmf.Records;
using SharpEmf.Records.Control.Eof;
using SharpEmf.Records.Control.Header;

namespace SharpEmf;

[PublicAPI]
public sealed class EnhancedMetafile
{
    /// <summary>
    /// Header of the EMF file
    /// </summary>
    public EmfMetafileHeader Header { get; }

    /// <summary>
    /// Records of the EMF file
    /// </summary>
    public IReadOnlyList<EnhancedMetafileRecord> Records { get; }

    /// <summary>
    /// End of file record
    /// </summary>
    public EmrEof Eof { get; }

    private EnhancedMetafile(EmfMetafileHeader header, IReadOnlyList<EnhancedMetafileRecord> records, EmrEof eof)
    {
        Header = header;
        Records = records;
        Eof = eof;
    }

    public static EnhancedMetafile LoadFromFile(string path)
    {
        using var fs = File.OpenRead(path);
        var header = (EmfMetafileHeader)EnhancedMetafileRecord.Parse(fs);
        fs.Seek(header.Size - fs.Position, SeekOrigin.Current);

        var records = new List<EnhancedMetafileRecord>((int)header.Records - 2);

        while (fs.Position < fs.Length)
        {
            var record = EnhancedMetafileRecord.Parse(fs);
            if (record is EmrEof eof)
            {
                return new EnhancedMetafile(header, records, eof);
            }

            records.Add(record);
        }

        throw new EmfParseException("EOF record not found");
    }
}