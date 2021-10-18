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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Instantiate(playerRing, Canvas.transform);
        yield return new WaitForSeconds(3.0f);
        IsSpawn = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<GrowingRing>() !=null)
        {
            if(other.GetComponent<GrowingRing>().IsRingInisde())
            {
                DisplayBox.GetComponent<Text>().text = "Its in a ring";
                if (Input.GetKeyDown(KeyCode.P))
                    Destroy(other.gameObject);

            }
        }
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
