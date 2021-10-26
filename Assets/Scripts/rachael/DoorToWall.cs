using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToWall : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
