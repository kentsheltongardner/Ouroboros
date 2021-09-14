using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Ouroboros
{
    using static Constants;
    using static Helpers;
    using static Input;
    public class Renderer
    {
        public static void Render(Data d, Graphics g, Size displaySize)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            RectangleF view = Viewport(displaySize.Width, displaySize.Height, d.w, d.h);
            g.FillRectangle(new SolidBrush(Color.FromArgb(32, 32, 32)), view);
            DrawFloor(d, g, view);
            DrawOuroboros(d, g, view);
            if (inputMode == InputModeEdit)
            {
                DrawEditGrid(d, g, view, displaySize);
            }
            if (d.satisfied && d.movementFrame == MovementFrames)
            {
                int alpha = 255 * d.winFrame / WinFrames;
                g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)), view);
            }
        }
        public static void DrawFloor(Data d, Graphics g, RectangleF view)
        {
            float[] x = new float[d.w];
            float[] y = new float[d.h];
            float cellSize = view.Width / d.w;
            for (int i = 0; i < d.w; i++)
            {
                x[i] = view.X + view.Width * i / d.w;
            }
            for (int i = 0; i < d.h; i++)
            {
                y[i] = view.Y + view.Height * i / d.h;
            }
            for (int i = 0; i < d.w; i++)
            {
                for (int j = 0; j < d.h; j++)
                {
                    DrawFloor(d, i, j, g, x[i], y[j], cellSize);
                }
            }
        }
        public static void DrawFloor(Data d, int gridX, int gridY, Graphics g, float x, float y, float cellSize)
        {
            float cx = x + cellSize * 0.5f;
            float cy = y + cellSize * 0.5f;
            if (d.position[gridX, gridY] != PositionSpace)
            {
                Pen pen         = new Pen(Color.FromArgb(128, 128, 128), cellSize * 0.125f);
                pen.StartCap    = LineCap.Round;
                pen.EndCap      = LineCap.Round;
                int dir         = d.rail[gridX, gridY];

                if (inputMode == InputModeEdit)
                {
                    switch (d.position[gridX, gridY])
                    {
                        case PositionRailXY:    g.FillRectangle(new SolidBrush(Color.FromArgb(64, 255, 255, 255)), x, y, cellSize, cellSize); break;
                        case PositionRailX:     g.FillRectangle(new SolidBrush(Color.FromArgb(32, 255, 255, 255)), x, y, cellSize, cellSize); break;
                        case PositionRailY:     g.FillRectangle(new SolidBrush(Color.FromArgb(16, 255, 255, 255)), x, y, cellSize, cellSize); break;
                    }
                }

                if ((dir & DirectionE) == DirectionE)
                {
                    g.DrawLine(pen, cx, cy, x + cellSize, cy);
                }
                if ((dir & DirectionS) == DirectionS)
                {
                    g.DrawLine(pen, cx, cy, cx, y + cellSize);
                }
                if ((dir & DirectionW) == DirectionW)
                {
                    g.DrawLine(pen, cx, cy, x, cy);
                }
                if ((dir & DirectionN) == DirectionN)
                {
                    g.DrawLine(pen, cx, cy, cx, y);
                }
            }

            switch (d.position[gridX, gridY])
            {
                case PositionRailXY:
                case PositionRailX:
                case PositionRailY:
                {
                    switch (d.type[gridX, gridY])
                    {
                        case ReqCover:
                        {
                            DrawSquare(g, cx, cy, 0.25f * cellSize, Color.FromArgb(128, 128, 128));
                            switch (d.color[gridX, gridY])
                            {
                                case ColorRed:      DrawSquare(g, cx, cy, cellSize * 0.15f, Color.FromArgb(255, 0, 0)); break;
                                case ColorGreen:    DrawSquare(g, cx, cy, cellSize * 0.15f, Color.FromArgb(0, 255, 0)); break;
                                case ColorBlue:     DrawSquare(g, cx, cy, cellSize * 0.15f, Color.FromArgb(0, 0, 255)); break;
                            }
                            break;
                        }
                        case ModIgnoreNext:
                        {
                            DrawPolygon(g, 4, 1, cx, cy, cellSize * 0.35f, 0.0f, Color.FromArgb(128, 128, 128));
                            switch (d.color[gridX, gridY])
                            {
                                case ColorRed:      DrawPolygon(g, 4, 1, cx, cy, cellSize * 0.2f, 0.0f, Color.FromArgb(255, 0, 0)); break;
                                case ColorGreen:    DrawPolygon(g, 4, 1, cx, cy, cellSize * 0.2f, 0.0f, Color.FromArgb(0, 255, 0)); break;
                                case ColorBlue:     DrawPolygon(g, 4, 1, cx, cy, cellSize * 0.2f, 0.0f, Color.FromArgb(0, 0, 255)); break;
                            }
                            break;
                        }
                        case ReqPair:
                        {
                            DrawCircle(g, cx, cy, cellSize * 0.25f, Color.FromArgb(128, 128, 128));
                            switch (d.color[gridX, gridY])
                            {
                                case ColorRed:      DrawCircle(g, cx, cy, cellSize * 0.15f, Color.FromArgb(255, 0, 0)); break;
                                case ColorGreen:    DrawCircle(g, cx, cy, cellSize * 0.15f, Color.FromArgb(0, 255, 0)); break;
                                case ColorBlue:     DrawCircle(g, cx, cy, cellSize * 0.15f, Color.FromArgb(0, 0, 255)); break;
                            }
                            break;
                        }
                    }
                    break;
                }
                case PositionSpace:
                {
                    switch (d.type[gridX, gridY])
                    {
                        case SpaceStar:
                            DrawPolygon(g, 6, 1, cx, cy, cellSize * 0.35f, (float)(1.5 * Math.PI), Color.FromArgb(128, 128, 128));
                            switch (d.color[gridX, gridY])
                            {
                                case ColorRed:      DrawPolygon(g, 6, 1, cx, cy, cellSize * 0.25f, (float)(1.5 * Math.PI), Color.FromArgb(255, 0, 0)); break;
                                case ColorGreen:    DrawPolygon(g, 6, 1, cx, cy, cellSize * 0.25f, (float)(1.5 * Math.PI), Color.FromArgb(0, 255, 0)); break;
                                case ColorBlue:     DrawPolygon(g, 6, 1, cx, cy, cellSize * 0.25f, (float)(1.5 * Math.PI), Color.FromArgb(0, 0, 255)); break;
                            }
                            break;
                    }
                    break;
                }
            }
        }




        public static void DrawOuroboros(Data d, Graphics g, RectangleF view)
        {
            float cellSize = view.Width / d.w;

            for (int i = 0; i < d.prev; i++)
            {
                DrawSegment(d, d.ouroboros[i], g, view, cellSize);
            }

            Segment head = d.ouroboros[d.length - 1];
            Segment prev = d.ouroboros[d.prev];

            float scalar = (float)d.movementFrame / MovementFrames;

            scalar = (float)((-Math.Cos(Math.PI * scalar* scalar) + 1) * 0.5f);

            int xDif = head.x - prev.x;
            int yDif = head.y - prev.y;

            float prevX = view.X + (cellSize * 0.5f) + (prev.x * cellSize);
            float prevY = view.Y + (cellSize * 0.5f) + (prev.y * cellSize);

            float offsetX = xDif * scalar * cellSize;
            float offsetY = yDif * scalar * cellSize;

            float headX = prevX + offsetX;
            float headY = prevY + offsetY;

            int length = (int)(Math.Abs(xDif + yDif) * DotCount * scalar);

            int xDir = Math.Sign(xDif);
            int yDir = Math.Sign(yDif);

            for (int i = 0; i <= length; i++)
            {
                
                float offset = cellSize * i / DotCount;
                DrawCircle(g, prevX + offset * xDir, prevY + offset * yDir, cellSize * 0.05f, Color.White);
            }


            DrawCircle(g, headX, headY, cellSize * 0.15f, Color.White);
        }
        public static void DrawSegment(Data d, Segment s, Graphics g, RectangleF view, float cellSize)
        {
            if (s.direction != DirectionNone)
            {
                float cx = view.X + cellSize * s.x + cellSize * 0.5f;
                float cy = view.Y + cellSize * s.y + cellSize * 0.5f;
                Point direction = CartesianDirection(s.direction);
                for (int i = 0; i <= DotCount; i++)
                {
                    float offset = cellSize * i / DotCount;
                    DrawCircle(g, cx + offset * direction.X, cy + offset * direction.Y, cellSize * 0.05f, Color.White);
                }
            }
        }

        public static void DrawEditGrid(Data d, Graphics g, RectangleF view, Size displaySize)
        {
            Pen thick = new Pen(Color.FromArgb(64, 255, 255, 255), 3.0f);
            Pen thin = new Pen(Color.FromArgb(64, 255, 255, 255), 1.0f);

            int thinW = d.w * 3;
            int thinH = d.h * 3;

            if (view.Contains(mouseX, mouseY))
            {
                float cellSize = CellSize(displaySize.Width, displaySize.Height, d.w, d.h);
                Point smallPosition = GridPosition(displaySize.Width, displaySize.Height, d.w * 3, d.h * 3, mouseX, mouseY);
                float smallX = view.X + smallPosition.X * cellSize / 3.0f;
                float smallY = view.Y + smallPosition.Y * cellSize / 3.0f;
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128, 128)), smallX, smallY, cellSize / 3.0f, cellSize / 3.0f);
                Point largePosition = GridPosition(displaySize.Width, displaySize.Height, d.w, d.h, mouseX, mouseY);
                float largeX = view.X + largePosition.X * cellSize;
                float largeY = view.Y + largePosition.Y * cellSize;
                g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128, 128)), largeX, largeY, cellSize, cellSize);
            }

            for (int i = 0; i <= thinW; i++)
            {
                float x = view.X + view.Width * i / thinW;
                if (i % 3 == 0)
                {
                    g.DrawLine(thick, x, view.Y, x, view.Y + view.Height);
                }
                else
                {
                    g.DrawLine(thin, x, view.Y, x, view.Y + view.Height);
                }
            }
            for (int i = 0; i <= thinH; i++)
            {
                float y = view.Y + view.Height * i / thinH;
                if (i % 3 == 0)
                {
                    g.DrawLine(thick, view.X, y, view.X + view.Width, y);
                }
                else
                {
                    g.DrawLine(thin, view.X, y, view.X + view.Width, y);
                }
            }
        }

        public static void DrawSquare(Graphics g, float x, float y, float r, Color c)
        {
            g.FillRectangle(new SolidBrush(c), x - r, y - r, r * 2.0f, r * 2.0f);
        }
        public static void DrawCircle(Graphics g, float x, float y, float r, Color c)
        {
            g.FillEllipse(new SolidBrush(c), x - r, y - r, r * 2.0f, r * 2.0f);
        }
        public static void DrawPolygon(Graphics g, int sides, int skip, float x, float y, float r, float theta, Color c)
        {
            PointF[] p = new PointF[sides];
            
            for (int i = 0; i < sides; i++)
            {
                double omega = theta + 2.0 * skip / sides * Math.PI * i;
                p[i].X = x + (float)(Math.Cos(omega) * r);
                p[i].Y = y + (float)(Math.Sin(omega) * r);
            }
            g.FillPolygon(new SolidBrush(c), p, FillMode.Winding);
        }
    }
}
