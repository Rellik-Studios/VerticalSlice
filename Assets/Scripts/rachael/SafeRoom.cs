using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoom : MonoBehaviour
{
    //temporary value until we fixed this on player
    bool IsSafe = false;
    
    //when they enter safe room
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSafe = true;
            if (other.GetComponentInParent<RespawnManager>() != null)
            {
                other.GetComponentInParent<RespawnManager>().SetPosition(this.transform);
                Debug.Log("You have entered the safe room");

            }
            if (other.GetComponentInParent<PlayerSave>() != null)
            {
                //saves the player data into the system
                other.GetComponentInParent<PlayerSave>().SavePlayer(true);
            }
        }
    }
    //when they exit safe room
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSafe = false;
            Debug.Log("You have exited the safe room");
        }
    }
}
