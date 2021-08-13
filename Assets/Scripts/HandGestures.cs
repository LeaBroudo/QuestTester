using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class PointingEvent : UnityEvent<LeftOrRight, Transform, Vector3> {}
public class GestureEvent : UnityEvent<LeftOrRight> {}

public class HandGestures : MonoBehaviour
{

    private HandPresence handPresence;
    private OVRHand hand; 
    private OVRSkeleton skeleton;

    public GameObject g1;
    public GameObject g2;

    public GestureEvent IndexPinchingStarted;
    public GestureEvent IndexPinchingEnded;
    public GestureEvent IndexIsPinching;
    public bool isIndexPinching;

    public PointingEvent HandPointingStarted;
    public PointingEvent HandPointingEnded;
    public PointingEvent HandIsPointing;
    public bool isHandPointing;
    
    private LeftOrRight leftOrRight; 
    
    // Start is called before the first frame update
    void Start()
    {
        handPresence = GetComponent<HandPresence>();
        hand = GetComponent<OVRHand>();
        skeleton = GetComponent<OVRSkeleton>();
        leftOrRight = GetComponent<LeftOrRight>();

        isIndexPinching = false;

        if (IndexPinchingStarted == null)
            IndexPinchingStarted = new GestureEvent();

        if (IndexPinchingEnded == null)
            IndexPinchingEnded = new GestureEvent();
        
        if (IndexIsPinching == null)
            IndexIsPinching = new GestureEvent();
        
        isHandPointing = false;

        if (HandPointingStarted == null)
            HandPointingStarted = new PointingEvent();

        if (HandPointingEnded == null)
            HandPointingEnded = new PointingEvent();
        
        if (HandIsPointing == null)
            HandIsPointing = new PointingEvent();

        var gestureableObjects = FindObjectsOfType<InteractableSphere>(true);
        for(int i = 0; i < gestureableObjects.Length; i++)
        {
            gestureableObjects[i].gameObject.GetComponent<InteractableSphere>().SubscribeToEvents(
                IndexPinchingStarted,
                IndexPinchingEnded,
                IndexIsPinching,
                HandPointingStarted,
                HandPointingEnded,
                HandIsPointing
            );
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (hand.HandConfidence == OVRHand.TrackingConfidence.Low) { //TODO: transparency for this conditional
        if (handPresence.isHandShown()) {  
            
            CheckIndexFingerPinch();
            CheckHandPoint();

        } 
        else {
            isHandPointing = false;
            isIndexPinching = false;
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
                IndexPinchingStarted.Invoke(leftOrRight);
            } 
            else if (!isIndexPinching_new && isIndexPinching) {
                //Debug.Log("IndexPinchingEnded");
                IndexPinchingEnded.Invoke(leftOrRight);
            } 
            
            if (isIndexPinching_new) {
                //Debug.Log("IndexIsPinching");
                IndexIsPinching.Invoke(leftOrRight);
            }

            isIndexPinching = isIndexPinching_new;
        } 
    
    }

    public void CheckHandPoint() {

            
        //Debug.Log("Pointer Pose:" + hand.PointerPose.position);
        if (g1 != null) g1.transform.position = this.transform.position;
        if (g2 != null) g2.transform.position = this.transform.position + hand.PointerPose.forward;

        bool isHandPointing_new = hand.IsPointerPoseValid;

        if (isHandPointing_new && !isHandPointing) {
            //Debug.Log("HandPointingStarted");
            HandPointingStarted.Invoke(leftOrRight, hand.PointerPose, hand.gameObject.transform.position);
        } 
        else if (!isHandPointing_new && isHandPointing) {
            //Debug.Log("HandPointingEnded");
            HandPointingEnded.Invoke(leftOrRight, hand.PointerPose, hand.gameObject.transform.position);
        } 
        
        if (isHandPointing_new) {
            //Debug.Log("HandIsPointing");
            HandIsPointing.Invoke(leftOrRight, hand.PointerPose, hand.gameObject.transform.position);
        }

        isHandPointing = isHandPointing_new;
    
    }
}
