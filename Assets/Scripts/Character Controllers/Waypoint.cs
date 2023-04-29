using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Directionality targetDirection;

    private void OnTriggerEnter(Collider other)
    {
        var crab = other.GetComponentInParent<AiController>();
        if (crab != null)
        {
            crab.randomDirection = targetDirection;
        }
    }
}