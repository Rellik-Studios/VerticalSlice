using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurniture : MonoBehaviour
{
    public GameObject[] LoopObjects;
    public GameObject[] Rooms;
    public GameObject[] HUB_Doors;
    public GameObject roomMain;
    public GameObject roomNoDoor;
    public GameObject LocationPosition;

    public bool noDoor = false;

    public int Index
    {
        get;
        private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in LoopObjects)
        {
            obj.SetActive(false);
        }
        if (LoopObjects.Length != 0)
        {
            LoopObjects[Index].SetActive(true);
        }

        //SavingTimeEra();

    }

    public void EndofTimeEra(Collider other)
    {
        if (other.GetComponentInParent<RespawnManager>() != null)
        {
            
            other.GetComponentInParent<RespawnManager>().Teleport(LocationPosition.transform);


            Debug.Log("You have moved to a new location");
        }
        if (Index <= (LoopObjects.Length - 2))
        {
            //disable the object
            LoopObjects[Index].SetActive(false);

            Index++;

            LoopObjects[Index].SetActive(true);

        }

        StartCoroutine(SavingProgress(other));
        //closing the doors in the gameplay after each loop
        foreach (GameObject door in HUB_Doors)
        {

            door.GetComponent<MyDoorAnimator>().CloseTheDoor();
        }

        Debug.Log("Player has finished this time era");
    }

    //to allow the saving of the rotation and position more properly in the
    IEnumerator SavingProgress(Collider other)
    {
        yield return new WaitForEndOfFrame();
        if (other.GetComponentInParent<PlayerSave>() != null)
        {
            //saves the player data into the system
            //other.GetComponentInParent<PlayerSave>().SavePlayer(true);
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 200f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Contact");
        if (other.CompareTag("Player"))
        {
            if (other.GetComponentInParent<RespawnManager>() != null)
            {
                other.GetComponentInParent<RespawnManager>().Teleport(LocationPosition.transform);
                
                Debug.Log("You have moved to a new location");
            }
            if (Index <= (LoopObjects.Length - 2))
            {
                //disable the object
                LoopObjects[Index].SetActive(false);

                Index++;

                //Setting the new location for the player after they go through the hole

                //after raising the index by one
                LoopObjects[Index].SetActive(true);

                ////making the player appear in a room with no door
                //roomNoDoor.SetActive(true);
                //roomMain.SetActive(false);

            }
            if(other.GetComponentInParent<PlayerSave>() != null)
            {
                //saves the player data into the system
                other.GetComponentInParent<PlayerSave>().SavePlayer();
            }

            Debug.Log("Time Era has changed");
        }
    }
    public void MakeDoorAppear()
    {
        roomMain.SetActive(true);
        roomNoDoor.SetActive(false);
    }

    //ONLY FOR PLAYER PREF
    public void SavingTimeEra()
    {
        //for the saving purposes---------------
        if (PlayerPrefs.HasKey("Index") == true)
        {
            Index = PlayerPrefs.GetInt("Index");
            if (Index <= (LoopObjects.Length - 2))
            {

                foreach (GameObject obj in LoopObjects)
                {
                    obj.SetActive(false);
                }
                LoopObjects[Index].SetActive(true);

            }
        }
        else
        {
            PlayerPrefs.SetInt("Index", Index);
        }
        //---------------------------------------
    }


    public void SaveIndex(int num)
    {
        Index = num;
    }
    public void LoadTimeEra()
    {
        if (Index <= (LoopObjects.Length - 2))
        {

            foreach (GameObject obj in LoopObjects)
            {
                obj.SetActive(false);
            }

            LoopObjects[Index].SetActive(true);

        }
    }

}
