using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSphere : MonoBehaviour, IHandInteractable
{
    private MeshRenderer meshRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollisionStarted(GameObject bodyPart, Collision collision, HandOutputArguments handArgs) {
        Debug.Log("Sphere touched: "+bodyPart);
        //meshRenderer.materials[0] = actionMat;
        meshRenderer.material.color = Color.red;
    }
    public void CollisionEnded(GameObject bodyPart, Collision collision, HandOutputArguments handArgs) {
        Debug.Log("Sphere exited: "+bodyPart);
        //meshRenderer.materials[0] = baseMat;
        meshRenderer.material.color = Color.white;
    }

}
