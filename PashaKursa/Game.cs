using System;
using System.Drawing;
using System.Windows.Forms;

namespace PashaKursa
{
    class Game
    {
        private readonly Field _field;
        private readonly MainForm _gameForm;

        public Game(Field field, MainForm gameForm)
        {
            this._field = field;
            this._gameForm = gameForm;
        }

        public void Start()
        {
            for (int i = 0; i < _field.Height; i++)
                for (int j = 0; j < _field.Width; j++)
                    _field.Cells[i, j].button.MouseDown += ButtonClick;

            foreach (var k in _field.Cells)
                _gameForm.Controls.Add(k.button);

            _gameForm.ChangeFlagCountLabel((_field.Mines - _field.FlagCount).ToString());
            _gameForm.Width = _field.Width * (Cell.Width + 2) + 2 + 35 + 20;
            _gameForm.Height = _field.Height * (Cell.Height + 2) + 50;
            _field.CheckBombs();
            _gameForm.ShowDialog();
        }

        private void ButtonClick(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            var position = this.FindCell(button);
            if (e.Button == MouseButtons.Right)
            {
                if (!_field.Cells[position.Y, position.X].isChecked)
                {
                    if (_field.FlagCount < _field.Mines)
                    {
                        button.Image = Properties.Resources.flag as Bitmap;
                        _field.Cells[position.Y, position.X].isChecked = true;
                        _field.FlagCount++;
                    }
                    else
                        MessageBox.Show(@"Out of flags", @"Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    button.Image = null;
                    _field.Cells[position.Y, position.X].isChecked = false;
                    _field.FlagCount--;
                }
                _gameForm.ChangeFlagCountLabel((_field.Mines - _field.FlagCount).ToString());
                CheckWin();
            }
            else if (!_field.Cells[position.Y, position.X].isChecked)
            {
                if (_field.Cells[position.Y, position.X].isMine)
                {
                    MessageBox.Show(@"YOU LOSE!", @"LOOSER!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else
                    if (_field.Cells[position.Y, position.X].value == 0)
                        _field.CheckEmptyCell(position.Y, position.X);
                    else
                        _field.ChangeCell(position.Y, position.X);
            }
        }

        public Point FindCell(Button button)
        {
            for (int i = 0; i < _field.Height; i++)
                for (int j = 0; j < _field.Width; j++)
                        if (_field.Cells[i, j].button == button)
                            return new Point(j, i);
            return new Point(0, 0);
        }

        private void CheckWin()
        {
            int counter = 0;
            for (int i = 0; i < _field.Height; i++)
                for (int j = 0; j < _field.Width; j++)
                    if (_field.Cells[i, j].isChecked && _field.Cells[i, j].isMine)
                        counter++;
            if (counter == _field.Mines)
            {
                MessageBox.Show("YOU WON!", "WINNER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
        }
    }
}