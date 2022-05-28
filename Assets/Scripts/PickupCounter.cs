using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCounter : MonoBehaviour
{
    public int batteryCount;
    public int glowStickCount;

    void OnTriggerEnter(Collider collider) {
        print(collider.name);

        // Check to see if the item is a pickup
        if (collider.tag == "Pickup")
        {
            Debug.Log("Collided with a pickup item");

            // Increment counter based on item type
            if(collider.GetComponent<PickableItem>().isBattery) {
                Debug.Log("Battery!");
                batteryCount += collider.GetComponent<PickableItem>().numItems;
            }
            if(collider.GetComponent<PickableItem>().isGlowStick) {
                Debug.Log("Glowstick!");
                glowStickCount += collider.GetComponent<PickableItem>().numItems;
            }
        }
    }
}
