using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour{


	[SerializeField]
	public Tilemap walls;
	//public TileBase[,] nodeArray { get; set; } 
	public GameObject[,] nodeGrid { get; set; } 
	//public GameObject[][] nodeGrid2;
	[SerializeField]
	public GameObject nodePrefab;
	[SerializeField]
	public GameObject blockPrefab;
	[SerializeField]
	public Grid gridBase;
	void Start ()  {
		
			
		Debug.Log ("Printing Map");

		BoundsInt bounds = walls.cellBounds;
		TileBase[] allTiles = walls.GetTilesBlock(bounds);
		int bsx = bounds.size.x;
		int bsy = bounds.size.y;
		Debug.Log("x size: "+bsx);
		Debug.Log("y size: "+bsy);
		//nodeArray= new TileBase[bsy,bsx];
		nodeGrid= new GameObject[bsy,bsx];

		//nodeGrid2 =  new GameObject[bsy][];


		for (int y = 0; y < bsy; y++) {
			
			string s = "";
			//Debug.Log (" y "+ y);
			GameObject[] nodeRow= new GameObject[bsx];
			for (int x = 0; x < bsx; x++) {
				TileBase tile = allTiles [x + y * bsx];

				if (tile != null) {
					s += "  1";
					Debug.Log ("Platform at : " +x +" - "+ y);
				
					//Debug.Log ("Tile put at x: "+x+" y: "+j);
					//nodeArray [bsy-1-y,x] = tile;
					GameObject block = (GameObject)Instantiate (blockPrefab, new Vector3 (x-10 + 0.5f + gridBase.transform.position.x, y-5 + 0.5f+ gridBase.transform.position.y, 0), Quaternion.Euler (0, 0, 0));

					nodeGrid [y,x] = block;
					//nodeRow [x] = node;

				} else {
					s += "  0" ;
					print ("new node: X: " + x + " Y: " + y);
	
					GameObject node = (GameObject)Instantiate (nodePrefab, new Vector3 (x-10 + 0.5f + gridBase.transform.position.x, y-5 + 0.5f+ gridBase.transform.position.y, 0), Quaternion.Euler (0, 0, 0));
					node.GetComponent<NodeClass> ().setNodeValues (x, y, 0);

					//nodeArray [bsy-1-y,x] = null;
					nodeGrid [y,x] = node;
					//nodeRow [x] = node;
				}
			}
			Debug.Log ("y="+y+"|"+s);
			//nodeGrid2 [y] = nodeRow;


		}
		Debug.Log ("____________________________________________________________________________");

		/*
		print ("2nd grid");
		for (int y = 0; y < nodeGrid2.Length; y++) {
			string s2 = "";
			for (int x = 0; x < nodeGrid2[y].Length; x++) {
				if (nodeGrid2 [y][ x] != null) {

					s2 += " node";
				} else {
					s2 += " null";
				}
			}
			Debug.Log ("y= "+ y+" |   "+s2);

		}


*/







		print ("primary grid");
		int rowLength = nodeGrid.GetLength(0);
		int colLength = nodeGrid.GetLength(1);

		for (int i = 0; i < rowLength; i++){
			string s2 = "";
			for (int j = 0; j < colLength; j++){
				//Debug.Log ("y: "+i+ " - x: " + j);
				//print(nodeGrid [i, j].name);
				if (nodeGrid [i, j].name == "Node(Clone)") {

					s2 += " node";
				} else {
					s2 += " block";
				}

			}
			Debug.Log ("y= "+ i+" |   "+s2);
		}








		/*
		int rowLength = nodeArray.GetLength(1);
		int colLength = nodeArray.GetLength(0);

		for (int i = 0; i < colLength; i++){
			string s2 = "";
			for (int j = 0; j < rowLength; j++){
				Debug.Log ("x: "+i + " - y: " + j);
				if (nodeArray [i, j] != null) {

					s2 += " "+nodeArray [i, j].name;
				} else {
					s2 += " null";
				}

				 
			}
			Debug.Log (s2);
		}
		*/
	

				}
	// Update is called once per frame
	void Update () {


		
	}

	public void createNodes(){
	
	
	
	
	
	
	}
}
