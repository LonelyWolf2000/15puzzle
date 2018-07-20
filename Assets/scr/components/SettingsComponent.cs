using UnityEngine;
using UnityEngine.UI;

namespace puzzle15
{
    public class SettingsComponent : MonoBehaviour
    {
        public Button btn_Settings;
        public Button btn_Quit;
        public Slider sld_Vulume;
        public Toggle tgl_Volume;
        public Dropdown dd_Difficult;

        public GameController gameController;
        public SoundManager soundManager;

        //---------------------------------------------
        void Start()
        {
            gameObject.SetActive(false);
            soundManager.SetVolume(sld_Vulume.value);
            OnMuteUnmute();
        }

        public void ShowHideSettings()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            gameController.Pause = gameObject.activeSelf;
            gameController.HideWinWindow();
        }

        public void OnDropDownSelect()
        {
            switch (dd_Difficult.value)
            {
                case 0:
                    gameController.Difficult = Difficult.Trollbuster;
                    break;
                case 1:
                    gameController.Difficult = Difficult.Returd;
                    break;
                case 2:
                    gameController.Difficult = Difficult.Easy;
                    break;
                case 3:
                    gameController.Difficult = Difficult.Normal;
                    break;
                case 4:
                    gameController.Difficult = Difficult.Hard;
                    break;
                case 5:
                    gameController.Difficult = Difficult.VeryHard;
                    break;
            }
        }

        public void OnMuteUnmute()
        {
            soundManager.MuteUnmute(tgl_Volume.isOn);
        }

        public void OnChangeVolume(float value)
        {
            soundManager.SetVolume(value);
            soundManager.ShufflePlay();
        }
    }
}