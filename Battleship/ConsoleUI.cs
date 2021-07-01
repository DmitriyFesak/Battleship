using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class ConsoleUI : IUserInterface
    {
        private static ConsoleUI consoleUI = new ConsoleUI();

        public static void Main()
        {
            Game game = new Game(consoleUI);
            int choice = consoleUI.ShowPreStartUI();
        }

        public int ShowPreStartUI()
        {
            Console.WriteLine("Wellcome to battleship!");
            int choice = 0;
            bool success = false;

            do
            {
                Console.WriteLine("Input 1 to change your name;");
                Console.WriteLine("Input 2 to change enemy's name;");
                Console.WriteLine("Input 3 to place ships manually;");
                Console.WriteLine("Input 4 to place ships randomly;");
                Console.WriteLine("Input 0 to exit;");
                success = Int32.TryParse(Console.ReadLine(), out choice);
                if (!success)
                {
                    choice = -1;
                }
            } while (!success || choice < 0 || choice > 4);

            return choice;
        }
        public void AutoLocation()
        {
            throw new NotImplementedException();
        }

        public void MakeMove()
        {

        }

        public void ManualLocation()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerName()
        {
            throw new NotImplementedException();
        }

        public void UpdateBattlefield()
        {
            throw new NotImplementedException();
        }
    }
}
