using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoneCollisionDetector : MonoBehaviour
{

    private Transform transformToFollow;
    private HandGrabber handGrabber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transformToFollow != null) {
            this.transform.position = transformToFollow.position;
        }
    }

    public void SetTransformToFollow(Transform trans) {
        transformToFollow = trans;
    }

    void OnTriggerEnter(Collider otherCollider) {
        Debug.Log("Triggered!!");
        handGrabber.OnTriggerEnter(otherCollider);
    }

    void OnTriggerExit(Collider otherCollider) {
        Debug.Log("Trigger exit!!");
        handGrabber.OnTriggerExit(otherCollider);
    }

    public void SetCollisionCallback(HandGrabber _handGrabber) {
        handGrabber = _handGrabber;
    }
}
