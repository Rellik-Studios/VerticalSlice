using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;

public class SaveCanvas : MonoBehaviour
{
    [SerializeField] GameObject saveCanvas;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject camObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            saveCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            DisbleMovement();
        }
    }
    public void DisbleMovement()
    {
        playerObject.GetComponent<CharacterController>().enabled = false;
        camObject.GetComponent<PlayerFollow>().enabled = false;
    }
    public void EnableMovement()
    {
        playerObject.GetComponent<CharacterController>().enabled = true;
        camObject.GetComponent<PlayerFollow>().enabled = true;

    }

}
