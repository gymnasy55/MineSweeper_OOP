using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
                Left = Y,
                Top = X,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = Cell.Width,
                Height = Cell.Height,
                Image = null,
                FlatStyle = FlatStyle.Flat,
                //BorderStyle = BorderStyle.FixedSingle,
                AutoSize = false,
                Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204))),
                BackColor = Color.LightGray,
                ForeColor = Color.LightGray,
                Text = ""
            };
            //.MouseEnter += new EventHandler(LabelEnter);
            //label.MouseLeave += new EventHandler(LabelLeave);
        }

        private void LabelEnter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BorderStyle = BorderStyle.Fixed3D;
        }

        private void LabelLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
