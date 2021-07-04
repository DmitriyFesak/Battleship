using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    interface IUserInterface
    {
        // TODO: input output parameters
        public ShotResult MakeMove(Game game);
        public void UpdateBattlefield(Game game);
        public void PrintResult(Shot shot, String name);
        public void ShowAlreadyShotMsg();
        public void SetPlayerName(int playerId, Game game);
        public void ManualLocation();
        public void AutoLocation(Game game);
        public string StartGame(Game game);
    }
}
