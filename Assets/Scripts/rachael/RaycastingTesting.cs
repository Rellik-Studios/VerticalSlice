using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Himanshu;
public class RaycastingTesting : MonoBehaviour
{
    [SerializeField] private float raydist = 5.0f;
    public GameObject ObjectInFront { get; private set; }
    public GameObject player;
    public Image m_indication;

    void Start()
    {
        ObjectInFront = null;
    }

    // Update is called once per frame
    void Update()
    {
        //detecting all hits from raycast
        Debug.DrawRay(transform.position, transform.forward * raydist, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raydist);

        /*
        foreach (var hit in hits)
        {
            //hit object doesnt detect itself
            if (hit.collider.gameObject != gameObject)
            {
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                //Debug.Log($"hit {hit.collider.gameObject.name}");
                ObjectInFront = hit.collider.gameObject;
                break;
            }
        }
        */

        //if they arent any hits
        if (hits.Length == 0)
        {
            ObjectInFront = null;
            m_indication.enabled = false;
            return;
        }

        //if there is a hit take the first one as a default
        RaycastHit closestHit = hits[0];
        m_indication.enabled = true;

        //find the closest hit
        foreach (var hit in hits)
        {
            if (closestHit.distance > hit.distance)

                closestHit = hit;
        }
        ObjectInFront = closestHit.collider.gameObject;
        ObjectIdentify();

    }
    void ObjectIdentify()
    {
        if(ObjectInFront.GetComponent<IInteract>() != null)
        {

            //if its an item
            if (ObjectInFront.GetComponent<CollectableObject>() != null)
            {
                m_indication.sprite = Resources.Load<Sprite>("Pickup");
                return;
            }
            //if its a hiding spot
            if(ObjectInFront.GetComponent<HidingSpot>() != null)
            {
                //if inflected
                //if(ObjectInFront.GetComponent<HidingSpot>().infectStared )
                if(!ObjectInFront.GetComponent<HidingSpot>().isActive)
                {
                    m_indication.sprite = Resources.Load<Sprite>("Rewind");
                    return;
                }
                //non infected
                else
                {
                    m_indication.sprite = Resources.Load<Sprite>("Hide");
                    return;
                }
            }
            //if its a placing an item on clock
            if (ObjectInFront.GetComponent<GrandfatherClock>() != null)
            {
                //if the gear is within possession
                if (player.GetComponent<PlayerInteract>().m_placedDown)
                {
                    m_indication.sprite = Resources.Load<Sprite>("Place");
                    return;
                }
                // no gear
                else
                {
                    m_indication.enabled = false;
                    return;
                }
            }

        }
        else if (ObjectInFront.GetComponent<IEnemy>() != null)
        {
            m_indication.sprite = Resources.Load<Sprite>("Stop");
            //show UI
            return;
        }
        else
        {
            
            m_indication.enabled = false;
            return;
            //show no UI
        }
    }
    void ObjectInteraction(LayerMask layer)
    {
        if(LayerMask.NameToLayer("Item") == layer)
        {
            print("You found a Item");
            return;
            
        }
        if (LayerMask.NameToLayer("HidingSpot") == layer)
        {
            print("You found a Hiding Spot");
            return;
        }



    }
}
