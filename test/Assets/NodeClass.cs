using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeClass : MonoBehaviour{
	public int x;
	public int y;
	public int cost=0;
	public int type;
	public int manhattanDistance;
	public GameObject parent;

	[SerializeField]
	public Grid grid;




	void Start(){
	
	}
	void Update(){
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		print ("Triggered by " + coll.name);
		grid = GameObject.FindObjectOfType<Grid> ();
		if (coll.name == "AI") {
			print ("AI Triggered: "+ gameObject.name);
			grid.GetComponent<AStar> ().setAINode (this.gameObject);

		}
		if (coll.name == "Target") {
			print ("Target Triggered: "+ gameObject.name);
			grid.GetComponent<AStar> ().setTagretNode (this.gameObject);

		}


	}
	public void setNodeValues (int x, int y, int type){
		this.x= x;
		this.y= y;
		this.type= type;
		//Debug.Log ("node made at x: " + x + " y: " + y + " cost: " + cost);

	}
	public void setParentNode(GameObject parentNode){
		parent = parentNode;
	}
	public GameObject getParent(){
		return parent;
	}

	public int getCost(){
		return cost;
	}
}
