using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public bool isBattery;
    public bool isGlowStick;
    public int numItems;

    void Start() {
        Debug.Log("started!");
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log("Entered the collision");
    }
}
