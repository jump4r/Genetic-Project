using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetBehaviour : MonoBehaviour {

	private Ship ship;
	private int team; // BLUE TEAM = 1 ||| RED TEAM = 2
	private GameObject target; // This shit's current target.

	private List<GameObject> targetPool = new List<GameObject>(); // List of available targets

	// Use this for initialization
	void Start () {
		ship = GetComponent<Ship> ();
		team = ship.GetTeam ();

		// Declare and add to target pool
		GameObject[] allAgents = GameObject.FindGameObjectsWithTag ("Agent");
		foreach (GameObject agent in allAgents) {
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

	}

	/// <summary>
	/// Finds a random target.
	/// </summary>
	public void FindTarget() {
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
}
