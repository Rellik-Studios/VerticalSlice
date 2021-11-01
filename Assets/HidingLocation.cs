using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Himanshu;
using UnityEngine;

public class HidingLocation : MonoBehaviour, IInteract
{
    private HidingSpot m_hidingSpot;
    void Start()
    {
        m_hidingSpot = transform.GetComponentInParent<HidingSpot>();
    }

    void Update()
    {
        
    }

    public void Execute(PlayerInteract _player)
    {
        m_hidingSpot.BeginHide(this, _player);
        Debug.Log("Hiding is begining");
    }

    public void TurnOn()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    public void TurnOff()
    {
        GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
}
