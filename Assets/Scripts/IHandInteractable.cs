using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandInteractable
{
    void CollisionStarted(GameObject bodyPart, Collision collision, HandOutputArguments handArgs);
    void CollisionEnded(GameObject bodyPart, Collision collision, HandOutputArguments handArgs);
}
