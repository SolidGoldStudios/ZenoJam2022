using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int toScene;
    public SceneTransitionFade fadeSceneScript;

    void OnTriggerEnter(Collider collider) {
        if (fadeSceneScript != null) {
            fadeSceneScript.FadeOut(toScene);
        } else {
            SceneManager.LoadScene(sceneBuildIndex:toScene);
        }
    }
}
