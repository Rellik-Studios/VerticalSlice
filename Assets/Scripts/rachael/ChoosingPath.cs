using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class ChoosingPath : MonoBehaviour
{
    public GameObject m_player;
    public GameObject assignRoom;
    public GameObject assignDoor;
    public ChangeFurniture eraChanging;
    public string assignedName;

    [SerializeField] private int route;
    // Start is called before the first frame update
    void Awake()
    {
      
        if (PlayerPrefs.HasKey(assignedName))
        {
            //
            //PlayerPrefs.SetInt(assignedName, 1);
            route = PlayerPrefs.GetInt(assignedName);
            eraChanging.Rooms[route] = assignRoom;
            assignDoor.GetComponent<DoorToWall>().TransformDoorToWall();

            Destroy(this);
        }
        
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Contact");
        if (other.CompareTag("Player"))
        {
            if (eraChanging != null)
            {
                route = eraChanging.Index;
                eraChanging.Rooms[route] = assignRoom;
                //PlayerPrefs.SetInt(assignedName, route);
                //assignDoor.GetComponent<DoorToWall>().TransformDoorToWall();
                Destroy(this);
            }
        }
    }
}
