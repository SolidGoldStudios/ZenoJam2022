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
    public GameObject switchTarget;

    private bool switchFlipped = false;

    void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        if (intensity > 0 && !switchFlipped) {
            intensity -= Time.deltaTime * 3 / duration;

            if (intensity < 1f && isSwitch && switchFlipped) intensity = 1f;
            if (intensity < 0) intensity = 0;

            mat.SetColor("_EmissionColor", color * intensity);
        }
    }

    public void Glow()
    {
        if (intensity < 3f) {
            // intensity += 0.001f;
            intensity += Time.deltaTime;
        }

        if (intensity > 1f && isSwitch && !switchFlipped) {
            MoveableObject mov = switchTarget.GetComponent<MoveableObject>();
            if (mov) mov.Move();

            switchFlipped = true;
        }
    }
}
