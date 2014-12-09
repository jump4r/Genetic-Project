using UnityEngine;
using System.Collections;

public class Individual : MonoBehaviour {

	private static int geneLength = 4;					
	private int[] genes = new int[geneLength];	// Genes for individual

	// Fitness Variables for Calculation
	public float fitness = 0;						// Calcuate fitness
	public int numTimesHit = 0;
	public int numHits = 0;
	public int numKills = 0;

	public int indexInPopulation;

	private FitnessCalculation fitnessCalculator;

	// Use this for initialization
	void Start () {
		Initialize ();
		// PrintGene ();
		fitnessCalculator = GameObject.Find ("_SCRIPTS").GetComponent<FitnessCalculation> ();
	}

	// Public Initialze Indivdual. Declare Genes. 
	public void Initialize() {
		for (int i = 0; i < geneLength; i++) {
			int gene = Random.Range (0, 2);
			genes[i] = gene;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/* Prints the Individual Gene */
	private void PrintGene() {
		string rtn = "";
		for (int i = 0; i < genes.Length; i++) {
			rtn += genes[i].ToString ();
		}
		Debug.Log ("Indivudual Gene Sequence: " + rtn);
	}

	/* Getters and setters */
	public int GetGene(int index) {
		return genes[index];
	}

	public void SetGene(int index, int value) {
		genes [index] = value;
		fitness = 0;
	}

	// Return Size of the Gene
	public int Size() {
		return genes.Length;
	}

	// Return Current Fitness
	public float GetFitness() {
		if (fitness == 0) {
			// Get the fitness
			fitness = fitnessCalculator.GetFitness(this);
		}
		return fitness;
	}
}
