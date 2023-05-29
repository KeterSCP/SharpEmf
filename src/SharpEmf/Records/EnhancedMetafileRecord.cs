﻿using JetBrains.Annotations;
using SharpEmf.Enums;
using SharpEmf.Extensions;
using SharpEmf.Records.Eof;
using SharpEmf.Records.Header;

namespace SharpEmf.Records;

[PublicAPI]
public abstract record EnhancedMetafileRecord
{
    /// <summary>
    /// Record type
    /// </summary>
    public abstract EmfRecordType Type { get; }

    /// <summary>
    /// Record size in bytes
    /// </summary>
    public abstract uint Size { get; }

    public static EnhancedMetafileRecord Parse(Stream stream)
    {
        var type = (EmfRecordType)stream.ReadUInt32();
        var size = stream.ReadUInt32();

        return type switch
        {
            EmfRecordType.EMR_HEADER => EmfMetafileHeader.Parse(stream, type, size),
            EmfRecordType.EMR_EOF => EmrEof.Parse(stream, type, size),
            _ => SkipRecord(stream, type, size)
        };
    }

    private static EnhancedMetafileRecord SkipRecord(Stream stream, EmfRecordType type, uint size)
    {
        Console.WriteLine($"Skipping record of type {type} with size {size}");
        stream.Seek(size - 8, SeekOrigin.Current);
        return null!;
    }
}