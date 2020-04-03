using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// Variables
	[SerializeField] Weapon weapon = null;
	Coroutine firing;
	bool isFiring = false;
	
	// Update is called once per frame
	void Update() {
		Shoot();
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
