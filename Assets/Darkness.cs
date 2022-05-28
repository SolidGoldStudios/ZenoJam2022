using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darkness : MonoBehaviour
{
    public Slider slider;

    public void SetMaxDarkness(int darkness) {
        slider.maxValue = darkness;
        slider.value = darkness;
    }

    public void SetDarkness(int darkness)
    {
        slider.value = darkness;
    } 
}
