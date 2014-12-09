using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject bullet;
	
	// Materila List
	public Material color;
	public Material reloadColor;

	// Player and Health Management
	private int health = 100;
	public int team; // BLUE TEAM = 1 ||| RED TEAM = 2

	// Ammo Management
	private int ammoCount = 20;
	private int ammoCapacity = 20;
	private float shootCooldown = -1;
	private float reloadTime = 0.6f;

	// Movement and Rotation
	private float moveSpeed = 6f;

	// Behaviors and Individual
	AttackBehaviour attack;
	TargetBehaviour target;
	MovementBehaviour movement;

	Individual individual;

	// Use this for initialization
	void Start () {
		attack = GetComponent<AttackBehaviour> ();
		target = GetComponent<TargetBehaviour> ();
		movement = GetComponent<MovementBehaviour> ();

		individual = GetComponent<Individual> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate 
		movement.Rotate ();

		// Move
		movement.Move ();

		// Shoot
		attack.Shoot ();

		// Reload
		attack.ReloadInit ();

		shootCooldown -= Time.deltaTime;
	}

	// Spend <reloadTime> seconds to reload back to the max ammo cap;
	void Reload() {
		ammoCount = 20;
		gameObject.renderer.material = color;
	}

	// Inflict <damage> to Agent's health
	public void InflictDamage(int damage) {
		IndividualHitByEnemy ();
		health -= damage;
		if (health < 0) {
			Die ();
		}

	}

	// Kill Agent, Will probalby have it respawn mayyybe? If you get the time brah do what you gotta do. 
	void Die() {
		// Update FitnessCalculator with fitness of this indidvidual
		FitnessCalculation fc = GameObject.Find ("_SCRIPTS").GetComponent<FitnessCalculation> ();
		fc.SetFitness (individual, true);

		Destroy (gameObject);
	}

	/* Get and Set Functions */
	public int GetAmmoCount() {
		return ammoCount;
	}

	public void DecreaseAmmoCount() {
		ammoCount--;
	}

	public void SetAmmoCount(int a) {
		ammoCount = a;
	}

	public int GetAmmoCapacity () {
		return ammoCapacity;
	}

	public int GetTeam() {
		return team;
	}

	// Individual Fitness Functions
	public void IndividualHitEnemy() {
		individual.numHits++;
	}

	public void IndividualHitByEnemy() {
		individual.numTimesHit++;
	}
}
