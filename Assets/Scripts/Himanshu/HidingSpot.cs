using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public class HidingSpot : MonoBehaviour, IInteract
    {
        private List<Transform> m_hidingSpots;
        private int m_hidingIndex;
        private PlayerInteract m_player;
        public bool m_cupboard;

        [SerializeField] private Shader m_shader;
        private Animator m_animator;

        private Renderer m_cubeRenderer;
        private bool aInfect
        {
            get => m_animator.GetBool("infect");
            set => m_animator.SetBool("infect", value);
        }
        private bool aDisInfect
        {
            get => m_animator.GetBool("disinfect");
            set => m_animator.SetBool("disinfect", value);
        }
        
        private float aSpeed
        {
            get => m_animator.GetFloat("speed");
            set => m_animator.SetFloat("speed", value);
        }

        public bool aOpen
        {
            get => m_animator.GetBool("open");
            set => m_animator.SetBool("open", value);
        }
        
        public bool aClose
        {
            get => m_animator.GetBool("close");
            set => m_animator.SetBool("close", value);
        }

        [SerializeField] private float m_distortionValue;
        public float distortionValue
        {
            get => m_distortionValue;
            set
            {
                m_distortionValue = value;
                if(m_cupboard)
                    transform.Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", value);
                else
                    transform.Find("GFX").Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", value);
            }
        }

        public bool isActive
        {
            get;
            set;
        }
        
        public int hidingIndex
        {
            get => m_hidingIndex;
            set
            {
                if(m_hidingSpots.Count < 4)
                    m_hidingIndex = value < 0 ? 0 : value > m_hidingSpots.Count - 1 ? m_hidingSpots.Count - 1 : value;
                else
                    m_hidingIndex = value < 0 ? m_hidingSpots.Count - 1 : value > m_hidingSpots.Count - 1 ? 0 : value;

                if (m_player != null)
                {
                    m_player.SetPositionAndRotation(m_hidingSpots[m_hidingIndex]);
                    
                    m_player.GetComponent<CharacterController>().enabled = false;
                }
            }
        }

        public bool infectStared { get; set; }

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            isActive = true;
            m_hidingSpots = new List<Transform>();

            if (transform.Find("Cube") != null)
            {
                transform.Find("Cube").GetComponent<Renderer>().material = new Material(m_shader);
                m_cubeRenderer = transform.Find("Cube").GetComponent<Renderer>();
            }
            else
            {
                transform.Find("GFX").Find("Cube").GetComponent<Renderer>().material = new Material(m_shader);
                m_cubeRenderer = transform.Find("GFX").Find("Cube").GetComponent<Renderer>();
            }
            var hidingSpots = transform.Find("HidingSpots");
            for (int i = 0; i < hidingSpots.childCount; i++)
            {
                if(hidingSpots.GetChild(i).gameObject.activeInHierarchy)
                    m_hidingSpots.Add(hidingSpots.GetChild(i));
            }
        }

        public void Execute(PlayerInteract _player)
        {
            if (isActive)
            {
                m_player = _player;
                m_player.SetPositionAndRotation(m_hidingSpots[hidingIndex], m_cupboard ? 1.0f : 0f);
                _player.Hide(this);
            }

            else
            {
                if (_player.timeReverse)
                {
                    _player.timeReverse = false;
                    this.Invoke(() => { _player.timeReverse = true;}, 5f);
                    StartCoroutine(_player.m_timeRewind.FillBar(2f));
                    StartCoroutine(_player.m_timeRewind.FillBar(3f, -1, 2f));
                    _player.PlayTimeRewind();
                    DisInfect(2f);
                }
            }
        }

        

        private void DisInfect(float _time)
        {
            infectStared = false;
            
            aDisInfect = true;
            aInfect = false;
            aSpeed = 1f / _time;
            
            StartCoroutine(eInfect(true, _time));
        }

        public void Disable()
        {
            m_player = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                //m_cubeRenderer.SetFloat("DistorionLevel", 0.02f);
                //transform.Find("Cube").GetComponent<Renderer>().material.SetFloat("DistortionLevel", 0.02f);
                distortionValue = m_distortionValue;
            }
            if (m_player !=  null)
            {
                if (!isActive)
                {
                    m_player.Kick();
                    m_player = null;
                    return;
                }
                StartCoroutine(IndexHandler());
            }
        }

        private IEnumerator IndexHandler()
        {
            var movement = m_player.m_playerInput.movement;

            if (Input.GetKeyDown(KeyCode.A))
            {
                hidingIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                hidingIndex++;
            }

            yield return null;
        }

        public void Infect()
        {
            
            aDisInfect = false;
            aInfect = true;
            aSpeed = 1f / 3f;

            infectStared = true;
            StartCoroutine(eInfect(false, 3f));
        }

        IEnumerator eInfect(bool _state, float _time)
        {
            //Gradually apply Distortion here
            var counter = 0f;
            
            while (_state ? distortionValue > 0f : distortionValue < 0.02f) 
            {
                distortionValue = Mathf.Lerp(distortionValue,_state ? -0.001f : 0.021f, Time.deltaTime * _time);
                counter += Time.deltaTime / _time;
                Debug.Log(distortionValue);
                yield return null;
            }
            isActive = _state;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent<Renderer>(out Renderer _renderer))
                    _renderer.material.color = _state? Color.white : Color.red;
            }
            
        }
    }
}