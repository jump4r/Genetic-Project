using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public static int GENERATION_SPEED;

	private GameObject owner;
	private int team = -1;
	private float bulletSpeed = 30f;
	private float timeAlive = 3f;
	private int damage = 10;
	
	// Use this for initialization
	void Start () {
		
	}

	// Initalize from Instantiation
	public void Initialize(int _team, GameObject _owner) {
		team = _team;
		owner = _owner;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * bulletSpeed);
		if (timeAlive < 0f) {
			Destroy (gameObject);		
		}
		timeAlive -= Time.deltaTime;
	}

	/// <summary>
	/// Gets the team.
	/// </summary>
	/// <returns>The team.</returns>
	public int GetTeam() {
		return team;
	}

	/// <summary>
	/// Destroys bullet, Deals damange, updates the owner of the bullet that it hit an enemy
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Agent" && !col.isTrigger) {
			if (col.gameObject.GetComponent<Ship>().team != team) {
				Destroy (gameObject);
				if (owner != null)
					owner.GetComponent<Ship>().IndividualHitEnemy ();
				col.gameObject.GetComponent<Ship>().InflictDamage(damage);
			}
		}
	}
}
