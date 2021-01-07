using System;
using System.Windows.Forms;
using PashaKursa.Properties;

namespace PashaKursa
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            Icon = Resources.mine;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var mode = Mode.Custom;
            if (btn.Name == "btnEasy")
                mode = Mode.Easy;
            else if (btn.Name == "btnMedium")
                mode = Mode.Medium;
            else if (btn.Name == "btnHard") mode = Mode.Hard;

            var field = new Field(mode);
            var form = new MainForm(field.Width);
            var game = new Game(field, form);
            game.Start();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            var customPropertiesForm = new CustomPropertiesForm();
            Hide();
            customPropertiesForm.ShowDialog();
            Application.Exit();
        }
    }
}
