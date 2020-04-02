using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Get current scene and load the next one
	// Or main menu if at the end scene
	public void LoadNextScene() {
		if ((SceneManager.GetActiveScene().buildIndex + 1) <= (int)SceneID.EndScene) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else {
			SceneManager.LoadScene((int)SceneID.MainMenu);
			Destroy(GameObject.FindObjectOfType<GameMaster>());
		}
	}

	public void LoadFirstLevel() {
		Destroy(GameObject.FindObjectOfType<GameMaster>());
		SceneManager.LoadScene((int)SceneID.Level1);
	}

	public void LoadEndScene() {
		SceneManager.LoadScene((int)SceneID.EndScene);
	}

	public void ExitToDesktop(){
		Application.Quit();
	}
}
