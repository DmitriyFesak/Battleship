using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    interface IUserInterface
    {
        // TODO: input output parameters
        public void MakeMove();
        public void UpdateBattlefield();
        public void SetPlayerName();
        public void ManualLocation();
        public void AutoLocation();
    }
}
