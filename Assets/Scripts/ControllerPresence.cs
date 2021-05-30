using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerPresence : MonoBehaviour
{
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab; 
    
    private InputDevice controllerInput;
    public InputDeviceCharacteristics controllerCharacteristics; 

    public GameObject spawnedController;
    public GameObject spawnedHand;
    private Animator handAnimator;
    
    public bool showController = false;
    public float triggerValue = 0f;
    public bool primaryButtonValue = false;
    public Vector2 primary2DAxisValue = new Vector2(0,0);
    public float gripValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        FindController(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerInput == null || !controllerInput.isValid) {
            FindController();
        }
        else {
            
            if (!showController && spawnedController.activeSelf) {
            
                spawnedController.SetActive(false);
                spawnedHand.SetActive(true);

            }
            else if (showController && !spawnedController.activeSelf) {

                spawnedController.SetActive(true);
                spawnedHand.SetActive(false);
            }
            
            BroadcastButtonBindings(controllerInput);
            UpdateHandAnimation();
        }
        
    }

    private void BroadcastButtonBindings(InputDevice controller) {
        
        controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonVal);
        primaryButtonValue = primaryButtonVal;
        //Debug.Log("Pressing Primary Button "+controller.name);

        controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal);
        triggerValue = triggerVal;
        Debug.Log("Trigger pressed "+controller.name+": "+triggerValue);

        controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisVal);
        primary2DAxisValue = primary2DAxisVal;
        //Debug.Log("Primary Touchpad "+controller.name+": " + primary2DAxisValue);

        controllerInput.TryGetFeatureValue(CommonUsages.grip, out float gripVal);
        gripValue = gripVal;
        //Debug.Log("Grip Value "+controller.name+": " + gripValue);

    }

    private void FindController() {

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        
        if (devices.Count > 0) {
            controllerInput = devices[0];
            
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == controllerInput.name);
            if (prefab) {
                spawnedController = Instantiate(prefab, transform);
            }
            else {
                Debug.LogError("Did not find corresponding controller model.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHand = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHand.GetComponent<Animator>();
        } 
    }

    private void UpdateHandAnimation() {

        if (controllerInput.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {

            handAnimator.SetFloat("TriggerButton", triggerValue);
        }
        else {
            handAnimator.SetFloat("TriggerButton", 0);
        }

        if (controllerInput.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) {

            handAnimator.SetFloat("Grip", gripValue);
        }
        else {
            handAnimator.SetFloat("Grip", 0);
        }
    } 
}
