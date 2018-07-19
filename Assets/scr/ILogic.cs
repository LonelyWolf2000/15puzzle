namespace puzzle15
{
    public interface ILogic
    {
        IChip EmptyCell { get; }
        IChip[,] Field { get; }

        void InitField(int size);
        void InitField(int sizeX, int sizeY);
        void ResetField();
        void SetCustomField(IChip[,] field);
        bool MoveChip(IChip chipCoord);
        bool CheckWin();
    }
}