using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TeleportationActivationHandler : MonoBehaviour
{
    
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;

    public XRRayInteractor leftInteractionRay;
    public XRRayInteractor rightInteractionRay;

    public float activationThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (leftTeleportRay) {
            bool showTeleportRay = CheckIfActivated(leftTeleportRay) && !CheckIfInteractionRayHovering(leftInteractionRay);
            leftTeleportRay.gameObject.SetActive(showTeleportRay);
        }

        if (rightTeleportRay) {
            bool showTeleportRay = CheckIfActivated(rightTeleportRay) && !CheckIfInteractionRayHovering(rightInteractionRay);
            rightTeleportRay.gameObject.SetActive(showTeleportRay);
        }
    }

    public bool CheckIfActivated(XRController controller) {

        //Debug.Log("XX inputs: "+controller.inputDevice + " " + teleportActivationButton);
        //controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal);
        //return triggerVal > activationThreshold;

        controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickVal);
        Debug.Log("XX inputs: "+controller.inputDevice + " " + joystickVal);
        return joystickVal.y > activationThreshold;
    }

    public bool CheckIfInteractionRayHovering(XRRayInteractor interactionRay) {

        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        return interactionRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
    }
}
