using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PashaKursa
{
    public partial class MainForm : Form
    {
        private readonly Field field;
        private int time;
        private Label timerCount;

        public MainForm(Field field)
        {
            InitializeComponent();
            this.field = field;
            time = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var k in field.Cells)
            {
                this.Controls.Add(k.button);
            }
            this.Controls.Add(field.FlagCountLabel);
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            timerCount = new Label
            {
                Left = field.Width * (Cell.Width + 2) + 3,
                Top = 5,
                Width = 35,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                ForeColor = Color.FromArgb(0, 0, 0),
                Text = time.ToString()
            };
            this.Controls.Add(timerCount);
            this.Width = (field.Width * (Cell.Width + 2) + 2) + timerCount.Width + 20;
            this.Height = field.Height * (Cell.Height + 2) + 50;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            time++;
            timerCount.Text = time.ToString();
        }
    }
}
