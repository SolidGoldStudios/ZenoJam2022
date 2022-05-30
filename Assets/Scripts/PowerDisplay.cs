using System;
using TMPro;
using UnityEngine;

public class PowerDisplay : MonoBehaviour
{
    public TextMeshProUGUI powerText;
    public float maxPower;
    public string startingText;
    public float startingPower;
    public float rechargeAmount;
    public int rechargeDelay;

    // Internal vars
    private readonly float minPower = 0;
    private float currentPower;
    private DateTime rechargeTime;

    public void Start()
    {
        Reset();
    }

    void Reset()
    {
        SetText($"{startingText} {maxPower}");
        Set(maxPower);
    }

    void SetText(string text)
    {
        powerText.text = text;
    }

    public void Set(float amount)
    {
        if (amount > maxPower)
        {
            currentPower = maxPower;
            return;
        }

        if (amount < minPower)
        {
            currentPower = minPower;
            rechargeTime = DateTime.Now.AddSeconds(rechargeDelay);
            return;
        }

        currentPower = amount;

        if (currentPower == minPower)
        {
            rechargeTime = DateTime.Now.AddSeconds(rechargeDelay);
        }

    }

    public float Get()
    {
        return currentPower;
    }

    public void Increase(float amount)
    {
        Set(currentPower + amount);
        SetText($"{startingText} {Math.Round(currentPower)}");
    }

    public void Decrease(float amount)
    {
        Set(currentPower - amount);
        SetText($"{startingText} {Math.Round(currentPower)}");
    }

    private bool ShouldRecharge(DateTime rt)
    {
        if (rt < DateTime.Now)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug/Testing Helpers
        // Reset power
        if (Input.GetKeyDown(KeyCode.M))
        {
            Set(100);
            SetText($"{startingText} {Math.Round(currentPower)}");
        }

        // Increase power
        if (Input.GetKey(KeyCode.X))
        {
            Increase(1);
            SetText($"{startingText} {Math.Round(currentPower)}");
        }

        // Decrease power
        if (Input.GetKey(KeyCode.Z))
        {
            Decrease(1);
            SetText($"{startingText} {Math.Round(currentPower)}");
        }

        // Set to one
        if (Input.GetKey(KeyCode.C))
        {
            Set(1);
            SetText($"{startingText} {Math.Round(currentPower)}");
        }

        if (currentPower == 0)
        {
            powerText.fontMaterial.SetFloat("_GlowPower", 1);
            powerText.UpdateMeshPadding();
        }
        else
        {
            powerText.fontMaterial.SetFloat("_GlowPower", 0);
            powerText.UpdateMeshPadding();
        }

        if (currentPower == minPower && ShouldRecharge(rechargeTime))
        {
            Debug.Log("Recharging?");
            Set(rechargeAmount);
            SetText($"{startingText} {Math.Round(currentPower)}");
        }
    }


    [ContextMenu("TestAll")]
    void TestAll()
    {
        TestSet();
        TestIncrease();
        TestDecrease();
        TestShouldRecharge();
    }

    [ContextMenu("TestSet")]
    void TestSet()
    {
        Debug.Log($"Testing Set");

        string testName = "cannot set over maximum";
        float setPower = 9001;
        float expected = 100;
        Set(setPower);
        if (Get() != expected)
        {
            Debug.LogError($"TestSet case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }
        Reset();

        testName = "cannot set below zero";
        setPower = -10;
        expected = 0;
        Set(setPower);
        if (Get() != expected)
        {
            Debug.LogError($"TestSet case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }
        Reset();
    }

    [ContextMenu("TestIncrease")]
    void TestIncrease()
    {
        Debug.Log($"Testing Increase Power");

        string testName = "do not increase if already at 100";
        float expected = 100;
        Start();
        Increase(10);
        if (Get() != expected)
        {
            Debug.LogError($"TestIncrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }


        testName = "max power caps at 100";
        expected = 100;
        Start();
        Set(90);
        Increase(25);
        if (Get() != expected)
        {
            Debug.LogError($"TestIncrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }

        testName = "successful decrease";
        expected = 75;
        Start();
        Set(50);
        Increase(25);
        if (Get() != expected)
        {
            Debug.LogError($"TestIncrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }

        Reset();
    }

    [ContextMenu("TestDecrease")]
    void TestDecrease()
    {
        Debug.Log($"Testing Decrease Power");

        string testName = "do not decrease if already at 0";
        float expected = 0;
        Start();
        Set(0);
        Decrease(10);
        if (Get() != expected)
        {
            Debug.LogError($"TestDecrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }


        testName = "do not decrease if already at 0";
        expected = 0;
        Start();
        Set(10);
        Decrease(15);
        if (Get() != expected)
        {
            Debug.LogError($"TestDecrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }

        testName = "successful decrease";
        expected = 90;
        Start();
        Set(100);
        Decrease(10);
        if (Get() != expected)
        {
            Debug.LogError($"TestDecrease case: \"{testName}\" failed! want: {expected}, got: {currentPower}");
        }

        Reset();
    }

    [ContextMenu("TestShouldRecharge")]
    void TestShouldRecharge()
    {
        Debug.Log($"Testing Increase Power");

        DateTime rechargeTime;
        bool expected;
        bool actual;

        string testName = "should not recharge";
        rechargeTime = DateTime.Now;
        expected = false;
        actual = ShouldRecharge(rechargeTime);
        if (expected != actual)
        {
            Debug.LogError($"TestShouldRecharge case: \"{testName}\" failed! want: {expected}, got: {currentPower}: rechargeTime: {rechargeTime} nowTime: {DateTime.Now}");
        }

        testName = "should recharge";
        rechargeTime = DateTime.Now.AddSeconds(-rechargeDelay - 2);
        expected = true;
        actual = ShouldRecharge(rechargeTime);
        if (expected != actual)
        {
            Debug.LogError($"TestShouldRecharge case: \"{testName}\" failed! want: {expected}, got: {currentPower}: rechargeTime: {rechargeTime} nowTime: {DateTime.Now}");
        }
    }
}

