using UnityEngine;
using System.Collections;

public class MovementBehaviour : MonoBehaviour {

	private float moveSpeed = 5f; // Movement Speed determined by Gene[]
	private float rotationSpeed = 5f; // Rotaiton Speed determined by Gene[]

	TargetBehaviour target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Move this agent.
	/// </summary>
	public void Move() {
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
	}
}
