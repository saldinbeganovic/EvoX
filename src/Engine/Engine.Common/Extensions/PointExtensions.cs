using System.Drawing;

namespace EvoX.Engine.Common.Extensions;
public static class PointExtensions
{
    public static PointF CenterP(this RectangleF rec)
    {
        return new PointF(
            rec.X + (rec.Width / 2f),
            rec.Y + (rec.Height / 2f));
    }
}
