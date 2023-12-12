using SharpEmf.Enums;
using SharpEmf.Objects;

namespace SharpEmf.Svg;

internal class PlaybackDeviceContext
{
    public BackgroundMode BkMode { get; set; }
    public PolygonFillMode PolyFillMode { get; set; }
    public LogBrushEx SelectedBrush { get; set; }
    public LogPenEx SelectedPen { get; set; }
}