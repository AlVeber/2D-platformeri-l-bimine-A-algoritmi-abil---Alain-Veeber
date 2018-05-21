using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class AIPosition : MonoBehaviour {
	[SerializeField]
	public GameObject AI;
	[SerializeField]
	public Tilemap floor;


	// Use this for initialization
	void Start () {
		getAIPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void getAIPosition(){
		/*Vector3 pz = Camera.main.ScreenToWorldPoint (AI.transform.position.Equals);
		pz.z = 0;
		GridLayout grid = transform.parent.GetComponentsInParent<GridLayout>();
		Vector3Int cellPosition = grid.WorldToCell (AI);
		print ("AI at position:" + cellPosition);
*/
	}

}
