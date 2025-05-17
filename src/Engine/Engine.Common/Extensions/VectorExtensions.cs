using System.Drawing;
using System.Numerics;

namespace EvoX.Engine.Common.Extensions;
public static class VectorExtensions
{
    public static Vector2 Center(this RectangleF rec)
    {
        return new Vector2(rec.X + (rec.Width / 2f), rec.Y + (rec.Height / 2f));
    }
}