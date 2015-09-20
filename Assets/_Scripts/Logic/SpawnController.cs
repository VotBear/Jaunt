using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	public GameObject spawn;
	public Vector2 minDistFromPlayer;
	public Vector2 maxDistFromPlayer;
	public float startDelay;
	public float startInterval;
	public float minInterval;
	public float intervalChangeRate;
	public int quantity;
	public int iterations;	//infinite if 0
	 
	private Vector2 playerPos;
	private float interval;
	private int iter;

	void Start () {  
		iter = 0;
		interval = startInterval;
		Invoke("SpawnEnemy", startDelay);
	}

	void SpawnEnemy () 
	{ 
		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp != null)
			playerPos = temp.GetComponent<Transform> ().position;
		else
			return;

		for (int i=1; i<=quantity; ++i) {
			Vector3 pos =GeneratePosition ();
			Instantiate (spawn, pos, Quaternion.identity);
		}

		if (iterations > 0) {
			++iter;
			if (iter >= iterations) return;
		}

		Invoke("SpawnEnemy", interval);
		interval = Mathf.Clamp (interval - intervalChangeRate, minInterval, startInterval);
	}

	Vector3 GeneratePosition()
	{
		float minX, minY, maxX, maxY;
		
		//within maxdist from player AND arena limits
		minX = Mathf.Max (GameController.XYMin.x , playerPos.x - maxDistFromPlayer.x);
		minY = Mathf.Max (GameController.XYMin.y , playerPos.y - maxDistFromPlayer.y);
		maxX = Mathf.Min (GameController.XYMax.x , playerPos.x + maxDistFromPlayer.x);
		maxY = Mathf.Min (GameController.XYMax.y , playerPos.y + maxDistFromPlayer.y);

		Vector3 ret = new Vector3 (Random.Range (minX, maxX), Random.Range (minY, maxY), 0f);

		//cannot be too close to player;
		while (Mathf.Abs (playerPos.x - ret.x) <= minDistFromPlayer.x &&
			   Mathf.Abs (playerPos.y - ret.y) <= minDistFromPlayer.y) {
			ret = new Vector3 (Random.Range (minX, maxX), Random.Range (minY, maxY), 0f);
			//Debug.Log("FAIL");
			break;
		} 

		return ret;

	}
}

