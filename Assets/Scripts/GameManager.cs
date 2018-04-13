using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager gm = null;
	public AudioClip[] clips;
	int clipIndex = 0;

	AudioSource audioSource;

	void Awake () {
		if (gm == null)
			gm = this;
		else if (gm != this)
       		Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
	}
	void Start () {
		audioSource.clip = clips[Random.Range(0, clips.Length)];
	}

	void Update () {
		if (!audioSource.isPlaying)
		{
			clipIndex = (clipIndex + Random.Range(1, clips.Length)) % clips.Length;
			audioSource.clip = clips[clipIndex];
			audioSource.Play();
		}
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		if (Input.GetKeyDown(KeyCode.N))
			audioSource.Stop();
	}
}
