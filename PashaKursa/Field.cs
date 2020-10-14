using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines { get; set; }
        public static int X { get; set; } = 2;
        public static int Y { get; set; } = 2;
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
            switch (mode)
            {
                case Mode.Custom:
                    this.Width = Width;
                    this.Height = Height;
                    this.Mines = Mines;

                    CreateField();
                    break;
                default:
                    new Field(mode);
                    break;
            }
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
                    cells[i, j].button.MouseClick += ButtonClick;
                    x += Cell.Width + 2;
                }
                y += Cell.Height + 2;
                x = Field.X;
            }
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
            
            //gfys
            if(e.Button == MouseButtons.Right)
            {

            }
            else
            {
                if(cells[position.X, position.Y].isMine)
                {
                    MessageBox.Show("YOU LOSE!", "LOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if(cells[position.X, position.Y].value == 0)
                    {
                        CheckEmptyCell(position.X, position.Y);
                        Relaxation();
                    }
                    else
                    {
                        cells[position.X, position.Y].button.Text = Convert.ToString(cells[position.X, position.Y].value);
                        cells[position.X, position.Y].button.ForeColor = Color.Navy;
                        cells[position.X, position.Y].button.Enabled = false;
                    }
                }
            }
            





            /*if (e.Button == MouseButtons.Right)
            {
                if (numflag <= 0)
                {
                    numflag = 0;
                    MessageBox.Show("You have run out of flags!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    numflag--;
                    label.Image = Image.FromFile("flag.png");
                    bool checkflag = CheckFlag(label);
                    if (checkflag == true)
                    {
                        numflag++;
                        label.Image = null;
                        label.Invalidate();
                    }
                    else
                    {
                        label.Image = Image.FromFile("flag.png");
                    }
                }
            }
            else
            {
                timer1.Enabled = true;
                label.BackColor = Color.LightSlateGray;
                int X = label.Left;
                int Y = label.Top;
                row = Convert.ToInt32(Math.Truncate(((Y - Data.Standart.Y) / Convert.ToDouble(Cell.Height))));
                col = Convert.ToInt32(Math.Truncate(((X - Data.Standart.X) / Convert.ToDouble(Cell.Width))));
                if (labels[row, col].value == 0)
                {
                    CheckEmptyCell(labels, row, col);
                }
                label.ForeColor = Color.Blue;
                label.Enabled = false;
                if (label.Text == "B")
                {
                    label.BackColor = Color.Red;
                    MessageBox.Show("YOU LOSE!", "LOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }*/
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
            cells[x, y].button.ForeColor = Color.Navy;
            cells[x, y].button.Enabled = false;
            cells[x, y].button.Text = "";
            //cells[x, y].button.BackColor = Color.LightSlateGray;
            if ((x > 0))
            {
                if ((cells[x - 1, y].value == 0) && (cells[x - 1, y].button.Enabled == true))
                {
                    CheckEmptyCell(x - 1, y);
                }
                else
                {
                    cells[x - 1, y].button.ForeColor = Color.Navy;
                    cells[x - 1, y].button.Enabled = false;
                    cells[x - 1, y].button.Text = Convert.ToString(cells[x - 1, y].value);
                    //cells[x - 1, y].button.BackColor = Color.LightSlateGray;
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
                    cells[x + 1, y].button.ForeColor = Color.Navy;
                    cells[x + 1, y].button.Enabled = false;
                    cells[x + 1, y].button.Text = Convert.ToString(cells[x + 1, y].value);
                    //cells[x + 1, y].button.BackColor = Color.LightSlateGray;
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
                    cells[x, y + 1].button.ForeColor = Color.Navy;
                    cells[x, y + 1].button.Enabled = false;
                    cells[x, y + 1].button.Text = Convert.ToString(cells[x, y + 1].value);
                    //cells[x, y + 1].button.BackColor = Color.LightSlateGray;
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
                    cells[x, y - 1].button.ForeColor = Color.Navy;
                    cells[x, y - 1].button.Enabled = false;
                    cells[x, y - 1].button.Text = Convert.ToString(cells[x, y - 1].value);
                    //cells[x, y - 1].button.BackColor = Color.LightSlateGray;
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
                    cells[x + 1, y + 1].button.ForeColor = Color.Navy;
                    cells[x + 1, y + 1].button.Enabled = false;
                    cells[x + 1, y + 1].button.Text = Convert.ToString(cells[x + 1, y + 1].value);
                    //cells[x + 1, y + 1].button.BackColor = Color.LightSlateGray;
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
                    cells[x - 1, y - 1].button.ForeColor = Color.Navy;
                    cells[x - 1, y - 1].button.Enabled = false;
                    cells[x - 1, y - 1].button.Text = Convert.ToString(cells[x - 1, y - 1].value);
                    //cells[x - 1, y - 1].button.BackColor = Color.LightSlateGray;
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
                    cells[x - 1, y + 1].button.ForeColor = Color.Navy;
                    cells[x - 1, y + 1].button.Enabled = false;
                    cells[x - 1, y + 1].button.Text = Convert.ToString(cells[x - 1, y + 1].value);
                    //cells[x - 1, y + 1].button.BackColor = Color.LightSlateGray;
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
                    cells[x + 1, y - 1].button.ForeColor = Color.Navy;
                    cells[x + 1, y - 1].button.Enabled = false;
                    cells[x + 1, y - 1].button.Text = Convert.ToString(cells[x + 1, y - 1].value);
                    //cells[x + 1, y - 1].button.BackColor = Color.LightSlateGray;
                }
            }
        }

        private void Relaxation()
        {
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    if (cells[i, j].button.Text == "0")
                        cells[i, j].button.Text = "";
        }
    }
}
