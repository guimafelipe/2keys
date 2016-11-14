using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

	public GameObject enemyBlockPrefab;

	private int atualBlockInd = 0;
	private string[] levelRoute;
	private int blockNumber = 6;
	public int teamMemberNumber = 4;

	public float aestheticalAdjustmentDistance = 2f;

	private int maximumHP = 100;
	public int currentHP = 100;

	private GameObject atualBlock;
	private GameObject enemyTeam;//private GameObject enemyTeam;
	// Use this for initialization

	[SerializeField]
	private HealthIndicator healthIndicator;  //Declare the status indicator

	void Start () {

		currentHP = maximumHP;
		healthIndicator.SetHealth (currentHP, maximumHP);  //set the initial health of the enemy

		enemyTeam = GameObject.Find("Team2");

		var enemyTeamBehaviour = enemyTeam.GetComponent<EnemyTeamBehaviour>();

		enemyTeamBehaviour.CreateEnemyChinchilas(teamMemberNumber);

		levelRoute = new string[blockNumber];
		levelRoute [0] = "-+";
		levelRoute [1] = "+-";
		levelRoute [2] = "--+";
		levelRoute [3] = "+++";
		levelRoute [4] = "--++";
		levelRoute [5] = "--+++";   //Set the arrow configuration in the block, each ind is a block. So,there are 6 block in this level


		GameObject _newBlock = Instantiate (enemyBlockPrefab);  //create the first block of the level
		//_newBlock.transform.parent = gameObject.transform;
		_newBlock.transform.position = new Vector3(transform.position.x + aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical adjustment in the screen
		var _blockInstance = _newBlock.transform.GetComponent<EnemyArrowBlockBehaviour> ();  //get the script of the new block
		if (_blockInstance) {
			_blockInstance.CreateArrows (levelRoute[0]); //Create the arrows of the first block
			Debug.Log("chamou a criação de setas");
		}
		atualBlock = _newBlock; //set the first block as the atual block
	}

	// Update is called once per frame
	void Update () {
		if (atualBlock) { //if there is a block...
			if (Input.GetKeyDown ("down")){  //get the user input
				KeyPressed ('-');
			}
			if (Input.GetKeyDown("up")) {
				KeyPressed ('+');
			}
		}
	}

	public void NextBlock(){ //change the block of arrows
		atualBlockInd++;  //update the indice
		Destroy (atualBlock); //destroy the last block finished
		if (atualBlockInd < blockNumber) {  //see if there is another block to be created in this level
			GameObject _newBlock = Instantiate (enemyBlockPrefab); //create the enemy block
			_newBlock.transform.position = new Vector3(transform.position.x + aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical adjustment in the screen
			//_newBlock.transform.parent = gameObject.transform;
			var blockInstance = _newBlock.transform.GetComponent<EnemyArrowBlockBehaviour> (); //get the script component of the created block
			if (blockInstance) {
				blockInstance.CreateArrows (levelRoute [atualBlockInd]);   //set the arrows configuration of the new block
			}
			atualBlock = _newBlock;  //atualize the atual block
		}
	}

	void KeyPressed(char _signal){
		var actualBlockAction = atualBlock.GetComponent<EnemyArrowBlockBehaviour> (); 
		if(actualBlockAction){
			actualBlockAction.GetArrowPressed (_signal);   //send the user's input to the atual block
		}
	}

	public void GetDamage(int _damageValue){
		currentHP -= _damageValue;
		healthIndicator.SetHealth (currentHP, maximumHP);

	}

	void CheckDeath(){
		if (currentHP <= 0) {
			Debug.Log ("enemy is ded");
		}
	}

}
