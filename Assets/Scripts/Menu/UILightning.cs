using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILightning : MonoBehaviour {
	public float duration = 6f;
	
	UnityEngine.UI.Text text;
	void Start () {
		text = GetComponent<UnityEngine.UI.Text>();
	}

	void Update () {
		float alpha = Mathf.PingPong(Time.time, duration) / duration;

		text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - alpha * 0.6f);
	}
}
