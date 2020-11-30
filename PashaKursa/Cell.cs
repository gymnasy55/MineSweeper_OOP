using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Cell
    {
        public static int Width { get; set; } = 40;
        public static int Height { get; set; } = 40;
        public Button button { get; set; }
        public int value { get; set; }
        public bool isMine { get; set; } = false;
        public bool isChecked { get; set; } = false;

        public Cell(int X, int Y)
        {
            value = 0;
            button = new Button
            {
                Left = X,
                Top = Y,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = Cell.Width,
                Height = Cell.Height,
                Image = null,
                FlatStyle = FlatStyle.Flat,
                AutoSize = false,
                Font = new Font("Arial", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204))),
                BackColor = Color.LightGray,
                ForeColor = Color.LightGray,
                Text = ""
            };
        }

    }
}
