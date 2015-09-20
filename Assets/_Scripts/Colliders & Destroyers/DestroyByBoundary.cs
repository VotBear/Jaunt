using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	
	void OnTriggerExit2D(Collider2D other)
	{ 
		HealthManager temp = other.GetComponent<HealthManager>();
		if (temp != null) {
			temp.GetComponentInParent<EnemyScore>().score = 0;
			temp.Damage(10000);
		}
	} 
}
