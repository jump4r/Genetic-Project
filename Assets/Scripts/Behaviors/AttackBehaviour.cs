using UnityEngine;
using System.Collections;

public class AttackBehaviour : MonoBehaviour {

	private Ship ship;
	private int team;
	private GameObject bullet;
	
	private float shotCooldown = 0.2f;
	private bool shotLoaded = true;

	// Use this for initialization
	void Start () {
		ship = gameObject.GetComponent<Ship> ();
		team = ship.GetTeam ();
		bullet = Resources.Load<GameObject> ("Bullet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Shoot forwards once every < shotCooldown > seconds.
	public void Shoot() {
		if (ship.GetAmmoCount() > 0 && shotLoaded) {
			// Create and Initialize Bullet
			GameObject tmp = (GameObject)GameObject.Instantiate (bullet, transform.position, transform.rotation);
			Bullet b = tmp.GetComponent<Bullet>();
			b.Initialize(team);
			
			ship.DecreaseAmmoCount();
			shotLoaded = false;
			Invoke ("LoadShot", shotCooldown);
		}
	}

	void LoadShot() {
		shotLoaded = true;
	}
}
