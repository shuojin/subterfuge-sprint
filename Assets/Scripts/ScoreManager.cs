using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text hiScoreText;

	public float score;
	public float hiScore;

	public float pointsPerSecond;
	public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("HighScore") != null) 
		{
			hiScore = PlayerPrefs.GetFloat ("HighScore");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreIncreasing) 
		{
			score += pointsPerSecond * Time.deltaTime;
		}
		if (score > hiScore) 
		{
			hiScore = score;
			PlayerPrefs.SetFloat ("HighScore", hiScore);
		}
		scoreText.text = "Score: " + Mathf.Round(score);
		hiScoreText.text = "High Score: " + Mathf.Round(hiScore);
	}

	public void AddScore (int pointsToAdd) {
		score += pointsToAdd;
	}
}
