using UnityEngine;

namespace puzzle15
{
    public class WinWindow : MonoBehaviour
    {
        private Animation anim;

        // Use this for initialization
        void Start()
        {
            HideWin();
            anim = GetComponent<Animation>();
        }

        public void ShowWin()
        {
            gameObject.SetActive(true);
            anim.Play();
        }

        public void HideWin()
        {
            gameObject.SetActive(false);
        }
    }
}