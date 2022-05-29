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
    private float fireTimer;
    private bool laserOff = true;
    private Transform lastHit;
    

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
        if (lastHit != null)
        {
            var hitMeshRenderer = lastHit.GetComponent<MeshRenderer>();
            hitMeshRenderer.enabled = false;
            lastHit = null;
        }
        
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate && powerDisplay.Get() > 0)
        {
            laserBeam.enabled = true;
            beamOn.Play();
            beamSound.Play();
            beamBurn.Play();
        }

        if (powerDisplay.Get() == 0)
        {
            laserBeam.enabled = false;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            fireTimer = 0;
            laserBeam.enabled = false;
            sparks.Stop();
            //beamOff.Play();
            beamSound.Stop();
            beamBurn.Stop();
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
                
                
                Debug.Log(hit.transform.gameObject.name);
                // if (hit.transform.gameObject != lastHit) {
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
