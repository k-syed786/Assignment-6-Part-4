using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
	public float speed;
	public float rightEdge;
	public float leftEdge;
	public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void Update()
	{
		if(gm.gameOver==true){
			return;
		}
		float horizontal = Input.GetAxis ("Horizontal");

		transform.Translate (Vector2.right * horizontal * Time.deltaTime * speed);

		if(transform.position.x < leftEdge){
			transform.position = new Vector2 (leftEdge, transform.position.y);
		}

		if(transform.position.x > rightEdge){
			transform.position = new Vector2 (rightEdge, transform.position.y);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag ("extraLife")){
			gm.UpdateLives (1);
			Destroy(other.gameObject);
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerUp);
		}
			else if(other.CompareTag("score50")){
				gm.UpdateScore (50);
				Destroy(other.gameObject);
				SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerUp);
			}
		else if(other.CompareTag("score250")){
			gm.UpdateScore (250);
			Destroy(other.gameObject);
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerUp);
		}
		else if(other.CompareTag("negativeLife")){
			gm.UpdateLives (-1);
			Destroy(other.gameObject);
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerDown);
		}
		else if(other.CompareTag("scoreDown")){
			gm.UpdateScore (-100);
			Destroy(other.gameObject);
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.powerDown);
		}
		}

}
