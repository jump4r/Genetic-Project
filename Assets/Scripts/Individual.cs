﻿using UnityEngine;
using System.Collections;

public class Individual : MonoBehaviour {

	private static int geneLength = 4;					
	private int[] genes = new int[geneLength];	// Genes for individual
	private int fitness = 0;						// Calcuate fitness

	// Use this for initialization
	void Start () {
		Initialize ();
		PrintGene ();
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

	public int Size() {
		return genes.Length;
	}

	public int GetFitness() {
		if (fitness == 0) {
			// Get the fitness
		}
		return fitness;
	}
}