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

    void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (laserBeam.enabled)
        {
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            laserBeam.SetPosition(0, laserOrigin.position);
            laserBeam.SetPosition(1, rayOrigin+(mainCamera.transform.forward * laserRange));
        }
        
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            laserBeam.enabled = true;
            laserBeam.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, laserRange))
            {
                laserBeam.SetPosition(1, hit.point);
            }
            else
            {
                laserBeam.SetPosition(1, rayOrigin+(mainCamera.transform.forward * laserRange));
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            fireTimer = 0;
            laserBeam.enabled = false;
        }
    }
    
}
