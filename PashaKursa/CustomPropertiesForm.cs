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
            this.Icon = Resources.mine;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int width = Convert.ToInt32(txtLenght.Text);
                int height = Convert.ToInt32(txtHeight.Text);
                int Mines = Convert.ToInt32(txtMines.Text);
                if (width <= 0 || height <= 0 || Mines <= 0 || (width * height < Mines)) 
                    MessageBox.Show(@"Incorrect!", @"Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else 
                {
                    Field field = new Field(Mode.Custom, width, height, Mines);
                    Game game = new Game(field, new MainForm(field.Width));
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
