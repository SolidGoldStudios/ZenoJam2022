using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Betterbeam : MonoBehaviour
{

    public Camera mainCamera;
    public Transform laserOrigin;
    public VisualEffect sparks;
    public VisualEffect newBeam;
    public float laserRange;
    public float laserDuration;
    public float fireRate = 0.2f;
    
    private LineRenderer laserBeam;
    private float fireTimer;
    

    // private GameObject lastHit;

    void Awake()
    {
        laserBeam = GetComponent<LineRenderer>();
        newBeam.SetFloat("Length", laserRange);
        // sparks.Stop();
    }
    
    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            //laserBeam.enabled = true;
            newBeam.SetFloat("Lifetime", .3f);
            newBeam.Play();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            laserBeam.enabled = false;
            fireTimer = 0;
            newBeam.Stop();
            newBeam.SetFloat("Lifetime", 0);
            // sparks.Stop();
            // lastHit = null;
        }
        
        if (laserBeam.enabled)
        {

            Vector3[] beamPositions = new Vector3[laserBeam.positionCount];
            //laserBeam.SetPosition(0, laserOrigin.position);
            beamPositions[0] = laserOrigin.position;
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            
            
            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, laserRange))
            {
                //laserBeam.SetPosition(1, hit.point);
                Vector3 distanceToHit = hit.point - newBeam.transform.position;
                float distanceOnZ = Mathf.Abs(distanceToHit.z);
                
                newBeam.SetFloat("Length", distanceOnZ);
                Debug.Log(distanceOnZ);
                
                beamPositions[1] = hit.point;
                // sparks.transform.position = hit.point;
                
                Debug.Log(hit.transform.gameObject.name);
                // if (hit.transform.gameObject != lastHit) {
                    LightUp lightUp = hit.transform.GetComponent<LightUp>();
                    if (lightUp) lightUp.Glow();

                //     lastHit = hit.transform.gameObject;
                // }
            }
            else
            {
                //laserBeam.SetPosition(1, rayOrigin+(mainCamera.transform.forward * laserRange));
                beamPositions[1] = rayOrigin + (mainCamera.transform.forward * laserRange);
                // sparks.Stop();
            }
            
            laserBeam.SetPositions(beamPositions);
            newBeam.transform.position = laserOrigin.position;
        }
    }
    
}
