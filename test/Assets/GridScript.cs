using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{


	public Tilemap walls;
	public GameObject[,] nodeGrid { get; set; }
	public GameObject nodePrefab;
	public GameObject blockPrefab;
	public Grid gridBase;

	void Start ()
	{


		TileBase[] allTiles = walls.GetTilesBlock (walls.cellBounds);
		int bsx = walls.cellBounds.size.x;
		int bsy = walls.cellBounds.size.y;
		nodeGrid = new GameObject[bsy, bsx];


		for (int y = 0; y < bsy; y++) {
			for (int x = 0; x < bsx; x++) {
				
				TileBase tile = allTiles [x + y * bsx];

				if (tile != null) {
					GameObject block = (GameObject)Instantiate (blockPrefab, new Vector3 (x + 0.5f + gridBase.transform.position.x, y + 0.5f + gridBase.transform.position.y, 0), Quaternion.Euler (0, 0, 0));
					nodeGrid [y, x] = block;


				} else {
					GameObject node = (GameObject)Instantiate (nodePrefab, new Vector3 (x + 0.5f + gridBase.transform.position.x, y + 0.5f + gridBase.transform.position.y, 0), Quaternion.Euler (0, 0, 0));
					node.GetComponent<NodeClass> ().setNodeValues (x, y, 0);
					nodeGrid [y, x] = node;
				}
			}


		}


		//Printing nodeGrid

		/*
		print ("printing grid");

		int row = nodeGrid.GetLength (0);
		int col = nodeGrid.GetLength (1);

		for (int i = 0; i < row; i++) {
			string s2 = "";
			for (int j = 0; j < col; j++) {
				if (nodeGrid [i, j].name == "Node(Clone)") {
					s2 += " n";
				} else {
					s2 += " b";
				}
			}
			Debug.Log ("y= " + i + " |   " + s2);
		}
		*/




	}
	// Update is called once per frame
	void Update ()
	{


		
	}


}
