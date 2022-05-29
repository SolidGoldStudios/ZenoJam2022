using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public AudioSource beamSound;
    public AudioSource beamOn;
    public AudioSource beamOff;
    public AudioSource beamBurn;

    public PowerDisplay powerDisplay;

    private LineRenderer laserBeam;
    private Transform lastHit;


    void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
        powerDisplay.Start();
        sparks.Stop();
    }

    void LaserOn()
    {
        laserBeam.enabled = true;
        beamOn.Play();
        beamSound.Play();
        beamBurn.Play();
    }

    void LaserOff()
    {
        laserBeam.enabled = false;
        sparks.Stop();
        beamSound.Stop();
        beamOff.Play();
        beamBurn.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        bool firing = Input.GetButton("Fire1");
        if (lastHit != null)
        {
            var hitMeshRenderer = lastHit.GetComponent<MeshRenderer>();
            hitMeshRenderer.enabled = false;
            lastHit = null;
        }

        if (powerDisplay.Get() > 0 && firing && Time.deltaTime > 0)
        {
            LaserOn();
        }

        if (powerDisplay.Get() == 0 || !firing)
        {
            LaserOff();
        }

        if (laserBeam.enabled)
        {
            powerDisplay.Decrease(fireRate);
            Vector3[] beamPositions = new Vector3[laserBeam.positionCount];
            beamPositions[0] = laserOrigin.position;
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out RaycastHit hit, laserRange))
            {
                beamPositions[1] = hit.point;
                sparks.transform.position = hit.point;
                sparks.Play();

                // Hit detection for spider shields
                var hitObject = hit.transform;
                if (hitObject.CompareTag("Spider"))
                {
                    var hitMeshRenderer = hitObject.GetComponent<MeshRenderer>();
                    if (hitMeshRenderer != null)
                    {
                        hitMeshRenderer.enabled = true;
                    }

                    lastHit = hit.transform;
                }

                LightUp lightUp = hit.transform.GetComponent<LightUp>();
                if (lightUp) lightUp.Glow();

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
