using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;
using JetBrains.Annotations;
using System;

public class SpooderCuddle : MonoBehaviour
{
    public AudioSource spiderBite;
    public float rotateSpeed = 360f;

    void OnTriggerEnter(Collider collider)
    {

        GameObject player = GameObject.Find("Player");
        Vector3 playerloc = player.transform.position;

        // Get all transform objects, iterate through and rotate spooders.
        Transform[] objs = UnityEngine.Object.FindObjectsOfType<Transform>();

        float totalX = 0f;
        float totalY = 0f;
        float spooderCount = 0f;
        for (int i = 0; i < objs.Length; i++)
        {
            Transform obj = objs[i];
            if (obj.tag != "Spider")
            {
                continue;
            }

            totalX += obj.position.x;
            totalY += obj.position.y;

            Vector3 targetDir = playerloc - obj.position;
            float rotateStep = rotateSpeed * 100 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(obj.forward, targetDir, rotateStep, 0.0f);
            obj.rotation = Quaternion.LookRotation(newDir);
        }

        spiderBite.Play();
        float spooderCenterX = totalX / spooderCount;
        float spooderCenterY = totalY / spooderCount;
        Vector3 spooderCenter = new()
        {
            x = spooderCenterX,
            y = spooderCenterY
        };
        Vector3 spooderDir = playerloc - spooderCenter;
        float playerRotateStep = rotateSpeed * 50 * Time.deltaTime;
        Vector3 spooders = Vector3.RotateTowards(player.transform.forward, spooderDir, playerRotateStep, 0.0f);
        player.transform.rotation = Quaternion.LookRotation(spooderDir);

        StartCoroutine(RealizeDeath(1, () =>
        {
            Die();
        }));
    }

    IEnumerator RealizeDeath(float time, Action task)
    {
        yield return new WaitForSeconds(time);
        task();
    }

    private void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
