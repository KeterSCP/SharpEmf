using SharpEmf.WmfTypes.Bitmap;
using SkiaSharp;

namespace SharpEmf.Svg;

internal static class BitmapUtils
{
    public static unsafe string DibToPngBase64(byte[] dibData, BitmapInfoHeader bitmapHeader)
    {
        // TODO: this assumes that the DIB is 24-bit RGB, handle other cases

        using var bitmap = new SKBitmap();

        var width = bitmapHeader.Width;
        var height = bitmapHeader.Height;

        var rgbaData = DibToRgba(dibData, bitmapHeader);

        fixed(byte* rgbaDataPtr = rgbaData)
        {
            bitmap.InstallPixels(new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Unpremul), (nint)rgbaDataPtr, width * 4);
        }

        using var pngData = bitmap.Encode(SKEncodedImageFormat.Png, 100);
        var resultBase64Str = Convert.ToBase64String(pngData.Span);

        return resultBase64Str;
    }

    public static unsafe byte[] DibToRgba(byte[] dibData, BitmapInfoHeader bitmapHeader)
    {
        var usedBytes = (ushort)bitmapHeader.BitCount / 8 * bitmapHeader.Width;
        // DIB data is padded to the nearest DWORD (4-byte) boundary
        var padding = RoundUpToNearestMultipleOf4(usedBytes) - usedBytes;

        var rgbaStride = bitmapHeader.Width * 4;

        byte[] rgbaData = new byte[rgbaStride * bitmapHeader.Height];

        fixed (byte* dibDataPtr = dibData)
        {
            fixed (byte* rgbaDataPtr = rgbaData)
            {
                var dibDataPtr2 = dibDataPtr;
                var rgbaDataPtr2 = rgbaDataPtr;

                for (int y = 0; y < bitmapHeader.Height; y++)
                {
                    // Due to the fact that emf arrays after reading are reversed, padding skipping is done before the X-row loop
                    // Bytes order in EMF file: B, G, R, PADDED, PADDED, B, G, R, PADDED, PADDED, ...
                    // After reversing:         PADDED, PADDED, R, G, B, PADDED, PADDED, R, G, B, ...
                    dibDataPtr2 += padding;

                    for (int x = 0; x < bitmapHeader.Width; x++)
                    {
                        var r = *dibDataPtr2;
                        var g = *(dibDataPtr2 + 1);
                        var b = *(dibDataPtr2 + 2);
                        const byte a = 0xFF;

                        *rgbaDataPtr2 = r;
                        *(rgbaDataPtr2 + 1) = g;
                        *(rgbaDataPtr2 + 2) = b;
                        *(rgbaDataPtr2 + 3) = a;

                        dibDataPtr2 += 3;
                        rgbaDataPtr2 += 4;
                    }
                }
            }
        }

        return rgbaData;
    }

    private static int RoundUpToNearestMultipleOf4(int num)
    {
        return (num + 3) / 4 * 4;
    }
}