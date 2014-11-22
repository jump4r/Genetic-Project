using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private float moveSpeed = 4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)) {
			//rigidbody.velocity = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
			transform.position = new Vector3 (transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y, -1f);
			//Debug.Log ("Move Right please");
			//RotateDirection (-90);
		}
		
		if (Input.GetKey (KeyCode.A)) {
			transform.position = new Vector3 (transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, -1f);
			//RotateDirection (90);
		}
		
		if (Input.GetKey (KeyCode.W)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), -1f);
		}
		
		if (Input.GetKey (KeyCode.S)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), -1f);
			//RotateDirection (180);
		}
	}
}
