using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Himanshu;
public class SceneChanger : MonoBehaviour
{
    public GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Ending()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
    public void Continue()
    {
        m_player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        if (m_player.GetComponent<RespawnManager>() != null)
            m_player.GetComponent<RespawnManager>().Respawn();
        m_player.GetComponent<PlayerInteract>().m_timeRewind.fillAmount = 0;
        m_player.GetComponent<PlayerInteract>().m_timeStop.fillAmount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ending();
        }
        
    }
}
