using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabber : OVRGrabber 
{
    
    
    private HandGestures handGestures;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        handGestures = GetComponent<HandGestures>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckToGrab();
    }

    void CheckToGrab() {

        // Debug.Log("isPinching: "+handGestures.isIndexPinching+"\nGrabCandid: "+m_grabCandidates.Count);
        // Debug.Log("GripTrans: "+m_gripTransform+"\nm_grabVolumes: "+m_grabVolumes.Length);
        //if (!m_grabbedObj && handGestures.isIndexPinching && m_grabCandidates.Count > 0) {
        if (handGestures.isIndexPinching && m_grabCandidates.Count > 0) {
            GrabBegin();
        }
        else if (m_grabbedObj != null && !handGestures.isIndexPinching) {
            GrabEnd(); 
        }
    }

    protected override void GrabEnd()
    {
        if (m_grabbedObj) {
            
            Vector3 linearVelocity = (transform.position - m_lastPos) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles) / Time.fixedDeltaTime;
        
            GrabbableRelease(linearVelocity, angularVelocity);
        }

        GrabVolumeEnable(true);
    }

    public void AddGripTransform(Transform gripTrans) {

        m_gripTransform = gripTrans;
    }

    public void AddGrabVolumes(List<Collider> grabVols) {

        m_grabVolumes = grabVols.ToArray();
    }

    public override void OnTriggerEnter(Collider otherCollider) {
        base.OnTriggerEnter(otherCollider);
    }

    public override void OnTriggerExit(Collider otherCollider) {
        base.OnTriggerExit(otherCollider);
    }
}
