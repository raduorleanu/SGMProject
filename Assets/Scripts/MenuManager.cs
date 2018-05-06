using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utillity;

public class MenuManager : MonoBehaviour
{

	public int index = 0;
	
	public void PlayRandom() {
		GameManager.RandomGeneration = true;
		SceneManager.LoadScene(1);
	}
	
	public void PlayNormal() {
		GameManager.RandomGeneration = false;
		SceneManager.LoadScene(1);
	}

	public void QuitGame() {
		Debug.Log("Application Quit");
		Application.Quit();
	}

	public void Options() {
		GameManager.RandomGeneration = true;
	}
}
