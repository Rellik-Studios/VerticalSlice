using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingTransition : MonoBehaviour
{
    [SerializeField] GameObject doorframe;
    [SerializeField] GameObject door;
    [SerializeField] GameObject wall;

    private bool fadeOut = false;
    private bool fadeIn = false;
    public float fadeSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Color wallColor = this.wall.GetComponent<Renderer>().material.color;
        wallColor = new Color(wallColor.r, wallColor.g, wallColor.b, 0.0f);

        wall.GetComponent<Renderer>().material.color = wallColor;
        //wall.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            FadeOutObject();
        }
        if(fadeOut)
        {
            Color outDoorColor = this.doorframe.GetComponent<Renderer>().material.color;
            Color inDoorColor = this.door.GetComponent<Renderer>().material.color;
            Color wallColor = this.wall.GetComponent<Renderer>().material.color;

            float fadeOutAmount = outDoorColor.a - (fadeSpeed * Time.deltaTime);
            float fadeInAmount = wallColor.a + (fadeSpeed * Time.deltaTime);

            outDoorColor = new Color(outDoorColor.r, outDoorColor.g, outDoorColor.b, fadeOutAmount);
            inDoorColor = new Color(inDoorColor.r, inDoorColor.g, inDoorColor.b, fadeOutAmount);
            wallColor = new Color(wallColor.r, wallColor.g, wallColor.b, fadeInAmount);


            doorframe.GetComponent<Renderer>().material.color = outDoorColor;
            door.GetComponent<Renderer>().material.color = inDoorColor;
            wall.GetComponent<Renderer>().material.color = wallColor;

            if (outDoorColor.a <=0)
            {
                fadeOut = false;
                door.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    public void FadeOutObject()
    {
        fadeOut = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeOutObject();
            wall.SetActive(true);
        }
    }
}
