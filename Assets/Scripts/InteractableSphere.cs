using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSphere : MonoBehaviour, IHandInteractable, IHandGrabbable, IHandGestureable
{
    private MeshRenderer meshRenderer;
    public bool isBeingGrabbed { get; set; }

    public float kAnimSpeed { get; set; }

    public Transform transToFollow { get; set; }
    public IEnumerator GrabAnimation { get; set; }
    public IEnumerator ReleaseAnimation { get; set; }

    public bool isHandPointing { get; set; }
    public bool isIndexPinching { get; set; }
    public static HandGestures handGestures;

    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        kAnimSpeed = 0.3f;
        isBeingGrabbed = false;

        if (handGestures == null) {
            GameObject.Find();
        }
    }

    // Update is called once per frame
    void Update()
    {
        DuringGrab();
    }

    public void CollisionStarted(GameObject bodyPart, Collision collision, HandOutputArguments handArgs) {
        Debug.Log("Sphere touched: "+bodyPart);
        //meshRenderer.materials[0] = actionMat;
        meshRenderer.material.color = Color.red;

        // TODO: check that it's index collision
        // if (handArgs.GetGestureProperties().isIndexPinching) {
        //     StartGrab(bodyPart.transform);
        // }

        //StartGrab(bodyPart.transform);
    }

    public void CollisionEnded(GameObject bodyPart, Collision collision, HandOutputArguments handArgs) {
        Debug.Log("Sphere exited: "+bodyPart);
        //meshRenderer.materials[0] = baseMat;
        meshRenderer.material.color = Color.white;
    }

    public void StartGrab(Transform transToFollow) {

        Debug.Log("started grab");
        if (ReleaseAnimation != null)
        {
            StopCoroutine(ReleaseAnimation);
            ReleaseAnimation = null;
        }

        isBeingGrabbed = true;

        GrabAnimation = MoveAToB(this.transform, transToFollow);
        StartCoroutine(GrabAnimation);
    }

    public void DuringGrab() {

        if (isBeingGrabbed && GrabAnimation == null) {
            
            Debug.Log("during grab");
            this.transform.position = transToFollow.position;
            this.transform.rotation = transToFollow.rotation;
        }
    }
    
    public void EndGrab(Transform posToReturnTo) {

        isBeingGrabbed = false;
        if (GrabAnimation != null)
            StopCoroutine(GrabAnimation);

        ReleaseAnimation = MoveAToB(this.transform, posToReturnTo);
        StartCoroutine(ReleaseAnimation);
    }

    public void EndGrab() {

        isBeingGrabbed = false;
        if (GrabAnimation != null)
            StopCoroutine(GrabAnimation);
    }

    public IEnumerator MoveAToB(Transform a, Transform b) {

        float distToTarget = Vector3.Distance(a.position, b.position);
        while (distToTarget > 0.001f)
        {
            Debug.Log("moving...");
            a.position = Vector3.Lerp(a.position, b.position, kAnimSpeed);
            distToTarget = Vector3.Distance(a.position, b.position);
            yield return new WaitForFixedUpdate();
        }

        GrabAnimation = null;
        yield return null;
    }


    public void SubscribeToEvents() {

    }
    public void IndexPinchingStart() {

    }
    public void IndexPinchingEnd() {

    }
    public void IndexIsPinching() {

    }

    public void HandPointingStarted() {
        
    }
    public void HandPointingEnded() {

    }
    public void HandIsPointing() {

    }
}
