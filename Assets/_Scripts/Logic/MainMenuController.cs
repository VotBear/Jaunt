using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public int items;
	public TransparencyManager[] transp;
	public ParticleEmitter[] partic;

	private int selected;
	private int next;

	void Start () {
		selected = 0;
		transp [selected].SetTransparency (1);
	}
	
	// Update is called once per frame
	void Update () { 
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			next = (selected + items - 1) % items;
			ChangeSelection(next);
		} 
		
		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			next = (selected + 1) % items;
			ChangeSelection(next);
		} 
		
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			Select();
		} 
	}

	void ChangeSelection(int next){
		UpdateView (selected, 0);
		selected = next;
		UpdateView (selected, 1);
	}

	void UpdateView(int index, int id){
		Debug.Log (index);
		if (id == 0) {	//stop animation
			if (transp[index]!=null){
				transp[index].FadeToTransparency(.3f ,.2f );	
			}
			if (partic[index]!=null){
				partic[index].enabled = false;
			}

		} else {		//start animation
			if (transp[index]!=null){
				transp[index].FadeToTransparency(1f ,.2f );	
			}
			if (partic[index]!=null){
				partic[index].enabled = true;
			}

		}
	}

	void Select(){

		if (selected == 0) { //newgame
			TransitionController.instance.TransitionToScene("Survival");
			//Application.LoadLevel("Survival"); 
		
		} else if (selected == 1) { //tutorial
			TransitionController.instance.TransitionToScene("Tutorial");
			//Application.LoadLevel("Tutorial"); 
		
		} else if (selected == 2) { //exit
			Application.Quit();
		
		}
	}
}
