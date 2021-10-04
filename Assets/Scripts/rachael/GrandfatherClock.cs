using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.Serialization;

public class GrandfatherClock : MonoBehaviour, IInteract
{
    [SerializeField] GameObject m_triggerDoor;
    [SerializeField] GameObject m_goalDoor;

    [SerializeField] GameObject Gears;
    [SerializeField] GameObject Face;
    [SerializeField] GameObject Gong;
    [SerializeField] GameObject Hands;

    private int m_previousPieces = 0;
    [SerializeField] private AudioClip clip;

    [SerializeField] private AudioClip m_pickUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnableTriggerDoor()
    {
        //if a peace is placed on clock
        m_triggerDoor.SetActive(true);
    }

    void OpenTheDoor()
    {
        //once all the piece are put in place then the door opens
        m_goalDoor.GetComponent<DoorScript>().DoorOpening();
    }
    //when the piece of the clock is being placed
    public void Execute(PlayerInteract _player)
    {

        if(m_previousPieces == _player.m_numOfPieces)   return;
        
        
        GetComponent<AudioSource>()?.PlayOneShot(m_pickUp);

        switch (_player.m_numOfPieces)
        {
            case 1:
               Gears.SetActive(true);
               GetComponent<AudioSource>().Play(); 
               m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                _player.m_placedDown = false;
                break;
            case 2:
                Face.SetActive(true);
                m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                _player.m_placedDown = false;
                break;
            case 3:
                GetComponent<AudioSource>().clip = clip;
                GetComponent<AudioSource>().Play();
                Gong.SetActive(true);
                m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                _player.m_placedDown = false;
                break;
            case 4:
                Hands.SetActive(true);
                _player.m_placedDown = false;
                OpenTheDoor();
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }
        
    }
}
