using System;
using System.Drawing;
using System.Windows.Forms;
using PashaKursa.Properties;

namespace PashaKursa
{
    public partial class MainForm : Form
    {
        private int _time;
        private readonly Label _timerCountLabel;
        private readonly Label _flagCountLabel;

        public MainForm(int fieldWidth)
        {
            InitializeComponent();
            Icon = Resources.mine;
            _time = 0;
            var timer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            timer.Tick += Timer_Tick;

            _timerCountLabel = new Label
            {
                Left = fieldWidth * (Cell.Width + 2) + 3,
                Top = 5,
                Width = 35,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = Color.FromArgb(0, 0, 0),
                Text = _time.ToString()
            };
            Controls.Add(_timerCountLabel);

            _flagCountLabel = new Label
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
            Controls.Add(_flagCountLabel);
        }

        public void ChangeFlagCountLabel(string text)
        {
            _flagCountLabel.Text = text;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _time++;
            _timerCountLabel.Text = _time.ToString();
        }
    }
}
