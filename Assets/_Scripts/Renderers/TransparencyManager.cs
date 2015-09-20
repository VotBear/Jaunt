using UnityEngine;
using UnityEngine.UI;
using System.Collections; 
using System.Collections.Generic; 

public class TransparencyManager : MonoBehaviour {
  
	public float startAlpha;
	 
	private float prevAlpha;
	private float targetAlpha; 
 
	public Text text;
	public Image img;
	public bool isText; 

	void Start () {
		SetAlpha(startAlpha);  
	} 

	public void FadeToTransparency(float target, float fadeDuration){
		FadeToTransparency (GetAlpha(), target, fadeDuration);
	}
	
	public void FadeToTransparency(float startf, float target, float fadeDuration){
		prevAlpha = startf;
		targetAlpha = target;
		StopCoroutine ("Fade");
		StartCoroutine("Fade",fadeDuration);
	}
	
	public void OscillateTransparency(float target, float fadeDuration){ 
		OscillateTransparency (GetAlpha (), target, fadeDuration);
	}

	public void OscillateTransparency(float startf, float target, float fadeDuration){ 
		StartCoroutine( Oscillate( startf, target, fadeDuration) ); 
	}

	public void SetTransparency(float target){
		targetAlpha = target;
		SetAlpha (targetAlpha);
	}

	void SetAlpha(float alpha){  
		if (isText) {
			Color col = text.color;
			col.a = alpha;
			text.color = col;
		} else {
			Color col = img.color;
			col.a = alpha;
			img.color = col; 
		}
	}

	float GetAlpha(){
		if (isText) {
			Color col = text.color;
			return col.a;
		} else {
			Color col = img.color;
			return col.a; 
		}
	}

	IEnumerator Oscillate(float startf, float target, float duration){
		//StopAllCoroutines ();
		prevAlpha = startf;
		targetAlpha = target; 
		yield return StartCoroutine("Fade",duration);
		
		prevAlpha = target;
		targetAlpha = startf; 
		yield return StartCoroutine("Fade",duration);
	}

	IEnumerator Fade(float duration) {
		//StopAllCoroutines ();
		for (float t = 0f; t <= duration; t += 0.01f) {  

			float newAlpha = prevAlpha + ( ( (targetAlpha - prevAlpha) * (t) ) / duration );
			SetAlpha(newAlpha);
		
			yield return new WaitForSeconds(0.01f);
		
		}
	} 
}
