using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Himanshu
{

    public class PlayerInteract : MonoBehaviour
    {
        private bool m_spotted;
        public GameObject LoseScreen;

        private List<EnemyController> m_enemies;
        public  IEnumerator FillBar(Image _fillImage, float _time, int _dir = 1, float _waitTime = 0f)
        {
            yield return new WaitForSeconds(_waitTime);
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                _fillImage.fillAmount += _dir * Time.deltaTime / _time;
                yield return null;
            }
        }

        private bool playingUp = false;
        private bool playingDown = false;
        private Coroutine m_fillRoutine;

        private int m_enemySpotNum;

        private int enemySpotNum
        {
            get => m_enemySpotNum;
            set
            {
                if (m_enemySpotNum != value)
                {
                    m_enemySpotNum = value;
                    spotted = m_enemySpotNum > 0;
                }
            }
        }
        
        public bool spotted
        {
            get => m_spotted;
            set
            {
                if (m_spotted != value)
                {
                    if (value)
                    {
                        StopCoroutine(m_fillRoutine);
                        m_fillRoutine = StartCoroutine(FillBar(m_danger, 8f / enemySpotNum));
                    }
                    else 
                    {
                        StopCoroutine(m_fillRoutine);
                        m_fillRoutine = StartCoroutine(FillBar(m_danger, 3f, -1));
                    }
                    m_spotted = value;
                }
            }
        }

        [Header("Images")] 
        public Image m_timeRewind;
        public Image m_timeStop;
        public Image m_danger;


        public float dangerBarVal
        {
            get => m_danger.fillAmount;
            set => m_danger.fillAmount = value;
        }
        public bool interactHold => m_playerInput.interactHold;

        private int m_bulletCount = 1;

        public int bulletCount
        {
            get => m_bulletCount;
            private set => m_bulletCount = value;
        }

        public bool timeReverse { get; set; }

        public bool cloudedVision
        {
            set
            {
                if (value)
                {
                    StopCoroutine(m_kickRoutine);
                    m_kickRoutine = StartCoroutine(eLensDistortion(2f));
                }
                else
                {
                    StopCoroutine(m_kickRoutine);
                    m_kickRoutine = StartCoroutine(eLensDistortion(1f, false));
                }
            } 
            
        }

        private float m_lensDistort;
        private float m_lensScale;
        private float lensDistort
        {
            get => m_lensDistort;
            set
            {
                m_lensDistort = value;
                if (GameObject.FindObjectOfType<Camera>().GetComponent<Volume>().profile
                    .TryGet(out LensDistortion _lensDistortion))
                {
                    _lensDistortion.intensity.value = m_lensDistort;
                    _lensDistortion.scale.value = m_lensScale;
                }
            }
        }

        private float lensScale
        {
            get => m_lensScale;
            set
            {
                m_lensScale = value;
            }
        }

        private IEnumerator eLensDistortion(float _time, bool _distort = true)
        {
            if (_distort)
            {
                var count = 0f;
                while (count < _time)
                {
                    count += Time.deltaTime;
                    lensScale = Mathf.Lerp(lensScale, 0.1f, Time.deltaTime * _time);
                    lensDistort = Mathf.Lerp(lensDistort,0.74f, Time.deltaTime * _time);
                    yield return null;
                }

                lensScale = 0.1f;
                lensDistort = 0.74f;
            }

            else
            {
                var count = 0f;
                while (count < _time || lensScale < 1f || lensDistort > 0f)
                {
                    count += Time.deltaTime;
                    lensScale = Mathf.Lerp(lensScale, 1.1f, Time.deltaTime * _time);
                    lensDistort = Mathf.Lerp(lensDistort,-0.1f, Time.deltaTime * _time);
                    yield return null;
                }

                lensScale = 1f;
                lensDistort = 0f;

            }
            
        }

        [Header("General")]
        public bool m_hiding;
        public int m_numOfPieces = 0;
        public bool m_placedDown = false;

        [Header("Audio")]
        [SerializeField] private AudioClip m_rewindAudio;
        [SerializeField] private AudioClip m_timeStopAudio;
        public PlayerInput m_playerInput;
        private RaycastingTesting m_raycastingTesting;
        private HidingSpot m_hidingSpot;
        private PlayerFollow m_playerFollow;
        private Coroutine m_kickRoutine;


        private void OnEnable()
        {
            m_enemies = GameObject.FindObjectsOfType<EnemyController>().ToList();
            Debug.Log(m_enemies.Count);
        }

        private void Start()
        {
            m_kickRoutine = StartCoroutine(temp());
            m_fillRoutine = StartCoroutine(temp());
            m_playerFollow = GameObject.FindObjectOfType<PlayerFollow>();
            m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
            m_playerInput = GetComponent<PlayerInput>();
            timeReverse = true;

        }

        private void Update()
        {
            if (m_playerInput.interact && !m_hiding)
            {
                m_raycastingTesting.ObjectInFront?.GetComponent<IInteract>()?.Execute(this);
            }
            else if(m_playerInput.interact)
            {
                Unhide();
            }

            if (m_playerInput.interact)
            {
                m_raycastingTesting.ObjectInFront?.GetComponent<IEnemy>()?.Shoot(this);
            }

            if (dangerBarVal == 1f && !LoseScreen.activeInHierarchy)
            {
                LoseScreen.SetActive(true);
                m_enemies.All(t => t.toPatrol = true);
                dangerBarVal = 0;
                gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                //SceneManager.LoadScene(1);
            }
            enemySpotNum = m_enemies.Count(_enemy => _enemy.m_spotted);
            
            Debug.Log(m_enemySpotNum);

            
            
            
            if (m_hiding && dangerBarVal < 0.1f)
                dangerBarVal = 0f;


        }

        public void Unhide()
        {
            if (m_hidingSpot.m_cupboard)
            {
                StartCoroutine(eUnHide());
            }
            else
            {
                
                GetComponent<CharacterController>().enabled = true;
                GetComponent<CharacterController>().Move(m_playerFollow.transform.forward * 3f);
                m_playerFollow.ResetRotationLock();
                m_hidingSpot.Disable();
                m_hiding = false;
                m_hidingSpot = null;
            }
        }

        IEnumerator temp()
        {
            yield return null;
        }

        private IEnumerator eUnHide()
        {
            m_hidingSpot.aOpen = true;
            m_hidingSpot.aClose = false;
            yield return new WaitForSeconds(1f);
            //transform.Translate(m_playerFollow.transform.forward * 3f);
            m_hidingSpot.aOpen = false;
            m_hidingSpot.aClose = true;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<CharacterController>().Move(m_playerFollow.transform.forward * 3f);

            m_playerFollow.ResetRotationLock();
            m_hidingSpot.Disable();
            m_hiding = false;
            m_hidingSpot = null;            
        }

        private IEnumerator TimeHandler()
        {
            if (m_bulletCount > 0)
            {
                m_bulletCount--;
                Time.timeScale = 0.1f;

                yield return new WaitForSeconds(6f * Time.timeScale);

                Time.timeScale = 1f;
            }
        }

        public void Hide(HidingSpot _hidingSpot)
        {
            if (_hidingSpot.m_cupboard)
            {
                StartCoroutine(eHide(_hidingSpot));
            }
            else
            {
                m_hidingSpot = _hidingSpot;
                GetComponent<CharacterController>().enabled = false;
                Debug.Log("Hiding now");
                m_hiding = true;
            }
        }

        private IEnumerator eHide(HidingSpot _hidingSpot)
        {
            _hidingSpot.aOpen = true;
            _hidingSpot.aClose = false;
            yield return new WaitForSeconds(1f);
            _hidingSpot.aOpen = false;
            _hidingSpot.aClose = true;
            
            m_hidingSpot = _hidingSpot;
            GetComponent<CharacterController>().enabled = false;
            Debug.Log("Hiding now");
            m_hiding = true;            
        }

        public void SetPositionAndRotation(Transform _transform, float _delay = 0)
        {
            if(_delay > 0f)
                StartCoroutine(eSetPositionAndRotation(_transform, _delay));
            else
            {
                transform.rotation = _transform.rotation;
                GetComponent<CharacterController>().enabled = false;
                transform.position = _transform.position;
                m_playerFollow.SetRotation(_transform, new Vector2(-30, 30));
            }
        }

        private IEnumerator eSetPositionAndRotation(Transform _transform,float _delay)
        {
            yield return new WaitForSeconds(_delay);
            transform.rotation = _transform.rotation;
            GetComponent<CharacterController>().enabled = false;
            transform.position = _transform.position;
            m_playerFollow.SetRotation(_transform, new Vector2(-30, 30));
            //GetComponent<CharacterController>().enabled = true;
        }

        public void Collect()
        {
            Debug.Log("Object Collected?");
            m_placedDown = true;
            m_numOfPieces++;
        }

        public void Shoot()
        {
            GetComponent<AudioSource>().PlayOneShot(m_timeStopAudio);
            m_bulletCount -= 1;
                        
            this.Invoke(() => { bulletCount = 1; }, 6f);
           
            
            StartCoroutine(m_timeStop.FillBar(0.1f));
            StartCoroutine(m_timeStop.FillBar(6f, -1));
        }

        public void UnSpot()
        {
            dangerBarVal = 0f;
            foreach (var enemy in m_enemies)
            {
                enemy.m_spotted = false;
            }
        }

        public void Kick()
        {
            cloudedVision = true;
            this.Invoke(() => { cloudedVision = false; }, 2f);
            this.Invoke(() => { Unhide(); }, 3f);

        }

        public void PlayTimeRewind()
        {
            GetComponent<AudioSource>().PlayOneShot(m_rewindAudio);
        }
    }
}