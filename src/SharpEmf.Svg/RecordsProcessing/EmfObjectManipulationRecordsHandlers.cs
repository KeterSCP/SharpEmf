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
            if (stockObject is StockObject.WHITE_BRUSH)
            {
                state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                    brushStyle: BrushStyle.BS_SOLID,
                    color: new ColorRef(0xFF, 0xFF, 0xFF));
            }
            else if (stockObject is StockObject.LTGRAY_BRUSH)
            {
                state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                    brushStyle: BrushStyle.BS_SOLID,
                    color: new ColorRef(0xC0, 0xC0, 0xC0));
            }
            else if (stockObject is StockObject.GRAY_BRUSH)
            {
                state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                    brushStyle: BrushStyle.BS_SOLID,
                    color: new ColorRef(0x80, 0x80, 0x80));
            }
            else if (stockObject is StockObject.DKGRAY_BRUSH)
            {
                state.CurrentPlaybackDeviceContext.SelectedBrush = new LogBrushEx(
                    brushStyle: BrushStyle.BS_SOLID,
                    color: new ColorRef(0x40, 0x40, 0x40));
            }
            else if (stockObject is StockObject.NULL_BRUSH)
            {
                state.CurrentPlaybackDeviceContext.SelectedBrush = LogBrushEx.Null;
            }
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