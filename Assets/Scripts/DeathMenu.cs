﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public void RestartGame() {
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void ReturnToMain(string levelToLoad) {
		SceneManager.LoadScene (levelToLoad);
	}
}


