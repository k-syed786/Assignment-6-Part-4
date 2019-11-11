using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void StartMenu(){
		SceneManager.LoadScene("Menu");
	}
	public void Lvl1(){
		SceneManager.LoadScene("Level01");
	}
	public void Quit(){
		Application.Quit();
	}
}
