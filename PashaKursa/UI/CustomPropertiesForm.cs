using System;
using System.Windows.Forms;
using PashaKursa.Properties;

namespace PashaKursa
{
    public partial class CustomPropertiesForm : Form
    {
        public CustomPropertiesForm()
        {
            InitializeComponent();
            Icon = Resources.mine;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                var width = Convert.ToInt32(txtLenght.Text);
                var height = Convert.ToInt32(txtHeight.Text);
                var mines = Convert.ToInt32(txtMines.Text);
                if (width <= 0 || height <= 0 || mines <= 0 || width * height < mines)
                    MessageBox.Show(@"Incorrect!", @"Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else 
                {
                    var field = new Field(Mode.Custom, width, height, mines);
                    var form = new MainForm(field.Width);
                    var game = new Game(field, form);
                    game.Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Incorrect!", @"Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
