﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager gm = null;
	public int nbLevels = 6;
	public AudioClip[] clips;
	int clipIndex = 0;

	[HideInInspector]public bool testMenu = false;
	[HideInInspector]public float difficulty = 140f;
	[HideInInspector]public AudioSource audioSource;

	void Awake () {
		if (gm == null)
			gm = this;
		else if (gm != this)
       		Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
		if (PlayerPrefs.HasKey("musicVolume"))
			audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
		else
			audioSource.volume = 0.5f;
	}
	void Start () {
		audioSource.clip = clips[0];
		initLevels(false);
	}

	void Update () {
		if (!audioSource.isPlaying)
		{
			clipIndex = (clipIndex + Random.Range(1, clips.Length)) % clips.Length;
			audioSource.clip = clips[clipIndex];
			audioSource.Play();
		}
		if (Input.GetKeyDown(KeyCode.M))
			audioSource.Stop();
	}

	public void initLevels(bool reset = false)
	{
		for (int i = 1; i <= nbLevels; ++i)
		{
			if (!PlayerPrefs.HasKey("unlocked" + i) || reset)
				PlayerPrefs.SetInt("unlocked" + i, 0);
			if (!PlayerPrefs.HasKey("success" + i) || reset)
				PlayerPrefs.SetInt("success" + i, 0);
			if (!PlayerPrefs.HasKey("newSuccess" + i) || reset)
				PlayerPrefs.SetInt("newSuccess" + i, 0);
		}
		PlayerPrefs.SetInt("unlocked1", 1);
	}
}
