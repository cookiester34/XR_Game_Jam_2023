using System;
using UnityEngine;

public class CollisionWarner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Am triggered");
        var parent = GetComponentInParent<Throwable>();
        var crabToWarn = other.GetComponentInParent<BaseController>();
        switch (parent.direction)
        {
            case Directionality.Left:
                crabToWarn.shouldDodgeFromLeft = true;
                break;
            case Directionality.Right:
                crabToWarn.shouldDodgeFromRight = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Am untriggered");
        var crabToWarn = other.GetComponentInParent<BaseController>();
        crabToWarn.shouldDodgeFromLeft = false;
        crabToWarn.shouldDodgeFromRight = false;
    }
}
