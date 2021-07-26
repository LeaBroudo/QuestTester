using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOrRight : MonoBehaviour
{
    public bool isRightHand;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isFromRightHand() {
        return isRightHand;
    }

    public bool isFromLeftHand() {
        return !isRightHand;
    }
}
