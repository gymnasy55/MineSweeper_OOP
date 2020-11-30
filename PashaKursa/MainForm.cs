using System;
using System.Drawing;
using System.Windows.Forms;
using PashaKursa.Properties;

namespace PashaKursa
{
    public partial class MainForm : Form
    {
        private int time;
        private readonly Label timerCount;
        private readonly Label flagCountLabel;

        public MainForm(int fieldWidth)
        {
            InitializeComponent();
            this.Icon = Resources.mine;
            time = 0;
            var timer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            timer.Tick += Timer_Tick;

            timerCount = new Label
            {
                Left = fieldWidth * (Cell.Width + 2) + 3,
                Top = 5,
                Width = 35,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = Color.FromArgb(0, 0, 0),
                Text = time.ToString()
            };
            Controls.Add(timerCount);

            flagCountLabel = new Label
            {
                Left = fieldWidth * (Cell.Width + 2) + 3,
                Top = 50,
                Width = 35,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = Color.FromArgb(255, 0, 0),
                Text = ""
            };
            Controls.Add(flagCountLabel);
        }

        public void ChangeFlagCountLabel(string text)
        {
            flagCountLabel.Text = text;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time++;
            timerCount.Text = time.ToString();
        }
    }
}
