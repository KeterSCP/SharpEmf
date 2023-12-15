using System.Text;
using SharpEmf.Records.Drawing;
using SharpEmf.Records.ObjectCreation;
using SharpEmf.Records.ObjectManipulation;
using SharpEmf.Records.PathBracket;
using SharpEmf.Records.State;
using SharpEmf.Svg.RecordsProcessing;

namespace SharpEmf.Svg;

public static class EmfSvgWriter
{
    public static string ConvertToSvg(EnhancedMetafile emf)
    {
        var sb = new StringBuilder();
        var state = new EmfState();

        EmfControlRecordsHandlers.HandleHeaderRecord(sb, state, emf.Header);

        foreach (var record in emf.Records)
        {
            switch (record)
            {
                case EmrSetMapMode setMapMode:
                    EmfStateRecordsHandlers.HandleSetMapModeRecord(state, setMapMode);
                    break;
                case EmrSetBkMode setBkMode:
                    EmfStateRecordsHandlers.HandleSetBkModeRecord(state, setBkMode);
                    break;
                case EmrSetWindowOrgEx setWindowOrgEx:
                    EmfStateRecordsHandlers.HandleSetWindowOrgExRecord(state, setWindowOrgEx);
                    break;
                case EmrSetViewportOrgEx setViewportOrgEx:
                    EmfStateRecordsHandlers.HandleSetViewportOrgExRecord(state, setViewportOrgEx);
                    break;
                case EmrSetWindowExtEx setWindowExtEx:
                    EmfStateRecordsHandlers.HandleSetWindowExtExRecord(state, setWindowExtEx);
                    break;
                case EmrSetViewportExtEx setViewportExtEx:
                    EmfStateRecordsHandlers.HandleSetViewportExtExRecord(state, setViewportExtEx);
                    break;
                case EmrSetPolyfillMode setPolyfillMode:
                    EmfStateRecordsHandlers.HandleSetPolyfillMode(state, setPolyfillMode);
                    break;
                case EmrCreateBrushIndirect createBrushIndirect:
                    EmfObjectCreationRecordsHandlers.HandleCreateBrushIndirect(state, createBrushIndirect);
                    break;
                case EmrSelectObject selectObject:
                    EmfObjectManipulationRecordsHandlers.HandleSelectObject(state, selectObject);
                    break;
                case EmrDeleteObject deleteObject:
                    EmfObjectManipulationRecordsHandlers.HandleDeleteObject(state, deleteObject);
                    break;
                case EmrExtCreatePen extCreatePen:
                    EmfObjectCreationRecordsHandlers.HandleExtCreatePen(state, extCreatePen);
                    break;
                case EmrPolyPolygon16 polyPolygon16:
                    EmfDrawingRecordsHandlers.HandlePolyPolygon16(sb, state, polyPolygon16);
                    break;
                case EmrBeginPath:
                    EmfPathBracketRecordsHandlers.HandleBeginPath(sb, state);
                    break;
                case EmrEndPath:
                    EmfPathBracketRecordsHandlers.HandleEndPath(sb, state);
                    break;
                case EmrMoveToEx moveToEx:
                    EmfStateRecordsHandlers.HandleMoveToEx(sb, state, moveToEx);
                    break;
                case EmrPolyBezierTo16 polyBezierTo16:
                    EmfDrawingRecordsHandlers.HandlePolybezierTo16(sb, state, polyBezierTo16);
                    break;
                case EmrCloseFigure:
                    EmfPathBracketRecordsHandlers.HandleCloseFigure(sb);
                    break;
            }
        }

        sb.AppendLine("</g>");
        sb.AppendLine("</svg>");
        return sb.ToString();
    }
}