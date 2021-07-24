using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChildLayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeLayersRecursively(this.transform, this.gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeLayersRecursively(Transform trans, int layerNum)
    {
        trans.gameObject.layer = layerNum;
        foreach(Transform child in trans)
        {            
            ChangeLayersRecursively(child, layerNum);
        }
    }
}
