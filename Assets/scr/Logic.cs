using System;
using UnityEngine;

namespace puzzle15
{
    class Logic : ILogic
    {
        //private const int _DEFAULTSIZE = 4;
        public IChip[,] Field
        {
            get { return _field; }
        }

        public IChip EmptyCell
        {
            get { return _emptyCell; }
        }

        private IChip _emptyCell;
        private IChip _buffer;
        private IChip[,] _field;
        private IChip[,] _etalonField;
        private int _sizeX;
        private int _sizeY;

        public void InitField(int size)
        {
            InitField(size, size);
        }

        public void InitField(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _field = new IChip[_sizeY, _sizeX];
            
            int value = 0;
            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    IChip newChep = new Chip(x, y);
                    newChep.Value = (++value != _field.Length) ? value : 0;
                    _field[y, x] = newChep;
                }
            }

            _emptyCell = new Chip(sizeX-1, sizeY-1);
            _InitEtalonField(_field);
        }

        public void SetCustomField(IChip[,] field)
        {
            _field = field;
            _InitEtalonField(field);
        }

        public void ResetField()
        {
            for (int y = 0; y < _sizeY; y++)
                for (int x = 0; x < _sizeX; x++)
                    _field[y, x] = _etalonField[y, x].CreateClone();

            _emptyCell = _field[_sizeY - 1, _sizeX - 1];
        }

        public bool MoveChip(IChip chipCoord)
        {
            if (chipCoord.Value == 0)
                return false;

            //Проверка находятся ли координаты в одной строке или столбце с пустой ячейкой
            if (chipCoord.PosX != _emptyCell.PosX
                && chipCoord.PosY != _emptyCell.PosY)
                return false;

            //Проверка является ли пустая ячейка соседней
            if (Math.Abs(chipCoord.PosX - _emptyCell.PosX) != 1
                && Math.Abs(chipCoord.PosY - _emptyCell.PosY) != 1)
                return false;

            IChip oldChipPos = chipCoord.CreateClone();

            _field[_emptyCell.PosY, _emptyCell.PosX] = _field[chipCoord.PosY, chipCoord.PosX];
            _field[_emptyCell.PosY, _emptyCell.PosX].SetPosition(_emptyCell);

            _field[oldChipPos.PosY, oldChipPos.PosX] = _emptyCell;
            _emptyCell.SetPosition(oldChipPos);
            
            return true;
        }

        public bool CheckWin()
        {
            for (int y = _sizeY-1; y > 0; y--)
            {
                for (int x = _sizeX-1; x > 0; x--)
                {
                    if (_field[y, x].Value != _etalonField[y, x].Value)
                        return false;
                }
            }

            return true;
        }

        //---------------------------------------------------------------------
        private void _InitEtalonField(IChip[,] field)
        {
            _etalonField = new IChip[_sizeY, _sizeX];

            for (int y = 0; y < _sizeY; y++)
                for (int x = 0; x < _sizeX; x++)
                    _etalonField[y, x] = field[y, x].CreateClone();
        }
    }
}
