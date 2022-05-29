using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Laserbeam : MonoBehaviour
{

    public Camera mainCamera;
    public Transform laserOrigin;
    public float laserRange;
    public float laserDuration;
    public float fireRate = 0.2f;
    public ParticleSystem sparks;

    private LineRenderer laserBeam;
    private float fireTimer;

    // HUD: Power
    public PowerDisplay powerDisplay;


    // private GameObject lastHit;

    void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
        powerDisplay.Start();
        sparks.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate && powerDisplay.Get() > 0)
        {
            laserBeam.enabled = true;
        }

        if (powerDisplay.Get() == 0)
        {
            laserBeam.enabled = false;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            laserBeam.enabled = false;
            fireTimer = 0;
            sparks.Stop();
            // lastHit = null;
        }

        if (laserBeam.enabled)
        {
            powerDisplay.Decrease(fireRate);
            Vector3[] beamPositions = new Vector3[laserBeam.positionCount];
            beamPositions[0] = laserOrigin.position;
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;


            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, laserRange))
            {
                beamPositions[1] = hit.point;
                sparks.transform.position = hit.point;
                sparks.Play();

                Debug.Log(hit.transform.gameObject.name);
                // if (hit.transform.gameObject != lastHit) {
                LightUp lightUp = hit.transform.GetComponent<LightUp>();
                if (lightUp) lightUp.Glow();

                //     lastHit = hit.transform.gameObject;
                // }
            }
            else
            {
                beamPositions[1] = rayOrigin + (mainCamera.transform.forward * laserRange);
                sparks.Stop();
            }

            laserBeam.SetPositions(beamPositions);
        }
    }

}
