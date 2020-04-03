using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField] Weapon weapon;
	GameObject shooter;

	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void FixedUpdate() {
		if(!GetComponent<SpriteRenderer>().isVisible) {
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject != shooter) {
			other.GetComponent<Character>().TakeDamage(weapon.GetDamage());
			Destroy(gameObject);
		}
	}

	public void SetShooter(GameObject value) {
		shooter = value;
	}
}
