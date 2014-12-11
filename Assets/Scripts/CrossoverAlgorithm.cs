using UnityEngine;
using System.Collections;

public class CrossoverAlgorithm : MonoBehaviour {

	private static float mutationRate = 0.05f;
	private static int mutationOffset = 2;

	private FitnessCalculation fc;

	// Use this for initialization
	void Start () {
		fc = GetComponent<FitnessCalculation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Crossover the specified individual1 and individual2.
	/// </summary>
	/// <param name="fittest">Fittest.</param>
	/// <param name="individual">Individual.</param>
	public int[] Crossover(int[] individual1, int[] individual2) {

		int crossPoint = Random.Range (0, individual1.Length);
		int[] rtn = new int[individual1.Length];
	
		for (int i = 0; i < rtn.Length; i++) {

			if (i <= crossPoint) {
				rtn[i] = individual1[i];
			}
			else {
				rtn[i] = individual2[i];
			}

			/* Another Crossover Algorithm */
			// rtn[i] = individual1[i] / 2 + individual2[i] / 2;
			Mutate (rtn);
		}
		return rtn;
	}

	private void Mutate(int[] individual) {
		// Account for mutations.
		if (Random.value <= mutationRate) {
			individual[Random.Range (0, individual.Length)] += mutationOffset;
			individual[Random.Range (0, individual.Length)] -= mutationOffset;
		}
	}
}
