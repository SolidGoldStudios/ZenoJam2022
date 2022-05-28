using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public Material mat;
    public Color color;
    public float intensity = 0;

    void Start()
    {
        mat.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        if (intensity > 0) {
            intensity -= Time.deltaTime / 10;
        } else {
            intensity = 0;
        }

        mat.SetColor("_EmissionColor", color * intensity);
    }

    public void Glow()
    {
        if (intensity < 1.5f) {
            intensity += 0.001f;
        }
    }
}
