using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Cell
    {
        public static int Width { get; set; } = 40;
        public static int Height { get; set; } = 40;
        public Button Button { get; set; }
        public int Value { get; set; }
        public bool IsMine { get; set; } = false;
        public bool IsChecked { get; set; } = false;

        public Cell(int x, int y)
        {
            Value = 0;
            Button = new Button
            {
                Left = x,
                Top = y,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = Width,
                Height = Height,
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
