using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Variables
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] Weapon weapon;

	// Internal Variables
	Coroutine firingCoroutine;

	// Debug


	// Start is called before the first frame update
	void Start() {
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetWeaponSprite();
	}

	// Update is called once per frame
	void Update() {
		Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		Shoot();
	}

	void Move(float moveX, float moveY) {
		Vector2 movementVector = new Vector2(moveX, moveY);
		transform.position = (Vector2)transform.position + movementVector.normalized * moveSpeed * Time.deltaTime;
	}

	void LookAt(Vector2 target) {
		float AngleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
	}

	// Shoot weapon
	// Weapon is only fired once if not automatic
	// Weapon will continuesly shoot as long as Fire1 button is held down
	// and weapon is automatic
	void Shoot() {
		if (Input.GetButtonDown("Fire1") && !weapon.GetIsAutomatic()) {
			GameObject projectile = Instantiate(weapon.GetProjectile(), transform.GetChild(0).position, transform.GetChild(0).rotation * Quaternion.Euler(0f, 0f, -90f)) as GameObject;
			projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * weapon.GetProjectileSpeed();
		} 
		else if (Input.GetButtonDown("Fire1")) {
			firingCoroutine = StartCoroutine(ContinuesShooting(weapon.GetFireRate()));
		}

		if (Input.GetButtonUp("Fire1")) {
			StopCoroutine(firingCoroutine);
		}
	}

	IEnumerator ContinuesShooting(float fireRate) {
		while(true) {
			GameObject projectile = Instantiate(weapon.GetProjectile(), transform.GetChild(0).position, transform.GetChild(0).rotation * Quaternion.Euler(0f, 0f, -90f)) as GameObject;
			projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * weapon.GetProjectileSpeed();
			yield return new WaitForSeconds(fireRate);
		}
	}
}
