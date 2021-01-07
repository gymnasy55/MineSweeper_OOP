using System;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    public class Game
    {
        private readonly Field _field;
        private readonly MainForm _gameForm;

        public Game(Field field, MainForm gameForm)
        {
            _field = field;
            _gameForm = gameForm;
        }

        public void Start()
        {
            foreach (var cell in _field.Cells)
            {
                cell.Button.MouseDown += ButtonClick;
                _gameForm.Controls.Add(cell.Button);
            }

            _gameForm.ChangeFlagCountLabelText(_field.Mines.ToString());

            _gameForm.Width = _field.Width * (Cell.Width + 2) + 2 + 35 + 20;
            _gameForm.Height = _field.Height * (Cell.Height + 2) + 50;

            _gameForm.ShowDialog();
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            var position = FindCell(button);

            if (e.Button == MouseButtons.Right)
            {
                if (_field.Cells[position.Y, position.X].IsChecked)
                {
                    button.Image = null;
                    _field.Cells[position.Y, position.X].IsChecked = false;
                    _field.FlagCount--;
                }
                else
                {
                    if (_field.FlagCount < _field.Mines)
                    {
                        button.Image = Properties.Resources.flag;
                        _field.Cells[position.Y, position.X].IsChecked = true;
                        _field.FlagCount++;
                    }
                    else
                        MessageBox.Show(@"Out of flags", @"Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                _gameForm.ChangeFlagCountLabelText((_field.Mines - _field.FlagCount).ToString());
                CheckWin();
            }
            else if (!_field.Cells[position.Y, position.X].IsChecked)
            {
                if (_field.Cells[position.Y, position.X].IsMine)
                {
                    MessageBox.Show(@"YOU LOSE!", @"LOOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else
                    if (_field.Cells[position.Y, position.X].Value == 0)
                        _field.CheckEmptyCells(position.Y, position.X);
                    else
                        _field.ChangeCell(position.Y, position.X);
            }
        }

        public Point FindCell(Button button)
        {
            for (var i = 0; i < _field.Height; i++)
                for (var j = 0; j < _field.Width; j++)
                    if (_field.Cells[i, j].Button == button)
                        return new Point(j, i);
            return new Point(0, 0);
        }

        private void CheckWin()
        {
            var counter = 0; //кол-во клеток с минами под флажком

            foreach (var cell in _field.Cells)
                if (cell.IsChecked && cell.IsMine)
                    counter++;

            if (counter == _field.Mines)
            {
                MessageBox.Show("YOU WON!", "WINNER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
        }
    }
}