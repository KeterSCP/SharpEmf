using SharpEmf.Enums;
using SharpEmf.Objects;
using SharpEmf.Records.ObjectManipulation;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfObjectManipulationRecordsHandlers
{
    public static void HandleSelectObject(EmfState state, EmrSelectObject selectObject)
    {
        var index = selectObject.IHObject;

        // Selecting a stock object
        if ((index & 0x80000000) != 0)
        {
            var stockObject = (StockObject)index;

            switch (stockObject)
            {
                case StockObject.WHITE_BRUSH:
                    state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                        brushStyle: BrushStyle.BS_SOLID,
                        color: new ColorRef(0xFF, 0xFF, 0xFF));
                    break;
                case StockObject.LTGRAY_BRUSH:
                    state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                        brushStyle: BrushStyle.BS_SOLID,
                        color: new ColorRef(0xC0, 0xC0, 0xC0));
                    break;
                case StockObject.GRAY_BRUSH:
                    state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                        brushStyle: BrushStyle.BS_SOLID,
                        color: new ColorRef(0x80, 0x80, 0x80));
                    break;
                case StockObject.DKGRAY_BRUSH:
                    state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                        brushStyle: BrushStyle.BS_SOLID,
                        color: new ColorRef(0x40, 0x40, 0x40));
                    break;
                case StockObject.NULL_BRUSH:
                    state.CurrentPlaybackDeviceContext.SelectedBrush = LogBrushEx.Null;
                    break;
                case StockObject.WHITE_PEN:
                    state.CurrentPlaybackDeviceContext.SelectedPen = new LogPenEx(
                        penStyle: PenStyle.PS_COSMETIC | PenStyle.PS_SOLID,
                        color: new ColorRef(0xFF, 0xFF, 0xFF));
                    break;
                case StockObject.BLACK_PEN:
                    state.CurrentPlaybackDeviceContext.SelectedPen = new LogPenEx(
                        penStyle: PenStyle.PS_COSMETIC | PenStyle.PS_SOLID,
                        color: new ColorRef(0x00, 0x00, 0x00));
                    break;
                case StockObject.NULL_PEN:
                    state.CurrentPlaybackDeviceContext.SelectedPen = LogPenEx.Null;
                    break;
                default:
                    Console.WriteLine($"Warning: Stock object {stockObject} is not supported");
                    // TODO: handle other stock objects
                    break;
            }

            return;
        }

        var graphicsObject = state.ObjectTable[index];

        if (graphicsObject.Type is GraphicsObjectType.Pen)
        {
            state.CurrentPlaybackDeviceContext.SelectedPen = graphicsObject.LogPen;
        }
        else if (graphicsObject.Type is GraphicsObjectType.Brush)
        {
            state.CurrentPlaybackDeviceContext.SelectedBrush = graphicsObject.LogBrush;
        }
        else if (graphicsObject.Type is GraphicsObjectType.Unknown)
        {
            Console.WriteLine("Warning: Unknown object type selected");
        }
    }

    public static void HandleDeleteObject(EmfState state, EmrDeleteObject deleteObject)
    {
        var index = deleteObject.IHObject;
        state.ObjectTable[index] = default;
    }
}