using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;


public class HeadBobber : MonoBehaviour
{
    
    public bool bobEnabled = true;
    public float amplitude = 0.015f;
    public float frequency = 10.0f;
    public float stabilizerDistance = 15f;
    public Transform mainCamera;
    public Transform cameraHolder;

    private float toggleSpeed = 0f;
    private Vector3 startPosition;
    private Vector3 playerLastPosition;
    private CharacterController playerController;
    
    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        startPosition = mainCamera.localPosition;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bobEnabled)
        {
            CheckForMotion();
            playerLastPosition = playerController.transform.position;
        }
    }

    private void CheckForMotion()
    {
        if (playerController.transform.position != playerLastPosition && playerController.isGrounded)
        {
            PlayMotion(FootstepMotion());
        }
        else
        {
            ResetPosition();
        }
        mainCamera.LookAt(StabilizeTarget());
    }

    private void PlayMotion(Vector3 motion)
    {
        mainCamera.localPosition += motion;
    }
    
    private void ResetPosition()
    {
        if (mainCamera.localPosition == startPosition) return;
        mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, startPosition, 1 * Time.deltaTime);
    }

    private Vector3 FootstepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private Vector3 StabilizeTarget()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y,
            transform.position.z);
        position += cameraHolder.forward * stabilizerDistance;
        return position;
    }
}
