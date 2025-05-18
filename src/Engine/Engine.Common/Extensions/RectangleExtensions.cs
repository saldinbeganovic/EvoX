using System.Drawing;

namespace EvoX.Engine.Common.Extensions;
public static class RectangleExtensions
{
    public static Rectangle AdjustLocation(this Rectangle rec, int x, int y)
    {
        rec.Offset(x, y);
        return rec;
    }

    public static Rectangle Round(this RectangleF recF)
    {
        var sysRec = Rectangle.Round(recF);
        return new Rectangle(sysRec.X, sysRec.Y, sysRec.Width, sysRec.Height);
    }

    public static RectangleF ToRectangleF(this Rectangle rec)
    {
        return new RectangleF(rec.X, rec.Y, rec.Width, rec.Height);
    }

    public static Rectangle ToRectangle(this RectangleF rec)
    {
        return new Rectangle(
            rec.X.ToInt32(),
            rec.Y.ToInt32(),
            rec.Width.ToInt32(),
            rec.Height.ToInt32()
        );
    }
}
