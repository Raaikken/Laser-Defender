using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Variables
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] Weapon weapon = null;

	// Internal Variables
	Coroutine firingCoroutine;
	
	// Start is called before the first frame update
	void Start() {
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetWeaponSprite();
	}

	// Update is called once per frame
	void Update() {
		Shoot();
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
}
