using SharpEmf.Enums;
using SharpEmf.Objects;

namespace SharpEmf.Svg;

internal class PlaybackDeviceContext
{
    public BackgroundMode BkMode { get; set; }
    public PolygonFillMode PolyFillMode { get; set; }
    public LogBrushEx SelectedBrush { get; set; }
    public LogPenEx SelectedPen { get; set; }
    public ColorRef TextColor { get; set; }
    // TODO: should this be static?
    public static int ClipId { get; set; } = 1;
}