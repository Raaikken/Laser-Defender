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
		currentMoveSpeed = normalMoveSpeed;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetWeaponSprite();
		if(patrolRoute != null) {
			for(int i = 0; i < patrolRoute.transform.childCount; i++) {
				waypoints.Add(patrolRoute.transform.GetChild(i));
			}
		}
	}

	private void Update() {
		if(waypoints.Count != 0) {
			Move(waypoints[nextWaypoint]);
			LookAt(waypoints[nextWaypoint].position);
		}
		else {
			Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
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

	public void TakeDamage(int damamge) {
		currentHealth -= damamge;
		if(currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
}
