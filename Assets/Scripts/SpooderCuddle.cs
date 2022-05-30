using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SpooderCuddle : MonoBehaviour
{
    public AudioSource spiderBite;
    private readonly float rotateSpeed = 360f;
    private bool triggered = false;
    private Image image;

    void Start() {
        image = GameObject.Find("YouDead").GetComponent<Image>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (triggered)
        {
            return;
        }
        triggered = true;
        spiderBite.Play();

        GameObject player = GameObject.Find("Player");
        Vector3 playerloc = player.transform.position;

        Transform[] objs = FindObjectsOfType<Transform>();
        for (int i = 0; i < objs.Length; i++)
        {
            Transform obj = objs[i];
            if (obj.tag != "Spider")
            {
                continue;
            }

            Vector3 targetDir = playerloc - obj.position;
            float rotateStep = rotateSpeed * 100 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(obj.forward, targetDir, rotateStep, 0.0f);
            Animator animator = obj.GetComponentInParent<Animator>();
            animator.SetBool("moving", false);

            obj.rotation = Quaternion.LookRotation(newDir);
        }


        StartCoroutine(RealizeDeath(1, () =>
        {
            Die();
        }));
    }

    IEnumerator RealizeDeath(float time, Action task)
    {
        image.enabled = true;
        yield return new WaitForSeconds(time);
        task();
    }

    private void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
