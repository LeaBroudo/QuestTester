using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandInteractable
{
    GameObject rightIndexColliding { get; set; }
    GameObject leftIndexColliding { get; set; }

    void IndexCollisionStarted(LeftOrRight leftOrRight, GameObject bodyPart, Collision collision);
    void IndexCollisionEnded(LeftOrRight leftOrRight, GameObject bodyPart, Collision collision);
}
