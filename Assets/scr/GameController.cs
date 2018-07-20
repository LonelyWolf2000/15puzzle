using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace puzzle15
{
    public enum Difficult
    {
        Trollbuster = 1,
        Returd = 10,
        Easy = 25,
        Normal = 50,
        Hard = 100,
        VeryHard = 150
    }

    public class GameController : MonoBehaviour, IGameController
    {
        public Difficult Difficult = Difficult.Returd;
        public int Size = 4;
        public float SpeedShuffle = 0.25f;

        public SoundManager SoundManager;
        public Button btn_Start;
        public Button btn_Restart;
        public Text txt_Steps;
        public Text txt_Time;
        public WinWindow WinWindow;
        public bool Pause { get; set; }

        private ChipComponent[] _chipsInScene;
        private ILogic _logicGame;
        private ChipComponent _emptyCellPos;
        private int countSteps;
        private string countTime = "Time: 00:00:00";
        private bool _isGameReady;
        private Coroutine _timerRoutine;

        public int CountSteps
        {
            get { return countSteps; }
        }

        public string Time
        {
            get { return countTime; }
        }

        // Use this for initialization
        void Start()
        {
            txt_Time.text = countTime;
            GameObject go = GameObject.Find("Chips");

            if(go == null)
                return;

            _chipsInScene = new ChipComponent[go.transform.childCount];

            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject childGo = go.transform.GetChild(i).gameObject;
                ChipComponent chCmp = childGo.AddComponent<ChipComponent>();
                chCmp.GameController = this;
                _chipsInScene[i] = chCmp;

                if (i == go.transform.childCount - 1)
                    _emptyCellPos = chCmp;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool CheckNewStepsRecord()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckNewTimeRecord()
        {
            throw new System.NotImplementedException();
        }

        public ILogic InitGame(int level, int size)
        {
            if (_logicGame == null)
                _logicGame = new Logic();

            if (_logicGame.Field == null || _logicGame.Field.Length != size * 2)
            {
                _logicGame.InitField(size);
                _SetValuesChipsComponents();
            }

            StartCoroutine(_Shuffle(_logicGame, level));

            return _logicGame;
        }

        public void StartGame()
        {
            InitGame((int)Difficult, Size);

            btn_Start.transform.gameObject.SetActive(false);
            btn_Restart.transform.gameObject.SetActive(true);
            
            Timer();
        }

        public void ReStartGame()
        {
            _isGameReady = false;
            HideWinWindow();

            foreach (var ch in _chipsInScene)
                ch.ResetPosition();

            _logicGame.ResetField();
            _SetValuesChipsComponents();
            countSteps = 0;

            StartCoroutine(_Shuffle(_logicGame, (int)Difficult));
            Timer();
        }

        public bool OnClickChip(Transform sender, IChip dataChip)
        {
            if (!_isGameReady || !_logicGame.MoveChip(dataChip))
                return false;

            _SwapPos(sender.transform, _emptyCellPos.transform);
            Steps();
            SoundManager.ShufflePlay();

            if (_logicGame.CheckWin())
            {
                StopCoroutine(_timerRoutine);
                _isGameReady = false;
                WinWindow.ShowWin();
                SoundManager.SuccessPlay();
            }

            return true;
        }

        public bool SaveStatisticGame(IProfile profile)
        {
            throw new System.NotImplementedException();
        }

        public void Steps()
        {
            countSteps++;
            txt_Steps.text = "Steps: " + countSteps;
        }

        public void Timer()
        {
            if (_timerRoutine != null)
                StopCoroutine(_timerRoutine);

            _timerRoutine = StartCoroutine(_Timer());
        }

        public void HideWinWindow()
        {
            WinWindow.HideWin();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        //--------------------------------------------------------------------------
        private void _SetValuesChipsComponents()
        {
            int i = 0;
            foreach (IChip ch in _logicGame.Field)
            {
                _chipsInScene[i].Chip = ch;
                i++;
            }
        }

        private IEnumerator _Timer()
        {
            int ms = 0;
            int ss = 0;
            int mm = 0;

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if(!_isGameReady || Pause)
                    continue;

                ms = ms + 10;
                if (ms > 99)
                {
                    ms = 0;
                    ss++;
                    if (ss > 59)
                    {
                        ss = 0;
                        mm++;
                    }
                }

                countTime = "Time: " + mm + ":" + ss + ":" + ms;
                txt_Time.text = countTime;
            }
        }

        private IEnumerator _Shuffle(ILogic logic, int level)
        {
            btn_Start.enabled = false;
            btn_Restart.enabled = false;

            Random rnd = new Random();
            int lastValShuffled = -1;

            for (int i = 0; i < level; i++)
            {
                yield return new WaitForSeconds(SpeedShuffle);

                List<IChip> adjCellsCoords = new List<IChip>();
                foreach (var cell in logic.EmptyCell.GetAdjacentCells(AdjacementCount.Cells4))
                {
                    if (_CheckPosition(cell) && _logicGame.Field[cell.PosY, cell.PosX].Value != lastValShuffled)
                        adjCellsCoords.Add(cell);
                }

                IChip rndCoord = adjCellsCoords[rnd.Next(0, adjCellsCoords.Count)];
                IChip tempCh = _logicGame.Field[rndCoord.PosY, rndCoord.PosX];

                lastValShuffled = tempCh.Value;
                _logicGame.MoveChip(tempCh);
                _SwapPos(_chipsInScene[tempCh.Value - 1].transform, _emptyCellPos.transform);
                SoundManager.ShufflePlay();
            }

            btn_Start.enabled = true;
            btn_Restart.enabled = true;
            _isGameReady = true;
            SoundManager.StartingPlay();
        }

        private void _SwapPos(Transform chA, Transform chB)
        {
            Vector3 senderOldPos = chA.position;
            chA.position = chB.position;
            chB.position = senderOldPos;
        }

        //Возвращает true, если проверяемые координаты находятся в границах массива
        private bool _CheckPosition(IChip coord)
        {
            return _CheckPosition(coord.PosX, coord.PosY);
        }

        private bool _CheckPosition(int x, int y)
        {
            if (x < 0 || x > Size - 1
                      || y < 0 || y > Size - 1)
                return false;

            return true;
        }
    }
}