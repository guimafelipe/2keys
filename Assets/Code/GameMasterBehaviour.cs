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

	private float timeBetweenBlocks = 1.5f;

	private float secondsToStart = 3f;

	[SerializeField]
	private DisplayScript display;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("_playerManager");
		enemy = GameObject.Find ("_enemyTeamManager");

		atualRound = 0;
		canCreateNewRound = false;

		levelRoute = BlockMap.GetBlock (atualRound);

		storedEnemyDmg = 0;
		storedPlayerDmg = 0;

		StartCoroutine(InitialCountdown());
	}

	IEnumerator InitialCountdown(){
		yield return new WaitForSeconds(3f);
		player.GetComponent<PlayerManager>().InitialSetup (levelRoute);
		enemy.GetComponent<EnemyManager>().InitialSetup (levelRoute);
		display.EndOfCountdown ();
	}

	// Update is called once per frame
	void Update () {
		if (secondsToStart > 0) {
			secondsToStart -= Time.deltaTime;
			display.SetCountdown (secondsToStart);
		}
		if (canCreateNewRound) {
			//Debug.Log ("Criou novo round");
			StartCoroutine (TimePreparation());
			canCreateNewRound = false;
		}
	}

	void CreateNewRound(){
		atualRound++;
		display.SetRound (atualRound + 1);
		storedEnemyDmg = 0; //Reset the damages for the new round
		storedPlayerDmg = 0;
		waitingEnemy = false;
		waitingPlayer = false;
		levelRoute = BlockMap.GetBlock (atualRound);
		player.GetComponent<PlayerManager> ().NextBlock (levelRoute);
		enemy.GetComponent<EnemyManager> ().NextBlock (levelRoute);
		canCreateNewRound = false;
	}

	IEnumerator TimePreparation(){    //Only for aesthetics purposes
		//Debug.Log("Entrou no wait area");
		yield return new WaitForSeconds (timeBetweenBlocks); 
		//Debug.Log ("Esperou os segundos");
		CreateNewRound ();
	}

	public void PlayerEnded(int _result){
		//Debug.Log ("Entered player ended");
		if (waitingPlayer) {
			DoDamage (storedEnemyDmg, _result);		
		}
		if (_result == levelRoute.Length) {
			//Debug.Log ("Player deu dano full");
			int _enemyDmg = enemy.GetComponent<EnemyManager> ().AtualArrowsValue ();
			DoDamage (_enemyDmg, _result);
			enemy.GetComponent<EnemyManager> ().StopSignal();
			canCreateNewRound = true;
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
			player.GetComponent<PlayerManager> ().StopSignal();
			canCreateNewRound = true;
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

	public void GameOver(string _whoDied){
		canCreateNewRound = false;
		Debug.Log ("game over called");
		if (_whoDied == "player") {
			//Do something to indicate the player victory
			GetComponent<GameOverFade>().BeginFade(1);
		}
		else if (_whoDied == "enemy"){
			//Do something to indicate the player defeat
			GetComponent<GameOverFade>().BeginFade(1);
		}
	}
}
