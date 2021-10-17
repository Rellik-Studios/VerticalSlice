using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qteSystem : MonoBehaviour
{

    public GameObject DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;


    // Update is called once per frame
    void Update()
    {
        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            //StartCoroutine(CountDown());
            if (QTEGen == 1)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[P]";
            }
        }

        if (QTEGen == 1)
        {
            if(Input.anyKeyDown)
            {
                if(Input.GetKeyDown(KeyCode.P))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

            
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if(CorrectKey ==1)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Pass!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;

        }
        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Fail!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;

        }
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3.5f);
        if(CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Too slow";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

}


