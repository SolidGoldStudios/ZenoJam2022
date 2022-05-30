using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLightSwitch : MonoBehaviour
{
    private Material mat;
    public Color color;
    public float intensity = 0;
    public GameObject[] switchTargets;

    private bool switchFlipped = false;
    private AudioSource audioSource;

    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", Color.blackgit checkout);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Glow()
    {
        if (intensity < 3f) {
            // intensity += 0.001f;
            intensity += Time.deltaTime;
            mat.SetColor("_EmissionColor", color * intensity);
        }

        if (intensity > .5f && !switchFlipped) {
            foreach (GameObject go in switchTargets)
            {
                MoveableObject mov = go.GetComponent<MoveableObject>();
                if (mov) mov.Move();           

                LightUp lu = go.GetComponent<LightUp>();
                if (lu) lu.SwitchOn();
            }

            if (audioSource) audioSource.Play();

            switchFlipped = true;
        }
    }
}
