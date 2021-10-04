using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bolt;
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

        private float distortionValue
        {
            get => m_distortion.GetComponent<Renderer>().material.GetFloat("DistortionSpeed");
            set => m_distortion.GetComponent<Renderer>().material.SetFloat("DistortionSpeed", value);

        }
        private bool m_toPatrol;
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
            

           
            m_spotted = true;
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

        
        //Interface Requirement
        public void Shoot(PlayerInteract _player)
        {
            if (_player.bulletCount > 0)
            {
                distortionValue = 0f;
                
                this.Invoke(() => { distortionValue = -0.2f;}, 6f);
                m_frozen = true;
                _player.Shoot();
                StartCoroutine(UnFreeze());
            }
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
                if (m_agent.remainingDistance < 0.1f)
                    m_agent.SetDestination(m_patrolPoints[index++].position);
                else
                    Debug.Log("");
            
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