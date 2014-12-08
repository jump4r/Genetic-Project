using UnityEngine;
using System.Collections;

public class FitnessCalculation : MonoBehaviour {

	private int[] solution = new int[4];
	// Use this for initialization
	void Start () {
		
	}
	// Set a possible solution?
	private void SetSolution(int[] newSolution) {
		solution = newSolution;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Return fitness of the individaul
	public int GetFitness(Individual individual) {
		int fitness = 0;
		// Calculate fitness with a specific algorithm

		return fitness;
	}

	// Return the Maximum Fitness based off of algorithm
	public int GetMaxFitness() {
		return 1;
	}
}
