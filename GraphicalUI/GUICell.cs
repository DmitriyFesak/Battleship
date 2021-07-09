using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GraphicalUI
{
    class GUICell
    {
        public Button button { get; private set; }
        public TypeOfShip placed { get; set; }

        public GUICell(Button button)
        {
            this.button = button;
            placed = TypeOfShip.None;
        }
    }
}
