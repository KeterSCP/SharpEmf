using SharpEmf.Objects;

namespace SharpEmf.Svg;

internal struct GraphicsObject
{
    public GraphicsObjectType Type { get; set; }
    public LogBrushEx LogBrush { get; set; }
    public LogPenEx LogPen { get; set; }
}