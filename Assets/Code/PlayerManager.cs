using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject blockPrefab;

	private string arrowsConfig;
	public int teamMemberNumber = 4;

	public float aestheticalAdjustmentDistance;

	private bool canSendSignal;

	private int maximumHP = 100;
	public int currentHP;

	private GameObject gameMaster;
	private GameObject atualBlock;
	private GameObject playerTeam;//private GameObject enemyTeam;
	// Use this for initialization

	[SerializeField]
	private HealthIndicator healthIndicator;  //Declare the status indicator

	public void InitialSetup (string firstBlock) {

		currentHP = maximumHP;
		healthIndicator.SetHealth (currentHP, maximumHP);  //set the initial health of the team

		canSendSignal = true;

		gameMaster = GameObject.Find ("Game Master");
		playerTeam = GameObject.Find("Team1");

		var playerTeamBehaviour = playerTeam.GetComponent<TeamBehaviour>();
		//var enemyTeamBehaviour = enemyTeam.GetComponent<TeamBehaviour>();

		playerTeamBehaviour.CreateChinchilas(teamMemberNumber);
		//enemyTeam.CreateChinchilas(teamMemberNumber);

		GameObject _newBlock = Instantiate (blockPrefab);  //create the first block of the level
		//_newBlock.transform.parent = gameObject.transform;
		_newBlock.transform.position = new Vector3(transform.position.x + aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical ajust in the screen
		var _blockInstance = _newBlock.transform.GetComponent<ArrowBlockBehaviour> ();  //get the script of the new block
		if (_blockInstance) {
			arrowsConfig = firstBlock;
			_blockInstance.CreateArrows (firstBlock); //Create the arrows of the first block
		}
		atualBlock = _newBlock; //set the first block as the atual block
	}
	
	// Update is called once per frame
	void Update () {
		CheckDeath ();
		if (atualBlock && canSendSignal) { //if there is a block...
			if (Input.GetKeyDown ("s")){  //get the user input
				KeyPressed ('-');
			}
			if (Input.GetKeyDown ("w")) {
				KeyPressed ('+');
			}
		}
	}

	public void NextBlock(string blockMap){ //change the block of arrows
		Destroy (atualBlock); //destroy the last block finished
		canSendSignal = true;
		GameObject _newBlock = Instantiate (blockPrefab); //create the block
		_newBlock.transform.position = new Vector3(transform.position.x +aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical adjustment in the screen

		var blockInstance = _newBlock.transform.GetComponent<ArrowBlockBehaviour> (); //get the script component of the created block
		if (blockInstance) {
			arrowsConfig = blockMap;
			blockInstance.CreateArrows (blockMap);   //set the arrows configuration of the new block
		}
		atualBlock = _newBlock;  //atualize the atual block
	}

	public void EndedBlock(int _arrowsGotRight){
		gameMaster.GetComponent<GameMasterBehaviour> ().PlayerEnded (_arrowsGotRight);
		Debug.Log ("Player ended");
	}

	public int AtualArrowsValue(){
		return atualBlock.GetComponent<ArrowBlockBehaviour> ().correctedArrows;
	}

	void KeyPressed(char _signal){
		var actualBlockAction = atualBlock.GetComponent<ArrowBlockBehaviour> (); 
		if(actualBlockAction){
			actualBlockAction.GetArrowPressed (_signal);   //send the user's input to the atual block
		}
	}

	public void GetDamage(int _damageValue){
		currentHP -= _damageValue;
		//Debug.Log ("Deu damage player " + _damageValue);
		healthIndicator.SetHealth (currentHP, maximumHP);

	}

	void CheckDeath(){ //need to send death to game master
		if (currentHP <= 0) {
			gameMaster.GetComponent<GameMasterBehaviour> ().GameOver ("player");
		}
	}


	public void TotalUrro(){

		var _teamBehaviour = playerTeam.GetComponent<TeamBehaviour> ();
		if (_teamBehaviour) {
			_teamBehaviour.FazOUrroTotal (arrowsConfig);
		}
		//Function called on the chinchilas when total urro is done
	}

	public void PartialUrro(int _result){
		var _teamBehaviour = playerTeam.GetComponent<TeamBehaviour>();
		if (_teamBehaviour) {
			_teamBehaviour.FazOUrroPartial (arrowsConfig, _result);
		}
//		Debug.Log ("roar!");
	}

	public void StopSignal(){
		canSendSignal = false;
	}
}
