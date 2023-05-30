﻿using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpEmf.Extensions;

internal static class StreamExtensions
{
    internal static int ReadInt32(this Stream stream)
    {
        Span<byte> buffer = stackalloc byte[4];
        stream.ReadExactly(buffer);
        return BinaryPrimitives.ReadInt32LittleEndian(buffer);
    }

    internal static ushort ReadUInt16(this Stream stream)
    {
        Span<byte> buffer = stackalloc byte[2];
        stream.ReadExactly(buffer);
        return BinaryPrimitives.ReadUInt16LittleEndian(buffer);
    }

    internal static uint ReadUInt32(this Stream stream)
    {
        Span<byte> buffer = stackalloc byte[4];
        stream.ReadExactly(buffer);
        return BinaryPrimitives.ReadUInt32LittleEndian(buffer);
    }

    internal static ulong ReadUInt64(this Stream stream)
    {
        Span<byte> buffer = stackalloc byte[8];
        stream.ReadExactly(buffer);
        return BinaryPrimitives.ReadUInt64LittleEndian(buffer);
    }

    internal static UInt128 ReadUInt128(this Stream stream)
    {
        Span<byte> buffer = stackalloc byte[16];
        stream.ReadExactly(buffer);
        return BinaryPrimitives.ReadUInt128LittleEndian(buffer);
    }

    internal static string ReadUnicodeString(this Stream stream, int length)
    {
        Span<byte> buffer = length <= 1024 ? stackalloc byte[length] : new byte[length];
        stream.ReadExactly(buffer);
        return Encoding.Unicode.GetString(buffer);
    }

    internal static T ReadEnum<T>(this Stream stream) where T : Enum
    {
        var size = Unsafe.SizeOf<T>();

        Span<byte> buffer = stackalloc byte[size];
        stream.ReadExactly(buffer);

        return Unsafe.As<byte, T>(ref MemoryMarshal.GetReference(buffer));
    }
}