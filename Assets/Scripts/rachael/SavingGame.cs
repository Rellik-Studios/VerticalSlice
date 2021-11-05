using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Himanshu;

public class SavingGame : MonoBehaviour
{
    public ChangeFurniture eraChanging;
    public RespawnManager respawnManager;
    public PlayerInteract player;

    [SerializeField] bool testing_Mode = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerInteract>();
        //for the saving purposes---------------

        if (!testing_Mode)
        {
            if (!PlayerPrefs.HasKey("Player_X"))
            {
                PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
                PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
                PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);


                PlayerPrefs.SetInt("pieces", player.m_numOfPieces);

                PlayerPrefs.SetInt("Index", eraChanging.Index);

                //PlayerPrefs.SetInt("NumLine", 0);

                //PlayerPrefs.SetString("TimeEra", "Place");

                PlayerPrefs.SetInt("Death", player.m_deathCount);

                //PlayerPrefs.Save();

            }
            else
            {
                LoadPoint();

                //if(GetComponent<SavingGame>() != null)
                //{
                //    GetComponent<Player>().LoadPlayer();
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SavePoint();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {

            PlayerPrefs.DeleteAll();
        }
    }

    public void SavePoint()
    {
        PlayerPrefs.SetFloat("Player_X", gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Player_Y", gameObject.transform.position.y);
        PlayerPrefs.SetFloat("Player_Z", gameObject.transform.position.z);


        PlayerPrefs.SetInt("pieces", player.m_numOfPieces);

        PlayerPrefs.SetInt("Index", eraChanging.Index);


        PlayerPrefs.SetInt("Death", player.m_deathCount);
    }
    void LoadPoint()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<CharacterController>().enabled = false;

        gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_X"), PlayerPrefs.GetFloat("Player_Y"), PlayerPrefs.GetFloat("Player_Z"));

        player.m_numOfPieces = PlayerPrefs.GetInt("pieces");

        eraChanging.SavingTimeEra();

        player.m_deathCount = PlayerPrefs.GetInt("Death");


        GetComponent<RespawnManager>().SetPosition(gameObject.transform);
        GetComponent<RespawnManager>().Respawn();
        

        GetComponent<CharacterController>().enabled = true;
    }
}
