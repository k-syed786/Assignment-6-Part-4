using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	public Rigidbody2D rigidBody;
	public bool play;
	public Transform paddle;
	public Transform ball;
	public float speed;
	public Transform destroy;
	public GameManager gm;
	public Transform powerUp;
	public Transform powerDown;
	public Transform scoreUp50;
	public Transform scoreUp250;
	public Transform scoreDown;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
		rigidBody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
		if(gm.gameOver){
			transform.position = ball.position;
			play=false;
		}
		if(!play){
			transform.position = paddle.position;
		}

		if(Input.GetButtonDown("Fire1") && !play){
			play = true;

			rigidBody.AddForce(Vector2.up * speed);
		}
    }

	void OnTriggerEnter2D (Collider2D other){
		if(other.CompareTag("Bottom")){
			Debug.Log("Ball Hit");
			rigidBody.velocity = Vector2.zero;
			play = false;
			gm.UpdateLives(-1);

		}
		SoundManager.Instance.PlayOneShot (SoundManager.Instance.die);

    }

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.name == "Paddle"){
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.paddleHit);
		}
		if((other.gameObject.name == "leftPanel") || (other.gameObject.name == "rightPanel")){
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.wallHit);
		}
		if(other.transform.CompareTag("Brick")){
			BrickScript brickScript = other.gameObject.GetComponent<BrickScript >();
			if(brickScript.ability > 1 ) {
				brickScript.BreakBrick();
		}
			else{
			int random = Random.Range(1,501);
			if(random < 50 && random > 450) {
				Instantiate (powerUp,other.transform.position,other.transform.rotation);
			}
				else if(random > 100 && random < 250) {
					Instantiate (scoreUp50,other.transform.position,other.transform.rotation);
				}
				else if(random == 250) {
					Instantiate (scoreUp250,other.transform.position,other.transform.rotation);
				}
            else if(random > 50 && random <= 100 && random >= 400 && random <= 450 )
                {
                    Instantiate(powerDown, other.transform.position, other.transform.rotation);
                }
            else if(random > 250 && random < 350)
                {
                    Instantiate(scoreDown, other.transform.position, other.transform.rotation);
                }
				
			Transform newDestroy = Instantiate(destroy, other.transform.position, other.transform.rotation);
			Destroy(newDestroy.gameObject,2);
				gm.UpdateScore (brickScript.points);
			gm.UpdateNumberOfBricks ();
				Destroy(other.gameObject);
			}
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.brickHit);
	}
}
}