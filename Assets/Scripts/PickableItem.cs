using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public float batteryValue;
    public PowerDisplay powerDisplay;

    void OnTriggerEnter(Collider collider)
    {
        powerDisplay.Increase(batteryValue);

        Destroy(gameObject);
    }
}
