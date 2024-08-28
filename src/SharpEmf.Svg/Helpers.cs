using SharpEmf.Enums;

namespace SharpEmf.Svg;

internal static class Helpers
{
    public static (double X, double Y) GetScalingForCurrentMapMode(this EmfState state)
    {
        double scalingX;
        double scalingY;
        double windowOrgX = 0.0;
        double windowOrgY = 0.0;
        double viewPortOrgX = 0.0;
        double viewPortOrgY = 0.0;
        switch (state.MapMode)
        {
            case MapMode.MM_TEXT:
                scalingX = 1.0;
                scalingY = 1.0;
                break;
            // case MapMode.MM_LOMETRIC:
            //     // convert to 0.1 mm to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.1 * 1;
            //     scalingY = states->pxPerMm * 0.1 * -1;
            //     break;
            // case MapMode.MM_HIMETRIC:
            //     // convert to 0.01 mm to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.01 * 1;
            //     scalingY = states->pxPerMm * 0.01 * -1;
            //     break;
            // case MapMode.MM_LOENGLISH:
            //     // convert to 0.01 inch to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.01 * mmPerInch * 1;
            //     scalingY = states->pxPerMm * 0.01 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_HIENGLISH:
            //     // convert to 0.001 inch to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.001 * mmPerInch * 1;
            //     scalingY = states->pxPerMm * 0.001 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_TWIPS:
            //     // convert to 1 twips to pixel and invert Y
            //     scalingX = states->pxPerMm / 1440 * mmPerInch * 1;
            //     scalingY = states->pxPerMm / 1440 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_ISOTROPIC:
            //     if (states->windowExSet && states->viewPortExSet)
            //     {
            //         scalingX = states->viewPortExX / states->windowExX;
            //     }
            //     else
            //     {
            //         scalingX = 1.0;
            //     }
            //
            //     scalingY = scalingX;
            //     windowOrgX = states->windowOrgX;
            //     windowOrgY = states->windowOrgY;
            //     viewPortOrgX = states->viewPortOrgX;
            //     viewPortOrgY = states->viewPortOrgY;
            //     break;
            case MapMode.MM_ANISOTROPIC:
                scalingX = (double)state.ViewportExtent.Cx / state.WindowExtent.Cx;
                scalingY = (double)state.ViewportExtent.Cy / state.WindowExtent.Cy;

                break;
            default:
                scalingX = 1.0;
                scalingY = 1.0;
                break;
        }
        return (scalingX, scalingY);
    }

    public static (double X, double Y) ScalePointForCurrentMapMode(this EmfState state, double x, double y)
    {
        double scalingX;
        double scalingY;
        double windowOrgX = 0.0;
        double windowOrgY = 0.0;
        double viewPortOrgX = 0.0;
        double viewPortOrgY = 0.0;
        switch (state.MapMode)
        {
            case MapMode.MM_TEXT:
                scalingX = 1.0;
                scalingY = 1.0;
                break;
            // case MapMode.MM_LOMETRIC:
            //     // convert to 0.1 mm to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.1 * 1;
            //     scalingY = states->pxPerMm * 0.1 * -1;
            //     break;
            // case MapMode.MM_HIMETRIC:
            //     // convert to 0.01 mm to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.01 * 1;
            //     scalingY = states->pxPerMm * 0.01 * -1;
            //     break;
            // case MapMode.MM_LOENGLISH:
            //     // convert to 0.01 inch to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.01 * mmPerInch * 1;
            //     scalingY = states->pxPerMm * 0.01 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_HIENGLISH:
            //     // convert to 0.001 inch to pixel and invert Y
            //     scalingX = states->pxPerMm * 0.001 * mmPerInch * 1;
            //     scalingY = states->pxPerMm * 0.001 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_TWIPS:
            //     // convert to 1 twips to pixel and invert Y
            //     scalingX = states->pxPerMm / 1440 * mmPerInch * 1;
            //     scalingY = states->pxPerMm / 1440 * mmPerInch * -1;
            //     break;
            // case MapMode.MM_ISOTROPIC:
            //     if (states->windowExSet && states->viewPortExSet)
            //     {
            //         scalingX = states->viewPortExX / states->windowExX;
            //     }
            //     else
            //     {
            //         scalingX = 1.0;
            //     }
            //
            //     scalingY = scalingX;
            //     windowOrgX = states->windowOrgX;
            //     windowOrgY = states->windowOrgY;
            //     viewPortOrgX = states->viewPortOrgX;
            //     viewPortOrgY = states->viewPortOrgY;
            //     break;
            case MapMode.MM_ANISOTROPIC:
                scalingX = (double)state.ViewportExtent.Cx / state.WindowExtent.Cx;
                scalingY = (double)state.ViewportExtent.Cy / state.WindowExtent.Cy;

                break;
            default:
                scalingX = 1.0;
                scalingY = 1.0;
                break;
        }
        return ((x - windowOrgX) * scalingX + viewPortOrgX, (y - windowOrgY) * scalingY + viewPortOrgY);
    }
}