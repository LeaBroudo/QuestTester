using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandGrabbable
{
    bool isBeingGrabbed { get; set; }
    float kAnimSpeed { get; set; }
    Transform transToFollow { get; set; }
    IEnumerator GrabAnimation { get; set; }
    IEnumerator ReleaseAnimation { get; set; }

    IEnumerator MoveAToB(Transform a, Transform b);
    void StartGrab(Transform transToFollow);
    void DuringGrab();
    void EndGrab(); 
    void EndGrab(Transform posToReturnTo);
    
}
