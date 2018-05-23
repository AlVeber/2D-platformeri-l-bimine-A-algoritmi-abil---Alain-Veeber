using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeClass : MonoBehaviour
{
	public int jumpValue = 2;
	public bool jumpVertical = false;
	public bool lastVertical = false;
	public int x;
	public int y;
	public int cost = 0;
	public int type;
	public int manhattanDistance;
	public GameObject parent;
	public Grid grid;




	void Start ()
	{
	
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		//print ("Triggered by " + coll.name);
		grid = GameObject.FindObjectOfType<Grid> ();
		if (coll.name == "NPC") {
			//print ("NPC Triggered: "+ gameObject.name);
			grid.GetComponent<AStar> ().setAINode (this.gameObject);

		}
		if (coll.name == "Target" || coll.name == "Target(Clone)") {
			//print ("Target Triggered: "+ gameObject.name);
			grid.GetComponent<AStar> ().setTagretNode (this.gameObject);

		}


	}

	public void setNodeValues (int x, int y, int type)
	{
		this.x = x;
		this.y = y;
		this.type = type;
		//Debug.Log ("Node created at x: " + x + " y: " + y );

	}

	public void setParentNode (GameObject parentNode)
	{
		parent = parentNode;
	}

	public GameObject getParent ()
	{
		return parent;
	}

	public int getCost ()
	{
		return cost;
	}
}
