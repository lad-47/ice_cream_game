using UnityEngine;
using System.Collections;

public class ScoopController : MonoBehaviour {
	private Rigidbody rb;
	private GameObject scoop;
	private GameController gameController;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		scoop = this.gameObject;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void FixedUpdate(){
	}

	void OnCollisionEnter(Collision col){
		if (!rb.isKinematic) {
			Transform check = col.transform;
			if (rb.transform.position.x - check.position.x <= .55 && rb.transform.position.y >= 0.5) {
				rb.isKinematic = true;
				gameController.AddScore(10);
				scoop.transform.position = new Vector3(check.position.x,
				                                       1.5f,
				                                       check.position.z);
				GameObject[] scoops = GameObject.FindGameObjectsWithTag ("Scoop");
				foreach (GameObject go in scoops) {
					go.transform.position = new Vector3 (go.transform.position.x,
			                                    		 go.transform.position.y - 0.9f,
			                                   			 go.transform.position.z);
				}
			}
		}
	}
}