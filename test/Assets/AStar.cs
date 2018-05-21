using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStar : MonoBehaviour {
	private GameObject targetNode;
	private GameObject AINode;
	[SerializeField]
	public Grid grid;
	[SerializeField]
	public Sprite torch;
	public GameObject[,] nodeGrid;
	// Use this for initialization
	private bool isAbleToStart= true;
	public List<GameObject> pathList = new List<GameObject> ();
	public List<GameObject> openSet = new List<GameObject> ();
	public List<GameObject> closedSet = new List<GameObject> ();
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targetNode != null && AINode != null && isAbleToStart == true) {
			isAbleToStart = false;
			Astar ();
		}

	}

	public void Astar(){
		

			nodeGrid = grid.GetComponent<GridScript> ().nodeGrid;
			print ("Started A*");
			int rowLength = nodeGrid.GetLength (1);
			int colLength = nodeGrid.GetLength (0);
			print ("Astar node grid");
			for (int i = 0; i < colLength; i++) {
				string s2 = "";
				for (int j = 0; j < rowLength; j++) {
					//Debug.Log ("x: "+i + " - y: " + j);
				if (nodeGrid [i, j].name == "Node(Clone)") {

						s2 += " " + nodeGrid [i, j].name;
						GameObject node = nodeGrid [i, j];
						node.GetComponent<NodeClass> ().manhattanDistance = Mathf.Abs (targetNode.GetComponent<NodeClass>().x-
							node.GetComponent<NodeClass>().x )+Mathf.Abs (targetNode.GetComponent<NodeClass>().y-node.GetComponent<NodeClass>().y);
						print ("node " +" x: "+ nodeGrid [i, j].GetComponent<NodeClass> ().x+ " y: "+ nodeGrid [i, j].GetComponent<NodeClass> ().y+" got manhattanValue" + node.GetComponent<NodeClass> ().manhattanDistance);


					} else {
						s2 += " plat";
					}


				}
				Debug.Log (s2);
			}
		
			Search ();
	
	
		targetNode = null;
		isAbleToStart = false;
	}






	public void  setTagretNode(GameObject targetNode){
		this.targetNode = targetNode;
		//Debug.Log ("Astar has target node");

		Debug.Log ("Astar has target node: X:"+ targetNode.GetComponent<NodeClass>().x + " Y: " + targetNode.GetComponent<NodeClass>().y);
	}
	public void setAINode(GameObject AINode){
		this.AINode = AINode;
		//Debug.Log ("Astar has AI node");
		Debug.Log ("Astar has AI node: X:"+ AINode.GetComponent<NodeClass>().x + " Y: " + AINode.GetComponent<NodeClass>().y);
	}
	

	public void Search(){
		




		print ("Started search");
		openSet.Add (AINode);
		int targetX = targetNode.GetComponent<NodeClass> ().x;
		int targetY = targetNode.GetComponent<NodeClass> ().y;
		GameObject currentNode = AINode;
		int count = 0;
		int i = 0;
		int jumpLeft = 2;
		bool solutionFound = false;
		bool forceDown = false;
		while (openSet.Count!=0) {
			currentNode = getCheapest ();

			i++;
			count++;

			int currentX = currentNode.GetComponent<NodeClass> ().x;
			int currentY = currentNode.GetComponent<NodeClass> ().y;

			print ("current node x: "+ currentX+ " y : "+ currentY);
			if (currentX == targetX && currentY == targetY) {
				print ("Found at X= "+currentX+ " Y= "+currentY);
				solutionFound = true;
				getSolution (currentNode);
				break;
			}




			closedSet.Add (currentNode);
			openSet.Remove (currentNode);


			GameObject right = nodeGrid [currentY,currentX + 1];
			GameObject left = nodeGrid [currentY,currentX - 1];
			GameObject up = nodeGrid [ currentY+1, currentX];
			GameObject down = nodeGrid [currentY-1,currentX];

			if (down.name == "Block(Clone)") {
				print ("Grounded!!!");
				jumpLeft = 2;
			} 

			if (jumpLeft < 1) {
				print ("!!!!!!!!!!! Forced to go down");
				//forceDown = true;
			}


			if(down.name != "Block(Clone)") {
				jumpLeft--;
				print ("In air!!! jumpLeft: "+ jumpLeft);
			}




			if (forceDown == true) {
				
				print ("!!!!!!!!!!! Forced to get to lower node ");
					//openSet.Add (down);
					//down.GetComponent<NodeClass> ().cost = count;
					//down.GetComponent<NodeClass> ().parent = currentNode;
					forceDown = false;

			} else {

				if ((right.name != "Block(Clone)") && (!openSet.Contains (right)) && (!closedSet.Contains (right))) {
					print ("Checking right node x: " + right.GetComponent<NodeClass> ().x + " y: " + right.GetComponent<NodeClass> ().y + " count: " + count);
					openSet.Add (right);
					right.GetComponent<NodeClass> ().cost = count+1;
					right.GetComponent<NodeClass> ().parent = currentNode;

				}
				if ((left.name != "Block(Clone)") && (!openSet.Contains (left)) && (!closedSet.Contains (left))) {
					print ("Checking left node x: " + left.GetComponent<NodeClass> ().x + " y: " + left.GetComponent<NodeClass> ().y + " count: " + count);
					openSet.Add (left);
					left.GetComponent<NodeClass> ().cost = count+1;
					left.GetComponent<NodeClass> ().parent = currentNode;
				}
				if (up.name != "Block(Clone)" && !openSet.Contains (up) && (!closedSet.Contains (up)) && (jumpLeft > 0)) {
					print ("Checking upper node x: " + up.GetComponent<NodeClass> ().x + " y: " + up.GetComponent<NodeClass> ().y + " count: " + count);
					openSet.Add (up);
					up.GetComponent<NodeClass> ().cost = count+2;
					up.GetComponent<NodeClass> ().parent = currentNode;
				}
				if (down.name != "Block(Clone)" && !openSet.Contains (down) && (!closedSet.Contains (down))) {
					print ("Checking lower node x: " + down.GetComponent<NodeClass> ().x + " y: " + down.GetComponent<NodeClass> ().y + " count: " + count);
					openSet.Add (down);
					down.GetComponent<NodeClass> ().cost = count;
					down.GetComponent<NodeClass> ().parent = currentNode;
				}
			}
			/*if (((right.name=="Block(Clone)") || openSet.Contains(right)|| (closedSet.Contains(right))) &&
				(((left.name=="Block(Clone)") ||  openSet.Contains(left))|| (closedSet.Contains(left))) &&
				(((up.name=="Block(Clone)") ||  openSet.Contains(up))|| (closedSet.Contains(up)))&&
				(((down.name=="Block(Clone)") ||  openSet.Contains(down))|| (closedSet.Contains(down)))) {
				print ("Removing current node from open set x: "+ currentX+ " y : "+ currentY);


			} */
			print ("openSet printing");
			for (int h = 0; h < openSet.Count; h++) {

				print("x: "+openSet[h].GetComponent<NodeClass>().x +" y: "+openSet[h].GetComponent<NodeClass>().y+ " m: "+ openSet[h].GetComponent<NodeClass>().manhattanDistance+ " c: "+ openSet[h].GetComponent<NodeClass>().cost);


			}


			if (i > 1000) {
				
				break;
			}


		}

		if(solutionFound==false){
			print ("Target not found!!!!!!!!!!!!!!!!!!!!");
		}
	}
	
	
	
	public  GameObject getCheapest(){
	print ("Getting getCheapest node");
		int min = 9999999;
		GameObject cheapestNode = null;
		for (int i = 0; i < openSet.Count; i++) {
		
			int newMin = openSet [i].GetComponent<NodeClass> ().manhattanDistance + openSet [i].GetComponent<NodeClass> ().cost;
			if ( newMin  < min) {
				min = newMin;
				cheapestNode= openSet [i];
			}


		}
		print ("Cheapest node x: " + cheapestNode.GetComponent<NodeClass> ().x + " y: " + cheapestNode.GetComponent<NodeClass> ().y);
		return cheapestNode;

	}
	public void getSolution(GameObject currentNode){
		
		print ("Final Path") ;
		while (currentNode.GetComponent<NodeClass> ().parent != null) {
			pathList.Add (currentNode);
			currentNode.GetComponent<SpriteRenderer> ().sprite = torch;
			print("x: "+currentNode.GetComponent<NodeClass>().x +" y: "+currentNode.GetComponent<NodeClass>().y);
			currentNode= currentNode.GetComponent<NodeClass> ().parent;
		}

	}
		



	}











