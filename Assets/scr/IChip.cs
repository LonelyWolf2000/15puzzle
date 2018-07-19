using System.Collections.Generic;

namespace puzzle15
{
    public interface IChip
    {
        int PosX { get; }
        int PosY { get; }
        int Value { get; set; }

        void SetPosition(int x, int y);
        void SetPosition(IChip newCoord);
        IChip CreateClone();
        bool EqualsCoord(IChip coord);
        List<IChip> GetAdjacentCells(AdjacementCount count);
    }
}