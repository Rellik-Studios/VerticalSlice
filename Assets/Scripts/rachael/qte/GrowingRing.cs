using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingRing : MonoBehaviour
{
    Image playerRing;

    bool isInRing = false;

    public float speed = 1.0f;

    public float lowestRing = 1350.0f;
    public float highestRing = 1550.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRing = GetComponent<Image>();
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
            Destroy(gameObject);
        }
    }
    public bool IsRingInisde()
    {
        return isInRing;
    }
    public bool OuterRing()
    {
        return (playerRing.rectTransform.sizeDelta.x >= lowestRing && playerRing.rectTransform.sizeDelta.y >= lowestRing);

    }
    public bool InnerRing()
    {
        return (playerRing.rectTransform.sizeDelta.x <= highestRing && playerRing.rectTransform.sizeDelta.y <= highestRing);

    }
}
