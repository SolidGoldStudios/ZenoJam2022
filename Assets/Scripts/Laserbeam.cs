using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserbeam : MonoBehaviour
{

    public Camera mainCamera;
    public Transform laserOrigin;
    public float laserRange;
    public float laserDuration;
    public float fireRate = 0.2f;
    
    private LineRenderer laserBeam;
    private float fireTimer;
    // private GameObject lastHit;

    void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            laserBeam.enabled = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            laserBeam.enabled = false;
            fireTimer = 0;
            // lastHit = null;
        }

        if (laserBeam.enabled)
        {
            laserBeam.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, laserRange))
            {
                laserBeam.SetPosition(1, hit.point);
                Debug.Log(hit.transform.gameObject.name);
                // if (hit.transform.gameObject != lastHit) {
                    LightUp lightUp = hit.transform.GetComponent<LightUp>();
                    if (lightUp) lightUp.Glow();

                //     lastHit = hit.transform.gameObject;
                // }
            }
            else
            {
                laserBeam.SetPosition(1, rayOrigin+(mainCamera.transform.forward * laserRange));
            }
        }
    }
    
}
