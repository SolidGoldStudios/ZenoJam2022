using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightLogic : MonoBehaviour
{
    
    
    public float mouseSense = 100f;
    public Transform flashlightBody;
    public Light flashLightBeam;
    
    float xRotation = 0f;
    float yRotation = 0f;

    public bool batteryDrain;
    public bool lightState;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            lightState = !lightState;
            flashLightBeam.enabled = lightState;
        }

        if (batteryDrain)
        {
            flashLightBeam.intensity--;
        }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime * .5f;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime * .8f;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        yRotation = Mathf.Clamp(yRotation, -10f, 10f);

        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}