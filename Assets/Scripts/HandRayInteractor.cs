using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRayInteractor : MonoBehaviour
{
    private LineRenderer line;

    private bool isShowingRay;

    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        isShowingRay = false;
        line.enabled = isShowingRay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnRay(Transform pointerPose, Vector3 startPos) {

        if (!isShowingRay) {
            
            isShowingRay = true;
            line.enabled = isShowingRay;
            UpdatePointerPose(pointerPose, startPos);

        }
    }

    public void UpdatePointerPose(Transform pointerPose, Vector3 startPos) {

        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos + pointerPose.forward);
    }

    public void TurnOffRay(Transform pointerPose, Vector3 startPos) {

        if (isShowingRay) {
            
            isShowingRay = false;
            line.enabled = isShowingRay;

        }
    }
}
