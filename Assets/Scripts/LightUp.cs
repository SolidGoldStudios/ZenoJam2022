using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    private Material mat;
    public Color color;
    public float intensity = 0;
    public float duration;
    public bool isSwitch;
    public GameObject[] switchTargets;

    private bool switchFlipped = false;
    private AudioSource audioSource;

    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", Color.black);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (intensity > 0 && !switchFlipped) {
            intensity -= Time.deltaTime * 3 / duration;

            if (intensity < 1f && isSwitch && switchFlipped) intensity = 1f;
            if (intensity < 0) intensity = 0;

            mat.SetColor("_EmissionColor", color * intensity);

            if (audioSource) audioSource.mute = intensity < 1f;
        }
    }

    public void Glow()
    {
        if (intensity < 3f) {
            // intensity += 0.001f;
            intensity += Time.deltaTime;
        }

        if (intensity > 1f && isSwitch && !switchFlipped) {
            foreach (GameObject go in switchTargets)
            {
                MoveableObject mov = go.GetComponent<MoveableObject>();
                if (mov) mov.Move();           

                LightUp lu = go.GetComponent<LightUp>();
                if (lu) lu.SwitchOn();
            }

            if (audioSource) audioSource.mute = intensity < 1f;
            switchFlipped = true;
        }
    }

    public void SwitchOn()
    {
        switchFlipped = true;
        intensity = 3f;
        mat.SetColor("_EmissionColor", color * intensity);
        if (audioSource) audioSource.mute = false;
    }
}
