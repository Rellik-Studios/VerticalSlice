using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    public int Death;

    public float[] position;
    
    public PlayerData(Player player)
    {
        Index = player.Index;
        Death = player.Death;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
