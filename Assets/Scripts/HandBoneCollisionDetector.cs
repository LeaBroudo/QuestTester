using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandBoneCollisionDetector : MonoBehaviour
{
    
    public GameObject currentCollidingObj;

    private SphereCollider sphereCollider;

    private PokeEvent CollisionStarted;
    private PokeEvent CollisionEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = this.gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0.01f;
        
        currentCollidingObj = null;

        Debug.Log("started handbonecollisiondetector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEvents(PokeEvent startEvent, PokeEvent endEvent) {

        CollisionStarted = startEvent;
        CollisionEnded = endEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // If it touches another part of the body...
            return;
        
        currentCollidingObj = other.gameObject;
        CollisionStarted.Invoke(currentCollidingObj);
        Debug.Log("TriggerEnterStarted: "+other.gameObject.layer+" "+currentCollidingObj.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) // If it touches another part of the body...
            return;

        CollisionEnded.Invoke(currentCollidingObj);
        Debug.Log("TriggerExitStarted: "+currentCollidingObj.name);
        currentCollidingObj = null;
    }


}
