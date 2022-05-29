using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource audioSource in audioSources) {
            audioSource.Play();
        }
    }
}
