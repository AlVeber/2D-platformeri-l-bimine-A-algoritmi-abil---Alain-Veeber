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
		bool savedNode = false;
		bool upBlock = false;
		bool verticalBlock = false;
		while (openSet.Count != 0) {
			if (savedNode == false) {
				
				currentNode = getCheapest ();
			} else {
				savedNode = false;
			}

			i++;
			count++;

			int currentX = currentNode.GetComponent<NodeClass> ().x;
			int currentY = currentNode.GetComponent<NodeClass> ().y;

			print ("current node x: " + currentX + " y : " + currentY + " lastvertical = " + currentNode.GetComponent<NodeClass> ().lastVertical+ " jumpval: " + currentNode.GetComponent<NodeClass> ().jumpValue);
			if (currentX == targetX && currentY == targetY) {
				print ("Found at X= " + currentX + " Y= " + currentY);
				solutionFound = true;
				getSolution (currentNode);
				break;
			}
				
			GameObject right = nodeGrid [currentY, currentX + 1];
			GameObject left = nodeGrid [currentY, currentX - 1];
			GameObject up = nodeGrid [currentY + 1, currentX];
			GameObject down = nodeGrid [currentY - 1, currentX];



			print ("check0 " + currentNode.GetComponent<NodeClass>().jumpValue);


			if (down.name == "Block(Clone)") {
				print ("Grounded!!!");
				jumpLeft = 2;
				currentNode.GetComponent<NodeClass> ().jumpValue = 2;
				upBlock = false;
				verticalBlock = false;
			} else {
				print ("Not grounded!!!");
				//currentNode.GetComponent<NodeClass> ().jumpValue = jumpLeft;
				if (currentNode.GetComponent<NodeClass> ().lastVertical == true && currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().jumpValue == 2) {
					print ("Walked into thin air parent jump value : "+ currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().jumpValue+
						"parent x : "+ currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().x+ " parent y: "+currentNode.GetComponent<NodeClass> ().parent.GetComponent<NodeClass> ().y);
					jumpLeft = 0;
					currentNode.GetComponent<NodeClass> ().jumpValue = 0;
					verticalBlock = true;
					upBlock = true;

				} else {	
					print ("check0 " + currentNode.GetComponent<NodeClass>().jumpValue);

					if (currentNode.GetComponent<NodeClass> ().jumpValue < 1) {
						print ("no more jumps allowed form this node");
						upBlock = true;
					} else {
						print ("Jumps allowed form this node");
						upBlock = false;
					}

					/*
					if (jumpLeft < 1 && upBlock == false) {
						print ("Blocking up");
						upBlock = true;
					} else {
						upBlock = false;
					}
					*/
					print ("check0 " + currentNode.GetComponent<NodeClass>().jumpValue);

					if (currentNode.GetComponent<NodeClass> ().jumpVertical == true) {
						print ("Blocking vertical");

						verticalBlock = true;
					} else {
						verticalBlock = false;
					}
					print ("check0 " + currentNode.GetComponent<NodeClass>().jumpValue);


					if (down.name != "Block(Clone)") {
						jumpLeft--;
						print ("In air!!! jumpLeft: " + jumpLeft);

					}
				}
			}
			

			
			print ("check0 " + currentNode.GetComponent<NodeClass>().jumpValue);

			closedSet.Add (currentNode);
			openSet.Remove (currentNode);




			if ((right.name != "Block(Clone)") && (!openSet.Contains (right)) && (!closedSet.Contains (right)) && verticalBlock == false) {
				if (currentNode.GetComponent<NodeClass>().jumpValue<2) {
					right.GetComponent<NodeClass> ().jumpVertical = true;
					right.GetComponent<NodeClass> ().jumpValue=currentNode.GetComponent<NodeClass>().jumpValue;


				}

				openSet.Add (right);
				right.GetComponent<NodeClass> ().lastVertical = true;
				right.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost+2;
				right.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking right node x: " + right.GetComponent<NodeClass> ().x + " y: " + right.GetComponent<NodeClass> ().y + " count: " + right.GetComponent<NodeClass> ().cost + "jumpVal"+ right.GetComponent<NodeClass> ().jumpValue);


			}


			if ((left.name != "Block(Clone)") && (!openSet.Contains (left)) && (!closedSet.Contains (left)) && verticalBlock == false) {
				print ("check1" + currentNode.GetComponent<NodeClass>().jumpValue);
				if (currentNode.GetComponent<NodeClass>().jumpValue<2) {
					print ("check2" + currentNode.GetComponent<NodeClass>().jumpValue);
					left.GetComponent<NodeClass> ().jumpVertical = true;
					left.GetComponent<NodeClass> ().jumpValue=currentNode.GetComponent<NodeClass>().jumpValue;

				}




				openSet.Add (left);
				left.GetComponent<NodeClass> ().lastVertical = true;
				left.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost+2;
				left.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking left node x: " + left.GetComponent<NodeClass> ().x + " y: " + left.GetComponent<NodeClass> ().y + " count: " + left.GetComponent<NodeClass> ().cost + "jumpVal"+ left.GetComponent<NodeClass> ().jumpValue);


			}
			if (up.name != "Block(Clone)" && !openSet.Contains (up) && (!closedSet.Contains (up)) && (upBlock == false)) {
				//print ("Checking upper node x: " + up.GetComponent<NodeClass> ().x + " y: " + up.GetComponent<NodeClass> ().y + " count: " + count);
				openSet.Add (up);
				up.GetComponent<NodeClass> ().lastVertical = false;
				up.GetComponent<NodeClass> ().jumpValue= Mathf.Abs( up.GetComponent<NodeClass> ().jumpValue- currentNode.GetComponent<NodeClass>().jumpValue -1);
				up.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost+3 ;
				up.GetComponent<NodeClass> ().parent = currentNode;
				print ("Checking upper node x: " + up.GetComponent<NodeClass> ().x + " y: " + up.GetComponent<NodeClass> ().y + " count: " + count+ " jumpval: " + up.GetComponent<NodeClass> ().jumpValue);
				jumpLeft--;



			}
			if (down.name != "Block(Clone)" && !openSet.Contains (down) && (!closedSet.Contains (down))) {
				print ("Checking lower node x: " + down.GetComponent<NodeClass> ().x + " y: " + down.GetComponent<NodeClass> ().y + " count: " + count);
				openSet.Add (down);
				down.GetComponent<NodeClass> ().lastVertical = false;
				down.GetComponent<NodeClass> ().jumpValue = 0;
				down.GetComponent<NodeClass> ().cost = currentNode.GetComponent<NodeClass> ().cost+1;
				down.GetComponent<NodeClass> ().parent = currentNode;
				verticalBlock = false;
			}

			
			print ("openSet printing");
			for (int h = 0; h < openSet.Count; h++) {

				print ("x: " + openSet [h].GetComponent<NodeClass> ().x + " y: " + openSet [h].GetComponent<NodeClass> ().y + " m: " + openSet [h].GetComponent<NodeClass> ().manhattanDistance + " c: " + openSet [h].GetComponent<NodeClass> ().cost);


			}


			if (i > 2000) {
				
				break;
			}

		}
			

			if (solutionFound == false) {
				print ("Target not found!!!!!!!!!!!!!!!!!!!!");
			}
		
	}
	

	
	
	public  GameObject getCheapest(){
	print ("Getting getCheapest node");
		int min = 9999999;
		GameObject cheapestNode = null;
		for (int i = 0; i < openSet.Count; i++) {
		
			int newMin = openSet [i].GetComponent<NodeClass> ().manhattanDistance + openSet [i].GetComponent<NodeClass> ().cost;
			if ( newMin  <= min) {
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











