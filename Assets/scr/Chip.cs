using System.Collections.Generic;

namespace puzzle15
{
    public enum AdjacementCount
    {
        Cells4,
        //Cells6,
        //Cells8
    }

    public class Chip : IChip
    {
        private int currentX;
        private int currentY;
        private int value;

        public int PosX
        {
            get { return currentX; }

            //set
            //{
            //    throw new System.NotImplementedException();
            //}
        }

        public int PosY
        {
            get { return currentY; }
            //set
            //{
            //    throw new System.NotImplementedException();
            //}
        }

        public int Value
        {
            get { return value; }

            set { this.value = value; }
        }

        public Chip()
        {
            currentX = -1;
            currentY = -1;
            value = -1;
        }

        public Chip(int x, int y)
        {
            SetPosition(x, y);
        }

        public Chip(int x, int y, int value)
        {
            this.value = value;
            SetPosition(x, y);
        }

        public void SetPosition(int x, int y)
        {
            currentX = x;
            currentY = y;
        }

        public void SetPosition(IChip newCoord)
        {
            currentX = newCoord.PosX;
            currentY = newCoord.PosY;
        }

        public List<IChip> GetAdjacentCells(AdjacementCount count)
        {
            List<IChip> adjacentCells = new List<IChip>
            {
                new Chip(currentX, currentY - 1),
                new Chip(currentX + 1, currentY),
                new Chip(currentX, currentY + 1),
                new Chip(currentX - 1, currentY)
            };

            return adjacentCells;
        }

        public IChip CreateClone()
        {
            return new Chip(currentX, currentY, value);
        }

        public bool EqualsCoord(IChip coord)
        {
            return currentX == coord.PosX && currentY == coord.PosY;
        }
    }
}