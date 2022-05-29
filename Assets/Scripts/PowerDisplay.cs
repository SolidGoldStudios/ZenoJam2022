using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerDisplay : MonoBehaviour
{

    public TextMeshProUGUI powerText;
    public string startingText;
    public float maxPower;
    public float startingPower;
    public float currentPower;

    // Internal vars
    private float newPower;

    public void Start()
    {
        Debug.Log("Starting PowerDisplay");
        Reset();
        Set(startingPower);
    }

    void Reset()
    {
        Debug.Log("Resetting PowerDisplay");
        SetText(startingText);
    }

    void SetText(string text)
    {
        Debug.Log($"Setting PowerDisplay text to {text}");
        powerText.text = text;
    }

    public float Get()
    {
        return currentPower;
    }

    public void Set(float amount)
    {
        newPower = currentPower + amount;
        if (newPower > maxPower)
        {
            newPower = maxPower;
        }

        currentPower = newPower;
        SetText($"{startingText} {currentPower}");
    }

    public void Increase(float amount)
    {
        newPower = currentPower + amount;
        if (newPower > maxPower)
        {
            newPower = maxPower;
        }
        currentPower = newPower;
        SetText($"{startingText} {Math.Round(currentPower)}");
    }

    public void Decrease(float amount)
    {
        if (currentPower == 0)
        {
            return;
        }

        newPower = currentPower - amount;
        if (newPower < 0)
        {
            currentPower = 0;
            SetText($"{startingText} {currentPower}");
            return;
        }

        currentPower = newPower;
        SetText($"{startingText} {Math.Round(currentPower)}");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug/Testing Helpers
        // Reset power
        if (Input.GetKeyDown(KeyCode.M))
        {
            Set(100);
        }

        // Increase power
        if (Input.GetKey(KeyCode.X))
        {
            Increase(1);
        }

        // Decrease power
        if (Input.GetKey(KeyCode.Z))
        {
            Decrease(1);
        }

        if (currentPower == 0)
        {
            powerText.fontSharedMaterial.SetFloat("_GlowPower", 1);
            powerText.UpdateMeshPadding();
        }
        else
        {
            powerText.fontSharedMaterial.SetFloat("_GlowPower", 0);
            powerText.UpdateMeshPadding();
        }
    }

    [ContextMenu("TestPowerDecrease")]
    void TestPowerDecrease()
    {
        Debug.Log($"Testing Decrease Power");
        Start();

        // Do not decrease if already at 0
        // Starting 0, decrease by 10
        float expected = 0;
        Set(0);
        Decrease(10);
        if (currentPower != expected)
        {
            Debug.LogError($"unexpected result when decreasing 10 from 100! want: {expected}, got: {currentPower}");
        }

        // Do not decrease below 0
        // Starting 10, decrease by 15
        expected = 0;
        Set(10);
        Decrease(15);
        if (currentPower != expected)
        {
            Debug.LogError($"unexpected result when decreasing to negative value! want: {expected}, got: {currentPower}");
        }

        // Standard decrease
        // Starting 100, decrease by 10
        expected = 90;
        Set(100);
        Decrease(10);
        if (currentPower != expected)
        {
            Debug.LogError($"unexpected result when decreasing 10 from 100! want: {expected}, got: {currentPower}");
        }

        Reset();
    }
}

