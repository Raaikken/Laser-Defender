using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject {
	[SerializeField] float damage;
	[SerializeField] Sprite weaponSprite;
	[SerializeField] GameObject projectile;
	[SerializeField] float projectileSpeed;
	[SerializeField] bool isAutomatic;
	[SerializeField] float fireRate;

	public GameObject GetProjectile() {
		return projectile;
	}

	public Sprite GetWeaponSprite() {
		return weaponSprite;
	}
	
	public float GetProjectileSpeed() {
		return projectileSpeed;
	}

	public float GetFireRate() {
		return fireRate;
	}

	public bool GetIsAutomatic() {
		return isAutomatic;
	}
}
