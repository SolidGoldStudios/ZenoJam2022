using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    
    public float batteryValue;
    public PowerDisplay powerDisplay;

    void OnTriggerEnter(Collider collider)
    {
        powerDisplay.Increase(batteryValue);
        collider.GetComponent<ItemPickUpSound>().PlaySound();
        
        Destroy(gameObject);
    }
}
