using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresence : MonoBehaviour
{
    private OVRHand hand; 
    private SkinnedMeshRenderer handRenderer;

    private float lowConfidenceTime = 0;
    private float maxLowConfidenceTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        handRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hand.HandConfidence == OVRHand.TrackingConfidence.High) {
        if (hand.IsTracked && hand.HandConfidence == OVRHand.TrackingConfidence.High) { //TODO: Check if controllers active
            bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            float indexFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            OVRHand.TrackingConfidence confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

            Debug.Log(this.name+"\nisIndexFingerPinching: "+isIndexFingerPinching+"\nindexFingerPinchStrength: "+indexFingerPinchStrength +"\nconfidence: "+confidence);

            lowConfidenceTime = 0;
            if (!handRenderer.enabled) {
                handRenderer.enabled = true; 
            }

        } else {
            Debug.Log("Low confidence hand");
            
            lowConfidenceTime += Time.deltaTime;
            if (lowConfidenceTime > maxLowConfidenceTime) {
                handRenderer.enabled = false; 
            }
        }
    }
}
