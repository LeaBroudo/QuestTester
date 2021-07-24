using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PokeEvent : UnityEvent<GameObject> {}

public class HandCollisions : MonoBehaviour
{
    private OVRSkeleton skeleton;
    
    public PokeEvent IndexCollisionStarted;
    public PokeEvent IndexCollisionEnded;
 
    void Start() {
        
        skeleton = GetComponent<OVRSkeleton>();
        
        if (IndexCollisionStarted == null)
            IndexCollisionStarted = new PokeEvent();

        if (IndexCollisionEnded == null)
            IndexCollisionEnded = new PokeEvent();
        
        // Add collider to tip of index finger
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip) {
                
                HandBoneCollisionDetector detector = bone.Transform.gameObject.AddComponent<HandBoneCollisionDetector>() as HandBoneCollisionDetector;
                detector.SetEvents(IndexCollisionStarted, IndexCollisionEnded);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
