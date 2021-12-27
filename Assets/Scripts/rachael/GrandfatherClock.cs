using System.Linq;
using Himanshu;
using rachael.SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class GrandfatherClock : MonoBehaviour, IInteract
    {
        [FormerlySerializedAs("changingManager")] [SerializeField] ChangeFurniture m_changingManager;
        [FormerlySerializedAs("playerInteract")] [SerializeField] PlayerInteract m_playerInteract;

        [FormerlySerializedAs("LoopRoom")]
        
        [Header("objects")]
        [SerializeField] GameObject m_loopRoom;
        [FormerlySerializedAs("wall")] [SerializeField] GameObject m_wall;
        [FormerlySerializedAs("FinalRoom")] [SerializeField] GameObject m_finalRoom;
        [FormerlySerializedAs("bookstand")] [SerializeField] GameObject m_bookstand;

        [FormerlySerializedAs("Gears")]
        [Header("Clock pieces")]
                                                [SerializeField] GameObject m_gears;
        [FormerlySerializedAs("Face")]  [SerializeField] GameObject m_face;
        [FormerlySerializedAs("Gong")]  [SerializeField] GameObject m_gong;
        [FormerlySerializedAs("Hands")] [SerializeField] GameObject m_hands;

        private int m_previousPieces = 0;
        [FormerlySerializedAs("clip")] [SerializeField] private AudioClip m_placedDown;

        [SerializeField] private AudioClip m_pickUp;

        // Start is called before the first frame update
        void Start()
        {
            
            PlayerPrefs.SetInt("ClockIndex", 0);
            //foreach (GameObject obj in changingManager.Rooms)
            //{
            //    DefineRoom()
            //}

            // if (!m_playerInteract.m_placedDown)
            // {
            //
            //     for (int i = 0; i < m_playerInteract.m_numOfPieces; i++)
            //     {
            //         if (m_changingManager.m_rooms[i] != null)
            //         {
            //
            //             DefineRoom(m_changingManager.m_rooms[i].name);
            //         
            //         }
            //     }
            //     if(m_playerInteract.m_numOfPieces == 4)
            //     {
            //         m_loopRoom.SetActive(false);
            //         m_finalRoom.SetActive(true);
            //         m_wall.SetActive(false);
            //         m_bookstand.SetActive(false);
            //     }
            // }
            // else
            // {
            //     for (int i = 0; i < m_playerInteract.m_numOfPieces-1; i++)
            //     {
            //         if (m_changingManager.m_rooms[i] != null)
            //         {
            //             DefineRoom(m_changingManager.m_rooms[i].name);
            //         }
            //     }
            // }
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
            var pieces = _player.m_inventory.Keys.Where(t => t.m_objectName.Contains("Clock_"));

            foreach (var piece in pieces)
            {
                if (!transform.parent.parent.Find(piece.m_objectName).gameObject.activeSelf)
                {
                    if (PlayerPrefs.HasKey("ClockIndex"))
                    {
                        PlayerPrefs.SetInt("ClockIndex", PlayerPrefs.GetInt("ClockIndex") + 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ClockIndex", 1);
                    }

                    PlayerPrefs.SetInt(piece.m_objectName.Replace("Clock", "Loop"), PlayerPrefs.GetInt("ClockIndex") - 1);

                    
                    transform.parent.parent.Find(piece.m_objectName).gameObject.SetActive(true);
                    _player.m_inventory.Remove(piece);
                    _player.m_testInventory = _player.m_inventory.Keys.ToList();
                    break;
                }
            }
            
            // if (m_playerInteract.m_placedDown)
            // {
            //     if (m_previousPieces == _player.m_numOfPieces) return;
            //
            //
            //     GetComponent<AudioSource>()?.PlayOneShot(m_placedDown);
            //
            //
            //     DefineRoom(m_changingManager.m_rooms[_player.m_numOfPieces - 1].name);
            //     _player.m_placedDown = false;
            //
            //     FindObjectOfType<PlayerSave>().SavePlayer();
            //     _player.SaveProcess.SetTrigger("Save");
            //
            //
            //     if (m_playerInteract.m_numOfPieces == 4)
            //     {
            //         m_loopRoom.SetActive(false);
            //         m_finalRoom.SetActive(true);
            //         m_wall.SetActive(false);
            //         m_bookstand.SetActive(false);
            //         GetComponent<AudioSource>()?.Play();
            //     }
            // }
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

        public void CanExecute(Raycast _raycast)
        {
            //if the gear is within possession
            // if (_raycast.m_player.GetComponent<PlayerInteract>().m_placedDown)
            // {
            //     if (_raycast.m_indication != null)
            //         _raycast.m_indication.sprite = Resources.Load<Sprite>("Place");
            // }
            // // no gear
            // else
            // {
            //     if (_raycast.m_indication != null)
            //         _raycast.m_indication.enabled = false;
            // }
        }

        public void DefineRoom(string _roomName)
        {
            if (PlayerPrefs.HasKey("ClockIndex"))
            {
                PlayerPrefs.SetInt("ClockIndex", PlayerPrefs.GetInt("ClockIndex") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("ClockIndex", 1);
            }
            switch(_roomName)
            {
                case "Loop_gear":
                    m_gears.SetActive(true);
                    break;
                case "Loop_face":
                    m_face.SetActive(true);
                    PlayerPrefs.SetInt("Loop_Face", PlayerPrefs.GetInt("ClockIndex") - 1);
                    break;
                case "Loop_mouth":
                    m_gong.SetActive(true);
                    PlayerPrefs.SetInt("Loop_Mouth", PlayerPrefs.GetInt("ClockIndex") - 1);
                    break;
                case "Loop_hand":
                    m_hands.SetActive(true);
                    PlayerPrefs.SetInt("Loop_Hand", PlayerPrefs.GetInt("ClockIndex") - 1);
                    break;
            }
        }


        public void TimePiece(PlayerInteract _player)
        {
            // switch (_player.m_numOfPieces)
            // {
            //     case 1:
            //         m_gears.SetActive(true);
            //         break;
            //     case 2:
            //         m_gears.SetActive(true);
            //         m_face.SetActive(true);
            //         break;
            //     case 3:
            //         m_gears.SetActive(true);
            //         m_face.SetActive(true);
            //         m_gong.SetActive(true);
            //         break;
            //     case 4:
            //         m_gears.SetActive(true);
            //         m_face.SetActive(true);
            //         m_gong.SetActive(true);
            //         m_hands.SetActive(true);
            //         break;
            //     default:
            //         print("Incorrect intelligence level.");
            //         break;
            // }
        }
    }
}
