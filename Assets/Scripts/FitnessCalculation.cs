using UnityEngine;
using System.Collections;

public class FitnessCalculation : MonoBehaviour {

	// Solution, may not be used.
	private int[] solution = new int[4];

	// Fitness for each individual
	public float[] fitnessForIndividual;

	// Population
	private Population population;

	// Use this for initialization
	void Start () {
		population = GetComponent<Population> ();
		fitnessForIndividual = new float[population.popSize];
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
	public int GetMaxFitness() {
		int maxFitness = -1;
		foreach ( int f in fitnessForIndividual ) {
			if (f > maxFitness) {
				maxFitness = f;
			}
		}
		return maxFitness;
	}
}
