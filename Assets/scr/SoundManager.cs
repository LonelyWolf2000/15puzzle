using UnityEngine;

namespace puzzle15
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource[] audioSources;
        private AudioSource as_Button;
        private AudioSource as_Shuffle;
        private AudioSource as_Starting;
        private AudioSource as_Success;

        //public Dictionary<string, AudioClip> i;
        // Use this for initialization
        void Start()
        {
            audioSources = GetComponents<AudioSource>();

            as_Button = audioSources[0];
            as_Shuffle = audioSources[1];
            as_Starting = audioSources[2];
            as_Success = audioSources[3];
        }

        public void ButtonClickPlay()
        {
            as_Button.Play();
        }

        public void ShufflePlay()
        {
            as_Shuffle.Play();
        }

        public void StartingPlay()
        {
            as_Starting.Play();
        }

        public void SuccessPlay()
        {
            as_Success.Play();
        }

        public void MuteUnmute(bool mute)
        {
            foreach (var audioSource in audioSources)
                audioSource.mute = mute;
        }

        public void SetVolume(float volumeValue)
        {
            foreach (var audioSource in audioSources)
                audioSource.volume = volumeValue;
        }
    }
}