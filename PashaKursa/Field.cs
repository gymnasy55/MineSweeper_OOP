using System;
using System.Collections.Generic;
using System.Drawing;

namespace PashaKursa
{
    public class Field
    {
        private static int X { get; } = 2;
        private static int Y { get; } = 2;
        public List<Point> Minespos { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int Mines { get; }
        public Cell[,] Cells { get; }
        public int FlagCount { get; set; } = 0;

        public Field(Mode mode)
        {
            switch (mode)
            {
                case Mode.Easy:
                    Width = 9;
                    Height = 9;
                    Mines = 10;
                    break;
                case Mode.Medium:
                    Width = 16;
                    Height = 16;
                    Mines = 40;
                    break;
                case Mode.Hard:
                    Width = 30;
                    Height = 16;
                    Mines = 99;
                    break;
            }
            Cells = new Cell[Height, Width];
            CreateField();
        }

        public Field(Mode mode, int width, int height, int mines)
        {
            Width = width;
            Height = height;
            Mines = mines;
            Cells = new Cell[Height, Width];
            CreateField();
        }

        private void CreateField()
        {
            Minespos = new List<Point>(Mines);
            int x = X;
            int y = X;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Cells[i, j] = new Cell(x, y);
                    x += Cell.Width + 2;
                }

                y += Cell.Height + 2;
                x = Y;
            }
            TakeBombs();
        }

        private void TakeBombs()
        {
            Random rnd = new Random();
            for (int i = 0; i < Mines; i++)
            {
                int rnd_X;
                int rnd_Y;
                do
                {
                    rnd_X = rnd.Next(0, Width);
                    rnd_Y = rnd.Next(0, Height);
                }
                while (Cells[rnd_Y, rnd_X].isMine);
                Minespos.Add(new Point(rnd_X, rnd_Y));
                Cells[rnd_Y, rnd_X].isMine = true;
            }
        }

        public void CheckBombs()
        {
            for (int i = 0; i < this.Mines; i++)
            {
                int x = this.Minespos[i].X;
                int y = this.Minespos[i].Y;

                if ((x > 0))
                    this.Cells[y, x - 1].value++;
                if ((x < this.Width - 1))
                    this.Cells[y, x + 1].value++;
                if ((y < this.Height - 1))
                    this.Cells[y + 1, x].value++;
                if ((y > 0))
                    this.Cells[y - 1, x].value++;
                if ((x < this.Width - 1) && (y < this.Height - 1))
                    this.Cells[y + 1, x + 1].value++;
                if ((x > 0) && (y > 0))
                    this.Cells[y - 1, x - 1].value++;
                if ((x > 0) && (y < this.Height - 1))
                    this.Cells[y + 1, x - 1].value++;
                if ((x < this.Width - 1) && (y > 0))
                    this.Cells[y - 1, x + 1].value++;
            }
        }
        public void CheckEmptyCell(int y, int x)
        {
            ChangeCell(y, x);

            if ((x > 0))
                if ((this.Cells[y, x - 1].value == 0) && (this.Cells[y, x - 1].button.Enabled == true))
                    CheckEmptyCell(y, x - 1);
                else
                    ChangeCell(y, x - 1);
            if ((x < this.Width - 1))
                if ((this.Cells[y, x + 1].value == 0) && (this.Cells[y, x + 1].button.Enabled == true))
                    CheckEmptyCell(y, x + 1);
                else
                    ChangeCell(y, x + 1);
            if ((y < this.Height - 1))
                if ((this.Cells[y + 1, x].value == 0) && (this.Cells[y + 1, x].button.Enabled == true))
                    CheckEmptyCell(y + 1, x);
                else
                    ChangeCell(y + 1, x);
            if ((y > 0))
                if ((this.Cells[y - 1, x].value == 0) && (this.Cells[y - 1, x].button.Enabled == true))
                    CheckEmptyCell(y - 1, x);
                else
                    ChangeCell(y - 1, x);
            if ((x < this.Width - 1) && (y < this.Height - 1))
                if ((this.Cells[y + 1, x + 1].value == 0) && (this.Cells[y + 1, x + 1].button.Enabled == true))
                    CheckEmptyCell(y + 1, x + 1);
                else
                    ChangeCell(y + 1, x + 1);
            if ((x > 0) && (y > 0))
                if ((this.Cells[y - 1, x - 1].value == 0) && (this.Cells[y - 1, x - 1].button.Enabled == true))
                    CheckEmptyCell(y - 1, x - 1);
                else
                    ChangeCell(y - 1, x - 1);
            if ((x > 0) && (y < this.Height - 1))
                if ((this.Cells[y + 1, x - 1].value == 0) && (this.Cells[y + 1, x - 1].button.Enabled == true))
                    CheckEmptyCell(y + 1, x - 1);
                else
                    ChangeCell(y + 1, x - 1);
            if ((x < this.Width - 1) && (y > 0))
                if ((this.Cells[y - 1, x + 1].value == 0) && (this.Cells[y - 1, x + 1].button.Enabled == true))
                    CheckEmptyCell(y - 1, x + 1);
                else
                    ChangeCell(y - 1, x + 1);
        }

        public void ChangeCell(int y, int x)
        {
            if (!this.Cells[y, x].isChecked)
            {
                this.Cells[y, x].button.ForeColor = Color.Navy;
                this.Cells[y, x].button.Enabled = false;
                if (this.Cells[y, x].value != 0)
                    this.Cells[y, x].button.Text = Convert.ToString(this.Cells[y, x].value);
                this.Cells[y, x].button.BackColor = Color.LightSlateGray;
            }
        }


    }
}
