﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;

	public void PauseGame() {
		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
	}

	public void ResumeGame() {
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
	}

	public void RestartGame() {
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void ReturnToMain(string levelToLoad) {
		Time.timeScale = 1f;
		SceneManager.LoadScene (levelToLoad);
	}
}
