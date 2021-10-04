using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChangEnviroment : MonoBehaviour
{
    public GameObject[] EnvirObject;
    public GameObject[] LocationObject;
    public GameObject DoorTrigger;

    public int Index
    {
        get;
        private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in EnvirObject)
        {
            obj.SetActive(false);
        }
        if(EnvirObject.Length !=0)
        {
            EnvirObject[0].SetActive(true);
        }
        
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
                other.GetComponentInParent<RespawnManager>().Teleport(LocationObject[Index].transform);
                Debug.Log("You have moved to a new location");
            }
            if (Index <= (EnvirObject.Length - 2))
            {
                //disable the object
                EnvirObject[Index].SetActive(false);
                
                Index++;

                //Setting the new location for the player after they go through the hole
                

                //setting the trigger to disable so a certain door doesnt open
                //NOTE: this door is the main room door with the clock
                if(DoorTrigger !=null)
                {
                    DoorTrigger.SetActive(false);
                }
                //after raising the index by one
                EnvirObject[Index].SetActive(true);
            }

            Debug.Log("Environment has changed");
        }
    }
}
