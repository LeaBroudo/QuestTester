using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresence : MonoBehaviour
{
    public bool shouldShowHandPrefabs = false;
    public OVRHand hand; 
    private SkinnedMeshRenderer handRenderer;


    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        handRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hand.HandConfidence == OVRHand.TrackingConfidence.Low) { //TODO: transparency for this conditional
        // if (hand.IsTracked && !isControllerConnected()) {  
        //     bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        //     float indexFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        //     OVRHand.TrackingConfidence confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

        //     Debug.Log(this.name+"\nisIndexFingerPinching: "+isIndexFingerPinching+"\nindexFingerPinchStrength: "+indexFingerPinchStrength +"\nconfidence: "+confidence);

            

        // } else {
            
        // }
    }

    public void ShowHandPrefab() {

        if (!handRenderer.enabled) {
            handRenderer.enabled = true; 
        }
    }

    public void HideHandPrefab() {
        
        Debug.Log("Not tracking hands or show controller instead.");
            
        if (handRenderer.enabled) {
            handRenderer.enabled = false; 
        }
    }
}
