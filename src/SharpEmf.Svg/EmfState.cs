using SharpEmf.Enums;
using SharpEmf.WmfTypes;

namespace SharpEmf.Svg;

internal class EmfState
{
    public PointL WindowOrigin { get; set; }
    public PointL ViewportOrigin { get; set; }
    public SizeL WindowExtent { get; set; }
    public SizeL ViewportExtent { get; set; }
    public MapMode MapMode { get; set; }
    public GraphicsObject[] ObjectTable { get; set; }
    public PlaybackDeviceContext CurrentPlaybackDeviceContext { get; } = new();
    public float Scaling { get; set; }
}