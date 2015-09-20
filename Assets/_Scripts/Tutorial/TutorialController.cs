using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public int pagelimit;
	public GameObject healthbar;
	public GameObject jauntbar; 
	public int healthstart;
	public int jauntstart; 

	public static int pageno; 

	void Start () {
		pageno = 1;
		CheckTutorialText ();
 
	}

	void FixedUpdate(){ 
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (pageno < pagelimit) {
				pageno++;
				CheckTutorialText();
			}
			else {
				//change scene here
				TransitionController.instance.TransitionToScene("Survival");
				//Application.LoadLevel("Survival");
			} 
		}
	}

	void CheckTutorialText(){
		 
		jauntbar.SetActive(pageno >= jauntstart);
		healthbar.SetActive(pageno >= healthstart);
		foreach (GameObject temp in GameObject.FindGameObjectsWithTag("TutorialObject")){
 
			TutorialActivator tmp = temp.GetComponent<TutorialActivator>();
			if (tmp!=null){
				if (pageno >= tmp.startPage && TutorialController.pageno <= tmp.endPage) {
					tmp.SetActiveChildren(1);  
				} else {
					tmp.SetActiveChildren(0); 
				}
			}

			TutorialEnemySpawner tms = temp.GetComponent<TutorialEnemySpawner>();
			if (tms!=null){
				
			}
		}
	}
}
