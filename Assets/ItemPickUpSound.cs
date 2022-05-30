using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpSound : MonoBehaviour
{
    public AudioSource itemSound;

    public void PlaySound()
    {
        itemSound.Play();
    }
}
