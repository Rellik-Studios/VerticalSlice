using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingMaterial : MonoBehaviour
{
    public ChangeFurniture ChangeEnvir;
    public Material[] mat;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ChangeEnvir.Index < mat.Length)
            rend.sharedMaterial = mat[ChangeEnvir.Index];
    }
}
