using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestures : MonoBehaviour
{
    private HandPresence handPresence;
    private OVRHand hand; 
    private OVRSkeleton skeleton;

    public GameObject g1;
    public GameObject g2;
    
    // Start is called before the first frame update
    void Start()
    {
        handPresence = GetComponent<HandPresence>();
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (hand.HandConfidence == OVRHand.TrackingConfidence.Low) { //TODO: transparency for this conditional
        if (handPresence.isHandShown()) {  
            
            CheckIndexFingerPinch();
            CheckIndexFingerPoint();

        } 
    }

    public void CheckIndexFingerPinch() {

        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float indexFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        OVRHand.TrackingConfidence confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

        Debug.Log(skeleton.GetSkeletonType()+"\nisIndexFingerPinching: "+isIndexFingerPinching+"\nindexFingerPinchStrength: "+indexFingerPinchStrength +"\nconfidence: "+confidence);
    }

    public void CheckIndexFingerPoint() {

        if (hand.IsPointerPoseValid) {
            
            Debug.Log("Pointer Pose:" + hand.PointerPose.position);
            g1.transform.position = this.transform.position;
            g2.transform.position = this.transform.position + hand.PointerPose.forward;

        } else {
            Debug.Log("Pointer Pose not valid");
        }
    }
}
