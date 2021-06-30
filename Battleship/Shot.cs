using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Shot
    {
        private int x;
        private int y;
        private int result;
        public Shot(int y, int x)
        {
            this.x = x;
            this.y = y;
        }

        public void SetResult(int result)
        {
            this.result = result;
        }

        public int GetResult()
        {
            return result;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}
