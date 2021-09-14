using System;
using System.Collections.Generic;
using System.Text;

namespace Ouroboros
{
    public class Segment
    {
        public int x, y, direction;
        public Segment()
        {
            x = y = direction = 0;
        }
        public Segment(int x, int y, int direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    }
}
