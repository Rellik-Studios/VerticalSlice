using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
public class TrapDoor : MonoBehaviour
{
    PlayerInteract _player;
    public int m_assignedNum;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.m_numOfPieces == m_assignedNum)
        {
            Destroy(gameObject);
        }
    }
}
