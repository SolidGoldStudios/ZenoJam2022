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
        beamOn.Stop();
        beamSound.Stop();
        beamOff.Stop();
        beamBurn.Stop();
    }

    void LaserOn()
    {
        laserBeam.enabled = true;
        Debug.Log("LaserOn calling beamOn.Play next");
        beamOn.Play();
        beamSound.Play();
        beamBurn.Play();
    }

    void LaserOff()
    {
        laserBeam.enabled = false;
        sparks.Stop();
        Debug.Log("LaserOff calling beamOn.Play next");
        beamOn.Play();
        beamSound.Stop();
        beamBurn.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastHit != null)
        {
            var hitMeshRenderer = lastHit.GetComponent<MeshRenderer>();
            var shieldSound = lastHit.GetComponent<AudioSource>();
            hitMeshRenderer.enabled = false;
            lastHit = null;
        }

        if (!laserBeam.enabled && Input.GetButtonDown("Fire1") && powerDisplay.Get() > 0 && Time.deltaTime > 0)
        {
            Debug.Log("Turning laser on");
            LaserOn();
        }

        if (laserBeam.enabled && (powerDisplay.Get() == 0 || Input.GetButtonUp("Fire1")))
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
                    var shieldSound = hitObject.GetComponent<AudioSource>();
                    if (hitMeshRenderer != null)
                    {
                        hitMeshRenderer.enabled = true;

                        if (!shieldSound.isPlaying)
                        {
                            shieldSound.Play();
                        }
                            
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
