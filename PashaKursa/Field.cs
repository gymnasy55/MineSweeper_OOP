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
            var x = X;
            var y = Y;
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    Cells[i, j] = new Cell(x, y);
                    x += Cell.Width + 2;
                }

                y += Cell.Height + 2;
                x = X;
            }
            TakeBombs();
        }

        private void TakeBombs()
        {
            var rnd = new Random();
            for (var i = 0; i < Mines; i++)
            {
                int rndX;
                int rndY;
                do
                {
                    rndX = rnd.Next(0, Width);
                    rndY = rnd.Next(0, Height);
                }
                while (Cells[rndY, rndX].IsMine);
                Minespos.Add(new Point(rndX, rndY));
                Cells[rndY, rndX].IsMine = true;
            }
        }

        public void CheckBombs()
        {
            for (var i = 0; i < Mines; i++)
            {
                var x = Minespos[i].X;
                var y = Minespos[i].Y;

                if (x > 0)
                    Cells[y, x - 1].Value++;
                if (x < Width - 1)
                    Cells[y, x + 1].Value++;
                if (y < Height - 1)
                    Cells[y + 1, x].Value++;
                if (y > 0)
                    Cells[y - 1, x].Value++;
                if (x < Width - 1 && y < Height - 1)
                    Cells[y + 1, x + 1].Value++;
                if (x > 0 && y > 0)
                    Cells[y - 1, x - 1].Value++;
                if (x > 0 && y < Height - 1)
                    Cells[y + 1, x - 1].Value++;
                if (x < Width - 1 && y > 0)
                    Cells[y - 1, x + 1].Value++;
            }
        }
        public void CheckEmptyCell(int y, int x)
        {
            ChangeCell(y, x);

            if (x > 0)
                if (Cells[y, x - 1].Value == 0 && Cells[y, x - 1].Button.Enabled)
                    CheckEmptyCell(y, x - 1);
                else
                    ChangeCell(y, x - 1);
            if (x < Width - 1)
                if (Cells[y, x + 1].Value == 0 && Cells[y, x + 1].Button.Enabled)
                    CheckEmptyCell(y, x + 1);
                else
                    ChangeCell(y, x + 1);
            if (y < Height - 1)
                if (Cells[y + 1, x].Value == 0 && Cells[y + 1, x].Button.Enabled)
                    CheckEmptyCell(y + 1, x);
                else
                    ChangeCell(y + 1, x);
            if (y > 0)
                if (Cells[y - 1, x].Value == 0 && Cells[y - 1, x].Button.Enabled)
                    CheckEmptyCell(y - 1, x);
                else
                    ChangeCell(y - 1, x);
            if (x < Width - 1 && y < Height - 1)
                if (Cells[y + 1, x + 1].Value == 0 && Cells[y + 1, x + 1].Button.Enabled)
                    CheckEmptyCell(y + 1, x + 1);
                else
                    ChangeCell(y + 1, x + 1);
            if (x > 0 && y > 0)
                if (Cells[y - 1, x - 1].Value == 0 && Cells[y - 1, x - 1].Button.Enabled)
                    CheckEmptyCell(y - 1, x - 1);
                else
                    ChangeCell(y - 1, x - 1);
            if (x > 0 && y < Height - 1)
                if (Cells[y + 1, x - 1].Value == 0 && Cells[y + 1, x - 1].Button.Enabled)
                    CheckEmptyCell(y + 1, x - 1);
                else
                    ChangeCell(y + 1, x - 1);
            if (x < Width - 1 && y > 0)
                if (Cells[y - 1, x + 1].Value == 0 && Cells[y - 1, x + 1].Button.Enabled)
                    CheckEmptyCell(y - 1, x + 1);
                else
                    ChangeCell(y - 1, x + 1);
        }

        public void ChangeCell(int y, int x)
        {
            if (!Cells[y, x].IsChecked)
            {
                Cells[y, x].Button.ForeColor = Color.Navy;
                Cells[y, x].Button.Enabled = false;
                if (Cells[y, x].Value != 0)
                    Cells[y, x].Button.Text = Convert.ToString(Cells[y, x].Value);
                Cells[y, x].Button.BackColor = Color.LightSlateGray;
            }
        }
    }
}
