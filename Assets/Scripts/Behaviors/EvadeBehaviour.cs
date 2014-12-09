using UnityEngine;
using System.Collections;

public class EvadeBehaviour : MonoBehaviour {

	private GameObject targetedBy;
	private Quaternion evadeRotation;
	private Ship ship;

	public bool incomming = false;
	private int team = -1;

	// Use this for initialization
	void Start () {
		ship = GetComponent<Ship> ();
		team = ship.GetTeam ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		incomming = false;
	}

	/// <summary>
	/// Sets the target that is targeting this Agent
	/// </summary>
	public void SetTargetedBy(GameObject go) {
		targetedBy = go;
	}

	/// <summary>
	/// Removes the targeted by agent.
	/// </summary>
	public void RemoveTargetedBy() {
		targetedBy = null;	
	}

	/// <summary>
	/// Evades Bullets thrown by enemies
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerStay(Collider col) {
		if (col.tag == "Bullet") {
			if (col.gameObject.GetComponent<Bullet>().GetTeam () != team) { // Only dodge enemy team bullets.
				/* Vector3 targetDir = col.transform.position + transform.position;
				float step = Time.deltaTime;
				Vector3 _newDir = Vector3.RotateTowards(transform.up, targetDir, step, 0.0f);
				Vector3 newDir = new Vector3(_newDir.x, _newDir.y, 0f);
				Debug.DrawRay(transform.position, newDir, Color.red);
				evadeRotation = Quaternion.LookRotation(newDir); */

				Vector3 dir = col.transform.position + transform.position;
				Debug.DrawRay (transform.position, dir);
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
				evadeRotation = Quaternion.AngleAxis (angle, Vector3.forward);
				incomming = true;
			}
		}
	}

	/// <summary>
	/// Gets the evade rotation.
	/// </summary>
	/// <returns>The evade rotation.</returns>
	public Quaternion GetEvadeRotation() {
		return evadeRotation;
	}
}
