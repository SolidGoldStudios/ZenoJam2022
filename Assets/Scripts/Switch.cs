using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Material mat;
    public Color color;
    public GameObject switchTarget;
    public float intensity;

    private bool switchFlipped = false;

    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        if (intensity > 0 && !switchFlipped) {
            intensity -= Time.deltaTime * 3;

            if (intensity < 1f && switchFlipped) intensity = 1f;

            mat.SetColor("_EmissionColor", color * intensity);
        }
    }

    public void Glow()
    {
        if (intensity < 3f) {
            // intensity += 0.001f;
            intensity += Time.deltaTime;
        }

        if (intensity > 1f && !switchFlipped) {
            MoveableObject mov = switchTarget.GetComponent<MoveableObject>();
            if (mov) mov.Move();

            switchFlipped = true;
        }
    }
}
