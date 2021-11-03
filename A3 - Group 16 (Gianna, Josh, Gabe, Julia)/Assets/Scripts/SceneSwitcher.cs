//Script that manages switching to scenes. 
//Contains functions which are primarily to be called by buttons.

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	
	//Load Menu Scene
	public void ToMenu() {
		SceneManager.LoadScene("Main Menu");
	}
	
	//Load Scores Scene
	public void ToScores() {
		SceneManager.LoadScene("Scores");
	}
	
	//Load Level1 Scene
	public void ToLevel1() {
		SceneManager.LoadScene("Level1");
	}
	
	//Load Level2 Scene
	public void ToLevel2() {
		SceneManager.LoadScene("Level2");
	}
	
	//Load Level3 Scene
	public void ToLevel3() {
		SceneManager.LoadScene("Level3");
	}
	
}
