using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public enum CellStatus : sbyte
    {
        Unshot = 0,
        Miss = -1,
        Hit = 1,
        UnshotShip = 2
    }

        

        
}
