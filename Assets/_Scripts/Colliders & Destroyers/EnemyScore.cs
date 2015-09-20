using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

	public int score;

	private ScoreController target;

	void OnDestroy() { 
		GameObject temp = GameObject.FindWithTag ("GameController");
		if (temp != null) target = temp.GetComponent<ScoreController>();
		if (target != null) target.AddScore (score);
	}
}
