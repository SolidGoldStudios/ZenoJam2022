using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public Material mat;
    Color color;
    float intensity = 0;

    void Start()
    {
        color = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", Color.black);
        StartCoroutine(DelayAndGlow());
    }

    void Update()
    {
        // intensity += 0.1f * Time.deltaTime;
        // if (intensity > 1f) intensity = 0;

        // mat.SetColor("_EmissionColor", color * intensity);
    }

    IEnumerator DelayAndGlow()
    {
        yield return new WaitForSeconds(5);

        for (int i = 0; i < 10; i++) {
            intensity += 0.1f;
            mat.SetColor("_EmissionColor", color * intensity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
