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
        Field field;
        public MainForm(Field field)
        {
            InitializeComponent();
            this.field = field;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Width = field.Width * (Cell.Width + 8);
            this.Height = field.Height * (Cell.Height + 8);
            foreach (var k in field.cells)
            {
                this.Controls.Add(k.button);
            }
        }
    }
}
