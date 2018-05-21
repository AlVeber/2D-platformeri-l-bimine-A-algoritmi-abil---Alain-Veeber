using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
	[SerializeField]
	private float PlayerSpeed=1;
	private Vector2 direction;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		GetInput ();
		Move ();


	}

	private void Move(){
		transform.Translate(direction*PlayerSpeed*Time.deltaTime);
	}
	private void GetInput(){
		if (Input.GetKey(KeyCode.W)) {
			direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.S)) {
			direction += Vector2.down;
		}
		if (Input.GetKey(KeyCode.A)) {
			direction += Vector2.left;
		}
		if (Input.GetKey(KeyCode.D)) {
			direction += Vector2.right;
		}
	}
}
