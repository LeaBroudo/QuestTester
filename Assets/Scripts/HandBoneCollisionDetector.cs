using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandBoneCollisionDetector : MonoBehaviour
{
    
    private SphereCollider sphereCollider;
    private HandOutputArguments handOutputArguments;

    public HashSet<GameObject> collidedObjects;
    public int numCollidedObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = this.gameObject.AddComponent<SphereCollider>() as SphereCollider;
        //sphereCollider.isTrigger = true;
        sphereCollider.radius = 0.01f;

        collidedObjects = new HashSet<GameObject>();
        numCollidedObjects = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.layer == 7) // If it touches another part of the body...
        //     return;
        Debug.Log("HELLOOO!!");
        collidedObjects.Add(other.gameObject);
        numCollidedObjects++;

        // Interactable script lets the poked object respond to the poke
        IHandInteractable interactableScript = other.gameObject.GetComponent<IHandInteractable>();
        if (interactableScript != null) {
            Debug.Log("TriggerEnterStarted: "+other.gameObject.name);
            interactableScript.CollisionStarted(this.gameObject, other, handOutputArguments);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // if (other.gameObject.layer == 7) // If it touches another part of the body...
        //     return;

        // Interactable script lets the poked object respond to the poke
        IHandInteractable interactableScript = other.gameObject.GetComponent<IHandInteractable>();
        if (interactableScript != null) {
            interactableScript.CollisionEnded(this.gameObject, other, handOutputArguments);
            Debug.Log("TriggerExitStarted: "+other.gameObject.name);
        }

        
        collidedObjects.Remove(other.gameObject);
        numCollidedObjects--;
    }

    public void SetOutputArguments(HandOutputArguments handOutputArgs) {
        handOutputArguments = handOutputArgs;
    }


}
