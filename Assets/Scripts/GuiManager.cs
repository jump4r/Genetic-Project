using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	private int generation = 0; // Current Generation.
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
		GUI.Box (new Rect(Screen.width * 0.75f, Screen.height * 0.02f, Screen.width * .2f, Screen.height * 0.04f), "Current Generation: " + generation.ToString ());
	}
}
