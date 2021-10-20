using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTERing : MonoBehaviour
{
    public GameObject playerRing;
    public GameObject Canvas;
    public GameObject DisplayBox;

    bool IsSpawn = true;

    bool IsDecided = false;

    int numOfPass = 0;
    int numOfFail = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        numOfPass = 0;
        numOfFail = 0;
        IsSpawn = true;
        IsDecided = false;
        SetOrigin();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDecided)
        {

            if(numOfPass ==3)
            {
                Debug.Log("You get second chance");
                Canvas.SetActive(false);
            }
            else if(numOfFail ==3)
            {
                Debug.Log("You dont get second chance");
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
        yield return new WaitForSeconds(3.0f);
        GameObject temp = Instantiate(playerRing, Canvas.transform);

        temp.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(GetComponent<Image>().rectTransform.anchoredPosition.x, GetComponent<Image>().rectTransform.anchoredPosition.y);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<GrowingRing>() !=null)
        {
            if(other.GetComponent<GrowingRing>().IsRingInisde())
            {
                DisplayBox.GetComponent<Text>().text = "Its in a ring";
                if (Input.GetKeyDown(KeyCode.P))
                {

                    numOfPass++;
                    //Give the player a chance
                    Destroy(other.gameObject);
                    IsDecided = true;
                }

            }
            else
            {
                DisplayBox.GetComponent<Text>().text = "Its not in a ring";
                if (Input.GetKeyDown(KeyCode.P))
                {
                    numOfFail++;
                    //don't give them a chance
                    Destroy(other.gameObject);

                    IsDecided = true;
                }
            }
        }
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
