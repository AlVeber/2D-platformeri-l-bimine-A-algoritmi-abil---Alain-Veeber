using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AStar : MonoBehaviour
{

	public GameObject NPC;
	private GameObject targetNode;
	private GameObject NPCNode;
	public Grid grid;
	public Sprite torch;
	public GameObject[,] nodeGrid;
	private bool isAbleToStart = true;
	private bool astarWorking = false;
	private bool pathReady = false;
	private List<GameObject> pathList;
	private List<GameObject> openSet;
	private List<GameObject> closedSet;

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (targetNode != null && NPCNode != null && isAbleToStart == true && astarWorking == false) {
			isAbleToStart = false;
			Astar ();
		}



	}

	public void Astar ()
	{
		astarWorking = true;
		

		nodeGrid = grid.GetComponent<GridScript> ().nodeGrid;
		print ("Started A*");

		Search ();
	
	


		//isAbleToStart = true;
	}






	public void  setTagretNode (GameObject targetNode)
	{
		this.targetNode = targetNode;
		Debug.Log ("Astar has target node: X:" + targetNode.GetComponent<NodeClass> ().x + " Y: " + targetNode.GetComponent<NodeClass> ().y);
	}

	public void setAINode (GameObject NPCNode)
	{
		this.NPCNode = NPCNode;
		Debug.Log ("Astar has NPC node: X:" + NPCNode.GetComponent<NodeClass> ().x + " Y: " + NPCNode.GetComponent<NodeClass> ().y);
	}


	public void Search ()
	{
		
		pathList = new List<GameObject> ();
		openSet = new List<GameObject> ();
		closedSet = new List<GameObject> ();



		print ("Started search");
		openSet.Add (NPCNode);
		int targetX = targetNode.GetComponent<NodeClass> ().x;
		int targetY = targetNode.GetComponent<NodeClass> ().y;
		GameObject currentNode = NPCNode;
		int count = 0;
		bool solutionFound = false;
		bool upBlock = false;
		bool horizontalBlock = false;
		pathReady = false;

		while (openSet.Count != 0) {
			
				
			currentNode = getCheapest ();

			count++;

			int currentX = currentNode.GetComponent<NodeClass> ().x;
			int currentY = currentNode.GetComponent<NodeClass> ().y;

			print ("Current node x: " + currentX + " y : " + currentY + " lastvertical = " + currentNode.GetComponent<NodeClass> ().lastVertical+ " jumpval: " + currentNode.GetComponent<NodeClass> ().jumpValue);

			if (currentX == targetX && currentY == targetY) {
				print ("Target found at X= " + currentX + " Y= " + currentY);
				solutionFound = true;
				getSolution (currentNode);
				break;
			}
				
			GameObject right = nodeGrid [currentY, currentX + 1];
			GameObject left = nodeGrid [currentY, currentX - 1];
			GameObject up = nodeGrid [currentY + 1, currentX];
			GameObject down = nodeGrid [currentY - 1, currentX];

		


			if (down.name == "Block(Clone)") {
				//print ("Grounded!!!");
				currentNode.GetComponent<NodeClass> ().jumpValue = 2;
				upBlock = false;
				horizontalBlock = false;
			} else {
				//print ("Not grounded!!!");
				//currentNode.GetComponent<NodeClass> ().jumpValue = jumpLeft;
				if (currentNode.GetComponent<NodeClass> ().lastVertical == true && currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().jumpValue == 2) {
					//print ("Walked into thin air parent jump value : " + currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().jumpValue +
					//"parent x : " + currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().x + " parent y: " + currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().y);
					currentNode.GetComponent<NodeClass> ().jumpValue = 0;
					horizontalBlock = true;
					upBlock = true;

				} else {	
					

					if (currentNode.GetComponent<NodeClass> ().jumpValue < 1) {
						//print ("No more jumps allowed form this node");
						upBlock = true;
					} else {
						//print ("Jumps allowed form this node");
						upBlock = false;
					}
						

					if (currentNode.GetComponent<NodeClass> ().jumpVertical == true) {
						//print ("Blocking horizontal");

						horizontalBlock = true;
					} else {
						horizontalBlock = false;
					}

				}
			}
			

			


			closedSet.Add (currentNode);
			openSet.Remove (currentNode);





			if (up.name != "Block(Clone)" && !openSet.Contains (up) && (!closedSet.Contains (up)) && (upBlock == false)) {
				//print ("Checking upper node x: " + up.GetComponent<NodeClass> ().x + " y: " + up.GetComponent<NodeClass> ().y + " count: " + count);
				openSet.Add (up);
				up.GetComponent<NodeClass> ().lastVertical = false;
				up.GetComponent<NodeClass> ().jumpValue = Mathf.Abs (up.GetComponent<NodeClass> ().jumpValue - currentNode.GetComponent<NodeClass> ().jumpValue - 1);
				up.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost + 3;
				up.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking upper node x: " + up.GetComponent<NodeClass> ().x + " y: " + up.GetComponent<NodeClass> ().y + " count: " + count + " jumpval: " + up.GetComponent<NodeClass> ().jumpValue);
				up.GetComponent<NodeClass> ().manhattanDistance = Mathf.Abs (targetNode.GetComponent<NodeClass> ().x -
				up.GetComponent<NodeClass> ().x) + Mathf.Abs (targetNode.GetComponent<NodeClass> ().y - up.GetComponent<NodeClass> ().y);
			



			}
			if (down.name != "Block(Clone)" && !openSet.Contains (down) && (!closedSet.Contains (down))) {
				print ("Checking lower node x: " + down.GetComponent<NodeClass> ().x + " y: " + down.GetComponent<NodeClass> ().y + " count: " + count);
				openSet.Add (down);
				down.GetComponent<NodeClass> ().lastVertical = false;
				down.GetComponent<NodeClass> ().jumpValue = 0;
				down.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost + 3;
				down.GetComponent<NodeClass> ().parent = currentNode;
				//horizontalBlock = false;
				down.GetComponent<NodeClass> ().manhattanDistance = Mathf.Abs (targetNode.GetComponent<NodeClass> ().x -
				down.GetComponent<NodeClass> ().x) + Mathf.Abs (targetNode.GetComponent<NodeClass> ().y - down.GetComponent<NodeClass> ().y);
			}

			if ((right.name != "Block(Clone)") && (!openSet.Contains (right)) && (!closedSet.Contains (right)) && horizontalBlock == false) {
				if (currentNode.GetComponent<NodeClass> ().jumpValue < 2) {
					right.GetComponent<NodeClass> ().jumpVertical = true;
					right.GetComponent<NodeClass> ().jumpValue = currentNode.GetComponent<NodeClass> ().jumpValue;


				}

				openSet.Add (right);
				right.GetComponent<NodeClass> ().lastVertical = true;
				right.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost + 2;
				right.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking right node x: " + right.GetComponent<NodeClass> ().x + " y: " + right.GetComponent<NodeClass> ().y + " count: " + right.GetComponent<NodeClass> ().cost + "jumpVal" + right.GetComponent<NodeClass> ().jumpValue);
				right.GetComponent<NodeClass> ().manhattanDistance = Mathf.Abs (targetNode.GetComponent<NodeClass> ().x -
				right.GetComponent<NodeClass> ().x) + Mathf.Abs (targetNode.GetComponent<NodeClass> ().y - right.GetComponent<NodeClass> ().y);

			}


			if ((left.name != "Block(Clone)") && (!openSet.Contains (left)) && (!closedSet.Contains (left)) && horizontalBlock == false) {
				if (currentNode.GetComponent<NodeClass> ().jumpValue < 2) {
					left.GetComponent<NodeClass> ().jumpVertical = true;
					left.GetComponent<NodeClass> ().jumpValue = currentNode.GetComponent<NodeClass> ().jumpValue;

				}

				openSet.Add (left);
				left.GetComponent<NodeClass> ().lastVertical = true;
				left.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost + 2;
				left.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking left node x: " + left.GetComponent<NodeClass> ().x + " y: " + left.GetComponent<NodeClass> ().y + " count: " + left.GetComponent<NodeClass> ().cost + "jumpVal" + left.GetComponent<NodeClass> ().jumpValue);
				left.GetComponent<NodeClass> ().manhattanDistance = Mathf.Abs (targetNode.GetComponent<NodeClass> ().x -
				left.GetComponent<NodeClass> ().x) + Mathf.Abs (targetNode.GetComponent<NodeClass> ().y - left.GetComponent<NodeClass> ().y);
				

			}


			//To print current openset
			/*print ("OpenSet printing");
			for (int h = 0; h < openSet.Count; h++) {

				print ("x: " + openSet [h].GetComponent<NodeClass> ().x + " y: " + openSet [h].GetComponent<NodeClass> ().y + " m: " + openSet [h].GetComponent<NodeClass> ().manhattanDistance + " c: " + openSet [h].GetComponent<NodeClass> ().cost);


			}*/



		}
			

		if (solutionFound == false) {
			print ("Target not found!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		}
		astarWorking = false;

	}


	
	
	public  GameObject getCheapest ()
	{
		int min = 9999999;
		GameObject cheapestNode = null;
		for (int i = 0; i < openSet.Count; i++) {	
			int newMin = openSet [i].GetComponent<NodeClass> ().manhattanDistance + openSet [i].GetComponent<NodeClass> ().cost;
			if (newMin <= min) {
				min = newMin;
				cheapestNode = openSet [i];
			}


		}
		//print ("Cheapest node x: " + cheapestNode.GetComponent<NodeClass> ().x + " y: " + cheapestNode.GetComponent<NodeClass> ().y);
		return cheapestNode;

	}

	public void getSolution (GameObject currentNode)
	{
		
		//print ("------------------------------Final Path-------------------------------------") ;
		while (currentNode.GetComponent<NodeClass> ().parent != null) {
			pathList.Add (currentNode);
			currentNode.GetComponent<SpriteRenderer> ().sprite = torch;
			//print("x: "+currentNode.GetComponent<NodeClass>().x +" y: "+currentNode.GetComponent<NodeClass>().y);
			currentNode = currentNode.GetComponent<NodeClass> ().parent;
		}

		pathReady = true;
		InvokeRepeating ("movePlayer", 1.0f, 0.3f);
		if (pathReady == false) {
			CancelInvoke ("movePlayer");
		}
	}

	public void movePlayer ()
	{
		GameObject nextNode = NPCNode;

		if (pathReady == true) {

			GameObject currentNode = nextNode;

			int currentX = currentNode.GetComponent<NodeClass> ().x;
			int currentY = currentNode.GetComponent<NodeClass> ().y;
			print ("Walking: current node x: " + currentX + " y : " + currentY);

			GameObject right = nodeGrid [currentY, currentX + 1];
			GameObject left = nodeGrid [currentY, currentX - 1];
			GameObject up = nodeGrid [currentY + 1, currentX];
			GameObject down = nodeGrid [currentY - 1, currentX];

			nextNode = pathList.Last ();
			pathList.Remove (pathList.Last ());
			//print ("Walking: next node x: " + nextNode.GetComponent<NodeClass> ().x + " y : " + nextNode.GetComponent<NodeClass> ().y);

			if (nextNode == left) {
				NPC.GetComponent<player> ().moveLeft ();
				//print ("Player moves left");

			} else if (nextNode == right) {

				NPC.GetComponent<player> ().moveRight ();
				//print ("Player moves right");

			} else if (nextNode == up) {

				NPC.GetComponent<player> ().moveUp ();
				//print ("Player moves up");

			} else if ((nextNode == down)) {
				NPC.GetComponent<player> ().moveDown ();
				//print ("Player moves down");

			} else {
				print ("Something wrong!!!!!!!! No movement direction!!!!!!!");
				pathReady = false;
			}
		
			if (nextNode.GetComponent<NodeClass> ().x == targetNode.GetComponent<NodeClass> ().x &&
			    nextNode.GetComponent<NodeClass> ().y == targetNode.GetComponent<NodeClass> ().y) {
				print ("Walking: Target found at X= " + currentX + " Y= " + currentY);

				pathReady = false;
				pathList = null;
				targetNode = null;

			}






		}

	}
		




}











