using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandBoneCollisionDetector : MonoBehaviour
{
    
    private SphereCollider sphereCollider;

    private PokeEvent CollisionStarted;
    private PokeEvent CollisionEnded;

    public HashSet<GameObject> collidedObjects;
    public int numCollidedObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = this.gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0.01f;

        collidedObjects = new HashSet<GameObject>();
        numCollidedObjects = 0;
        
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
        
        collidedObjects.Add(other.gameObject);
        numCollidedObjects++;

        CollisionStarted.Invoke(other.gameObject);
        Debug.Log("TriggerEnterStarted: "+other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) // If it touches another part of the body...
            return;

        CollisionEnded.Invoke(other.gameObject);
        Debug.Log("TriggerExitStarted: "+other.gameObject.name);
        
        collidedObjects.Remove(other.gameObject);
        numCollidedObjects--;
    }


}
