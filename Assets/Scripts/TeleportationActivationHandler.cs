using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TeleportationActivationHandler : MonoBehaviour
{
    
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;

    public float activationThreshold = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay) {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
        }

        if (rightTeleportRay) {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller) {

        //Debug.Log("XX inputs: "+controller.inputDevice + " " + teleportActivationButton);
        controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal);
        return triggerVal > activationThreshold;
    }
}
