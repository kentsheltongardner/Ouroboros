using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ouroboros
{
    using static Constants;
    public static class Helpers
    {
        public static void Toggle(ref int flags, int positions)
        {
            flags = (flags & ~positions) | (~flags & positions);
        }
        public static int FloorCategory(int x, int y)
        {
            bool ex = (x & 1) == 0;
            bool ey = (y & 1) == 0;
            if (ex & ey) return PositionRailXY;
            else if (ex) return PositionRailY;
            else if (ey) return PositionRailX;
            else return PositionSpace;
        }
        public static Point GridPosition(int displayW, int displayH, int levelW, int levelH, int x, int y)
        {
            float cellSize = CellSize(displayW, displayH, levelW, levelH);
            RectangleF view = Viewport(displayW, displayH, levelW, levelH);
            int gridX = (int)(Math.Floor(x - view.X) / cellSize);
            int gridY = (int)(Math.Floor(y - view.Y) / cellSize);
            return new Point(gridX, gridY);
        }
        public static RectangleF Viewport(int displayW, int displayH, int levelW, int levelH)
        {
            float cellSize = CellSize(displayW, displayH, levelW, levelH);
            float w = cellSize * levelW;
            float h = cellSize * levelH;
            float x = (displayW - w) * 0.5f;
            float y = (displayH - h) * 0.5f;
            return new RectangleF(x, y, w, h);
        }
        public static float CellSize(int displayW, int displayH, int levelW, int levelH)
        {
            if (displayW * levelH > levelW * displayH)
            {
                return (float)displayH / levelH;
            }
            return (float)displayW / levelW;
        }
        public static int Direction(int xDir, int yDir)
        {
            if (xDir == -1)
            {
                if (yDir == -1) return DirectionNW;
                if (yDir == 0)  return DirectionW;
                if (yDir == 1)  return DirectionSW;
            }
            if (xDir == 1)
            {
                if (yDir == -1) return DirectionNE;
                if (yDir == 0)  return DirectionE;
                if (yDir == 1)  return DirectionSE;
            }
            if (yDir == -1)     return DirectionN;
            if (yDir == 1)      return DirectionS;
            return DirectionNone;
        }
        public static int OppositeDirection(int dir)
        {
            switch (dir)
            {
                case DirectionE:    return DirectionW;
                case DirectionSE:   return DirectionNW;
                case DirectionS:    return DirectionN;
                case DirectionSW:   return DirectionNE;
                case DirectionW:    return DirectionE;
                case DirectionNW:   return DirectionSE;
                case DirectionN:    return DirectionS;
                case DirectionNE:   return DirectionSW;
            }
            return DirectionNone;
        }
        public static Point CartesianDirection(int dir)
        {
            switch (dir)
            {
                case DirectionE:    return new Point(1, 0);
                case DirectionSE:   return new Point(1, 1);
                case DirectionS:    return new Point(0, 1);
                case DirectionSW:   return new Point(-1, 1);
                case DirectionW:    return new Point(-1, 0);
                case DirectionNW:   return new Point(-1, -1);
                case DirectionN:    return new Point(0, -1);
                case DirectionNE:   return new Point(1, -1);
            }
            return new Point(0, 0);
        }
    }
}
