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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Mode mode = Mode.Custom;
            switch(btn.Name)
            {
                case "btnEasy":
                    mode = Mode.Easy;
                    break;
                case "btnMedium":
                    mode = Mode.Medium;
                    break;
                case "btnHard":
                    mode = Mode.Hard;
                    break;
                default:
                    break;
            }
            Field field = new Field(mode);
            MainForm mainForm = new MainForm(field);
            Program.OpenForm(this, mainForm);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            CustomPropertiesForm customPropertiesForm = new CustomPropertiesForm();
            Program.OpenForm(this, customPropertiesForm);
        }
    }
}
