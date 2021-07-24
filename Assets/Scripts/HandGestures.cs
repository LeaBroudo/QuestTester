using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class PointingEvent : UnityEvent<Transform, Vector3> {}

public class HandGestures : MonoBehaviour
{

    private HandPresence handPresence;
    private OVRHand hand; 
    private OVRSkeleton skeleton;

    public GameObject g1;
    public GameObject g2;

    public UnityEvent IndexPinchingStarted;
    public UnityEvent IndexPinchingEnded;
    public UnityEvent IndexIsPinching;
    private bool isIndexPinching;

    public PointingEvent HandPointingStarted;
    public PointingEvent HandPointingEnded;
    public PointingEvent HandIsPointing;
    private bool isHandPointing;
    

    
    // Start is called before the first frame update
    void Start()
    {
        handPresence = GetComponent<HandPresence>();
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();

        isIndexPinching = false;

        if (IndexPinchingStarted == null)
            IndexPinchingStarted = new UnityEvent();

        if (IndexPinchingEnded == null)
            IndexPinchingEnded = new UnityEvent();
        
        if (IndexIsPinching == null)
            IndexIsPinching = new UnityEvent();
        
        isHandPointing = false;

        if (HandPointingStarted == null)
            HandPointingStarted = new PointingEvent();

        if (HandPointingEnded == null)
            HandPointingEnded = new PointingEvent();
        
        if (HandIsPointing == null)
            HandIsPointing = new PointingEvent();

    }

    // Update is called once per frame
    void Update()
    {
        //if (hand.HandConfidence == OVRHand.TrackingConfidence.Low) { //TODO: transparency for this conditional
        if (handPresence.isHandShown()) {  
            
            CheckIndexFingerPinch();
            CheckHandPoint();

        } 
    }

    public void CheckIndexFingerPinch() {

        bool isIndexPinching_new = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float indexFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        OVRHand.TrackingConfidence confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

        //Debug.Log(skeleton.GetSkeletonType()+"\nisIndexFingerPinching: "+isIndexPinching_new+"\nindexFingerPinchStrength: "+indexFingerPinchStrength +"\nconfidence: "+confidence);
    
        if (confidence == OVRHand.TrackingConfidence.High) {

            if (isIndexPinching_new && !isIndexPinching) {
                //Debug.Log("IndexPinchingStarted");
                IndexPinchingStarted.Invoke();
            } 
            else if (!isIndexPinching_new && isIndexPinching) {
                //Debug.Log("IndexPinchingEnded");
                IndexPinchingEnded.Invoke();
            } 
            
            if (isIndexPinching_new) {
                //Debug.Log("IndexIsPinching");
                IndexIsPinching.Invoke();
            }

            isIndexPinching = isIndexPinching_new;
        } 
    
    }

    public void CheckHandPoint() {

            
        //Debug.Log("Pointer Pose:" + hand.PointerPose.position);
        g1.transform.position = this.transform.position;
        g2.transform.position = this.transform.position + hand.PointerPose.forward;

        bool isHandPointing_new = hand.IsPointerPoseValid;

        if (isHandPointing_new && !isHandPointing) {
            //Debug.Log("HandPointingStarted");
            HandPointingStarted.Invoke(hand.PointerPose, hand.gameObject.transform.position);
        } 
        else if (!isHandPointing_new && isHandPointing) {
            //Debug.Log("HandPointingEnded");
            HandPointingEnded.Invoke(hand.PointerPose, hand.gameObject.transform.position);
        } 
        
        if (isHandPointing_new) {
            //Debug.Log("HandIsPointing");
            HandIsPointing.Invoke(hand.PointerPose, hand.gameObject.transform.position);
        }

        isHandPointing = isHandPointing_new;
    
    }
}
