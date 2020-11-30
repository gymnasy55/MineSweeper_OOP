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
            this.Icon = Resources.mine;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Mode mode = Mode.Custom;
            if (btn.Name == "btnEasy")
                mode = Mode.Easy;
            else if (btn.Name == "btnMedium")
                mode = Mode.Medium;
            else if (btn.Name == "btnHard") mode = Mode.Hard;

            Field field = new Field(mode);
            Game game = new Game(field, new MainForm(field.Width));
            game.Start();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            CustomPropertiesForm customPropertiesForm = new CustomPropertiesForm();
            this.Hide();
            customPropertiesForm.ShowDialog();
            Application.Exit();
        }
    }
}
