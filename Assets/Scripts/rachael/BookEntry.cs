using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.Serialization;

public class BookEntry : MonoBehaviour, IInteract
{
    public ChangeFurniture eraChanging;
    //public PlayerSave m_playerFile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Execute(PlayerInteract _player)
    {
        Debug.Log("Save Entry Point completed");
        GameObject temp_player = _player.gameObject;
        if (temp_player.GetComponent<PlayerSave>() != null)
        {
            // saves the player data into the system
            temp_player.GetComponent<PlayerSave>().SavePlayer();
            //Debug.Log(eraChanging.Rooms[eraChanging.Index].name);
            DefineRoom(eraChanging.Rooms[eraChanging.Index].name);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Contact");
    //    if (other.CompareTag("Player"))
    //    {
    //        if (other.GetComponentInParent<PlayerSave>() != null)
    //        {
    //            //saves the player data into the system
    //            other.GetComponentInParent<PlayerSave>().SavePlayer();
    //            //Debug.Log(eraChanging.Rooms[eraChanging.Index].name);
    //            DefineRoom(eraChanging.Rooms[eraChanging.Index].name);

    //        }
    //    }
    //}


    public void DefineRoom(string roomName)
    {
        switch (roomName)
        {
            case "Loop_gear":
                PlayerPrefs.SetInt("Loop_Gear", PlayerPrefs.GetInt("ClockIndex"));
                break;
            case "Loop_face":
                PlayerPrefs.SetInt("Loop_Face", PlayerPrefs.GetInt("ClockIndex"));
                break;
            case "Loop_mouth":
                PlayerPrefs.SetInt("Loop_Mouth", PlayerPrefs.GetInt("ClockIndex"));
                break;
            case "Loop_hand":

                PlayerPrefs.SetInt("Loop_Hand", PlayerPrefs.GetInt("ClockIndex"));
                break;
        }
    }
}
