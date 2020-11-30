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
    }
}
