using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour {
	public Transform slider;
	public float minValue = 0f;
	public float maxValue = 1f;

	float value;
	void Start () {
		value = slider.localScale.x * maxValue;
	}

	void Update () {
	}

	public void updateSlider()
	{
		slider.localScale += new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * 8f, 0f, 0f);
		if (slider.localScale.x > 1f)
			slider.localScale = new Vector3(1f, slider.localScale.y, slider.localScale.z);
		if (slider.localScale.x < 0f)
			slider.localScale = new Vector3(0f, slider.localScale.y, slider.localScale.z);
		value = slider.localScale.x * maxValue;
		PlayerPrefs.SetFloat("musicVolume", value);
		GameManager.gm.audioSource.volume = value;
	}

	public void setValue(float val)
	{
		slider.localScale = new Vector3(val, slider.localScale.y, slider.localScale.z);
		value = slider.localScale.x * maxValue;
		GameManager.gm.audioSource.volume = value;
	}
}
