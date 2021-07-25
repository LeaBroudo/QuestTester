using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOutputArguments : MonoBehaviour
{
    private HandGestures handGestures;
    private HandCollisions handCollisions;
    
    // Start is called before the first frame update
    void Start()
    {
        handGestures = this.gameObject.GetComponent<HandGestures>();
        handCollisions = this.gameObject.GetComponent<HandCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GestureProperties GetGestureProperties() {
        return new GestureProperties(handGestures);
    } 
}

public class GestureProperties 
{
    public bool isIndexPinching;
    public bool isHandPointing;
    public GestureProperties(HandGestures hg) {
        isIndexPinching = hg.isIndexPinching;
        isHandPointing = hg.isHandPointing;
    }
}