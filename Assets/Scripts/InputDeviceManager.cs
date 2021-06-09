using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDeviceManager : MonoBehaviour
{
    public HandPresence leftHandPresence;
    public HandPresence rightHandPresence;
    public OVRControllerHelper leftControllerHelper;
    public OVRControllerHelper rightControllerHelper;

    public GameObject HandInteractionObjects;
    public GameObject ControllerInteractionObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isControllerConnected()) {

            //Turn off Hand related objects
            HandInteractionObjects.SetActive(false);

            leftHandPresence.HideHandPrefab();
            rightHandPresence.HideHandPrefab();

            //Turn on Controller related objects
            ControllerInteractionObjects.SetActive(true);
            
        }
        else {

            //Turn off Controller related objects
            ControllerInteractionObjects.SetActive(false);

            //Turn on Hand related objects
            HandInteractionObjects.SetActive(true);

            if (leftHandPresence.isTracked()) {
                leftHandPresence.ShowHandPrefab();
            } else {
                leftHandPresence.HideHandPrefab();
            }

            if (rightHandPresence.isTracked()) {
                rightHandPresence.ShowHandPrefab();
            } else {
                rightHandPresence.HideHandPrefab();
            }
        }
        
    }

    public bool isControllerConnected() {
        bool leftControllerConnected = OVRInput.IsControllerConnected(leftControllerHelper.m_controller);
		bool rightControllerConnected = OVRInput.IsControllerConnected(rightControllerHelper.m_controller);
        bool controllerConnected = leftControllerConnected || rightControllerConnected;
        
        bool activeController = (controllerConnected && (leftControllerHelper.m_controller == OVRInput.Controller.LTouch)) || 
            (controllerConnected && (leftControllerHelper.m_controller == OVRInput.Controller.RTouch));

        return activeController;
    }
}
