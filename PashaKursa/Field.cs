using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Field
    {
        private static int x { get; } = 2;
        private static int y { get; } = 2;
        public int Width { get; }
        public int Height { get; }
        public int Mines { get; }
        public Cell[,] Cells { get; }
        private List<Point> minespos { get; set; }
        public int FlagCount { get; set; } = 0;
        public Label FlagCountLabel { get; set; }


        public Field(Mode mode)
        {
            switch (mode)
            {
                case Mode.Easy:
                    this.Width = 9;
                    this.Height = 9;
                    this.Mines = 10;
                    break;
                case Mode.Medium:
                    this.Width = 16;
                    this.Height = 16;
                    this.Mines = 40;
                    break;
                case Mode.Hard:
                    this.Width = 30;
                    this.Height = 16;
                    this.Mines = 99;
                    break;
            }
            Cells = new Cell[this.Height, this.Width];
            CreateField();
        }

        public Field(Mode mode, int Width, int Height, int Mines)
        {
            this.Width = Width;
            this.Height = Height;
            this.Mines = Mines;
            Cells = new Cell[this.Height, this.Width];
            CreateField();
        }

        private void CreateField()
        {
            minespos = new List<Point>(this.Mines);
            int x = Field.x;
            int y = Field.x;
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Cells[i, j] = new Cell(x, y);
                    Cells[i, j].button.MouseDown += ButtonClick;
                    x += Cell.Width + 2;
                }
                y += Cell.Height + 2;
                x = Field.y;
            }
            FlagCountLabel = new Label()
            {
                Left = this.Width * (Cell.Width + 2) + 3,
                Top = 50,
                Width = 35,
                BorderStyle = BorderStyle.Fixed3D,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                ForeColor = Color.FromArgb(255, 0, 0),
                Text = (this.Mines - this.FlagCount).ToString()
            };
            TakeBombs();
            CheckBombs();
        }

        private void TakeBombs()
        {
            Random rnd = new Random();
            for (int i = 0; i < this.Mines; i++)
            {
                int rnd_X;
                int rnd_Y;
                do
                {
                    rnd_X = rnd.Next(0, this.Width);
                    rnd_Y = rnd.Next(0, this.Height);
                }
                while (Cells[rnd_Y, rnd_X].isMine);
                minespos.Add(new Point(rnd_X, rnd_Y));
                Cells[rnd_Y, rnd_X].isMine = true;
            }
        }

        private void CheckBombs()
        {
            for (int i = 0; i < this.Mines; i++)
            {
                int x = minespos[i].X;
                int y = minespos[i].Y;                
                
                if ((x > 0))
                {
                    Cells[y, x - 1].value++;
                }
                if ((x < this.Width - 1))
                {
                    Cells[y, x + 1].value++;
                }
                if ((y < this.Height - 1))
                {
                    Cells[y + 1, x].value++;
                }
                if ((y > 0))
                {
                    Cells[y - 1, x].value++;
                }
                if ((x < this.Width - 1) && (y < this.Height - 1))
                {
                    Cells[y + 1, x + 1].value++;
                }
                if ((x > 0) && (y > 0))
                {
                    Cells[y - 1, x - 1].value++;
                }
                if ((x > 0) && (y < this.Height - 1))
                {
                    Cells[y + 1, x - 1].value++;
                }
                if ((x < this.Width - 1) && (y > 0))
                {
                    Cells[y - 1, x + 1].value++;
                }
            }
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Point position = FindCell(button);
            if(e.Button == MouseButtons.Right)
            {
                if (!Cells[position.Y, position.X].isChecked)
                {
                    if (this.FlagCount < this.Mines)
                    {
                        button.Image = Properties.Resources.flag as Bitmap;
                        Cells[position.Y, position.X].isChecked = true;
                        this.FlagCount++;
                    }
                    else
                    {
                        MessageBox.Show("Out of flags", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    button.Image = null;
                    Cells[position.Y, position.X].isChecked = false;
                    this.FlagCount--;
                }

                FlagCountLabel.Text = (this.Mines - this.FlagCount).ToString();
                CheckWin();
            }
            else if (!Cells[position.Y, position.X].isChecked)
            {
                if (Cells[position.Y, position.X].isMine)
                {
                    MessageBox.Show($@"YOU LOSE!", @"LOOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else
                {
                    if (Cells[position.Y, position.X].value == 0)
                    {
                        CheckEmptyCell(position.Y, position.X);
                    }
                    else
                    {
                        ChangeCell(position.Y, position.X);
                    }
                }
            }
        }

        private Point FindCell(Button button)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (Cells[i, j].button == button)
                    {
                        return new Point(j, i);
                    }
                }
            }
            return new Point(0, 0);
        }

        private void CheckEmptyCell(int y, int x)
        {
            ChangeCell(y, x);

            if ((x > 0))
            {
                if ((Cells[y, x - 1].value == 0) && (Cells[y, x - 1].button.Enabled == true))
                {
                    CheckEmptyCell(y, x - 1);
                }
                else
                {
                    ChangeCell(y, x - 1);
                }
            }
            if ((x < this.Width - 1))
            {
                if ((Cells[y, x + 1].value == 0) && (Cells[y, x + 1].button.Enabled == true))
                {
                    CheckEmptyCell(y, x + 1);
                }
                else
                {
                    ChangeCell(y, x + 1);
                }
            }
            if ((y < this.Height - 1))
            {
                if ((Cells[y + 1, x].value == 0) && (Cells[y + 1, x].button.Enabled == true))
                {
                    CheckEmptyCell(y + 1, x);
                }
                else
                {
                    ChangeCell(y + 1, x);
                }
            }
            if ((y > 0))
            {
                if ((Cells[y - 1, x].value == 0) && (Cells[y - 1, x].button.Enabled == true))
                {
                    CheckEmptyCell(y - 1, x);
                }
                else
                {
                    ChangeCell(y - 1, x);
                }
            }
            if ((x < this.Width - 1) && (y < this.Height - 1))
            {
                if ((Cells[y + 1, x + 1].value == 0) && (Cells[y + 1, x + 1].button.Enabled == true))
                {
                    CheckEmptyCell(y + 1, x + 1);
                }
                else
                {
                    ChangeCell(y + 1, x + 1);
                }
            }
            if ((x > 0) && (y > 0))
            {
                if ((Cells[y - 1, x - 1].value == 0) && (Cells[y - 1, x - 1].button.Enabled == true))
                {
                    CheckEmptyCell(y - 1, x - 1);
                }
                else
                {
                    ChangeCell(y - 1, x - 1);
                }
            }
            if ((x > 0) && (y < this.Height - 1))
            {
                if ((Cells[y + 1, x - 1].value == 0) && (Cells[y + 1, x - 1].button.Enabled == true))
                {
                    CheckEmptyCell(y + 1, x - 1);
                }
                else
                {
                    ChangeCell(y + 1, x - 1);
                }
            }
            if ((x < this.Width - 1) && (y > 0))
            {
                if ((Cells[y - 1, x + 1].value == 0) && (Cells[y - 1, x + 1].button.Enabled == true))
                {
                    CheckEmptyCell(y - 1, x + 1);
                }
                else
                {
                    ChangeCell(y - 1, x + 1);
                }
            }
        }

        private void ChangeCell(int y, int x)
        {
            if (!Cells[y, x].isChecked)
            {
                Cells[y, x].button.ForeColor = Color.Navy;
                Cells[y, x].button.Enabled = false;
                if (Cells[y, x].value != 0)
                    Cells[y, x].button.Text = Convert.ToString(Cells[y, x].value);
                Cells[y, x].button.BackColor = Color.LightSlateGray;
            }
        }

        private void CheckWin()
        {
            int counter = 0;

            for (int i = 0; i < this.Height; i++)
                for (int j = 0; j < this.Width; j++)
                    if (Cells[i, j].isChecked && Cells[i, j].isMine) 
                        counter++;
            if (counter == this.Mines)
            {
                MessageBox.Show("YOU WON!", "WINNER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
                
        }
    }
}
