using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCollider : MonoBehaviour
{
    public Image image;

    void OnTriggerEnter(Collider collider)
    {
        image.enabled = true;
        MoveableObject moveableObject = gameObject.GetComponent<MoveableObject>();
        moveableObject.Move();
    }
}
