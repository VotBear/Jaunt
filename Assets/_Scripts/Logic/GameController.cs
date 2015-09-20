using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public static GameController instance;

	public Vector2 MapMin;
	public Vector2 MapMax;
	public static Vector2 XYMin;
	public static Vector2 XYMax;
	public static bool paused;
	public TransparencyManager gameOverScreen;
	public TransparencyManager restartText;
	public TransparencyManager gameOverText;
	public GameObject pauseText;
	public GameObject playerPrefab;

	// Use this for initialization

	private bool restart;

	void Start () {
		instance = this;

		XYMax = MapMax;
		XYMin = MapMin;	

		restart = false;
		paused = false;
	}

	void Update(){  
		if (restart) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				TransitionController.instance.TransitionToScene("MainMenu");
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				restart = false;
				Application.LoadLevel (Application.loadedLevel);
			}
		} else {
			if (!paused) {
				if (Input.GetKeyDown (KeyCode.Escape)) {
					Pause ();
				}
			} else {
				if (Input.GetKeyDown (KeyCode.Escape)) {
					Unpause ();
					TransitionController.instance.TransitionToScene("MainMenu");
					//Application.LoadLevel("Tutorial");
					//Application.Quit ();
				}
				if (Input.GetKeyDown (KeyCode.Space)) {
					Unpause ();
				}
			}
		}
	}

	public void PlayerDeath(){
		GameOver ();
	}

	void GameOver(){ 
		gameOverText.FadeToTransparency(0f, 1f, 1.5f);
		restartText.FadeToTransparency(0f, 1f, 1.5f);
		gameOverScreen.FadeToTransparency(0f, 0.6f, 1.5f); 

		restart = true;
	}

	void UnGameover(){
		gameOverText.SetTransparency(0f);
		restartText.SetTransparency(0f);
		gameOverScreen.SetTransparency(0f); 
		
		restart = false;
	}

	public static Vector2 GetRandomCoord(){
		return new Vector2(Random.Range(XYMin.x, XYMax.x), Random.Range(XYMin.y, XYMax.y));
	}

	void Pause(){
		gameOverScreen.SetTransparency (.9f); 
		pauseText.gameObject.SetActive (true);
		Time.timeScale = 0;
		paused = true; 
	}

	void Unpause(){
		gameOverScreen.SetTransparency (0f);
		pauseText.gameObject.SetActive (false);
		Time.timeScale = 1;
		paused = false; 
	} 
}
