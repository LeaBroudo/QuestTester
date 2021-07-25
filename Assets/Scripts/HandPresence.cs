using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresence : MonoBehaviour
{
    private OVRHand hand; 
    private SkinnedMeshRenderer handRenderer;


    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        handRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHandPrefab() {

        if (!handRenderer.enabled) {
            handRenderer.enabled = true; 
        }
    }

    public void HideHandPrefab() {
                    
        if (handRenderer.enabled) {
            handRenderer.enabled = false; 
        }
    }

    public bool isHandShown() {
        return handRenderer.enabled;
    }

    public bool isTracked() {
        return hand.IsTracked;
    }
}
