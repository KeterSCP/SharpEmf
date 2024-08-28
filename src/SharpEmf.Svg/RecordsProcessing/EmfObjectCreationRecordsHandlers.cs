using SharpEmf.Records.ObjectCreation;

namespace SharpEmf.Svg.RecordsProcessing;

internal static class EmfObjectCreationRecordsHandlers
{
    public static void HandleCreateBrushIndirect(EmfState state, EmrCreateBrushIndirect createBrushIndirect)
    {
        var index = createBrushIndirect.IHBrush;

        state.ObjectTable[index].LogBrush = createBrushIndirect.LogBrush;
        state.ObjectTable[index].Type = GraphicsObjectType.Brush;
    }

    public static void HandleExtCreatePen(EmfState state, EmrExtCreatePen extCreatePen)
    {
        var index = extCreatePen.IHPen;

        state.ObjectTable[index].LogPen = extCreatePen.Elp;
        state.ObjectTable[index].Type = GraphicsObjectType.Pen;
    }
}