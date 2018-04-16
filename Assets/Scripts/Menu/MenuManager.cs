using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject[] menus;
	public SliderScript musicSlider;
	public float zoomRange = 0.3f;
	public float zoomSpeed = 5f;

	Transform selected = null;
	SliderScript selectedSlider = null;
	Vector3 selectedScale;
	float timer = 0f;
	int curMenu = 0;
	bool testMenu = false;

	void Start () {

		if (!PlayerPrefs.HasKey("musicVolume"))
			PlayerPrefs.SetFloat("musicVolume", 0.5f);
		if (musicSlider)
			musicSlider.setValue(PlayerPrefs.GetFloat("musicVolume"));
	}

	void Update () {
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			if (hit.collider.tag == "Button")
			{
				timer += Time.deltaTime;
				if (selected != hit.transform)
				{
					selected = hit.transform;
					selectedScale = selected.localScale;
				}
				else
				{
					float zoom = Mathf.PingPong(timer, zoomRange * zoomSpeed) / zoomSpeed;
					selected.localScale = new Vector3(selectedScale.x * (1f + zoom), selectedScale.y * (1f + zoom), selectedScale.z * (1f + zoom));
				}
			}
			else if (hit.collider.tag == "Slider")
			{
				selected = hit.transform;
				selectedScale = selected.localScale;
				selectedSlider = selected.GetComponent<SliderScript>();
			}
			else if (selected)
				unselectButton();
		}
		if (Input.GetMouseButtonDown(0) && selected && selected.tag == "Button")
		{
			UnityEngine.UI.Button button;
			if ((button = selected.GetComponent<UnityEngine.UI.Button>()))
				button.onClick.Invoke();
		}
		if (Input.GetMouseButton(0) && selected && selected.tag == "Slider")
			selectedSlider.updateSlider();
	}

	void unselectButton()
	{
		if (selectedSlider)
			selectedSlider = null;
		selected.localScale = selectedScale;
		selected = null;
		timer = 0;
	}

	public void loadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void loadMenu(int id)
	{
		
		if (id == 3)
		{
			id -= 1;
			testMenu = true;
		}
		else
			testMenu = false;
		menus[curMenu].SetActive(false);
		menus[id].SetActive(true);
		curMenu = id;
		if (selected)
			unselectButton();
	}

	public void reset()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetFloat("musicVolume", 0.5f);
		if (musicSlider)
			musicSlider.setValue(PlayerPrefs.GetFloat("musicVolume"));
	}

	public void exit()
	{
		Application.Quit();
	}
}
