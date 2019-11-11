using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public int lives;
	public int score;
	public Text livesText;
	public Text scoreText;
	public Text highScoreText;
	public bool gameOver;
	public bool gameComplete;
	public GameObject panel;
	public GameObject panel2;
	public GameObject loadPanel;
	public int bricksNumber;
	public Transform[] levels;
	public int currentLevel = 0;
    public BallScript bs;
    // Start is called before the first frame update
    void Start()
    {
		livesText.text = "" + lives;
		scoreText.text = "Score: " + score;
		bricksNumber = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UpdateLives(int livesChange){
		lives += livesChange;
		livesText.text= "" + lives;

		if(lives<=0){
			lives=0;
			GameOver();
		}
	}

	public void UpdateScore(int points){
		score += points;
		scoreText.text = "Score: " + score;

	}
	public void UpdateNumberOfBricks(){
		bricksNumber--;
		if(bricksNumber<=0){

			if(currentLevel >= levels.Length + 1){
				GameComplete();
			}
			else{
				loadPanel.SetActive (true);
				loadPanel.GetComponentInChildren<Text>().text = "Loading Level " + (currentLevel + 2);
				gameOver = true;
				Invoke("LoadLevel", 2f);
			}
		}
	}
	void LoadLevel(){
		currentLevel++;
		Instantiate(levels[currentLevel],Vector2.zero,Quaternion.identity);
		bricksNumber = GameObject.FindGameObjectsWithTag("Brick").Length;
		gameOver = false;
		loadPanel.SetActive (false);
        bs.speed = 200;
	}


	public void PlayAgain(){
		SceneManager.LoadScene("Level01");
	}

	void GameComplete(){
		gameComplete = true;
		panel2.SetActive(true);
		int highScore = PlayerPrefs.GetInt ("High Score");
		if(score > highScore) {
			PlayerPrefs.SetInt("High Score", score);

			highScoreText.text = "New High Score!: " + score;
		}
	}
	void GameOver(){
	SoundManager.Instance.PlayOneShot (SoundManager.Instance.gameOver);
		gameOver = true;
        SceneManager.LoadScene("GameOver");
		int highScore = PlayerPrefs.GetInt ("High Score");
		if(score > highScore) {
			PlayerPrefs.SetInt("High Score", score);

			highScoreText.text = "New High Score!: " + score;
		}else{
			highScoreText.text = "High Score: " + highScore;
		}
	}

	public void Quit(){
		SceneManager.LoadScene("Menu");
	}

}
