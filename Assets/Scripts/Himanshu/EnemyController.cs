﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Himanshu
{
    public class EnemyController : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject m_distortion;
      
        [SerializeField] private float m_hearingRadius = 5f;
        // private float distortionValue
        // {
        //     get => m_distortion.GetComponent<Renderer>().material.GetFloat("DistortionSpeed");
        //     set => m_distortion.GetComponent<Renderer>().material.SetFloat("DistortionSpeed", value);
        //
        // }
        private bool m_toPatrol;

        private float aSpeed
        {
            get => m_animator.GetFloat("speed");
            set => m_animator.SetFloat("speed", value);
        }

        [SerializeField] private Transform m_headBone;
        [SerializeField] private Transform m_neck1Bone;
        [SerializeField] private Transform m_neck2Bone;
        public float lookAngle
        {
            get => m_lookAngle;
            set
            {
                m_lookAngle = value;
                
                
                //m_headBone.gameObject.SetActive(false);
                m_headBone.transform.localRotation = Quaternion.Euler(value/ 2f, m_headBone.transform.localRotation.eulerAngles.y, m_headBone.transform.localRotation.eulerAngles.z);
                m_neck1Bone.transform.localRotation = Quaternion.Euler(value/ 4f, m_neck1Bone.transform.localRotation.eulerAngles.y,m_neck1Bone.transform.localRotation.eulerAngles.z);
                m_neck2Bone.transform.localRotation = Quaternion.Euler(value/ 4f, m_neck2Bone.transform.localRotation.eulerAngles.y,m_neck2Bone.transform.localRotation.eulerAngles.z);
            }
        }
        public bool toPatrol
        {
            get => m_toPatrol;
            set
            {
                if(GetComponent<StateMachine>().enabled)
                    m_toPatrol = value;
                else
                    m_toPatrol = false;
                
            }
        }

        public bool m_spotted;
        
        [Header("Attack")]
        [SerializeField] private float m_attackTimer;
        private float m_defaultAttackTimer;

        [Header("Patrol")] 
        [SerializeField] private float m_defaultPatrolWaitTime;

        [Header("Infect")] 
        [SerializeField] private List<HidingSpot> m_hidingSpotsToInfect;

        private HidingSpot m_hidingSpotToInfect;
        
        public UnityEvent m_command;
        [FormerlySerializedAs("frozen")] public bool m_frozen = false;
        public bool m_commandFinished { get; set; }

        [SerializeField] private bool m_noCommand;

        private List<Transform> m_patrolPoints = new List<Transform>();
        private int m_index;
        private NavMeshAgent m_agent;

        
        
        private RaycastHit[] m_hits = new RaycastHit[3];
        private float m_lookAngle;
        private Animator m_animator;

        private int index
        {
            get => m_index;
            set => m_index = value > m_patrolPoints.Count - 1 ? 0 : value < 0 ? m_patrolPoints.Count - 1 : value;
        }
        
        //Called through the Visual Script
        public void RunCommand()
        {
            m_command?.Invoke();

            if (m_noCommand)
                m_commandFinished = true;
        }
        private void Start()
        {
            m_animator = transform.Find("GFX").GetComponent<Animator>();
            m_agent = GetComponent<NavMeshAgent>();
            m_defaultAttackTimer = m_attackTimer;
            
            if (m_patrolPoints.Count == 0)
            {
                var patrolPointsParent = transform.Find("PatrolPoints");

                if (patrolPointsParent == null)
                {
                    return;
                    //throw new Exception($"Cannot Find Patrol Points in Enemy: {name}");
                }

                for (int i = 0; i < patrolPointsParent.childCount; i++)
                {
                    if(patrolPointsParent.GetChild(i).gameObject.activeInHierarchy)
                        m_patrolPoints.Add(patrolPointsParent.GetChild(i));
                }

                patrolPointsParent.SetParent(null);
            }
        }


        
        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(6f);
            m_frozen = false;
        }

        //Called through the Visual Script
        public void Attack()
        {
            m_attackTimer -= Time.deltaTime;
            if (m_attackTimer < 0f)
            {
                Debug.Log("attacking");
                m_attackTimer = m_defaultAttackTimer;
            }
            
            GameObject.FindObjectOfType<PlayerInteract>().Death();
            

           
            m_spotted = true;
        }

        private void Update()
        {
            aSpeed = m_agent.velocity.magnitude;
        }

        //Called through the Visual Script
        public void ResetAttack()
        {
            m_attackTimer = m_defaultAttackTimer;
        }

        public void ChaseUpdate()
        {
            Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);
            
            for (int i = 0; i <= 2; i++)
            {
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player") && m_hits[i].collider.GetComponentInParent<CharacterController>().enabled)
                {
                    m_spotted = true;
                    return;
                }
            }
            
            m_spotted = false;
        }

        
        

        public void PatrolStart()
        {
            m_spotted = false;

           
            if(m_patrolPoints.Count > 0)
                m_agent.SetDestination(m_patrolPoints[index].position);

        }

        public void PatrolUpdate()
        {
            if (m_agent.remainingDistance < 0.1f)
            {
                StartCoroutine(SetDestination());
            }
            
            Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);
            
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward * 18f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(0f, transform.up) * transform.forward * 20f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward * 18f);

        }

        IEnumerator SetDestination()
        {

            yield return new WaitForSeconds(m_defaultPatrolWaitTime);
            
            if(m_patrolPoints.Count > 0 && m_patrolPoints.Count < 5)
            {
                if (m_agent.remainingDistance < 0.1f)
                    m_agent.SetDestination(m_patrolPoints[index++].position);
            }
        
        
            else if(m_patrolPoints.Count >= 5)
                    if (m_agent.remainingDistance < 0.1f)
                        m_agent.SetDestination(m_patrolPoints[Random.Range(0, m_patrolPoints.Count - 1)].position);
            
            //Debug.Log(index);
        }

        public bool PatrolToChaseTransition()
        {
           
            
            for (int i = 0; i <= 2; i++)
            {
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player") && m_hits[i].collider.GetComponentInParent<CharacterController>().enabled && !m_hits[i].collider.GetComponentInParent<PlayerInteract>().m_hiding)
                {
                    return true;
                }
            }

            var player = FindObjectOfType<PlayerMovement>();
            var colliders = Physics.OverlapSphere(transform.position, m_hearingRadius * (player.crouching ? 0.5f : 1f));
            if (colliders.Any(t => t.CompareTag("Player")))
            {
                return true;
            }
            return false;
        }

        public bool PatrolToInfectTransition()
        {

            for (int i = 0; i <= 2; i++)
            {
                
                if (m_hits[i].collider != null &&
                    m_hidingSpotsToInfect.Any(t => t.gameObject.transform == m_hits[i].collider.transform))
                {
                    m_hidingSpotToInfect = m_hits[i].collider.GetComponent<HidingSpot>();
                    if (m_hidingSpotToInfect.isActive)
                        if (Random.Range(0, 10) <= 4)
                            return true;
                        else
                            return false;
                    else
                        return false;
                }
            }
            
            return false;
        }

        public bool InfectToPatrolTransition()
        {
            return !m_hidingSpotToInfect.isActive;
        }

        public void ChaseEnter()
        {

            StartCoroutine(eChaseEnter());
            
            //this.Invoke(()=>lookAngle = 0, 2f);
        }

        IEnumerator eChaseEnter()
        {

            var angle = Vector3.SignedAngle( transform.position, FindObjectOfType<PlayerInteract>().transform.position, Vector3.up);

            //angle -= 180f;
            while ( Mathf.Abs(lookAngle - angle) > 0.1f)
            {
                //m_agent.SetDestination(transform.position);    
                // dir = transform.position - FindObjectOfType<PlayerMovement>().transform.position;
                // angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Debug.Log(Mathf.Abs(lookAngle - angle));
                lookAngle = Mathf.Lerp(lookAngle, angle, Time.deltaTime * 3);
                yield return null;
            }

            //m_agent.enabled = true;


        }
        public void InfectUpdate()
        {
            m_agent.stoppingDistance = 4;
            m_agent.SetDestination(m_hidingSpotToInfect.gameObject.transform.position);
            
            if (m_agent.remainingDistance <= m_agent.stoppingDistance && !m_hidingSpotToInfect.infectStared)
            {
                m_hidingSpotToInfect.Infect();
            }
        }
    }
    
}