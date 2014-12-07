using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour {

	private GameObject[] individuals;

	// Public Varialbles for Inidividual Init.
	public int popSize;
	public bool init;

	// Game Object prefabs
	public GameObject[] prefabs;

	// Initialize Vector start positions
	public Vector3[] spawnLocations;

	// Use this for initialization
	void Start () {
		Debug.Log ("Population Script: Loaded");
		Initialize (spawnLocations.Length, true);
	}

	// Initialize Constructor
	private void Initialize(int populationSize, bool init) {
		individuals = new GameObject[populationSize];
		// Loop and create individuals
		for (int i = 0; i < populationSize; i++) {
			GameObject newIndividual = (GameObject)prefabs[i % 2];
			GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
			individuals[i] = rtn;
		}

		// Debug Log
		Debug.Log ("Individuals Created: " + individuals.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
