using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject bullet;
	
	// Materila List
	public Material color;
	public Material reload;

	// Player and Health Management
	private int health = 100;
	public int team;

	// Ammo Management
	private int ammoCount = 20;
	private float shootCooldown = -1;
	private float reloadTime = 0.6f;

	// Movement and Rotation
	private float moveSpeed = 6f;

	// Behaviors
	AttackBehaviour attack;

	// Use this for initialization
	void Start () {
		attack = GetComponent<AttackBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate 
		transform.Rotate (Vector3.forward);

		// Move
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);

		// Fire
		if (shootCooldown < 0) {
			Shoot ();
		}

		// Reload
		if (ammoCount <= 0) {
			Invoke ("Reload", reloadTime);
			gameObject.renderer.material = reload;
		}

		shootCooldown -= Time.deltaTime;
	}


	// Shoot forwards
	void Shoot() {

		/* if (ammoCount > 0) {
			// Create and Initialize Bullet
			GameObject tmp = (GameObject)GameObject.Instantiate (bullet, transform.position, transform.rotation);
			Bullet b = tmp.GetComponent<Bullet>();
			b.Initialize(team);

			ammoCount--;
			shootCooldown = 0.2f;
		} */

		attack.Shoot ();
	}


	// Spend <reloadTime> seconds to reload back to the max ammo cap;
	void Reload() {
		ammoCount = 20;
		gameObject.renderer.material = color;
	}

	// Inflict <damage> to Agent's health
	public void InflictDamage(int damage) {
		health -= damage;
		if (health < 0) {
			Die ();
		}
	}

	// Kill Agent, Will probalby have it respawn mayyybe? If you get the time brah do what you gotta do. 
	void Die() {
		Destroy (gameObject);
	}

	/* Get and Set Functions */
	public int GetAmmoCount() {
		return ammoCount;
	}

	public void DecreaseAmmoCount() {
		ammoCount--;
	}

	public int GetTeam() {
		return team;
	}
}
