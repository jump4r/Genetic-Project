using UnityEngine;
using System.Collections;

public class MovementBehaviour : MonoBehaviour {

	private float moveSpeed = 5f; // Movement Speed determined by Gene[]
	private float rotationSpeed = 5f; // Rotaiton Speed determined by Gene[]

	TargetBehaviour target;
	EvadeBehaviour evade;

	// Use this for initialization
	void Start () {
		target = GetComponent<TargetBehaviour> ();
		evade = GetComponent<EvadeBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Set Variables based off of the genes
	public void SetMoveSpeed(int _moveSpeed) {
		moveSpeed = (float)_moveSpeed * 2f;
	}

	public void SetRotationSpeed(int _rotationSpeed) {
		rotationSpeed = (float)_rotationSpeed * 2f;
	}

	/// <summary>
	/// Move this agent.
	/// </summary>
	public void Move() {
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);

		// Keep Movement constrained off the Z-axis, because the ships are bound to run into each other.
		Vector3 newPos = transform.position;
		newPos.z = 0;
		transform.position = newPos;
	}

	/// <summary>
	/// Manage the rotaiton of the object.
	/// </summary>
	public void Rotate() {
		// Dodge bullets incomming
		if (evade.incomming) {
			transform.rotation = Quaternion.Lerp (transform.rotation, evade.GetEvadeRotation(), Time.deltaTime * rotationSpeed);

			// Constrain rotation ALONG the Z-axis, because the ships are bound to run into each other.
			Quaternion newRotation = transform.rotation;
			newRotation.x = 0;
			newRotation.y = 0;
			transform.rotation = newRotation;
	
			return;
		}

		if (target.GetTarget () != null) {
			transform.rotation = Quaternion.Lerp (transform.rotation, target.GetAngle(target.GetTarget ()), Time.deltaTime * rotationSpeed);

			Quaternion newRotation = transform.rotation;
			newRotation.x = 0;
			newRotation.y = 0;
			transform.rotation = newRotation;
			return;
		}
	}
}
