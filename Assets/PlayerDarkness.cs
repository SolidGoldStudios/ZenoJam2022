using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarkness : MonoBehaviour
{

    public int minDarkness = 0;
    public int maxDarkness = 100;
    public int currentDarkness;

    public Darkness darkness;

    // Start is called before the first frame update
    void Start()
    {
        currentDarkness = maxDarkness;
        darkness.SetMaxDarkness(maxDarkness);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ReduceDarkness(10);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            darkness.SetDarkness(maxDarkness);
            currentDarkness = maxDarkness;
        }
        
    }

    void ReduceDarkness(int amount)
    {
        if (currentDarkness == minDarkness) {
            darkness.SetDarkness(maxDarkness);
            currentDarkness = maxDarkness;
        } else {
            currentDarkness -= amount;
            darkness.SetDarkness(currentDarkness);
        }

    }
}
