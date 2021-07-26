using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandGestureable
{
    bool isHandPointing { get; set; }
    bool isIndexPinching { get; set; }

    void SubscribeToEvents(
        GestureEvent IndexPinchingStarted,
        GestureEvent IndexPinchingEnded,
        GestureEvent IndexIsPinching,
        PointingEvent HandPointingStarted,
        PointingEvent HandPointingEnded,
        PointingEvent HandIsPointing);

    void IndexPinchingStart(LeftOrRight leftOrRight);
    void IndexPinchingEnd(LeftOrRight leftOrRight);
    void IndexIsPinching(LeftOrRight leftOrRight);

    void HandPointingStarted(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos);
    void HandPointingEnded(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos);
    void HandIsPointing(LeftOrRight leftOrRight, Transform pointerPose, Vector3 pos);
    
}
