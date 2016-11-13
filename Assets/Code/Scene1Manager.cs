using UnityEngine;
using System.Collections;

public class Scene1Manager : MonoBehaviour {

	public GameObject blockPrefab;
	private int atualBlockInd = 0;
	private string[] levelRoute;
	private int blockNumber = 6;
	private GameObject atualBlock;
	// Use this for initialization
	void Start () {
		levelRoute = new string[blockNumber];
		levelRoute [0] = "-+";
		levelRoute [1] = "+-";
		levelRoute [2] = "--+";
		levelRoute [3] = "+++";
		levelRoute [4] = "--++";
		levelRoute [5] = "--+++";


		GameObject newBlock = Instantiate (blockPrefab);
		var blockInstance = newBlock.transform.GetComponent<ArrowBlockBehaviour> ();
		if (blockInstance) {
			print (levelRoute [0]);
			blockInstance.CreateArrows (levelRoute[0]);
		}
		atualBlock = newBlock;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("s")) {
			KeyPressed ('-');
		}
		if (Input.GetKeyDown ("w")) {
			KeyPressed ('+');
		}

	}

	public void NextBlock(){
		atualBlockInd++;
		Destroy (atualBlock);
		if (atualBlockInd < blockNumber) {
			GameObject newBlock = Instantiate (blockPrefab);
			var blockInstance = newBlock.transform.GetComponent<ArrowBlockBehaviour> ();
			if (blockInstance) {
				blockInstance.CreateArrows (levelRoute [atualBlockInd]);
			}
			atualBlock = newBlock;
		}
	}

	void KeyPressed(char signal){
		var actualBlockAction = atualBlock.GetComponent<ArrowBlockBehaviour> ();
		if(actualBlockAction){
			actualBlockAction.GetArrowPressed (signal);
		}
	}

}
	