using UnityEngine;
using System.Collections;

public class FitnessCalculation : MonoBehaviour {

	// Solution, may not be used.
	private int[] solution = new int[4];

	// Fitness for each individual
	public float[] fitnessForIndividual;

	// Genes for each Individual
	public int[][] allGenes;

	// Population
	private Population population;

	// Use this for initialization
	void Start () {
		population = GetComponent<Population> ();

		fitnessForIndividual = new float[population.popSize];
		allGenes = new int[population.popSize][];

		for (int i = 0; i < fitnessForIndividual.Length; i++) {
			fitnessForIndividual[i] = 0;
		}
	}
	// Set a possible solution?
	private void SetSolution(int[] newSolution) {
		solution = newSolution;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Return fitness of the individaul
	// I don't feel like changing this everywhere so i'm not gonna
	public float GetFitness(Individual individual) {
		float fitness = 0;
		// Calculate fitness with a specific algorithm
		fitness = 1 - (individual.numTimesHit * .05f) + (individual.numHits * .01f) + (individual.numKills * .1f);
		fitnessForIndividual [individual.indexInPopulation] = fitness;
		return fitness;
	}

	/// <summary>
	/// Sets the fitness. ROFL THIS IS RECURSIVE. HASHTAG OOPS.
	/// </summary>
	/// <param name="individual">Individual.</param>
	public void SetFitness(Individual individual, bool recurse) {
		float fitness = 0;
		// Calculate fitness with a specific algorithm
		fitness = 1 - (individual.numTimesHit * .05f) + (individual.numHits * .01f) + (individual.numKills * .1f);
		fitnessForIndividual [individual.indexInPopulation] = fitness;

		// PrintGene (individual.indexInPopulation);

		// After Calculating the fitness if an agent that has died, figure out if we can end the game.
		// This might be cheating, but idk if there is a better way to do it.
		if (recurse)
			Invoke ("UpdateTotalRemainingAgents", 0.5f);
		//population.UpdateTotalRemainingAgents ();
	}

	private void UpdateTotalRemainingAgents() {
		population.UpdateTotalRemainingAgents ();
	}

	/// <summary>
	/// Gets the max fitness.
	/// </summary>
	/// <returns>The max fitness.</returns>
	public int GetMaxFitness(float[] pool) {
		int maxFitness = -1;
		for ( int i = 0; i < pool.Length; i++ ) {
			if (pool[i] > maxFitness) {
				maxFitness = i;
			}
		}
		return maxFitness;
	}

	public float[] GetBlueIndividualsFitness() {
		float[] blueIndividuals = new float[population.popSize / 2];
		for (int i = 0; i < blueIndividuals.Length; i++) {
			blueIndividuals[i] = fitnessForIndividual[i*2];
		}
		return blueIndividuals;
	}

	public float[] GetRedIndividualsFitness() {
		float[] redIndividuals = new float[population.popSize / 2];
		for (int i = 0; i < redIndividuals.Length; i++) {
			redIndividuals[i] = fitnessForIndividual[1 + i*2];
		}
		return redIndividuals;
	}

	// Debug
	/* Prints the Individual Gene */
	private void PrintGene(int index) {
		string rtn = "";
		for (int i = 0; i < allGenes[index].Length; i++) {
			rtn += allGenes[index][i].ToString ();
		}
		Debug.Log ("Indivudual Gene Sequence: " + rtn);
	}
}
