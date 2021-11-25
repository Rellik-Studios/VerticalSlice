using UnityEngine;

namespace Himanshu
{
    public class Distraction : MonoBehaviour, IInteract
    {
        private AudioSource m_audioSource;
        public bool m_playing;
        public bool playing
        {
            get => m_playing;
            set
            {
                m_playing = value;
                if(value)
                    m_audioSource.Play();
                else
                {
                    m_audioSource.Stop();
                }
            }
        }

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        public void Execute(PlayerInteract _player)
        {
            if (!m_audioSource.isPlaying)
            {
                //m_audioSource.Play();
                playing = true;
                this.Invoke(() => playing = false, m_audioSource.clip.length);
            }
        }
    }
}