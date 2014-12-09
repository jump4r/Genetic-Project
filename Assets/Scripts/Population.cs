using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour {

	private GameObject[] redIndividuals;
	private GameObject[] blueIndividuals;

	// Public Varialbles for Inidividual Init.
	public int popSize = 2;
	public bool init;
	public int totalAgentsRemaining;

	// Game Object prefabs
	public GameObject[] prefabs;

	// Initialize Vector start positions
	private Vector3[] spawnLocations;

	// For fitness calculaiton
	private FitnessCalculation fc;

	// Use this for initialization
	void Start () {
		fc = GetComponent<FitnessCalculation> ();
		spawnLocations = new Vector3[popSize];
		for (int i = 0; i < spawnLocations.Length; i++) {
			spawnLocations[i].x = Random.Range (-10f, 10f);
			spawnLocations[i].y = Random.Range (-10f, 10f);
			spawnLocations[i].z = 0;
		}
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
				rtn.GetComponent<Individual>().indexInPopulation = i;
				blueIndividuals[blueCurrentIndex] = rtn;
				blueCurrentIndex++;
			}

			if ( i % 2 == 1) {
				GameObject newIndividual = (GameObject)prefabs[i % 2];
				GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
				rtn.GetComponent<Individual>().indexInPopulation = i;
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

	// Get Population Sizes for Red and Blue.
	public int RedSize() {
		return redIndividuals.Length;
	}

	public int BlueSize() {
		return blueIndividuals.Length;
	}

	/// <summary>
	/// Updates the total remaining Agents
	/// </summary>
	public void UpdateTotalRemainingAgents() {
		GameObject[] totalAgents = GameObject.FindGameObjectsWithTag ("Agent");
		// If there is only one agent left, or only Agents of one team left get the fitness of that agent, this is the end game signal.
		int totalBlueAgents = 0;
		int totalRedAgents = 0;
		for (int i = 0; i < totalAgents.Length; i++) {
			if (totalAgents[i].GetComponent<Ship>().GetTeam() == 1) { // BLUE TEAM = 1 ||| RED TEAM = 2
				totalBlueAgents++;
			}
			else {
				totalRedAgents++;
			}
		}

		Debug.Log ("BLUE Agents: " + totalBlueAgents + ", RED Agents: " + totalRedAgents);
		if (totalBlueAgents == 0 || totalRedAgents == 0) {
			for (int i = 0; i < totalAgents.Length; i++) {
				Individual indi = totalAgents[i].GetComponent<Individual>();
				fc.SetFitness(indi, false);
			}
		}
	}

}
