using UnityEngine;
using System.Collections;

public class GameMasterBehaviour : MonoBehaviour {

	private GameObject player;
	private GameObject enemy;
	private int playerHealth;
	private int enemyHealth;

	private int storedPlayerDmg;
	private int storedEnemyDmg;

	private string levelRoute;

	private int atualRound;

	private bool canCreateNewRound;

	private bool waitingPlayer, waitingEnemy;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("_playerManager");
		enemy = GameObject.Find ("_enemyTeamManager");

		atualRound = 0;
		canCreateNewRound = false;

		levelRoute = BlockMap.GetBlock (atualRound);

		storedEnemyDmg = 0;
		storedPlayerDmg = 0;

		player.GetComponent<PlayerManager>().InitialSetup (levelRoute);
		enemy.GetComponent<EnemyManager>().InitialSetup (levelRoute);
		Debug.Log ("Fez initial setup");
	}
	
	// Update is called once per frame
	void Update () {
		if (canCreateNewRound) {
			CreateNewRound ();
			canCreateNewRound = false;
		}
	}

	void CreateNewRound(){
		atualRound++;
		storedEnemyDmg = 0; //Reset the damages for the new round
		storedPlayerDmg = 0;
		waitingEnemy = false;
		waitingPlayer = false;
		levelRoute = BlockMap.GetBlock (atualRound);
		player.GetComponent<PlayerManager> ().NextBlock (levelRoute);
		enemy.GetComponent<EnemyManager> ().NextBlock (levelRoute);
		canCreateNewRound = false;
	}

	public void PlayerEnded(int _result){
		Debug.Log ("Entered player ended");
		if (waitingPlayer) {
			DoDamage (storedEnemyDmg, _result);		
		}
		if (_result == levelRoute.Length) {
			Debug.Log ("Player deu dano full");
			int _enemyDmg = enemy.GetComponent<EnemyManager> ().AtualArrowsValue ();
			DoDamage (_enemyDmg, _result);
			CreateNewRound ();
		} else {
			waitingEnemy = true;
			storedPlayerDmg = _result;
		}
	}

	public void EnemyEnded(int _result){
		if (waitingEnemy) {
			DoDamage (_result, storedPlayerDmg);	
		}
		if (_result == levelRoute.Length) {
			int _playerDmg = player.GetComponent<PlayerManager> ().AtualArrowsValue ();
			DoDamage (_result, _playerDmg);
			CreateNewRound ();
		} else {
			waitingPlayer = true;
			storedEnemyDmg = _result;
		}
	}

	void DoDamage (int damageInPlayer, int damageInEnemy){
		var _playerBehaviour = player.GetComponent<PlayerManager> ();
		var _enemyBehaviour = enemy.GetComponent<EnemyManager> ();

		_playerBehaviour.GetDamage (damageInPlayer);
		_enemyBehaviour.GetDamage (damageInEnemy);

		if (damageInEnemy == levelRoute.Length) {
			_playerBehaviour.TotalUrro ();
		} else {
			_playerBehaviour.PartialUrro (damageInEnemy);
		}
		if (damageInPlayer == levelRoute.Length) {
			_enemyBehaviour.TotalUrro ();
		} else {
			_enemyBehaviour.PartialUrro (damageInPlayer);
		}

		canCreateNewRound = true;
	}
}
