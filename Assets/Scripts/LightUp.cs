using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public Material mat;
    public Color color;
    float intensity = 0;

    void Start()
    {
        mat.SetColor("_EmissionColor", Color.black);
    }

    public void Glow()
    {
        if (intensity < 10f) {
            intensity += 0.001f;
            mat.SetColor("_EmissionColor", color * intensity);
        }
        // StartCoroutine("GlowRoutine");
    }

    // IEnumerator GlowRoutine()
    // {
    //     for (int i = 0; i < 10; i++) {
    //         intensity += 0.1f;
    //         mat.SetColor("_EmissionColor", color * intensity);
    //         yield return new WaitForSeconds(0.03f);
    //     }
    // }
}
