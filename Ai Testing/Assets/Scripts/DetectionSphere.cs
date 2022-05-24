using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    public bool inRange = false;
    public bool LockOnTarget = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Target") return;
        inRange = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Target") return;
        inRange = false;
    }
}
