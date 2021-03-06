using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Himanshu
{

    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController m_characterController;
        private Rigidbody m_rigidbody;
        private PlayerInput m_playerInput;

        [SerializeField] private float m_speed = 5.0f;
        [SerializeField] private float m_gravity = 10.0f;
        private Vector3 m_playerVelocity = Vector3.zero;
        [SerializeField] private float m_jumpHeight;
        [SerializeField] private float m_groundDistance = 0.1f;
        private bool m_isGrounded;
        public bool crouching => m_playerInput.m_crouching;

        [SerializeField] private AudioClip m_breathingClip;
        [SerializeField] private float m_maxSprintTimer;
        private float m_sprintTimer;
        [SerializeField] private Image m_sprintImage;

        private float m_sprintNarratorTimer = 40f;
        private AudioSource m_audioSource;
        [SerializeField] private LayerMask m_groundMask;

        private float sprintTimer
        {
            get => m_sprintTimer;
            set
            {
                
                if (value < m_maxSprintTimer / 10.0f && value > 0.1f && m_sprintTimer > value)
                {
                    if (m_sprintNarratorTimer < 0f)
                    {
                        m_sprintNarratorTimer = 40f;
                        FindObjectOfType<Narrator>().breathing = true;
                    }

                    //m_audioSource.volume = 0.5f;
                    m_audioSource.PlayOneShot(m_breathingClip, 0.2f);
                    
                }
                m_sprintTimer = value;
                m_sprintImage.fillAmount = m_sprintTimer / m_maxSprintTimer;
            }
        }
        
        public Vector3 calculatedPosition
        {
            get => transform.position + (crouching ?  new Vector3(0f, 2f, 0f) : new Vector3(0f, 4f, 0f));
        }

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            sprintTimer = m_maxSprintTimer;
            m_playerInput = GetComponent<PlayerInput>();
            m_rigidbody = transform.Find("GFX").gameObject.GetComponent<Rigidbody>();
            m_characterController = GetComponent<CharacterController>();
        }


        private void Update()
        {
            m_isGrounded = Physics.Raycast(transform.position, -Vector3.up, m_groundDistance, m_groundMask, QueryTriggerInteraction.Ignore);
            Movement();
            Jump();
            m_sprintNarratorTimer -= Time.deltaTime;
        }

        private void Jump()
        {
            m_playerVelocity.y -= m_gravity;
            if (m_isGrounded) m_playerVelocity.y = 0f;
            if (m_playerInput.jump && m_isGrounded) 
                m_playerVelocity.y += Mathf.Sqrt(m_jumpHeight * 3.0f * m_gravity);
            m_characterController.Move(m_playerVelocity * Time.deltaTime);
        }

        private void Movement()
        {
            var movement = m_playerInput.movement.x * transform.right + m_playerInput.movement.z * transform.forward;
            m_characterController.Move(movement * (m_speed * (crouching ? 0.5f : 1f) * ((m_playerInput.sprint && sprintTimer > 0f && !crouching) ? 2.5f : 1.0f)  * Time.deltaTime));

            if (m_playerInput.sprint && sprintTimer > 0f && m_playerInput.movement.magnitude > 0f && !crouching)
            { 
                sprintTimer -= Time.deltaTime;
            }
            else if (sprintTimer < m_maxSprintTimer)
            {
                sprintTimer += Time.deltaTime / 4f;
            }
        }
    }
}
