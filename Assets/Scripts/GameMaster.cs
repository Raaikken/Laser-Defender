using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
	SceneLoader sceneLoader = null;

	// On first initialization
	void Awake() {
		int gameMasterCount = FindObjectsOfType<GameMaster>().Length;
		if(gameMasterCount > 1) {
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

	private void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	// Used for initialization if the object wasn't destroyed on load
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if(scene.buildIndex == (int)SceneID.EndScene) {
			transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	void OnSceneUnloaded(Scene current) {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	// Update is called once per frame
	void Update() {

	}
}
