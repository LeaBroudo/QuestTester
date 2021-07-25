using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandGestureable
{
    bool isHandPointing { get; set; }
    bool isIndexPinching { get; set; }

    void SubscribeToEvents();

    void IndexPinchingStart();
    void IndexPinchingEnd();
    void IndexIsPinching();

    void HandPointingStarted();
    void HandPointingEnded();
    void HandIsPointing();
    
}
