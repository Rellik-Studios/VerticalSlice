using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorAnimator : MonoBehaviour
{
    private Animator doorAnim;

    private bool doorOpen = false;

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
        doorAnim.SetBool("IsOpening", true);
        doorOpen = true;
    }
}
