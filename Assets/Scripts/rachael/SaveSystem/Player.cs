using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class Player : MonoBehaviour
{
    public ChangeFurniture eraChanging;
    public RespawnManager respawnManager;
    public PlayerInteract player;

    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    public int Death;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    void LoadPlayer()
    {

        GetComponent<CharacterController>().enabled = false;

        PlayerData data = SaveSystem.LoadPlayer();

        Cursor.lockState = CursorLockMode.Locked;

        Vector3 position = new Vector3(0,0,0);
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        Transform playerTransform = gameObject.transform;
        playerTransform.position = new Vector3(position.x, position.y, position.z);

        GetComponent<RespawnManager>().SetPosition(playerTransform);
        GetComponent<RespawnManager>().Respawn();

        GetComponent<CharacterController>().enabled = true;
    }
}
