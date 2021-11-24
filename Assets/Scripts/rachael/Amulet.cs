using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
using UnityEngine.Serialization;

public class Amulet : MonoBehaviour, IInteract
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(PlayerInteract _player)
    {

        _player.m_hasAmulet = true;
        Destroy(gameObject);


    }
}
