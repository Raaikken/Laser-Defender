using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// Variables
	[SerializeField] float normalMoveSpeed = 5f;
	[SerializeField] float alertedSpeedMultiplier = 2f;
	[SerializeField] GameObject patrolRoute = null;
	[SerializeField] Weapon weapon = null;

	// Internal Variables
	List<Transform> waypoints = new List<Transform>();
	int nextWaypoint = 0;
	bool isAlerted = false;
	float currentMoveSpeed = 0f;
	Coroutine firing;
	bool isFiring = false;


	// Debug


	// Start is called before the first frame update
	void Start() {
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetWeaponSprite();
		currentMoveSpeed = normalMoveSpeed;
		for(int i = 0; i < patrolRoute.transform.childCount; i++) {
			waypoints.Add(patrolRoute.transform.GetChild(i));
		}
	}

	// Update is called once per frame
	void Update() {
		Move(waypoints[nextWaypoint]);
		LookAt(waypoints[nextWaypoint].position);
		Shoot();
	}

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
}
