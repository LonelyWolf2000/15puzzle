using UnityEngine;

namespace puzzle15
{
    public interface IGameController
    {
        int CountSteps { get; }
        string Time { get; }

        ILogic InitGame(int level, int size);
        void StartGame();
        void ReStartGame();
        bool OnClickChip(Transform sender, IChip dataChip);
        void Timer();
        void Steps();
        bool CheckNewTimeRecord();
        bool CheckNewStepsRecord();
        bool SaveStatisticGame(IProfile profile);
    }
}
