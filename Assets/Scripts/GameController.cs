using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax;
}

public class GameController : MonoBehaviour {

	public GameObject scoop;
	public GameObject cone;
	public float speed;
	public Boundary boundary;
	private float move;
	private bool canMove;
	public Text scoreText;
	public Text strikeText;
	public Canvas ui;
	public float spawnWait;
	public float startWait;
	public Vector3 spawnValues;
	private bool gameOver;
	public Button restartButton;
	private int score;
	public GameObject[] spawns;
	private int strikes;

	// Use this for initialization
	void Start () {
		canMove = false;
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
		gameOver = false;
		restartButton.gameObject.SetActive (false);
		strikes = 0;
		strikeText.text = "";
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
	}

	void FixedUpdate(){
		if (canMove) {
			GameObject[] scoops = GameObject.FindGameObjectsWithTag ("Scoop");
			Vector3 movement = new Vector3 (move, 0, 0);
			foreach (GameObject go in scoops) {
				Rigidbody rb = go.GetComponent<Rigidbody> ();
				if(rb.isKinematic){
					rb.position += movement * speed;
					rb.position = new Vector3 (
					Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
					rb.position.y,
					rb.position.z);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while(true){
			Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			int spawnIndex = Random.Range (0, spawns.Length);
			Instantiate (spawns[spawnIndex], spawnPosition, spawnRotation);
			yield return new WaitForSeconds(spawnWait);
			if (gameOver){
				restartButton.gameObject.SetActive(true);
				break;
			}
		}
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void AddStrike(){
		strikes += 1;
		if (strikes == 3) {
			gameOver = true;
		}
		string str = "";
		for (int i=0; i<strikes; i++) {
			if(i<=2){
				str+="X";
			}
		}
		strikeText.text = str;
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	public void MoveRight(){
		move = 1;
		canMove = true;
	}

	public void MoveLeft(){
		move = -1;
		canMove = true;
	}

	public void stopMove(){
		canMove = false;
	}
}
