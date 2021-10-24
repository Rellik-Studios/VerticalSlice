using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingRing : MonoBehaviour
{
    Image playerRing;

    GameObject GameRing;

    bool isInRing = false;

    public float speed = 1.0f;

    public float lowestRing = 1350.0f;
    public float highestRing = 1550.0f;

    float RingSize;

    float betweenValues; 

    // Start is called before the first frame update
    void Start()
    {
        playerRing = GetComponent<Image>();
        if(FindObjectOfType<QTERing>() != null)
            GameRing = FindObjectOfType<QTERing>().gameObject;
        betweenValues = highestRing - lowestRing;
        RingSize = GameRing.GetComponent<Image>().rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerRing.rectTransform.sizeDelta = new Vector2(playerRing.rectTransform.sizeDelta.x + (speed), playerRing.rectTransform.sizeDelta.y + (speed));
        if (OuterRing() && InnerRing())
        {
            isInRing = true;
        }
        else
        {
            isInRing = false;
        }
        if(playerRing.rectTransform.sizeDelta.x >= 2000.0f || playerRing.rectTransform.sizeDelta.y >= 2000.0f)
        {
            GameRing.GetComponent<QTERing>().LateRing();
            Destroy(gameObject);
        }
    }
    public bool IsRingInisde()
    {
        return isInRing;
    }
    //public bool OuterRing()
    //{
    //    return (playerRing.rectTransform.sizeDelta.x >= lowestRing && playerRing.rectTransform.sizeDelta.y >= lowestRing);

    //}
    //public bool InnerRing()
    //{
    //    return (playerRing.rectTransform.sizeDelta.x <= highestRing && playerRing.rectTransform.sizeDelta.y <= highestRing);

    //}

    public bool OuterRing()
    {
        return (playerRing.rectTransform.sizeDelta.x >= (RingSize -betweenValues) && playerRing.rectTransform.sizeDelta.y >= (RingSize - betweenValues));

    }
    public bool InnerRing()
    {
        return (playerRing.rectTransform.sizeDelta.x <= (RingSize + betweenValues) && playerRing.rectTransform.sizeDelta.y <= (RingSize + betweenValues));

    }
}
