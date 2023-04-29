using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var crab = other.GetComponentInParent<AiController>();
        if (crab != null)
        {
            crab.randomDirection = crab.randomDirection == Directionality.Right ? Directionality.Left : Directionality.Right;
        }
    }
}
