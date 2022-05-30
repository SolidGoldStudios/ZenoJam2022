using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSense = 200f;
    public Transform playerBody;
    public Image reticuleActive;
    public Image reticuleInactive;
    public Camera mainCamera;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        if (Mathf.Abs(mouseX) > 500 || Mathf.Abs(mouseY) > 500) return;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out RaycastHit hit, 50f))
        {
            LightUp lightUp = hit.transform.GetComponent<LightUp>();
            SimpleLightSwitch sw = hit.transform.GetComponent<SimpleLightSwitch>();

            if (lightUp || sw) {
                reticuleActive.enabled = true;
                reticuleInactive.enabled = false;
            } else {
                reticuleActive.enabled = false;
                reticuleInactive.enabled = true;
            }
        }
        else
        {
            reticuleActive.enabled = false;
            reticuleInactive.enabled = true;
        }
    }
}