using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	public float speed;
	//public GameObject cam;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.up * speed * -1f;
	}

	void Update(){
		//cam.transform.position = new Vector3 (cam.transform.position.x,
		  //                                   cam.transform.position.y + speed * 1f,
		    //                                 cam.transform.position.z);
	}

	void FixedUpdate(){
		rb.transform.position = new Vector3 (rb.transform.position.x,
		                                    rb.transform.position.y - speed * 1f,
		                                    rb.transform.position.z);

	}
}
