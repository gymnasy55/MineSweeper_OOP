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
    public partial class CustomPropertiesForm : Form
    {
        public CustomPropertiesForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int Width = Convert.ToInt32(txtLenght.Text);
                int Height = Convert.ToInt32(txtHeight.Text);
                int Mines = Convert.ToInt32(txtMines.Text);
                if (Width <= 0 || Height <= 0 || Mines <= 0 || (Width * Height < Mines)) { MessageBox.Show("Incorrect!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else 
                {
                    Field field = new Field(Mode.Custom, Width, Height, Mines);
                    MainForm mainForm = new MainForm(field);
                    Program.OpenForm(this, mainForm);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
