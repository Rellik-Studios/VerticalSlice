using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class Portal : MonoBehaviour
{
    public PlayerInteract m_player;
    public ChangeFurniture changeFurniture;
    public GameObject portalObject;
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

            if(portalObject != null)
                portalObject.SetActive(true);
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;

            if (portalObject != null)
                portalObject.SetActive(false);
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
