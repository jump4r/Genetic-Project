using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public int generation = 0; // Current Generation.
	public int redWins = 0;
	public int blueWins = 0;
	public float maxFitness = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGeneration(int g) {
		generation = g;
	}

	void OnGUI() {
		GUI.Box (new Rect(Screen.width * 0.75f, Screen.height * 0.02f, Screen.width * .2f, Screen.height * 0.045f), "Current Generation: " + generation.ToString ());
		GUI.Box (new Rect(Screen.width * 0.02f, Screen.height * 0.02f, Screen.width * .2f, Screen.height * 0.045f), "Red Wins: " + redWins.ToString ());
		GUI.Box (new Rect(Screen.width * 0.02f, Screen.height * 0.08f, Screen.width * .2f, Screen.height * 0.045f), "Blue Wins: " + blueWins.ToString ());
		GUI.Box (new Rect(Screen.width * 0.02f, Screen.height * 0.14f, Screen.width * .2f, Screen.height * 0.05f), "Highest Fitness " + maxFitness.ToString ());
	}

	/// <summary>
	/// Updates the winner.
	/// </summary>
	/// <param name="winner">Winner.</param>
	public void UpdateWinner(string winner) {
		if (winner == "red")
			redWins++;
		else
			blueWins++;
	}

	/// <summary>
	/// Updates the max fitness.
	/// </summary>
	/// <param name="newMax">New max.</param>
	public void UpdateMaxFitness(float newMax) {
		maxFitness = newMax;
	}

	/// <summary>
	/// Updates the generation.
	/// </summary>
	public void UpdateGeneration() {
		generation++;
	}
}
