using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	private GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){
		if (!other.attachedRigidbody.isKinematic) {
			gameController.AddStrike();
		}
		Destroy (other.gameObject);
	}
}
