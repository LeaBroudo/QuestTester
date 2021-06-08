using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestures : MonoBehaviour
{
    private HandPresence handPresence;
    
    // Start is called before the first frame update
    void Start()
    {
        handPresence = GetComponent<HandPresence>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
