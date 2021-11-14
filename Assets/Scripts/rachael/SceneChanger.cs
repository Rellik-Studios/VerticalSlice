using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Himanshu;
public class SceneChanger : MonoBehaviour
{
    //public GameObject m_player;
    public GameObject ContinueBlock;
    // Start is called before the first frame update
    void Start()
    {
        if(ContinueBlock != null)
        {
            if (!PlayerPrefs.HasKey("SaveFile"))
            {
                ContinueBlock.SetActive(false);

            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPresent()
    {
        if (PlayerPrefs.GetInt("SaveFile") == 0)
        {
            ContinueBlock.SetActive(false);
        }
        else
        {
            ContinueBlock.SetActive(true);
        }
    }
    public void MainScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Death", 0);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Ending()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
    public void DeleteFile()
    {
        PlayerPrefs.DeleteAll();
    }
    //public void Continue()
    //{
    //    //m_player.SetActive(true);
    //    //MIGHT NEED TO REDO
    //    Cursor.lockState = CursorLockMode.Locked;
    //    if (m_player.GetComponent<RespawnManager>() != null)
    //        m_player.GetComponent<RespawnManager>().Respawn();
    //    m_player.GetComponent<PlayerInteract>().m_timeRewind.fillAmount = 0;
    //    m_player.GetComponent<PlayerInteract>().m_timeStop.fillAmount = 0;
    //    //m_player.GetComponent<PlayerInteract>().m_hiding = true;
    //}

    public void loseScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ending();
        }
        
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
