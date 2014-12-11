using UnityEngine;
using System.Collections;

public class AttackBehaviour : MonoBehaviour {

	private Ship ship;
	private int team; // BLUE TEAM = 1 ||| RED TEAM = 2
	private GameObject bullet;
	
	private float attackSpeed = 0.2f;	// Attack speed determed by Gene[]
	private bool shotLoaded = true;
	private float attackSensitivity = 2.5f;

	private float attackFOV = 20f;

	private float reloadTime = 0.6f;
	private float timeSinceLastAttack = 0f;

	private TargetBehaviour target;
	private EvadeBehaviour evade;

	// Use this for initialization
	void Start () {
		ship = gameObject.GetComponent<Ship> ();
		team = ship.GetTeam ();
		bullet = Resources.Load<GameObject> ("Bullet");

		target = GetComponent<TargetBehaviour> ();
		evade = GetComponent<EvadeBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastAttack += Time.deltaTime;
		if (timeSinceLastAttack > attackSensitivity) {
			if (evade.targetedBy != null && !evade.manModeEnabled) {
				evade.MAN_MODE ();
				timeSinceLastAttack = 0;
			}
			else {
				target.FindRandomTarget();
				timeSinceLastAttack = 0;
			}
		}
	}

	/* SET Variables based off of gene */
	public void SetAttackSpeed(int _attackSpeed) {
		attackSpeed = (float)_attackSpeed / 25f;
	}

	public void SetReloadTime(int _reloadTime) {
		reloadTime = (10 - (float)_reloadTime) / 10f;
	}

	public void SetAttackFOV(int _attackFOV) {
		attackFOV = (float)_attackFOV * 4f;
	}

	public void SetAttackSensitivity(int _attackSensitivity) {
		attackSensitivity = 1.5f + ((float)_attackSensitivity * .2f);
	}

	/// <summary>
	/// Shoot a bullet
	/// </summary>
	public void Shoot() {
		if (ship.GetAmmoCount() > 0 && shotLoaded && CanShoot ()) {
			// Create and Initialize Bullet
			GameObject tmp = (GameObject)GameObject.Instantiate (bullet, transform.position, transform.rotation);
			Bullet b = tmp.GetComponent<Bullet>();
			b.Initialize(team, gameObject);

			timeSinceLastAttack = 0f;

			ship.DecreaseAmmoCount();
			shotLoaded = false;
			Invoke ("LoadShot", attackSpeed);
		}
	}

	/// <summary>
	/// Loads the shot.
	/// </summary>
	void LoadShot() {
		shotLoaded = true;
	}

	/// <summary>
	/// Determines whether this instance can shoot, I.E. whether it's target is in it's feild of vision.
	/// </summary>
	/// <returns><c>true</c> if this instance can shoot; otherwise, <c>false</c>.</returns>
	private bool CanShoot() {
		GameObject targetGameObject = target.GetTarget ();
		if (targetGameObject == null) {
			return false;
		}

		Vector3 targetDir = targetGameObject.transform.position - transform.position;
		Vector3 selfDir = transform.up;
		float angle = Vector3.Angle (targetDir, selfDir);
		// Debug.Log ("Angle between " + name + " and " + targetGameObject.name + " is " + angle);
		Debug.DrawRay (targetDir, selfDir);
		if (angle < attackFOV) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Initializes Reload Function.
	/// </summary>
	/// <param name="initColor">Init color.</param>
	/// <param name="reloadColor">Reload color.</param>
	public void ReloadInit() {
		if (ship.GetAmmoCount() <= 0) {
			Invoke ("Reload", reloadTime);
			gameObject.renderer.material = ship.reloadColor;
		}
	}

	/// <summary>
	/// Reload this agent
	/// </summary>
	private void Reload() {
		ship.SetAmmoCount (ship.GetAmmoCapacity ());
		gameObject.renderer.material = ship.color;
	}
}
