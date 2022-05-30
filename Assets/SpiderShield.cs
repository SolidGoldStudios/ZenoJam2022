using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShield : MonoBehaviour
{

    public MeshRenderer shieldMeshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Laser entered");
        if (other.tag == "Laser")
        {
            shieldMeshRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Laser")
        {
            shieldMeshRenderer.enabled = false;
        }
    }
}
