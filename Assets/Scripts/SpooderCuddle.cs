using UnityEngine;
using UnityEngine.SceneManagement;

public class SpooderCuddle : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
