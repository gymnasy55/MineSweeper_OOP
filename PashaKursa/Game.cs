using System;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    class Game
    {
        private readonly Field field;
        private readonly MainForm gameForm;

        public Game(Field field, MainForm gameForm)
        {
            this.field = field;
            this.gameForm = gameForm;
        }

        public void Start()
        {
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    field.Cells[i, j].button.MouseDown += ButtonClick;

            foreach (var k in field.Cells)
                gameForm.Controls.Add(k.button);

            gameForm.ChangeFlagCountLabel((field.Mines - field.FlagCount).ToString());
            gameForm.Width = field.Width * (Cell.Width + 2) + 2 + 35 + 20;
            gameForm.Height = field.Height * (Cell.Height + 2) + 50;
            CheckBombs();
            gameForm.ShowDialog();
        }

        private void CheckBombs()
        {
            for (int i = 0; i < field.Mines; i++)
            {
                int x = field.Minespos[i].X;
                int y = field.Minespos[i].Y;

                if ((x > 0))
                {
                    field.Cells[y, x - 1].value++;
                }
                if ((x < field.Width - 1))
                {
                    field.Cells[y, x + 1].value++;
                }
                if ((y < field.Height - 1))
                {
                    field.Cells[y + 1, x].value++;
                }
                if ((y > 0))
                {
                    field.Cells[y - 1, x].value++;
                }
                if ((x < field.Width - 1) && (y < field.Height - 1))
                {
                    field.Cells[y + 1, x + 1].value++;
                }
                if ((x > 0) && (y > 0))
                {
                    field.Cells[y - 1, x - 1].value++;
                }
                if ((x > 0) && (y < field.Height - 1))
                {
                    field.Cells[y + 1, x - 1].value++;
                }
                if ((x < field.Width - 1) && (y > 0))
                {
                    field.Cells[y - 1, x + 1].value++;
                }
            }
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Point position = this.FindCell(button);
            if (e.Button == MouseButtons.Right)
            {
                if (!field.Cells[position.Y, position.X].isChecked)
                {
                    if (field.FlagCount < field.Mines)
                    {
                        button.Image = Properties.Resources.flag as Bitmap;
                        field.Cells[position.Y, position.X].isChecked = true;
                        field.FlagCount++;
                    }
                    else
                    {
                        MessageBox.Show("Out of flags", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    button.Image = null;
                    field.Cells[position.Y, position.X].isChecked = false;
                    field.FlagCount--;
                }
                gameForm.ChangeFlagCountLabel((field.Mines - field.FlagCount).ToString());
                CheckWin();
            }
            else if (!field.Cells[position.Y, position.X].isChecked)
            {
                if (field.Cells[position.Y, position.X].isMine)
                {
                    MessageBox.Show($@"YOU LOSE!", @"LOOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else
                {
                    if (field.Cells[position.Y, position.X].value == 0)
                        CheckEmptyCell(position.Y, position.X);
                    else
                        ChangeCell(position.Y, position.X);
                }
            }
        }

        public Point FindCell(Button button)
        {
            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width; j++)
                {
                    if (field.Cells[i, j].button == button)
                    {
                        return new Point(j, i);
                    }
                }
            }
            return new Point(0, 0);
        }

        public void CheckEmptyCell(int y, int x)
        {
            ChangeCell(y, x);

            if ((x > 0))
            {
                if ((field.Cells[y, x - 1].value == 0) && (field.Cells[y, x - 1].button.Enabled == true))
                    CheckEmptyCell(y, x - 1);
                else
                    ChangeCell(y, x - 1);
            }
            if ((x < field.Width - 1))
            {
                if ((field.Cells[y, x + 1].value == 0) && (field.Cells[y, x + 1].button.Enabled == true))
                    CheckEmptyCell(y, x + 1);
                else
                    ChangeCell(y, x + 1);
            }
            if ((y < field.Height - 1))
            {
                if ((field.Cells[y + 1, x].value == 0) && (field.Cells[y + 1, x].button.Enabled == true))
                    CheckEmptyCell(y + 1, x);
                else
                    ChangeCell(y + 1, x);
            }
            if ((y > 0))
            {
                if ((field.Cells[y - 1, x].value == 0) && (field.Cells[y - 1, x].button.Enabled == true))
                    CheckEmptyCell(y - 1, x);
                else
                    ChangeCell(y - 1, x);
            }
            if ((x < field.Width - 1) && (y < field.Height - 1))
            {
                if ((field.Cells[y + 1, x + 1].value == 0) && (field.Cells[y + 1, x + 1].button.Enabled == true))
                    CheckEmptyCell(y + 1, x + 1);
                else
                    ChangeCell(y + 1, x + 1);
            }
            if ((x > 0) && (y > 0))
            {
                if ((field.Cells[y - 1, x - 1].value == 0) && (field.Cells[y - 1, x - 1].button.Enabled == true))
                    CheckEmptyCell(y - 1, x - 1);
                else
                    ChangeCell(y - 1, x - 1);
            }
            if ((x > 0) && (y < field.Height - 1))
            {
                if ((field.Cells[y + 1, x - 1].value == 0) && (field.Cells[y + 1, x - 1].button.Enabled == true))
                    CheckEmptyCell(y + 1, x - 1);
                else
                    ChangeCell(y + 1, x - 1);
            }
            if ((x < field.Width - 1) && (y > 0))
            {
                if ((field.Cells[y - 1, x + 1].value == 0) && (field.Cells[y - 1, x + 1].button.Enabled == true))
                    CheckEmptyCell(y - 1, x + 1);
                else
                    ChangeCell(y - 1, x + 1);
            }
        }

        public void ChangeCell(int y, int x)
        {
            if (!field.Cells[y, x].isChecked)
            {
                field.Cells[y, x].button.ForeColor = Color.Navy;
                field.Cells[y, x].button.Enabled = false;
                if (field.Cells[y, x].value != 0)
                    field.Cells[y, x].button.Text = Convert.ToString(field.Cells[y, x].value);
                field.Cells[y, x].button.BackColor = Color.LightSlateGray;
            }
        }

        private void CheckWin()
        {
            int counter = 0;
            for (int i = 0; i < field.Height; i++)
                for (int j = 0; j < field.Width; j++)
                    if (field.Cells[i, j].isChecked && field.Cells[i, j].isMine)
                        counter++;
            if (counter == field.Mines)
            {
                MessageBox.Show("YOU WON!", "WINNER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
        }
    }
}