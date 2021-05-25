using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{

    public InputDevice alphaController;
    public InputDevice betaController;

    // Start is called before the first frame update
    void Start()
    {
        SetUpControllers();
    }

    // Update is called once per frame
    void Update()
    {
        alphaController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue) 
            Debug.Log("Pressing Primary Button");

        alphaController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f) 
            Debug.Log("Trigger pressed: "+triggerValue);

        alphaController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero) 
            Debug.Log("Primary Touchpad " + primary2DAxisValue);


    }

    private void SetUpControllers() {

        List<InputDevice> rightDevices = new List<InputDevice>();
        List<InputDevice> leftDevices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left;
        
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rightDevices);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, leftDevices);
        
        if (rightDevices.Count > 0) {
            alphaController = rightDevices[0];
        } 
        else {
            if (leftDevices.Count > 0) {
                alphaController = leftDevices[0];
                return;
            }
        }

        if (leftDevices.Count > 0) {
            betaController = leftDevices[0];
        }
    }
}
