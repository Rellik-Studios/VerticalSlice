using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
public class Gauge : MonoBehaviour
{
    public PlayerInteract playerinteract;
    public GameObject gauge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerinteract.dangerBarVal ==0)
        {
            gauge.SetActive(false);
        }
        else
        {
            gauge.SetActive(true);
        }
    }
}
