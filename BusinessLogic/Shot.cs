using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Shot
    {
        private int _x;
        private int _y;
        private ShotResult _result;
        public Shot(int y, int x)
        {
            _x = x;
            _y = y;
        }

        public void SetResult(ShotResult result)
        {
            _result = result;
        }

        public ShotResult GetResult()
        {
            return _result;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }
    }
}
