﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	public string menuSceneName = "Main_Menu";

	public SceneFader sceneFader;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P)) {
			Toggle ();
		}
	}

	public void Toggle (){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			Time.timeScale = 0f;
			Cursor.visible = true;
		} else {
			Time.timeScale = 1f;
			Cursor.visible = false;
		}	
	}

	public void Retry ()
	{
		Toggle ();
		sceneFader.FadeTo (SceneManager.GetActiveScene ().name);
	}

	public void Menu (){
		Toggle ();
		sceneFader.FadeTo (menuSceneName);
		Cursor.visible = true;
	}
		

}
