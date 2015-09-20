using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public Text scoretext;

	private int score; 

	void Start () {
		score = 0;
	}

	public void AddScore(int add){
		score += add;
	}
	
	// Update is called once per frame
	void Update () {
		scoretext.text = "" + score;
	}
}
