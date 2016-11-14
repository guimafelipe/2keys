using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemyBlockPrefab;

	private string arrowsConfig;
	public int teamMemberNumber = 4;

	public float aestheticalAdjustmentDistance = 2f;

	private int maximumHP = 100;
	public int currentHP = 100;

	private GameObject gameMaster;
	private GameObject atualBlock;
	private GameObject enemyTeam;//private GameObject enemyTeam;
	// Use this for initialization

	[SerializeField]
	private HealthIndicator healthIndicator;  //Declare the status indicator

	public void InitialSetup (string firstBlock) {

		currentHP = maximumHP;
		healthIndicator.SetHealth (currentHP, maximumHP);  //set the initial health of the enemy

		gameMaster = GameObject.Find ("Game Master");
		enemyTeam = GameObject.Find("Team2");

		var enemyTeamBehaviour = enemyTeam.GetComponent<EnemyTeamBehaviour>();

		enemyTeamBehaviour.CreateEnemyChinchilas(teamMemberNumber);

		GameObject _newBlock = Instantiate (enemyBlockPrefab);  //create the first block of the level
		//_newBlock.transform.parent = gameObject.transform;
		_newBlock.transform.position = new Vector3(transform.position.x + aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical adjustment in the screen
		var _blockInstance = _newBlock.transform.GetComponent<EnemyArrowBlockBehaviour> ();  //get the script of the new block
		if (_blockInstance) {
			arrowsConfig = firstBlock;
			_blockInstance.CreateArrows (firstBlock); //Create the arrows of the first block
			//Debug.Log("chamou a criação de setas");
		}
		atualBlock = _newBlock; //set the first block as the atual block
	}

	// Update is called once per frame
	void Update () {
		CheckDeath ();
		if (atualBlock) { //if there is a block...
			if (Input.GetKeyDown ("down")){  //get the user input
				KeyPressed ('-');
			}
			if (Input.GetKeyDown("up")) {
				KeyPressed ('+');
			}
		}
	}

	public void NextBlock(string blockMap){ //change the block of arrows
		Destroy (atualBlock); //destroy the last block finished

		GameObject _newBlock = Instantiate (enemyBlockPrefab); //create the enemy block
		_newBlock.transform.position = new Vector3(transform.position.x + aestheticalAdjustmentDistance, transform.position.y, transform.position.z); //Aesthetical adjustment in the screen
		var blockInstance = _newBlock.transform.GetComponent<EnemyArrowBlockBehaviour> (); //get the script component of the created block
		if (blockInstance) {
			arrowsConfig = blockMap;
			blockInstance.CreateArrows (blockMap);   //set the arrows configuration of the new block
		}
		atualBlock = _newBlock;  //atualize the atual block

	}

	public void EndedBlock(int _arrowsGotRight){
		gameMaster.GetComponent<GameMasterBehaviour> ().EnemyEnded (_arrowsGotRight);
		Debug.Log ("Enemy ended");
	}

	public int AtualArrowsValue(){
		return atualBlock.GetComponent<EnemyArrowBlockBehaviour> ().correctedArrows;
	}

	void KeyPressed(char _signal){
		var actualBlockAction = atualBlock.GetComponent<EnemyArrowBlockBehaviour> (); 
		if(actualBlockAction){
			actualBlockAction.GetArrowPressed (_signal);   //send the user's input to the atual block
		}
	}

	public void GetDamage(int _damageValue){
		currentHP -= _damageValue;
		Debug.Log ("deu dano no enemy " + _damageValue);
		healthIndicator.SetHealth (currentHP, maximumHP);

	}

	void CheckDeath(){
		if (currentHP <= 0) {
			gameMaster.GetComponent<GameMasterBehaviour> ().GameOver ("enemy");
		}
	}

	public void TotalUrro(){

		var _teamBehaviour = enemyTeam.GetComponent<EnemyTeamBehaviour> ();
		if (_teamBehaviour) {
			_teamBehaviour.FazOUrroTotal (arrowsConfig);
		}
		//Function called on the chinchilas when total urro is done
	}

	public void PartialUrro(int _result){
		var _teamBehaviour = enemyTeam.GetComponent<EnemyTeamBehaviour>();
		if (_teamBehaviour) {
			_teamBehaviour.FazOUrroPartial (arrowsConfig, _result);
		}
		Debug.Log ("roar!");
	}

}
