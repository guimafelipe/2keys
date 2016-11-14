using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject blockPrefab;

	private int atualBlockInd = 0;
	private string[] levelRoute;
	private int blockNumber = 6;
	public int teamMemberNumber = 4;

	private GameObject atualBlock;
	private GameObject playerTeam;//private GameObject enemyTeam;
	// Use this for initialization

	void Start () {
		
		playerTeam = GameObject.Find("Team1");
		//enemyTeam = GameObject.Find("Team2");

		var playerTeamBehaviour = playerTeam.GetComponent<TeamBehaviour>();
		//var enemyTeamBehaviour = enemyTeam.GetComponent<TeamBehaviour>();

		playerTeamBehaviour.CreateChinchilas(teamMemberNumber);
		//enemyTeam.CreateChinchilas(teamMemberNumber);

		levelRoute = new string[blockNumber];
		levelRoute [0] = "-+";
		levelRoute [1] = "+-";
		levelRoute [2] = "--+";
		levelRoute [3] = "+++";
		levelRoute [4] = "--++";
		levelRoute [5] = "--+++";   //Set the arrow configuration in the block, each ind is a block. So,there are 6 block in this level


		GameObject _newBlock = Instantiate (blockPrefab);  //create the first block of the level
		//_newBlock.transform.parent = gameObject.transform;
		_newBlock.transform.position = transform.position;
		var _blockInstance = _newBlock.transform.GetComponent<ArrowBlockBehaviour> ();  //get the script of the new block
		if (_blockInstance) {
			//print (levelRoute [0]);
			_blockInstance.CreateArrows (levelRoute[0]); //Create the arrows of the first block
		}
		atualBlock = _newBlock; //set the first block as the atual block
	}
	
	// Update is called once per frame
	void Update () {
		if (atualBlock) { //if there is a block...
			if (Input.GetKeyDown ("s")){  //get the user input
				KeyPressed ('-');
			}
			if (Input.GetKeyDown ("w")) {
				KeyPressed ('+');
			}
		}
	}

	public void NextBlock(){ //change the block of arrows
		atualBlockInd++;  //update the indice
		Destroy (atualBlock); //destroy the last block finished
		if (atualBlockInd < blockNumber) {  //see if there is another block to be created in this level
			GameObject _newBlock = Instantiate (blockPrefab); //create the block
			_newBlock.transform.position = transform.position;
			//_newBlock.transform.parent = gameObject.transform;
			var blockInstance = _newBlock.transform.GetComponent<ArrowBlockBehaviour> (); //get the script component of the created block
			if (blockInstance) {
				blockInstance.CreateArrows (levelRoute [atualBlockInd]);   //set the arrows configuration of the new block
			}
			atualBlock = _newBlock;  //atualize the atual block
		}
	}

	void KeyPressed(char _signal){
		var actualBlockAction = atualBlock.GetComponent<ArrowBlockBehaviour> (); 
		if(actualBlockAction){
			actualBlockAction.GetArrowPressed (_signal);   //send the user's input to the atual block
		}
	}

}
