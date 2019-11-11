using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
	public int points;
	public int ability;
	public Sprite damage;

	public void BreakBrick(){
		ability--;
		GetComponent<SpriteRenderer> ().sprite = damage;
	}

}
