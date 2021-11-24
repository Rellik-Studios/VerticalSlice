using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class MyDoorAnimator : MonoBehaviour
{
    private Animator doorAnim;

    private bool doorOpen = false;

    [SerializeField] private List<GameObject> enemiesToEnable;


    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }
    public void PlayAnimation()
    {
        if(!doorOpen)
        {
            OpenTheDoor();
        }
        else
        {
            CloseTheDoor();
        }
    }
    public void CloseTheDoor()
    {
        doorAnim.SetBool("IsOpening", false);
        doorOpen = false;
    }

    public void OpenTheDoor()
    {
        foreach (var enemy in enemiesToEnable)
        {
            enemy.GetComponent<StateMachine>().enabled = true;
        }
        doorAnim.SetBool("IsOpening", true);
        doorOpen = true;
    }
}
