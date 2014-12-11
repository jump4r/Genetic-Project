using UnityEngine;
using System.Collections;

public class Individual : MonoBehaviour {

	private static int geneLength = 8;					
	public int[] genes = new int[geneLength];	// Genes for individual

	// Fitness Variables for Calculation
	public float fitness = 0;						// Calcuate fitness
	public int numTimesHit = 0;
	public int numHits = 0;
	public int numKills = 0;

	public int indexInPopulation;

	 FitnessCalculation fitnessCalculator;
	Ship ship;

	// Use this for initialization
	void Start () {
		Initialize ();
		SetGene ();
		SetBehaviours ();
		// PrintGene ();
	}

	// Public Initialze Indivdual. Declare Genes. 
	public void Initialize() {
		fitnessCalculator = GameObject.Find ("_SCRIPTS").GetComponent<FitnessCalculation> ();
		ship = GetComponent<Ship> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sets the gene.
	/// Gene-Behaviour Relationships are as follows, in index - behaviour order
	/// 0 - Attack Speed
	/// 1 - Attack FOV
	/// 2 - Reload Speed
	/// 3 - Move Speed
	/// 4 - Rotation Speed
	/// 5 - Evade Detection Range. (Will gradually decrease over time as the Agent doesn't shoot)
	/// 6 - Likelyhood to Assist / Retarget.
	/// 7 - Dummy 
	/// </summary>
	private void SetGene() {
		for (int i = 0; i < genes.Length; i++) {
			genes[i] = 1;
		}

		// Give the Gene random values.
		int remainingPoints = 23;
		for (int i = 0; i < remainingPoints; i++) {
			int addToGene = Random.Range (0, 8);
			if (genes[addToGene] < 9) {
				genes[addToGene] += 1;
			}
		}

		fitnessCalculator.allGenes [indexInPopulation] = genes;
	}

	// Sets gene with preseet gene
	public void SetGene(int[] _genes) {
		Initialize ();
		genes = _genes;
		// PrintGene ();
		SetBehaviours();
	}

	/// <summary>
	/// Prints the gene.
	/// </summary>
	private void PrintGene() {
		string rtn = "";
		for (int i = 0; i < genes.Length; i++) {
			rtn += genes[i].ToString ();
		}
		Debug.Log ("Indivudual Gene Sequence, Generated from Crossover: " + rtn);
	}

	/// <summary>
	/// Sets the behavoiurs.
	/// </summary>
	private void SetBehaviours() {
		ship.attack.SetAttackSpeed (genes [0]);
		ship.attack.SetAttackFOV (genes [1]);
		ship.attack.SetReloadTime (genes [2]);
		ship.movement.SetMoveSpeed (genes [3]);
		ship.movement.SetRotationSpeed (genes [4]);
		ship.evade.SetEvadeSensitivity (genes [5]);
		ship.attack.SetAttackSensitivity (genes [6]);
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
