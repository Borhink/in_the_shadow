using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour {
	public int id = 1;
	int unlocked = 0;
	int success = 0;
	void Start () {
		if (PlayerPrefs.HasKey("unlocked" + id))
			unlocked = PlayerPrefs.GetInt("unlocked" + id);
		if (PlayerPrefs.HasKey("success" + id))
			success = PlayerPrefs.GetInt("success" + id);
		if (unlocked == 0)
			transform.parent.localScale /= 3;
		if (success == 1)
			transform.localEulerAngles -= new Vector3(transform.localEulerAngles.x, 0.2f, transform.localEulerAngles.z);
	}

	void Update () {
		
	}

	public void loadLevel(string name)
	{
		if (unlocked == 1)
			SceneManager.LoadScene(name);
	}
}
