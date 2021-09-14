using System;
using System.Collections.Generic;
using System.Text;

namespace Ouroboros
{
    public static class Constants
    {
        //  One or more heads exist on a grid (for now just one)
        //  A head can only move onto an unoccupied rail
        //  Clicking on an occupied pad moves the head back to that position

        //  0,0 1,0 2,0 3,0 4,0
        //  0,1 1,1 2,1 3,1 4,1
        //  0,2 1,2 2,2 3,2 4,2
        //  0,3 1,3 2,3 3,3 4,3
        //  0,4 1,4 2,4 3,4 4,4

        //  Pads at (e, e) positions
        //  Rails at (e, o) or (o, e) positions
        //  Space at (o, o) positions

        /*  Observations and ideas
         *  
         *  Obs: Requirements placed on the rails may have a linear component, bounded requirements may not
         *  Obs: 1-D requirements and 2-D requirements
         *  Obs: Select 1-D requirements by passing over them
         *  Obs: Select 2-D requirements by surrounding them
         *  
         *  
         *  
         *  Ide: Linear requirements of the form all after/all before
         *  Ide: Requirement requirements
         *  
         *  
         *  
         *  Req: Must be alone
         *  Req: Palindrome linear requirement (corresponding pair/overall structure)
         *  Req: Symmetrical, rectangular or square requirements (same for 1-D and 2-D)
         *  Req: Pair (go over or surround one, have to go over surround one)
         *  Req: All (go over or surround one, have to go over surround all)
         *  Req: Line must be CW/CCW
         *  Req: Must have +n area/perimeter/corners
         *  Req: Must pass over/by in direction(s)
         *  Req: Number of regions surrounded
         *  
         * 
         *  Mod: Ignore next/after/all/before
         *  Mod: Not next?
         *  Mod: Or next
         *  
         *  
         *  
         *  Good ideas
         *  
         *  Req: If you cover one, you must cover all
         *  Req: If you surround one, you must surround all (ooes not work in isolation)
         *  
         *  Regular vs pointed polygons (pointed must be alone in selection, regular must be together)
         *  
         *  
         */

        public const int TypeEmpty          = 0;

        public const int OuroborosTail      = 1;
        public const int OuroborosBody      = 2;
        public const int OuroborosHead      = 3;

        public const int InputModePlay      = 0;
        public const int InputModeEdit      = 1;

        public const int PositionSpace      = 0;
        public const int PositionRailX      = 1;
        public const int PositionRailY      = 2;
        public const int PositionRailXY     = 3;

        public const int DotCount           = 9;

        public const int MaxLength          = 100;

        public const int MovementFrames     = 12;
        public const int WinFrames          = 32;


        //  Rail Types

        public const int ReqCover           = 1;    //  Requires that the ouroboros pass over a color
        public const int ReqPair            = 2;
        //public const int ReqGroup           = 2;    //  Requires that the ouroboros group all instances of a color together
        //public const int ReqCW          = 0;    //  Requires that the ouroboros have a clockwise rotation
        //public const int ReqCCW         = 0;    //  Requires that the ouroboros have a counter-clockwise rotation
        //public const int ReqEdges       = 0;    //  Requires that the ouroboros have a certain number of edges (length)
        //public const int ReqVertices    = 0;    //  Requires that the ouroboros have a certain number of vertices (turns)
        //public const int ReqDirection   = 0;    //  Requires that the ouroboros cross the requirement in the indicated direction
        //public const int ReqSymX        = 0;    //  Requires that the ouroboros be horizontally symetrical
        //public const int ReqSymY        = 0;    //  Requires that the ouroboros be vertically symetrical

        public const int ModIgnoreNext      = 3;    //  Disregard the next requirement encountered
        //public const int ModNotNext     = 0;    //  Make sure the following requirement is not satisfied
        //public const int ModDisplace    = 0;    //  Questionable
        //public const int ModOr          = 0;    //  Satisfy either requirement before or after the or (implicit logical 'and' otherwise)
        //public const int ModOnly        = 0;    //  Must only satisfy the requirements covered (only makes sense after all conditions introduced)

        public const int RailCount          = 4;

        //  Space Types

        public const int SpaceStar          = 1;

        public const int SpaceCount         = 2;

        //  Attributes

        public const int DirectionNone  = 0b00000000;
        public const int DirectionE     = 0b00000001;
        public const int DirectionSE    = 0b00000010;
        public const int DirectionS     = 0b00000100;
        public const int DirectionSW    = 0b00001000;
        public const int DirectionW     = 0b00010000;
        public const int DirectionNW    = 0b00100000;
        public const int DirectionN     = 0b01000000;
        public const int DirectionNE    = 0b10000000;

        public const int ColorNone      = 0;
        public const int ColorRed       = 1;
        public const int ColorGreen     = 2;
        public const int ColorBlue      = 3;

        public const int ColorCount     = 4;
    }
}
