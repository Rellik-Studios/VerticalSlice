using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool isOpened = false;

    [SerializeField] private UnityEvent m_trigerredL1;
    [SerializeField] private UnityEvent m_trigerredL2;
    [SerializeField] private UnityEvent m_trigerredL3;
    [SerializeField] private UnityEvent m_trigerredL4;
    // Start is called before the first frame update
    void Start()
    {
        if (isOpened && openTrigger)
        {
            myDoor.SetBool("IsOpening", true);
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if((openTrigger && !isOpened) || (closeTrigger && isOpened))
                GetComponent<AudioSource>().Play();
            if(closeTrigger)
                other.GetComponentInParent<PlayerInteract>().UnSpot();
                
            switch (FindObjectOfType<ChangEnviroment>().Index)
            {
                case 0:
                    m_trigerredL1?.Invoke();
                    break;
                    
                case 1:
                    m_trigerredL2?.Invoke();
                    break;
                
                case 2:
                    m_trigerredL3?.Invoke();
                    break;
                
                case 3:
                    m_trigerredL4?.Invoke();
                    break;
                
                default:
                    
                    break;
            }
            //old anim

            //if(openTrigger && DoneOnce)
            //{
            //    myDoor.Play("DoorOpening", 0, 0.0f);
            //    DoneOnce = false;

            //}
            //if (closeTrigger && DoneOnce)
            //{
            //    myDoor.Play("DoorClosing", 0, 0.0f);
            //    DoneOnce = false;

            //}
            if (openTrigger)
            {
                myDoor.SetBool("IsOpening", true);
                isOpened = true;
            }

            if (closeTrigger)
            {
                myDoor.SetBool("IsOpening", false);
                isOpened = false;
            }


        }
    }

    public void DoorOpening()
    {
        GetComponent<AudioSource>().Play();
        myDoor.SetBool("IsOpening", true);
    }
    public void DoorClosing()
    {
        GetComponent<AudioSource>().Play();
        myDoor.SetBool("IsOpening", false);
    }

}
