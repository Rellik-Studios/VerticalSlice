using System;
using UnityEngine;
using UnityEngine.Events;

namespace Himanshu
{
    public class Distraction : MonoBehaviour, IInteract
    {
        private AudioSource m_audioSource;
        public bool m_playing;

        public bool m_DestroyAfterUse;

        public UnityEvent m_onExecute;
        
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
                    if(m_DestroyAfterUse)
                        Destroy(this);
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
                m_onExecute?.Invoke();
                playing = true;
                this.Invoke(() => playing = false, m_audioSource.clip.length);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!m_audioSource.isPlaying && other.CompareTag("Player"))
            {
                m_onExecute?.Invoke();
                //m_audioSource.Play();
                playing = true;
                //this.Invoke(() => playing = false, m_audioSource.clip.length);
                
            }
        }
    }
}