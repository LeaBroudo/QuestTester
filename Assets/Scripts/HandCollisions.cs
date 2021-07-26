using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PokeEvent : UnityEvent<GameObject> {}

public class HandCollisions : MonoBehaviour
{
    private OVRSkeleton skeleton;
    public HashSet<HandBoneCollisionDetector> collisionDetectors;
    private LeftOrRight leftOrRight; 
 
    void Start() {
        
        Physics.IgnoreLayerCollision(7, 7);

        skeleton = GetComponent<OVRSkeleton>();
        leftOrRight = GetComponent<LeftOrRight>();

        collisionDetectors = new HashSet<HandBoneCollisionDetector>();
        
        // Add collider to tip of index finger
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip) {
                
                HandBoneCollisionDetector detector = bone.Transform.gameObject.AddComponent<HandBoneCollisionDetector>() as HandBoneCollisionDetector;
                detector.SetLeftOrRight(leftOrRight);

                collisionDetectors.Add(detector);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
