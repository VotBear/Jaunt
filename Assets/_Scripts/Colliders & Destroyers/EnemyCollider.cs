using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			other.GetComponent<PlayerController>().Damage(1); 
		}
	}


	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
