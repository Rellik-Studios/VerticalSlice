using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class PlayerSave : MonoBehaviour
{
    public ChangeFurniture eraChanging;
    public RespawnManager respawnManager;
    public PlayerInteract player;
    //public SavingGame saveFile;


    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    //public int Death;
    public bool hasPiece;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if (PlayerPrefs.HasKey("SaveFile") && !gameManager.Instance.m_isSafeRoom)
        {
            LoadPlayer();
        }
        else if (PlayerPrefs.HasKey("SaveFile"))
        {
            LoadPlayer(true);
            gameManager.Instance.m_isSafeRoom = false;
        }
        else
        {
            SavePlayer();
            PlayerPrefs.SetInt("Death", 0);
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
        if(Input.GetKeyDown(KeyCode.H))
        {
            player.Death();
        }

    }

    public void SavePlayer(bool _isSafeRoom = false)
    {
        //saveFile.SavePoint();
        SavingValues();
        SaveSystem.SavePlayer(this, _isSafeRoom);
    }


    public void SavingValues()
    {
        Index = eraChanging.Index;
        numOfPieces = player.m_numOfPieces;
        //Death = player.m_deathCount;
        hasPiece= player.m_placedDown;
    }
    public void LoadingValues()
    {
        //making a transfer of data from the file to the scripts in gameplay
        eraChanging.SaveIndex(Index);
        player.m_numOfPieces = numOfPieces;
        player.m_deathCount = PlayerPrefs.GetInt("Death");
        player.m_placedDown = hasPiece;


    }
    public void LoadPlayer(bool _isSafeRoom = false)
    {

        PlayerData data = SaveSystem.LoadPlayer(_isSafeRoom);

        if (data != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<CharacterController>().enabled = false;

            numOfPieces = data.numOfPieces;
            Index = data.Index;
            //Death = data.Death;
            hasPiece = data.hasPiece;

            LoadingValues();

            eraChanging.LoadTimeEra();

            Vector3 position = new Vector3(0, 0, 0);
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            Transform playerTransform = gameObject.transform;
            playerTransform.position = new Vector3(position.x, position.y, position.z);
            
            
            Vector3 rotation = new Vector3(0, 0, 0);
            rotation.x = data.rotation[0];
            rotation.y = data.rotation[1];
            rotation.z = data.rotation[2];

            Quaternion newrot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            playerTransform.rotation = new Quaternion(newrot.x, newrot.y, newrot.z, newrot.w);

            GetComponent<RespawnManager>().SetPosition(playerTransform);
            GetComponent<RespawnManager>().Respawn();



            GetComponent<CharacterController>().enabled = true;
        }
    }
 
}