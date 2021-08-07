using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSphere : MonoBehaviour, IHandInteractable, IHandGestureable, IHandGrabbable
{
    private MeshRenderer meshRenderer;
    public bool isBeingGrabbed { get; set; }

    public float kAnimSpeed { get; set; }

    public GameObject objToFollow { get; set; }
    public IEnumerator GrabAnimation { get; set; }
    public IEnumerator ReleaseAnimation { get; set; }

    public bool isHandPointingRight { get; set; }
    public bool isHandPointingLeft { get; set; }
    public bool isIndexPinchingRight { get; set; }
    public bool isIndexPinchingLeft { get; set; }


    public GameObject rightIndexColliding { get; set; }
    public GameObject leftIndexColliding { get; set; }

    

    

    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        kAnimSpeed = 0.3f;
        isBeingGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightIndexColliding != null && isIndexPinchingRight) {
            StartGrab(rightIndexColliding);
        } 
        else if (leftIndexColliding != null && isIndexPinchingLeft) {
            StartGrab(leftIndexColliding);
        } 
        else {
            DuringGrab();
        }

        
    }

    public void IndexCollisionStarted(LeftOrRight leftOrRight, GameObject bodyPart, Collision collision) {
        
        if (leftOrRight.isFromRightHand()) {
            rightIndexColliding = bodyPart;
        } 
        else {
            leftIndexColliding = bodyPart;
        }
        
        //Debug.Log("Sphere touched: "+bodyPart);
        meshRenderer.material.color = Color.red;

    }

    public void IndexCollisionEnded(LeftOrRight leftOrRight, GameObject bodyPart, Collision collision) {
        //Debug.Log("Sphere exited: "+bodyPart);
        meshRenderer.material.color = Color.white;

        if (leftOrRight.isFromRightHand()) {
            rightIndexColliding = null;
        } 
        else {
            leftIndexColliding = null;
        }
    }

    public void StartGrab(GameObject objToFollow) {

        //Debug.Log("started grab");
        if (ReleaseAnimation != null)
        {
            StopCoroutine(ReleaseAnimation);
            ReleaseAnimation = null;
        }

        isBeingGrabbed = true;

        GrabAnimation = MoveAToB(this.gameObject, objToFollow);
        StartCoroutine(GrabAnimation);
    }

    public void DuringGrab() {

        if (isBeingGrabbed && GrabAnimation == null) {
            
            //Debug.Log("during grab");
            this.transform.position = objToFollow.transform.position;
            this.transform.rotation = objToFollow.transform.rotation;
        }
    }
    
    // public void EndGrab(GameObje posToReturnTo) {

    //     if (!isBeingGrabbed) return;
        
    //     isBeingGrabbed = false;
    //     if (GrabAnimation != null)
    //         StopCoroutine(GrabAnimation);

    //     ReleaseAnimation = MoveAToB(this.transform, posToReturnTo);
    //     StartCoroutine(ReleaseAnimation);
    // }

    public void EndGrab() {

        isBeingGrabbed = false;
        if (GrabAnimation != null)
            StopCoroutine(GrabAnimation);

    }

    public IEnumerator MoveAToB(GameObject a, GameObject b) {

        // float distToTarget = Vector3.Distance(a.transform.position, b.transform.position);
        // while (distToTarget > 0.001f)
        // {
        //     Debug.Log("moving...");
        //     a.transform.position = Vector3.Lerp(a.transform.position, b.transform.position, kAnimSpeed);
        //     distToTarget = Vector3.Distance(a.transform.position, b.transform.position);
        //     yield return new WaitForFixedUpdate();
        // }
        a.transform.position = b.transform.position;
        GrabAnimation = null;
        yield return null;
    }

    
    public void SubscribeToEvents(
        GestureEvent indexPinchingStarted,
        GestureEvent indexPinchingEnded,
        GestureEvent indexIsPinching,
        PointingEvent handPointingStarted,
        PointingEvent handPointingEnded,
        PointingEvent handIsPointing) 
    {
        indexPinchingStarted.AddListener(IndexPinchingStart);
        indexPinchingEnded.AddListener(IndexPinchingEnd);
        indexIsPinching.AddListener(IndexIsPinching);
        handPointingStarted.AddListener(HandPointingStarted);
        handPointingEnded.AddListener(HandPointingEnded);
        handIsPointing.AddListener(HandIsPointing);
    }
    
    public void IndexPinchingStart(LeftOrRight leftOrRight) {

        if (leftOrRight.isFromRightHand()) {
            isIndexPinchingRight = true;
        } else {
            isIndexPinchingLeft = true;
        }
        //Debug.Log("index start pinch");
    }
    public void IndexPinchingEnd(LeftOrRight leftOrRight) {
        
        if (leftOrRight.isFromRightHand()) {
            isIndexPinchingRight = false;
        } else {
            isIndexPinchingLeft = false;
        }
        //Debug.Log("index end pinch");

        EndGrab();
    }
    public void IndexIsPinching(LeftOrRight leftOrRight) {
        
    }

    public void HandPointingStarted(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos) {}
    public void HandPointingEnded(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos) {}
    public void HandIsPointing(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos) {}
}
