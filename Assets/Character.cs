using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
	[Header("Character")]
	[SerializeField] float maxHealth = 10;
	float currentHealth = 1;
	[SerializeField] float normalMoveSpeed = 5f;
	float currentMoveSpeed = 1f;
	[SerializeField] float alertedSpeedMultiplier = 5f;

	[Header("Pathing")]
	[SerializeField] GameObject patrolRoute = null;
	List<Transform> waypoints = new List<Transform>();
	int nextWaypoint = 0;

	[Header("Weapon")]
	[SerializeField] Weapon weapon = null;
	[SerializeField] int maxAmmoPistol = 5;
	[SerializeField] int maxAmmoSilenced = 5;
	[SerializeField] int maxAmmoSMG = 5;
	int currentAmmoPistol = 1;
	int currentAmmoSilenced = 1;
	int currentAmmoSMG = 1;

	[Header("UI Elements")]
	[SerializeField] Slider healthBar;

	bool isAlerted = false;
	bool isFiring = false;
	Coroutine firing;

	private void Start() {
		if(healthBar != null) {
			healthBar.value = healtPercentage();
		}
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
		if(healthBar != null) {
			healthBar.value = healtPercentage();
		}
		if(transform.tag != "Player") {
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

	float healtPercentage() {
		return currentHealth / maxHealth;
	}
}
