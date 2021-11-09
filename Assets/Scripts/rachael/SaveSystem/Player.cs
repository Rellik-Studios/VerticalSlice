using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class Player : MonoBehaviour
{
    public ChangeFurniture eraChanging;
    public RespawnManager respawnManager;
    public PlayerInteract player;
    //public SavingGame saveFile;


    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    public int Death;
    public bool hasPiece;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if(PlayerPrefs.HasKey("SaveFile"))
        {
            LoadPlayer();
        }
        else
        {
            SavePlayer();
            PlayerPrefs.SetInt("SaveFile", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //press key number 0
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SavePlayer();
        }
        //press key number 9
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.DeleteAll();
        }

    }

    public void SavePlayer()
    {
        //saveFile.SavePoint();

        SavingValues();

        SaveSystem.SavePlayer(this);
        
    }


    public void SavingValues()
    {
        Index = eraChanging.Index;
        numOfPieces = player.m_numOfPieces;
        Death = player.m_deathCount;
        hasPiece= player.m_placedDown;
    }
    public void LoadingValues()
    {
        //making a transfer of data from the file to the scripts in gameplay
        eraChanging.SaveIndex(Index);
        player.m_numOfPieces = numOfPieces;
        player.m_deathCount = Death;
        player.m_placedDown = hasPiece;
    }
    public void LoadPlayer()
    {

        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<CharacterController>().enabled = false;

            numOfPieces = data.numOfPieces;
            Index = data.Index;
            Death = data.Death;
            hasPiece = data.hasPiece;

            LoadingValues();

            eraChanging.LoadTimeEra();

            Vector3 position = new Vector3(0, 0, 0);
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
 
}