using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
	
	//public GameObject[] platforms;
	public ObjectPooler[] objectPools;
	public Transform generationPoint;

	private float[] platformWidths;
	private int platformSelector;

	public float gapMin;
	public float gapMax;

	private float gap;

	private float minHeight;
	private float maxHeight;
	public Transform maxHeightPoint;
	public float maxHeightChange;
	private float heightChange;

	private CoinGenerator coinGenerator;
	public float randomCoinThreshold;

	// Use this for initialization
	void Start () {
		//platformWidth = platform.GetComponent<BoxCollider2D> ().size.x;
		platformWidths = new float[objectPools.Length];
		for(int i = 0; i < platformWidths.Length; i++)
		{
			platformWidths [i] = objectPools [i].pooledObject.GetComponent<BoxCollider2D> ().size.x;		
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		coinGenerator = FindObjectOfType<CoinGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) 
		{
			gap = Random.Range (gapMin, gapMax);
			platformSelector = Random.Range(0, objectPools.Length);
			heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);
			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			} else if (heightChange < minHeight) 
			{
				heightChange = minHeight;
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + gap, heightChange, transform.position.z);

			//Instantiate (platforms[platformSelector], transform.position, transform.rotation);
			GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) 
			{
				coinGenerator.SpawnCoins (new Vector3 (transform.position.x, transform.position.y + 1.5f, transform.position.z));
			}
				
			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
		}
	}
}
