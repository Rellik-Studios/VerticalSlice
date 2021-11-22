using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToWall : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject wall;

    //private bool fadeOut = false;
    //public float fadeSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    FadeOutObject();
        //}
        //if(fadeOut)
        //{
        //    Color outDoorColor = this.GetComponent<Renderer>().material.color;
        //    //Color inDoorColor = GetComponentInChildren<Renderer>().material.color;
        //    float fadeAmount = outDoorColor.a - (fadeSpeed * Time.deltaTime);

        //    outDoorColor = new Color(outDoorColor.r, outDoorColor.g, outDoorColor.b, fadeAmount);
        //   // inDoorColor = new Color(inDoorColor.r, inDoorColor.g, inDoorColor.b, fadeAmount);
            

        //    this.GetComponent<Renderer>().material.color = outDoorColor;
        //    //GetComponentInChildren<Renderer>().material.color = inDoorColor;

        //    if(outDoorColor.a <=0)
        //    {
        //        fadeOut = false;
        //    }
        //}
    }

    //public void FadeOutObject()
    //{
    //    fadeOut = true;
    //}
    public void FadeHubWall()
    {
        wall.SetActive(true);
    }
    public void TransformDoorToWall()
    {
        door.SetActive(false);
        wall.SetActive(true);
    }
    public void TransformWallToDoor()
    {
        door.SetActive(true);
        wall.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.SetActive(false);
            wall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
