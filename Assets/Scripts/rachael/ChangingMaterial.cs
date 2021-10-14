using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingMaterial : MonoBehaviour
{
    public ChangEnviroment ChanngEnvir;
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
        if(ChanngEnvir.Index < mat.Length)
            rend.sharedMaterial = mat[ChanngEnvir.Index];
    }
}
