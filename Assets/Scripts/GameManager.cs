using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStart;

	public PlayerController player;
	private Vector3 playerStart;

	private PlatformDestroyer[] platformList;

	private ScoreManager scoreManager;

	public DeathMenu deathScreen;

	public AudioSource bgm;

	// Use this for initialization
	void Start () {
		platformStart = platformGenerator.position;
		playerStart = player.transform.position;

		scoreManager = FindObjectOfType<ScoreManager> ();

		bgm.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart() {
		scoreManager.scoreIncreasing = false;
		player.gameObject.SetActive (false);

		deathScreen.gameObject.SetActive (true);

		//StartCoroutine ("RestartCo");
	}

	public void Reset() {
		deathScreen.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) 
		{
			platformList [i].gameObject.SetActive (false);
		}
		player.transform.position = playerStart;
		platformGenerator.position = platformStart;
		player.gameObject.SetActive (true);

		scoreManager.score = 0;
		scoreManager.scoreIncreasing = true;

		bgm.Stop ();
		bgm.Play ();
	}

	/*
	public IEnumerator RestartCo() {
		scoreManager.scoreIncreasing = false;
		player.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) 
		{
			platformList [i].gameObject.SetActive (false);
		}
		player.transform.position = playerStart;
		platformGenerator.position = platformStart;
		player.gameObject.SetActive (true);

		scoreManager.score = 0;
		scoreManager.scoreIncreasing = true;
	}
	*/
}
