using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject spawn;
	public float delay;

	void Start () {
		Invoke ("Create", delay);
	}

	void Create(){
		Instantiate(spawn, transform.position, transform.rotation);
	}
}
