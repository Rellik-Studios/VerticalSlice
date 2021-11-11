using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.Serialization;

public class GrandfatherClock : MonoBehaviour, IInteract
{
    //[SerializeField] GameObject m_triggerDoor;
    //[SerializeField] GameObject m_goalDoor;
    
    [SerializeField] ChangeFurniture changingManager;
    [SerializeField] PlayerInteract playerInteract;

    [Header("Images")]
    [SerializeField] GameObject LoopRoom;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject FinalRoom;

    [Header("Clock pieces")]
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
        //foreach (GameObject obj in changingManager.Rooms)
        //{
        //    DefineRoom()
        //}

        if (!playerInteract.m_placedDown)
        {

            for (int i = 0; i < playerInteract.m_numOfPieces; i++)
            {
                if (changingManager.Rooms[i] != null)
                {

                    DefineRoom(changingManager.Rooms[i].name);
                }
            }
            if(playerInteract.m_numOfPieces == 4)
            {
                LoopRoom.SetActive(false);
                FinalRoom.SetActive(true);
                wall.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < playerInteract.m_numOfPieces-1; i++)
            {
                if (changingManager.Rooms[i] != null)
                {
                    DefineRoom(changingManager.Rooms[i].name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void EnableTriggerDoor()
    //{
    //    //if a peace is placed on clock
    //    m_triggerDoor.SetActive(true);
    //}

    //void OpenTheDoor()
    //{
    //    //once all the piece are put in place then the door opens
    //    m_goalDoor.GetComponent<DoorScript>().DoorOpening();
    //}
    //when the piece of the clock is being placed
    public void Execute(PlayerInteract _player)
    {

        if(m_previousPieces == _player.m_numOfPieces)   return;
        
        
        GetComponent<AudioSource>()?.PlayOneShot(m_pickUp);


        DefineRoom(changingManager.Rooms[_player.m_numOfPieces-1].name);
        _player.m_placedDown = false;


        if (playerInteract.m_numOfPieces == 4)
        {
            LoopRoom.SetActive(false);
            FinalRoom.SetActive(true);
            wall.SetActive(false);
        }
        //switch (_player.m_numOfPieces)
        //{
        //    case 1:
        //       Gears.SetActive(true);
        //       GetComponent<AudioSource>().Play(); 
        //       m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
        //        _player.m_placedDown = false;
        //        break;
        //    case 2:
        //        Face.SetActive(true);
        //        m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
        //        _player.m_placedDown = false;
        //        break;
        //    case 3:
        //        GetComponent<AudioSource>().clip = clip;
        //        GetComponent<AudioSource>().Play();
        //        Gong.SetActive(true);
        //        m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
        //        _player.m_placedDown = false;
        //        break;
        //    case 4:
        //        Hands.SetActive(true);
        //        _player.m_placedDown = false;
        //        OpenTheDoor();
        //        break;
        //    default:
        //        print("Incorrect intelligence level.");
        //        break;
        //}

    }
    public void DefineRoom(string roomName)
    {
        switch(roomName)
        {
            case "Loop_gear":
                Gears.SetActive(true);
                break;
            case "Loop_face":
                Face.SetActive(true);
                break;
            case "Loop_mouth":
                Gong.SetActive(true);
                break;
            case "Loop_hand":
                Hands.SetActive(true);
                break;
        }
    }


    public void TimePiece(PlayerInteract _player)
    {
        switch (_player.m_numOfPieces)
        {
            case 1:
                Gears.SetActive(true);
                break;
            case 2:
                Gears.SetActive(true);
                Face.SetActive(true);
                break;
            case 3:
                Gears.SetActive(true);
                Face.SetActive(true);
                Gong.SetActive(true);
                break;
            case 4:
                Gears.SetActive(true);
                Face.SetActive(true);
                Gong.SetActive(true);
                Hands.SetActive(true);
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }
    }
}
