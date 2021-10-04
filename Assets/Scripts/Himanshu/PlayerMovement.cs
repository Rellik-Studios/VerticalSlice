using System;
using UnityEngine;
using UnityEngine.Serialization;

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

        private void Start()
        {
            m_playerInput = GetComponent<PlayerInput>();
            m_rigidbody = transform.Find("GFX").gameObject.GetComponent<Rigidbody>();
            m_characterController = GetComponent<CharacterController>();
        }


        private void Update()
        {
            m_isGrounded = Physics.Raycast(transform.position, -Vector3.up, m_groundDistance);

            Movement();
            Jump();
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
            m_characterController.Move(movement * (m_speed * Time.deltaTime));
            
        }
    }
}