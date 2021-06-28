using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Shot
    {
        private int x;
        private int y;
        private String result;

        public Shot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetResult(String result)
        {
            this.result = result;
        }

        public String GetResult()
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
