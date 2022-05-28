using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCounter : MonoBehaviour
{
    public int batteryCount;
    public int GlowStickCount;

    void OnTriggerEnter(Collider collider) {
        Debug.Log("Collided with something");

        // Check to see if the item is a pickup
        if (collider.gameObject.tag == "Pickup")
        {
            Debug.Log("Collided with a pickup item");
        }
    }
}
