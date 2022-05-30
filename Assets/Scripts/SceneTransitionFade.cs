using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionFade : MonoBehaviour
{
	public float fadeTime;
	
	private bool fadingOut = false;
	private Image fadeScreen;
    private AudioSource[] audioSources;
	private float timer = 0.0f;
	
	private Color fadeColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	
	private int toScene;

    void Start()
    {
        fadeScreen = this.GetComponent<Image>();
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
		if (fadingOut)
		{
			timer += Time.deltaTime;
			if (fadeTime > timer)
			{
				fadeColor.a = (float)((255 * (timer / fadeTime)) / 255 );
				fadeScreen.color = fadeColor;

                foreach (AudioSource audioSource in audioSources) audioSource.volume = 1 - (timer / fadeTime);
			}
			else
			{
				fadeColor.a = 1.0f;
				fadeScreen.color = fadeColor;
				timer = 0.0f;
                SceneManager.LoadScene(sceneBuildIndex:toScene);        
			}
		}
    }
	
	public void FadeOut(int scene)
	{
		toScene = scene;
		fadingOut = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().immobilized = true;
	}
}