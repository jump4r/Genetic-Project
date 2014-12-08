using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour {

	private GameObject[] redIndividuals;
	private GameObject[] blueIndividuals;

	// Public Varialbles for Inidividual Init.
	public int popSize;
	public bool init;

	// Game Object prefabs
	public GameObject[] prefabs;

	// Initialize Vector start positions
	public Vector3[] spawnLocations;

	// Use this for initialization
	void Start () {
		// Debug.Log ("Population Script: Loaded");
		Initialize (spawnLocations.Length, true);
	}

	// Initialize Constructor
	private void Initialize(int populationSize, bool init) {
		redIndividuals = new GameObject[populationSize / 2];
		blueIndividuals = new GameObject[populationSize / 2];

		// Temporary Index Variables for the loop
		int redCurrentIndex = 0;
		int blueCurrentIndex = 0;

		// Loop and create individuals
		for (int i = 0; i < populationSize; i++) {
			// Initialize BLUE Individuals
			if ( i % 2 == 0 ) {
				GameObject newIndividual = (GameObject)prefabs[i % 2];
				GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
				blueIndividuals[blueCurrentIndex] = rtn;
				blueCurrentIndex++;
			}

			if ( i % 2 == 1) {
				GameObject newIndividual = (GameObject)prefabs[i % 2];
				GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
				redIndividuals[redCurrentIndex] = rtn;
				redCurrentIndex++;
			}
		}

		// Debug Log
		Debug.Log ("RED Individuals Created: " + redIndividuals.Length);
		Debug.Log ("BLUE Individuals Created: " + blueIndividuals.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Returns a Specific GameObject from the population
	public GameObject GetRedIndividual(int index) {
		return redIndividuals [index];
	}

	public GameObject GetBlueIndividual(int index) {
		return blueIndividuals [index];
	}

	// Get Fittest RED Agent
	public GameObject GetRedFittest() {
		GameObject fittest = redIndividuals [0];
		for (int i = 0; i < RedSize(); i++) {
			if (fittest.GetComponent<Individual>().GetFitness () < redIndividuals[i].GetComponent<Individual>().GetFitness()) {
				fittest = redIndividuals[i];
			}
		}
		return fittest;
	}

	// Get Fittest BLUE Agent
	public GameObject GetBlueFittest() {
		GameObject fittest = blueIndividuals [0];
		for (int i = 0; i < RedSize(); i++) {
			if (fittest.GetComponent<Individual>().GetFitness () < blueIndividuals[i].GetComponent<Individual>().GetFitness()) {
				fittest = blueIndividuals[i];
			}
		}
		return fittest;
	}

	// Get Population Size
	public int RedSize() {
		return redIndividuals.Length;
	}

	public int BlueSize() {
		return blueIndividuals.Length;
	}


}
