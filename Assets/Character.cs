using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Character")]
	[SerializeField] int maxHealth = 10;
	int currentHealth = 1;
	[SerializeField] float normalMoveSpeed = 5f;
	float currentMoveSpeed = 1f;
	[SerializeField] float alertedSpeedMultiplier = 5f;
	[SerializeField] Weapon weapon = null;

	[Header("Pathing")]
	[SerializeField] GameObject patrolRoute = null;
	List<Transform> waypoints = new List<Transform>();
	int nextWaypoint = 0;

	bool isAlerted = false;
	bool isFiring = false;
	Coroutine firing;

	private void Start() {
		currentHealth = maxHealth;
	}

	private void Update() {
		
	}
	
	// NPC Movement
	void Move(Transform target) {
		currentMoveSpeed = isAlerted ? normalMoveSpeed * alertedSpeedMultiplier : normalMoveSpeed;
		transform.position = Vector2.MoveTowards(transform.position, target.position, currentMoveSpeed * Time.deltaTime);
		if(transform.position == target.position) {
			nextWaypoint++;
		}

		if(nextWaypoint >= waypoints.Count) {
			nextWaypoint = 0;
		}
	}

	// PC Movement
	void Move(float moveX, float moveY) {
		Vector2 movementVector = new Vector2(moveX, moveY);
		transform.position = (Vector2)transform.position + movementVector.normalized * normalMoveSpeed * Time.deltaTime;
	}

	void LookAt(Vector2 target) {
		float AngleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
	}

	void Shoot() {
		RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, (transform.right), 10f, 1 << 8);
		if(hit.collider != null && hit.collider.tag == "Player" && !isFiring) {
			firing = StartCoroutine(InstantiateBullet());
		}
	}

	IEnumerator InstantiateBullet() {
		isFiring = true;
		GameObject projectile = Instantiate(weapon.GetProjectile(), transform.GetChild(0).position, transform.GetChild(0).rotation * Quaternion.Euler(0f, 0f, -90f)) as GameObject;
		projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * weapon.GetProjectileSpeed();
		projectile.GetComponent<Projectile>().SetShooter(gameObject);
		yield return new WaitForSeconds(0.5f);
		isFiring = false;
	}
	
	// Shoot weapon
	// Weapon is only fired once if not automatic
	// Weapon will continuesly shoot as long as Fire1 button is held down
	// and weapon is automatic
	void Shoot() {
		if (Input.GetButtonDown("Fire1") && !weapon.GetIsAutomatic()) {
			InstantiateBullet();
		} 
		else if (Input.GetButtonDown("Fire1")) {
			firingCoroutine = StartCoroutine(ContinuesShooting(weapon.GetFireRate()));
		}

		if (Input.GetButtonUp("Fire1") && firingCoroutine != null) {
			StopCoroutine(firingCoroutine);
		}
	}

	IEnumerator ContinuesShooting(float fireRate) {
		while(true) {
			InstantiateBullet();
			yield return new WaitForSeconds(fireRate);
		}
	}

	void InstantiateBullet() {
		GameObject projectile = Instantiate(weapon.GetProjectile(), transform.GetChild(0).position, transform.GetChild(0).rotation * Quaternion.Euler(0f, 0f, -90f)) as GameObject;
		projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * weapon.GetProjectileSpeed();
		projectile.GetComponent<Projectile>().SetShooter(gameObject);
	}

	public void TakeDamage(int damamge) {
		currentHealth -= damamge;
		if(currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
}
