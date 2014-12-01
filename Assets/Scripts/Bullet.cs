using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public static int GENERATION_SPEED;

	private float team = -1;
	private float bulletSpeed = 15f;
	private float timeAlive = 4f;
	private int damage = 10;
	
	// Use this for initialization
	void Start () {
		
	}

	// Initalize from Instantiation
	public void Initialize(int _team) {
		team = _team;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * bulletSpeed);
		if (timeAlive < 0f) {
			Destroy (gameObject);		
		}
		timeAlive -= Time.deltaTime;
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Agent") {
			if (col.gameObject.GetComponent<ShooterAI>().team != team) {
				Destroy (gameObject);
				col.gameObject.GetComponent<ShooterAI>().InflictDamage(damage);
			}
		}
	}
}
