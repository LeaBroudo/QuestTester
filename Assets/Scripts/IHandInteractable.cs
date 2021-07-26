using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandInteractable
{
    void CollisionStarted(GameObject bodyPart, Collision collision);
    void CollisionEnded(GameObject bodyPart, Collision collision);
}
