using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Field
    {
        public static int X { get; set; } = 2;
        public static int Y { get; set; } = 2;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines { get; set; }
        public Cell[,] cells { get; set; }
        public List<Point> minespos { get; set; }

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

            CreateField();
        }

        public Field(Mode mode, int Width, int Height, int Mines)
        {
            this.Width = Width;
            this.Height = Height;
            this.Mines = Mines;

            CreateField();
        }

        private void CreateField()
        {
            minespos = new List<Point>(this.Mines);
            int x = Field.X;
            int y = Field.Y;
            cells = new Cell[this.Width, this.Height];
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    cells[i, j] = new Cell(x, y);
                    cells[i, j].button.MouseDown += ButtonClick;
                    x += Cell.Width + 2;
                }
                y += Cell.Height + 2;
                x = Field.X;
            }

            Label flagCount = new Label
            {

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
                    rnd_X = rnd.Next(0, this.Width - 1);
                    rnd_Y = rnd.Next(0, this.Height - 1);
                }
                while (cells[rnd_X, rnd_Y].isMine);
                minespos.Add(new Point(rnd_X, rnd_Y));
                cells[rnd_X, rnd_Y].isMine = true;
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
                    cells[x - 1, y].value++;
                }
                if ((x < this.Width - 1))
                {
                    cells[x + 1, y].value++;
                }
                if ((y < this.Height - 1))
                {
                    cells[x, y + 1].value++;
                }
                if ((y > 0))
                {
                    cells[x, y - 1].value++;
                }
                if ((x < this.Width - 1) && (y < this.Height - 1))
                {
                    cells[x + 1, y + 1].value++;
                }
                if ((x > 0) && (y > 0))
                {
                    cells[x - 1, y - 1].value++;
                }
                if ((x > 0) && (y < this.Height - 1))
                {
                    cells[x - 1, y + 1].value++;
                }
                if ((x < this.Width - 1) && (y > 0))
                {
                    cells[x + 1, y - 1].value++;
                }
            }
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            Point position = FindCell(button);
            
            if(e.Button == MouseButtons.Right)
            {
                if (!cells[position.X, position.Y].isChecked)
                {
                    button.Image = Properties.Resources.flag as Bitmap;
                    cells[position.X, position.Y].isChecked = true;
                }
                else
                {
                    button.Image = null;
                    cells[position.X, position.Y].isChecked = false;
                }
                CheckWin();
            }
            else if (!cells[position.X, position.Y].isChecked)
            {
                if (cells[position.X, position.Y].isMine)
                {
                    MessageBox.Show("YOU LOSE!", "LOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else
                {
                    if (cells[position.X, position.Y].value == 0)
                    {
                        CheckEmptyCell(position.X, position.Y);
                    }
                    else
                    {
                        ChangeCell(position.X, position.Y);
                    }
                }
            }
        }

        private Point FindCell(Button button)
        {
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    if (cells[i, j].button == button)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return new Point(0, 0);
        }

        private void CheckEmptyCell(int x, int y)
        {
            ChangeCell(x, y);

            if ((x > 0))
            {
                if ((cells[x - 1, y].value == 0) && (cells[x - 1, y].button.Enabled == true))
                {
                    CheckEmptyCell(x - 1, y);
                }
                else
                {
                    ChangeCell(x - 1, y);
                }
            }
            if ((x < this.Width - 1))
            {
                if ((cells[x + 1, y].value == 0) && (cells[x + 1, y].button.Enabled == true))
                {
                    CheckEmptyCell(x + 1, y);
                }
                else
                {
                    ChangeCell(x + 1, y);
                }
            }
            if ((y < this.Height - 1))
            {
                if ((cells[x, y + 1].value == 0) && (cells[x, y + 1].button.Enabled == true))
                {
                    CheckEmptyCell(x, y + 1);
                }
                else
                {
                    ChangeCell(x, y + 1);
                }
            }
            if ((y > 0))
            {
                if ((cells[x, y - 1].value == 0) && (cells[x, y - 1].button.Enabled == true))
                {
                    CheckEmptyCell(x, y - 1);
                }
                else
                {
                    ChangeCell(x, y - 1);
                }
            }
            if ((x < this.Width - 1) && (y < this.Height - 1))
            {
                if ((cells[x + 1, y + 1].value == 0) && (cells[x + 1, y + 1].button.Enabled == true))
                {
                    CheckEmptyCell(x + 1, y + 1);
                }
                else
                {
                    ChangeCell(x + 1, y + 1);
                }
            }
            if ((x > 0) && (y > 0))
            {
                if ((cells[x - 1, y - 1].value == 0) && (cells[x - 1, y - 1].button.Enabled == true))
                {
                    CheckEmptyCell(x - 1, y - 1);
                }
                else
                {
                    ChangeCell(x - 1, y - 1);
                }
            }
            if ((x > 0) && (y < this.Height - 1))
            {
                if ((cells[x - 1, y + 1].value == 0) && (cells[x - 1, y + 1].button.Enabled == true))
                {
                    CheckEmptyCell(x - 1, y + 1);
                }
                else
                {
                    ChangeCell(x - 1, y + 1);
                }
            }
            if ((x < this.Width - 1) && (y > 0))
            {
                if ((cells[x + 1, y - 1].value == 0) && (cells[x + 1, y - 1].button.Enabled == true))
                {
                    CheckEmptyCell(x + 1, y - 1);
                }
                else
                {
                    ChangeCell(x + 1, y - 1);
                }
            }
        }

        private void ChangeCell(int x, int y)
        {
            if (!cells[x, y].isChecked)
            {
                cells[x, y].button.ForeColor = Color.Navy;
                cells[x, y].button.Enabled = false;
                if (cells[x, y].value != 0)
                    cells[x, y].button.Text = Convert.ToString(cells[x, y].value);
                cells[x, y].button.BackColor = Color.LightSlateGray;
            }
        }

        private void CheckWin()
        {
            int counter = 0;

            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    if (cells[i, j].isChecked && cells[i, j].isMine) 
                        counter++;
            if (counter == this.Mines)
            {
                MessageBox.Show("YOU WIN!!!", "WINNER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
                
        }
    }
}
