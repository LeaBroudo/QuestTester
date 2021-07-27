using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandGrabbable
{
    bool isBeingGrabbed { get; set; }
    float kAnimSpeed { get; set; }
    GameObject objToFollow { get; set; }
    IEnumerator GrabAnimation { get; set; }
    IEnumerator ReleaseAnimation { get; set; }

    IEnumerator MoveAToB(GameObject a, GameObject b);
    void StartGrab(GameObject objToFollow);
    void DuringGrab();
    void EndGrab(); 
    
}
