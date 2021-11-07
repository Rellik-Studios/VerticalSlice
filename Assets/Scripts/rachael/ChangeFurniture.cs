using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurniture : MonoBehaviour
{
    public GameObject[] LoopObjects;
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
            LoopObjects[0].SetActive(true);
        }

        //SavingTimeEra();

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

                //making the player appear in a room with no door
                roomNoDoor.SetActive(true);
                roomMain.SetActive(false);

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
