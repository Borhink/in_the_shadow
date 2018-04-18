using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour {
	public int id = 1;
	int unlocked = 0;
	int success = 0;
	void Start () {
	}

	void Update () {
		
	}
	void OnDisable()
    {
        if (GameManager.gm.testMenu == false && unlocked == 0)
			transform.parent.localScale = new Vector3(1f, 1f, 1f);
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("unlocked" + id))
			unlocked = PlayerPrefs.GetInt("unlocked" + id);
		if (PlayerPrefs.HasKey("success" + id))
			success = PlayerPrefs.GetInt("success" + id);
		if (GameManager.gm.testMenu == false && unlocked == 0)
			transform.parent.localScale /= 3;
		if (PlayerPrefs.GetInt("newUnlock" + id) == 1)
		{
			if (PlayerPrefs.GetInt("newSuccess" + id) != 1)
				StartCoroutine(unlockAnimation());
			PlayerPrefs.SetInt("newUnlock" + id, 0);
		}
		if (success == 1)
		{
			if (PlayerPrefs.GetInt("newSuccess" + id) == 1)
			{
				StartCoroutine(successAnimation());
				PlayerPrefs.SetInt("newSuccess" + id, 0);
			}
			else
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
		}
    }

	public void loadLevel(string name)
	{
		if (unlocked == 1 || GameManager.gm.testMenu)
			SceneManager.LoadScene(name);
	}

	IEnumerator successAnimation() {
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 25f, transform.localEulerAngles.z);
		for (int i = 0; i < 25; ++i)
		{
			transform.localEulerAngles -= new Vector3(0f, 1f, 0f);
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator unlockAnimation() {
		transform.parent.localScale /= 3;
		float growVal = 1f - transform.parent.localScale.x;
		for (int i = 0; i < 25; ++i)
		{
			transform.parent.localScale += new Vector3(growVal / 25f, growVal / 25f, growVal / 25f);
			yield return new WaitForSeconds(0.05f);
		}
	}
}
