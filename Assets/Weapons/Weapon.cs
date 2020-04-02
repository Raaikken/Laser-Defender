using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject {
	[SerializeField] float damage = 1f;
	[SerializeField] Sprite weaponSprite = null;
	[SerializeField] GameObject projectile = null;
	[SerializeField] float projectileSpeed = 10f;
	[SerializeField] bool isAutomatic = false;
	[SerializeField] float fireRate = 0f;

	public float GetDamage() {
		return damage;
	}

	public Sprite GetWeaponSprite() {
		return weaponSprite;
	}

	public GameObject GetProjectile() {
		return projectile;
	}
	
	public float GetProjectileSpeed() {
		return projectileSpeed;
	}

	public bool GetIsAutomatic() {
		return isAutomatic;
	}

	public float GetFireRate() {
		return fireRate;
	}
}
