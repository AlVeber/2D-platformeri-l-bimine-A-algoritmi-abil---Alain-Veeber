using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	/*[SerializeField]
	private float PlayerSpeed=1;
*/
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		movePlayer ();

	}
	void moveLeft(){
		transform.Translate(-1.0f,0.0f,0.0f);

	}
	void moveRight(){
		transform.Translate(1.0f,0.0f,0.0f);

	}
	void moveUp(){
		transform.Translate(0.0f,1.0f,0.0f);
	}
	void moveDown(){
		transform.Translate(0.0f,-1.0f,0.0f);

	}

	private void movePlayer(){
		if (Input.GetKeyDown (KeyCode.W)) {
			transform.Translate(0.0f,1.0f,0.0f);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			transform.Translate(0.0f,-1.0f,0.0f);
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			transform.Translate(-1.0f,0.0f,0.0f);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			transform.Translate(1.0f,0.0f,0.0f);
		}
	}
}
