using UnityEngine;
using System.Collections;

public class Population : MonoBehaviour {

	public GameObject[] redIndividuals;
	public GameObject[] blueIndividuals;

	// Public Varialbles for Inidividual Init.
	public int popSize = 2;
	public bool init;
	public int totalAgentsRemaining;
	public int generation = 0;

	// Game Object prefabs
	public GameObject[] prefabs;

	// Initialize Vector start positions
	private Vector3[] spawnLocations;

	// For fitness calculaiton
	private FitnessCalculation fc;
	private CrossoverAlgorithm crossover;

	// Game Time, games should not take more than one minue
	public float lifetime = 10f;
	private float timer = 10f;

	// All of the genes for all of the players.
	private int[][] allGenes;

	// Use this for initialization
	void Start () {
		fc = GetComponent<FitnessCalculation> ();
		crossover = GetComponent<CrossoverAlgorithm> ();

		spawnLocations = new Vector3[popSize];
		for (int i = 0; i < spawnLocations.Length; i++) {
			spawnLocations[i].x = Random.Range (-20f, 20f);
			spawnLocations[i].y = Random.Range (-20f, 20f);
			spawnLocations[i].z = 0;
		}
		// Debug.Log ("Population Script: Loaded");
		Initialize (popSize, true);
	}

	// Initialize Constructor
	private void Initialize(int populationSize, bool init) {
		redIndividuals = new GameObject[populationSize / 2];
		blueIndividuals = new GameObject[populationSize / 2];

		// Temporary Index Variables for the loop
		int redCurrentIndex = 0;
		int blueCurrentIndex = 0;

		// Remake the spawns
		spawnLocations = new Vector3[popSize];
		for (int i = 0; i < spawnLocations.Length; i++) {
			spawnLocations[i].x = Random.Range (-10f, 10f);
			spawnLocations[i].y = Random.Range (-10f, 10f);
			spawnLocations[i].z = 0;
		}

		// Loop and create individuals
		for (int i = 0; i < populationSize; i++) {
			// Initialize BLUE Individuals
			if ( i % 2 == 0 ) {
				GameObject newIndividual = (GameObject)prefabs[i % 2];
				GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
				rtn.GetComponent<Individual>().indexInPopulation = i;
				blueIndividuals[blueCurrentIndex] = rtn;
				if (!init) { // Set the genes to the preset ones we have
					rtn.GetComponent<Ship>().Initialize();
					rtn.GetComponent<Individual>().SetGene (allGenes[i]);
				}
				blueCurrentIndex++;
			}

			// Initialize RED Individuals
			if ( i % 2 == 1) {
				GameObject newIndividual = (GameObject)prefabs[i % 2];
				GameObject rtn = (GameObject)GameObject.Instantiate(newIndividual, spawnLocations[i], Quaternion.identity);
				rtn.GetComponent<Individual>().indexInPopulation = i;
				redIndividuals[redCurrentIndex] = rtn;
				if (!init) { // Set the genes to the preset ones we have
					rtn.GetComponent<Ship>().Initialize();
					rtn.GetComponent<Individual>().SetGene (allGenes[i]);
				}
				redCurrentIndex++;
			}
		}

		// Debug Log
		// Debug.Log ("RED Individuals Created: " + redIndividuals.Length);
		// Debug.Log ("BLUE Individuals Created: " + blueIndividuals.Length);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		// Reset Generation after < lifetime > seconds.
		if (timer < 0) {
			ResetGeneration();
		}
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

		// Debug.Log ("BLUE Agents: " + totalBlueAgents + ", RED Agents: " + totalRedAgents);
		if (totalBlueAgents == 0 || totalRedAgents == 0) {
			for (int i = 0; i < totalAgents.Length; i++) {
				Individual indi = totalAgents[i].GetComponent<Individual>();
				fc.SetFitness(indi, false);
			}
		}
	}

	/// <summary>
	/// Ends the generation. Creates a new one. Updates GUI.
	/// </summary>
	public void ResetGeneration() {
		Debug.Log ("Resetting Generation");
		KillAgents ();
		float[] redAgentsFitness = fc.GetRedIndividualsFitness () ;
		float[] blueAgentsFitness = fc.GetBlueIndividualsFitness ();

		int maxRedFitnessIndex = fc.GetMaxFitness (redAgentsFitness);
		Debug.Log ("Local Max Fitness (RED): " + fc.fitnessForIndividual[maxRedFitnessIndex * 2 + 1]);
		int maxBlueFitnessIndex = fc.GetMaxFitness (blueAgentsFitness);
		Debug.Log ("Local Max Fitness (BLUE): " + fc.fitnessForIndividual[maxBlueFitnessIndex * 2]);

		// Update GUI Elements.
		GuiManager gm = GetComponent<GuiManager> ();
		gm.UpdateMaxFitness(CalculateHighestFitness (redAgentsFitness[maxRedFitnessIndex], blueAgentsFitness[maxBlueFitnessIndex]));
		gm.UpdateGeneration ();

		// Perform crossover of the genes with crossover algorithm.
		int[][] endGenes = fc.allGenes;

		//Debug.Log ("RED Best Genome: " + fc.PrintGenome (endGenes[maxRedFitnessIndex * 2 + 1]));
		for (int i = 1; i < popSize; i += 2) {
			//Debug.Log ("Old Genome for index " + i + ": " + fc.PrintGenome (endGenes[i]));
			endGenes[i] = crossover.Crossover (endGenes[maxRedFitnessIndex * 2 + 1], endGenes[i]);
			//Debug.Log ("New Genome for index " + i + ": " + fc.PrintGenome (endGenes[i]));
		}

		for (int i = 0; i < popSize; i += 2) {
			endGenes[i] = crossover.Crossover (endGenes[maxBlueFitnessIndex * 2], endGenes[i]);
		}

		allGenes = endGenes;
		Initialize (popSize, false);
		ResetTimer ();
	}

	/// <summary>
	/// Kills the agents if time period has expired..
	/// </summary>
	private void KillAgents() {
		GameObject[] agents = GameObject.FindGameObjectsWithTag ("Agent");
		CalculateWinner (agents);
		for (int i = 0; i < agents.Length; i++) {
			fc.SetFitness (agents[i].GetComponent<Individual>(), false);
			//Debug.Log ("Fitness On Agent Index: " + agents[i].GetComponent<Individual>().indexInPopulation + " Set to " + fc.fitnessForIndividual[agents[i].GetComponent<Individual>().indexInPopulation]);
			Destroy (agents[i]);
		}
	}

	private void CalculateWinner(GameObject[] agents) {
		int redRemaining = 0;
		int blueRemaining = 0;

		for (int i = 0; i < agents.Length; i++) {
			if (agents[i].GetComponent<Ship>().GetTeam() == 1) {
				blueRemaining++;
			}

			else {
				redRemaining++;
			}
		}
		if (redRemaining > blueRemaining)
			GetComponent<GuiManager>().UpdateWinner("red");
		else 
			GetComponent<GuiManager>().UpdateWinner("blue");

	}

	private float CalculateHighestFitness(float redMax, float blueMax) {
		float currentMax = GetComponent<GuiManager> ().maxFitness;
		float localMax = Mathf.Max (redMax, blueMax);
		return Mathf.Max (currentMax, localMax);
	}

	/// <summary>
	/// Resets the timer.
	/// </summary>
	public void ResetTimer() {
		timer = lifetime;
	}

}
