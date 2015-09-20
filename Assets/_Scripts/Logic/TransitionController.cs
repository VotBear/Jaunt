using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {

	public Image[] lis;
	public static TransitionController instance;
	public bool startOpaque;
	public float maxHeight;

	public int frames;
	public float frameDuration;
	public float layerDelay;

	void Start () { 
		instance = this;

		if (startOpaque) {  
			SetHeight (maxHeight);
			StartCoroutine ("OpenTransition");
		} else {
			SetHeight (0); 
		}

	}

	public void TransitionToScene(string newSceneName){
		StartCoroutine (Transition (newSceneName));
	}

	void SetHeight(float height){
		foreach ( Image tmp in lis){
			tmp.rectTransform.sizeDelta = new Vector2 (tmp.rectTransform.sizeDelta.x , height);
		}
	}

	IEnumerator Transition(string newSceneName){
		yield return StartCoroutine("CloseTransition");
		yield return new WaitForSeconds(layerDelay * lis.Length + frames * frameDuration - 0.2f);
		Application.LoadLevel(newSceneName);  
	}

	IEnumerator CloseTransition(){
		SetHeight (0);
		foreach ( Image tmp in lis){
			StartCoroutine(Transition (tmp, 0, maxHeight));
			yield return new WaitForSeconds(layerDelay);
		}
		yield return new WaitForSeconds(1f);
	}
	
	IEnumerator OpenTransition(){
		SetHeight (maxHeight);
		foreach ( Image tmp in lis){
			StartCoroutine(Transition (tmp, maxHeight, 0));
			yield return new WaitForSeconds(layerDelay);
		}
		yield return new WaitForSeconds(1f);
	}
	
	IEnumerator Transition(Image img, float start, float end) {
		//StopAllCoroutines ();
		for (int t = 1; t <= frames; t += 1) {  

			float ratio = t;
			ratio /= (float) frames;
			float nexheight = start + ((end - start) * ratio);

			Color col = img.color;
			col.a = ratio;
			if (start>end) col.a = 1f-ratio;

			img.rectTransform.sizeDelta = new Vector2 (img.rectTransform.sizeDelta.x , nexheight);
			img.color = col;
			yield return new WaitForSeconds(frameDuration);
		}
	}
}
