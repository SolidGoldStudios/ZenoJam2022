using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    
    public float batteryValue;
    public PowerDisplay powerDisplay;

    public AudioSource pickUpSound;
    
    void OnTriggerEnter(Collider collider)
    {
        powerDisplay.Increase(batteryValue);
        pickUpSound.Play();
        
        Destroy(gameObject);
    }
}
