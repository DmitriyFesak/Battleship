using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public enum ShotResult : sbyte
    {
        Damaged = 0,
        Miss = -1,
        Destroyed = 1,
        AlreadyShot = 2
    }
}
