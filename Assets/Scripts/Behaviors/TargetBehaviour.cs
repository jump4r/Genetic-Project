using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetBehaviour : MonoBehaviour {

	private Ship ship;
	private int team = 0; // BLUE TEAM = 1 ||| RED TEAM = 2
	public GameObject target; // This shit's current target.

	private List<GameObject> targetPool = new List<GameObject>(); // List of available targets

	// Use this for initialization
	void Start () {
		ship = GetComponent<Ship> ();
		team = ship.GetTeam ();
	
		UpdateTargetPool ();
	}

	/// <summary>
	/// Update Target Pool
	/// </summary>
	public void UpdateTargetPool() {
		targetPool.Clear ();
		// Declare and add to target pool
		GameObject[] allAgents = GameObject.FindGameObjectsWithTag ("Agent");

		// Some dumbass error that makes it so that shit isn't referencing an object even though it totally is.
		/*
		if (allAgents.Length < 2) {
			return;
		}*/

		foreach (GameObject agent in allAgents) {
			Debug.Log (allAgents.Length);
			if (agent.GetComponent<Ship>().GetTeam () != team) {
				targetPool.Add(agent);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// Find a target -- Ideally this will be based off of what the genetic weights.
		if (target == null) {
			FindTarget ();
		}

		// Rotaiton Towards the target
		else {
			// transform.rotation = Quaternion.Lerp (transform.rotation, GetAngle (target), Time.deltaTime * 2f);
		}

	}

	/// <summary>
	/// Finds a random target.
	/// BUG: Update Target List after enemy dies.
	/// </summary>
	public void FindTarget() {
		// Return null if there is nothing in the target pool
		UpdateTargetPool ();
		if (targetPool.Count < 1) {
			return;
		}

		// Find Closest Target I suppose.
		float closestDist = 99f;
		int targetIndex = 0;
		for (int i = 0; i < targetPool.Count; i++) { // Still dumb af that List uses Count and Arrays use Length. Lord help me.
			float dist = Vector3.Distance (transform.position, targetPool[i].transform.position);
			if (dist < closestDist) {
				targetIndex = i;
				closestDist = dist;
			}
		}
		target = targetPool [targetIndex];
		target.GetComponent<EvadeBehaviour> ().SetTargetedBy (gameObject);
	}

	/// <summary>
	/// Finds target that is currently attacking another friendly agent. Probably called from AssistBehaviour.cs or something idk.
	/// </summary>
	/// <param name="helpTarget">Help target.</param>
	public void FindTarget(GameObject helpTarget) {

	}

	/// <summary>
	/// Sets the target.
	/// </summary>
	/// <param name="t">T.</param>
	void SetTarget(GameObject t) {
		target = t;
	}

	public GameObject GetTarget() {
		if (target != null) {
			return target;
		}

		return null;
	}

	/// <summary>
	/// Removes the target.
	/// </summary>
	void RemoveTarget() {
		target.GetComponent<EvadeBehaviour> ().RemoveTargetedBy ();
		target = null;
	}

	/// <summary>
	/// Gets the angle between the self & Target Object
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="temp">Temp.</param>
	public Quaternion GetAngle(GameObject target) {
		Vector3 dir = target.transform.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		// Debug.Log ("Current Angle is " + q);
		return q;
	}
}
