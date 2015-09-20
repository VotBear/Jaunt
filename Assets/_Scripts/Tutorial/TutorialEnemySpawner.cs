using UnityEngine;
using System.Collections;

public class TutorialEnemySpawner : MonoBehaviour {
	
	public GameObject spawn;
	public GameObject spawnAnim;
	public float delay;
	public int startPage;
	public int endPage;
	private bool rdy;
	private bool spawned;

	void Start () {
		//StartSpawn ();
		rdy = false;
		spawned = false;
	}

	void FixedUpdate(){

		if (!spawned) {
			if (TutorialController.pageno >= startPage){
				StartSpawn ();
				spawned = true;
			}
		}

		EnemyScore child = GetComponentInChildren<EnemyScore> ();
		if (rdy && child == null){
			Debug.Log("CHILD DEATH");
			StartSpawn ();
			rdy = false;
		}

		if (TutorialController.pageno > endPage) {
			Destroy (this.gameObject);
		}
	}

	void StartSpawn(){

		GameObject temp = Instantiate(spawnAnim,transform.position, transform.rotation) as GameObject;
		temp.SetActive (true);
		temp.transform.parent = this.transform;

		Invoke ("Create", delay);
	}

	void Create(){
		//Debug.Log ("AAA");
		GameObject temp = Instantiate(spawn,transform.position, transform.rotation) as GameObject;
		temp.SetActive (true);
		temp.transform.parent = this.transform;
		rdy = true;
		//Debug.Log ("BBB");
		//this.gameObject ();
	}
}
