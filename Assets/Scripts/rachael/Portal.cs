using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class Portal : MonoBehaviour
{
    public PlayerInteract m_player;
    public ChangeFurniture changeFurniture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_player.m_numOfPieces != changeFurniture.Index)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Contact");
        if (other.CompareTag("Player"))
        {
            changeFurniture.EndofTimeEra(other);
        }
    }
}
