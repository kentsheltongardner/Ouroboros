using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ouroboros
{

    //Git comment
    using static Constants;
    using static Helpers;
    public class Data
    {
        public int w, h, startX, startY, length, prev;
        public Segment[] ouroboros;
        public int[,] position, type, rail, direction, color, number;
        public int movementFrame = MovementFrames;
        public int winFrame = WinFrames;
        public bool satisfied;


        public Data(int w, int h)
        {
            this.w = w;
            this.h = h;
            startX = startY = 0;
            ClearLevel();
        }
        public void ClearLevel()
        {
            position = new int[w, h];
            type = new int[w, h];
            rail = new int[w, h];
            direction = new int[w, h];
            color = new int[w, h];
            number = new int[w, h];
            ouroboros = new Segment[MaxLength];

            SetCategories(0, 0, w, h);
            Fill(0, 0, w, h);
            ouroboros[0] = new Segment(0, 0, DirectionNone);
            length = 1;
            prev = 0;
            ResetVarables();
        }
        public void ResetOuroboros()
        {
            ResetVarables();
            ouroboros[0] = new Segment(startX, startY, DirectionNone);
            length = 1;
            prev = 0;
        }
        public void ResetVarables()
        {
            satisfied = false;
            winFrame = WinFrames;
            movementFrame = MovementFrames;
        }

        public void SetCategories(int x1, int y1, int x2, int y2)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    position[i, j] = FloorCategory(i, j);
                }
            }
        }
        public void Fill(int x1, int y1, int x2, int y2)   //  Must be in bounds
        {
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    switch (position[i, j])
                    {
                        case PositionRailX: rail[i, j] = DirectionE | DirectionW; break;
                        case PositionRailY: rail[i, j] = DirectionS | DirectionN; break;
                        case PositionRailXY:
                            if (i > 0) rail[i, j] |= DirectionW;
                            if (j > 0) rail[i, j] |= DirectionN;
                            if (i < w - 1) rail[i, j] |= DirectionE;
                            if (j < h - 1) rail[i, j] |= DirectionS;
                            break;
                    }
                }
            }
        }






        public void GrowWest()
        {
            int newW = w + 2;
            int[,] newPosition = new int[newW, h];
            int[,] newType = new int[newW, h];
            int[,] newRail = new int[newW, h];
            int[,] newDirection = new int[newW, h];
            int[,] newColor = new int[newW, h];
            int[,] newNumber = new int[newW, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i + 2, j] = position[i, j];
                    newType[i + 2, j] = type[i, j];
                    newRail[i + 2, j] = rail[i, j];
                    newDirection[i + 2, j] = direction[i, j];
                    newColor[i + 2, j] = color[i, j];
                    newNumber[i + 2, j] = number[i, j];
                }
            }
            w = newW;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            SetCategories(0, 0, 3, h);
            Fill(0, 0, 3, h);
        }
        public void GrowNorth()
        {
            int newH = h + 2;
            int[,] newPosition = new int[w, newH];
            int[,] newType = new int[w, newH];
            int[,] newRail = new int[w, newH];
            int[,] newDirection = new int[w, newH];
            int[,] newColor = new int[w, newH];
            int[,] newNumber = new int[w, newH];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i, j + 2] = position[i, j];
                    newType[i, j + 2] = type[i, j];
                    newRail[i, j + 2] = rail[i, j];
                    newDirection[i, j + 2] = direction[i, j];
                    newColor[i, j + 2] = color[i, j];
                    newNumber[i, j + 2] = number[i, j];
                }
            }
            h = newH;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            SetCategories(0, 0, w, 3);
            Fill(0, 0, w, 3);
        }
        public void GrowEast()
        {
            int newW = w + 2;
            int[,] newPosition = new int[newW, h];
            int[,] newType = new int[newW, h];
            int[,] newRail = new int[newW, h];
            int[,] newDirection = new int[newW, h];
            int[,] newColor = new int[newW, h];
            int[,] newNumber = new int[newW, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i, j] = position[i, j];
                    newType[i, j] = type[i, j];
                    newRail[i, j] = rail[i, j];
                    newDirection[i, j] = direction[i, j];
                    newColor[i, j] = color[i, j];
                    newNumber[i, j] = number[i, j];
                }
            }
            w = newW;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            SetCategories(w - 3, 0, w, h);
            Fill(w - 3, 0, w, h);
        }
        public void GrowSouth()
        {
            int newH = h + 2;
            int[,] newPosition = new int[w, newH];
            int[,] newType = new int[w, newH];
            int[,] newRail = new int[w, newH];
            int[,] newDirection = new int[w, newH];
            int[,] newColor = new int[w, newH];
            int[,] newNumber = new int[w, newH];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i, j] = position[i, j];
                    newType[i, j] = type[i, j];
                    newRail[i, j] = rail[i, j];
                    newDirection[i, j] = direction[i, j];
                    newColor[i, j] = color[i, j];
                    newNumber[i, j] = number[i, j];
                }
            }
            h = newH;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            SetCategories(0, h - 3, w, h);
            Fill(0, h - 3, w, h);
        }

        public void ShrinkWest()
        {
            int newW = w - 2;
            int[,] newPosition = new int[newW, h];
            int[,] newType = new int[newW, h];
            int[,] newRail = new int[newW, h];
            int[,] newDirection = new int[newW, h];
            int[,] newColor = new int[newW, h];
            int[,] newNumber = new int[newW, h];
            for (int i = 0; i < newW; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i, j] = position[i + 2, j];
                    newType[i, j] = type[i + 2, j];
                    newRail[i, j] = rail[i + 2, j];
                    newDirection[i, j] = direction[i + 2, j];
                    newColor[i, j] = color[i + 2, j];
                    newNumber[i, j] = number[i + 2, j];
                }
            }
            w = newW;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            for (int i = 0; i < h; i++)
            {
                rail[0, i] &= ~DirectionW;
            }
        }
        public void ShrinkNorth()
        {
            int newH = h - 2;
            int[,] newPosition = new int[w, newH];
            int[,] newType = new int[w, newH];
            int[,] newRail = new int[w, newH];
            int[,] newDirection = new int[w, newH];
            int[,] newColor = new int[w, newH];
            int[,] newNumber = new int[w, newH];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < newH; j++)
                {
                    newPosition[i, j] = position[i, j + 2];
                    newType[i, j] = type[i, j + 2];
                    newRail[i, j] = rail[i, j + 2];
                    newDirection[i, j] = direction[i, j + 2];
                    newColor[i, j] = color[i, j + 2];
                    newNumber[i, j] = number[i, j + 2];
                }
            }
            h = newH;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            for (int i = 0; i < w; i++)
            {
                rail[i, 0] &= ~DirectionN;
            }
        }
        public void ShrinkEast()
        {
            int newW = w - 2;
            int[,] newPosition = new int[newW, h];
            int[,] newType = new int[newW, h];
            int[,] newRail = new int[newW, h];
            int[,] newDirection = new int[newW, h];
            int[,] newColor = new int[newW, h];
            int[,] newNumber = new int[newW, h];
            for (int i = 0; i < newW; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    newPosition[i, j] = position[i, j];
                    newType[i, j] = type[i, j];
                    newRail[i, j] = rail[i, j];
                    newDirection[i, j] = direction[i, j];
                    newColor[i, j] = color[i, j];
                    newNumber[i, j] = number[i, j];
                }
            }
            w = newW;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            for (int i = 0; i < h; i++)
            {
                rail[w - 1, i] &= ~DirectionE;
            }
        }
        public void ShrinkSouth()
        {
            int newH = h - 2;
            int[,] newPosition = new int[w, newH];
            int[,] newType = new int[w, newH];
            int[,] newRail = new int[w, newH];
            int[,] newDirection = new int[w, newH];
            int[,] newColor = new int[w, newH];
            int[,] newNumber = new int[w, newH];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < newH; j++)
                {
                    newPosition[i, j] = position[i, j];
                    newType[i, j] = type[i, j];
                    newRail[i, j] = rail[i, j];
                    newDirection[i, j] = direction[i, j];
                    newColor[i, j] = color[i, j];
                    newNumber[i, j] = number[i, j];
                }
            }
            h = newH;
            position = newPosition;
            type = newType;
            rail = newRail;
            direction = newDirection;
            color = newColor;
            number = newNumber;
            for (int i = 0; i < w; i++)
            {
                rail[i, h - 1] &= ~DirectionS;
            }
        }

        public void Edit(MouseEventArgs e, Size displaySize)
        {
            Point large = GridPosition(displaySize.Width, displaySize.Height, w, h, e.X, e.Y);
            int lx = large.X;
            int ly = large.Y;
            if (lx >= 0 && lx < w && ly >= 0 && ly < h) //  If the click is inside the level
            {
                Point small = GridPosition(displaySize.Width, displaySize.Height, w * 3, h * 3, e.X, e.Y);
                int sx = small.X % 3;
                int sy = small.Y % 3;
                bool left = e.Button == MouseButtons.Left;
                bool right = e.Button == MouseButtons.Right;

                if (sx + sy == 4)
                {
                    color[lx, ly]++;
                    if (color[lx, ly] == ColorCount)
                    {
                        color[lx, ly] = ColorNone;
                    }
                }

                if (lx == 0 && sx == 0 && sy == 1) //  Resize west
                {
                    if (left) GrowWest();
                    else if (right && w > 3) ShrinkWest();
                }
                else if (ly == 0 && sy == 0 && sx == 1) //  Resize north
                {
                    if (left) GrowNorth();
                    else if (right && h > 3) ShrinkNorth();
                }
                else if (lx == w - 1 && sx == 2 && sy == 1) //  Resize east
                {
                    if (left) GrowEast();
                    else if (right && w > 3) ShrinkEast();
                }
                else if (ly == h - 1 && sy == 2 && sx == 1) //  Resize south
                {
                    if (left) GrowSouth();
                    else if (right && h > 3) ShrinkSouth();
                }
                else
                {
                    switch (position[lx, ly])
                    {
                        case PositionSpace:
                            if ((sx & sy) == 1) //  Center click
                            {
                                type[lx, ly]++;
                                if (type[lx, ly] == SpaceCount)
                                {
                                    type[lx, ly] = TypeEmpty;
                                }
                            }
                            break;
                        case PositionRailX:
                            if (sy == 1)
                            {
                                if ((sx & 1) == 0)
                                {
                                    type[lx, ly] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionE | DirectionW);
                                    Toggle(ref rail[lx + 1, ly], DirectionW);
                                    Toggle(ref rail[lx - 1, ly], DirectionE);
                                }
                                else
                                {
                                    type[lx, ly]++;
                                    if (type[lx, ly] == RailCount)
                                    {
                                        type[lx, ly] = TypeEmpty;
                                    }
                                }
                            }
                            else
                            {

                            }
                            break;
                        case PositionRailY:
                            if (sx == 1)
                            {
                                if ((sy & 1) == 0)
                                {
                                    type[lx, ly] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionS | DirectionN);
                                    Toggle(ref rail[lx, ly + 1], DirectionN);
                                    Toggle(ref rail[lx, ly - 1], DirectionS);
                                }
                                else
                                {
                                    type[lx, ly]++;
                                    if (type[lx, ly] == RailCount)
                                    {
                                        type[lx, ly] = TypeEmpty;
                                    }
                                }
                            }
                            else
                            {

                            }
                            break;
                        case PositionRailXY:
                            if (e.Button == MouseButtons.Right)
                            {
                                startX = lx;
                                startY = ly;
                                ResetOuroboros();
                            }
                            else if (e.Button == MouseButtons.Left)
                            {
                                if ((sx & sy) == 1)
                                {
                                    type[lx, ly]++;
                                    if (type[lx, ly] == RailCount)
                                    {
                                        type[lx, ly] = TypeEmpty;
                                    }
                                }
                                else if (sx == 2 && sy == 1 && lx < w - 1)
                                {
                                    type[lx + 1, ly] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionE);
                                    Toggle(ref rail[lx + 1, ly], DirectionE | DirectionW);
                                    Toggle(ref rail[lx + 2, ly], DirectionW);
                                }
                                else if (sx == 1 && sy == 2 && ly < h - 1)
                                {
                                    type[lx, ly + 1] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionS);
                                    Toggle(ref rail[lx, ly + 1], DirectionS | DirectionN);
                                    Toggle(ref rail[lx, ly + 2], DirectionN);
                                }
                                else if (sx == 0 && sy == 1 && lx > 0)
                                {
                                    type[lx - 1, ly] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionW);
                                    Toggle(ref rail[lx - 1, ly], DirectionE | DirectionW);
                                    Toggle(ref rail[lx - 2, ly], DirectionE);
                                }
                                else if (sx == 1 && sy == 0 && ly > 0)
                                {
                                    type[lx, ly - 1] = TypeEmpty;
                                    Toggle(ref rail[lx, ly], DirectionN);
                                    Toggle(ref rail[lx, ly - 1], DirectionS | DirectionN);
                                    Toggle(ref rail[lx, ly - 2], DirectionS);
                                }
                            }
                            break;
                    }
                }
            }
        }

        public Segment[,] SegmentGrid()
        {
            Segment[,] grid = new Segment[w, h];
            for (int i = 0; i < length; i++)
            {
                grid[ouroboros[i].x, ouroboros[i].y] = ouroboros[i];
            }
            return grid;
        }

        public void Update()
        {
            if (!satisfied && Satisfied())
            {
                satisfied = true;
                winFrame = 0;
            }
            if (winFrame < WinFrames)
            {
                winFrame++;
            }



            if (prev != length - 1)
            {
                movementFrame++;
                if (movementFrame == MovementFrames)
                {
                    prev = length - 1;
                }
            }
        }
        public bool CanPlay()
        {
            return movementFrame == MovementFrames && winFrame == WinFrames;
        }
        public void Play(MouseEventArgs e, Size displaySize)
        {
            if (CanPlay())
            {
                Point p = GridPosition(displaySize.Width, displaySize.Height, w, h, e.X, e.Y);
                int x = p.X;
                int y = p.Y;
                if (x >= 0 && x < w && y >= 0 && y < h && position[x, y] == PositionRailXY) //  If the click is inside the level
                {
                    Segment head = ouroboros[length - 1];
                    if (head.x == x && head.y == y)
                    {
                        ResetOuroboros();
                    }
                    else
                    {
                        int newLength = 1;  // Skip tail
                        for (; newLength < length; newLength++)
                        {
                            Segment s = ouroboros[newLength];
                            if (s.x == x &&
                                s.y == y &&
                                FloorCategory(s.x, s.y) == PositionRailXY)
                            {
                                break;
                            }
                        }

                        if (newLength < length)
                        {
                            length = newLength + 1;
                            prev = newLength;
                        }
                        else
                        {
                            if ((head.x == x) != (head.y == y))
                            {
                                int xDir = Math.Sign(x - head.x);
                                int yDir = Math.Sign(y - head.y);
                                int dir = Direction(xDir, yDir);
                                int opposite = OppositeDirection(dir);
                                Segment[,] grid = SegmentGrid();
                                if (IsPath(head.x, head.y, x, y, xDir, yDir, dir, opposite, grid))
                                {
                                    Grow(head.x, head.y, x, y, xDir, yDir, dir);
                                    movementFrame = 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        public bool IsPath(int x1, int y1, int x2, int y2, int xDir, int yDir, int dir, int opposite, Segment[,] grid)
        {
            Segment tail = ouroboros[0];
            if ((x1 == x2 && y1 == y2) || (
                x1 + xDir * 2 == tail.x &&
                y1 + yDir * 2 == tail.y &&
                (tail.direction & opposite) != opposite)) return true;

            if ((rail[x1, y1] & dir) != dir) return false;

            Segment next = grid[x1 + xDir * 2, y1 + yDir * 2];
            if (next != null && next.direction != DirectionNone) return false;

            return IsPath(x1 + xDir * 2, y1 + yDir * 2, x2, y2, xDir, yDir, dir, opposite, grid);
        }
        public void Grow(int x1, int y1, int x2, int y2, int xDir, int yDir, int dir)
        {
            Segment tail = ouroboros[0];
            if ((x1 == x2 && y1 == y2) ||
                (x1 == tail.x &&
                y1 == tail.y &&
                tail.direction != DirectionNone)) return;

            ouroboros[length - 1].direction = dir;
            ouroboros[length] = new Segment(x1 + xDir, y1 + yDir, DirectionNone);
            length++;
            Grow(x1 + xDir, y1 + yDir, x2, y2, xDir, yDir, dir);
        }














        public void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\levels";
            saveFileDialog.Filter = "Level file|*.lvl";
            saveFileDialog.Title = "Save level";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile());

                writer.WriteLine(w);
                writer.WriteLine(h);
                writer.WriteLine(startX);
                writer.WriteLine(startY);
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        writer.WriteLine(position[i, j]);
                        writer.WriteLine(type[i, j]);
                        writer.WriteLine(rail[i, j]);
                        writer.WriteLine(direction[i, j]);
                        writer.WriteLine(color[i, j]);
                        writer.WriteLine(number[i, j]);
                    }
                }
                writer.Close();
            }
        }
        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\levels";
            openFileDialog.Filter = "Level file|*.lvl";
            openFileDialog.Title = "Open level";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                OpenFile(openFileDialog.OpenFile());
            }
        }
        public void OpenFile(Stream fileStream)
        {
            StreamReader reader = new StreamReader(fileStream);

            w = Int32.Parse(reader.ReadLine());
            h = Int32.Parse(reader.ReadLine());
            startX = Int32.Parse(reader.ReadLine());
            startY = Int32.Parse(reader.ReadLine());

            position = new int[w, h];
            type = new int[w, h];
            rail = new int[w, h];
            direction = new int[w, h];
            color = new int[w, h];
            number = new int[w, h];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    position[i, j] = Int32.Parse(reader.ReadLine());
                    type[i, j] = Int32.Parse(reader.ReadLine());
                    rail[i, j] = Int32.Parse(reader.ReadLine());
                    direction[i, j] = Int32.Parse(reader.ReadLine());
                    color[i, j] = Int32.Parse(reader.ReadLine());
                    number[i, j] = Int32.Parse(reader.ReadLine());
                }
            }
            reader.Close();

            ouroboros = new Segment[MaxLength];
            ResetOuroboros();
        }

        //  Check to see if all requirements are satisfied
        public bool Satisfied()
        {
            //  Check to see if head is eating tail
            Segment head = ouroboros[length - 1];
            if (length == 1 || head.x != startX || head.y != startY)
            {
                return false;
            }

            //  Cover alls - cover one and all must be covered
            Segment[,] grid = SegmentGrid();
            bool coverR, coverG, coverB;
            coverR = coverG = coverB = false;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (type[i, j] == ReqCover && grid[i, j] != null)
                    {
                        switch (color[i, j])
                        {
                            case ColorRed:      coverR = true; break;
                            case ColorGreen:    coverG = true; break;
                            case ColorBlue:     coverB = true; break;
                        }
                    }
                }
            }
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (coverR && 
                        type[i, j] == ReqCover && 
                        color[i, j] == ColorRed && 
                        grid[i, j] == null) return false;
                    if (coverG &&
                        type[i, j] == ReqCover &&
                        color[i, j] == ColorGreen &&
                        grid[i, j] == null) return false;
                    if (coverB &&
                        type[i, j] == ReqCover &&
                        color[i, j] == ColorBlue &&
                        grid[i, j] == null) return false;
                }
            }

            return true;
        }
    }
}
