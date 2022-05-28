using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int toScene;

    void OnTriggerEnter(Collider collider) {
         SceneManager.LoadScene(sceneBuildIndex:1);        
    }
}
