using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Himanshu;
using UnityEngine;

public class HidingLocation : MonoBehaviour, IInteract
{
    private HidingSpot m_hidingSpot;
    [SerializeField] private bool m_futuristic;

    private bool visible
    {
        set
        {
            m_hidingSpot.m_animator.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = value;
        }
        
    }

    public Vector3 actualForward => GetComponent<CinemachineVirtualCamera>() != null ? transform.forward : transform.GetChild(0).forward;

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
        if(m_futuristic)
            StartCoroutine(eDisableRenderer(_player));
    }

    private IEnumerator eDisableRenderer(PlayerInteract _player)
    {
        yield return new WaitUntil(() => _player.m_hiding);
        visible = false;
    }

    public void TurnOn()
    {
        if(GetComponent<CinemachineVirtualCamera>() != null)
            GetComponent<CinemachineVirtualCamera>().enabled = true;
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<CinemachineVirtualCamera>() != null)
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    public void TurnOff()
    {
        if(GetComponent<CinemachineVirtualCamera>() != null)
            GetComponent<CinemachineVirtualCamera>().enabled = false;
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<CinemachineVirtualCamera>() != null)
            transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().enabled = false;
        if(m_futuristic)
            visible = true;
    }
}
