using System;
using System.Collections;
using System.Collections.Generic;
using Himanshu;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QTERing : MonoBehaviour
{
    public GameObject playerRing;
    public GameObject Canvas;
    public GameObject DisplayBox;

    bool IsSpawn = true;

    bool IsDecided = false;

    int numOfPass = 0;
    int numOfFail = 0;
    public bool m_result;
    private GameObject m_ring;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        numOfPass = 0;
        numOfFail = 0;
        IsSpawn = true;
        IsDecided = false;
        SetOrigin();
    }
    

    private void Update()
    {
        if (m_ring == null)
            return;

        if (m_ring.GetComponent<GrowingRing>().IsRingInisde()) 
        {
            DisplayBox.GetComponent<Text>().text = "Its in a ring";
            if (Input.GetKeyDown(KeyCode.P))
            {
                numOfPass++;
                //Give the player a chance
                Destroy(m_ring.gameObject);
                IsDecided = true;
                Pass();
            }

        }
        else
        {
            DisplayBox.GetComponent<Text>().text = "Its not in a ring";
            if (Input.GetKeyDown(KeyCode.P))
            {
                numOfFail++;
                //don't give them a chance
                Destroy(m_ring);

                IsDecided = true;
                Fail();
            }
        }
    }

    // Update is called once per frame
   
    void LateUpdate()
    {
        if(IsDecided)
        {
            if(numOfPass >= 1)
            {
                Debug.Log("You get second chance");
                Time.timeScale = 1f;
                m_result = true;
                Canvas.SetActive(false);
            }
            else if(numOfFail >= 1)
            {
                m_result = false;
                Debug.Log("You dont get second chance");
                Time.timeScale = 1f;
                Canvas.SetActive(false);
            }
            else
            {
                SetNewPosition();
                IsSpawn = true;
            }
            IsDecided = false;
        }

        if(IsSpawn)
            StartCoroutine(SpawnRing());
        //playerRing.rectTransform.sizeDelta = new Vector2(playerRing.rectTransform.sizeDelta.x  + (speed), playerRing.rectTransform.sizeDelta.y + (speed));
        //if(OuterRing() && InnerRing())
        //{
        //    Debug.Log("yes");
        //}
    }

    IEnumerator SpawnRing()
    {
        IsSpawn = false;
        float counter = 0f;
        yield return (StartCoroutine(Utility.WaitForRealSeconds(3f)));
        m_ring = Instantiate(playerRing, Canvas.transform);

        m_ring.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(GetComponent<Image>().rectTransform.anchoredPosition.x, GetComponent<Image>().rectTransform.anchoredPosition.y);
    }
    


    private void Pass()
    {
        
    }

    private void Fail()
    {
        
    }

    void SetOrigin()
    {
        GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0,0);
        GetComponent<Image>().rectTransform.sizeDelta = new Vector2(1550.0f , 1550.0f);
    }
    void SetNewPosition()
    {

        float Posx = Random.Range(-200, 200);
        float Posy = Random.Range(-300, 300);
        float size = Random.Range(1000, 1551);

        GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(Posx,Posy);
        GetComponent<Image>().rectTransform.sizeDelta = new Vector2(size, size);


    }
    public void LateRing()
    {
        IsDecided = true;
        numOfFail++;
    }

    //public bool OuterRing()
    //{
    //    return (playerRing.rectTransform.sizeDelta.x >= 1350.0f && playerRing.rectTransform.sizeDelta.y >= 1350.0f);

    //}
    //public bool InnerRing()
    //{
    //    return (playerRing.rectTransform.sizeDelta.x <= 1550.0f && playerRing.rectTransform.sizeDelta.y <= 1550.0f);

    //}
}
