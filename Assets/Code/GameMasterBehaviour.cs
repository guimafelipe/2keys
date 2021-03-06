﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

	private bool gameEnded = false;

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
		display.SetRound (atualRound + 1);
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
		if (gameEnded) {
			if (Input.GetKeyDown ("w") || Input.GetKeyDown ("s")) {
				SceneManager.LoadScene (0);
			}
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

		int maximumDamageThisRound = levelRoute.Length;

		int realDamageInPlayer = damageInPlayer - (maximumDamageThisRound - damageInPlayer);
		int realDamageInEnemy = damageInEnemy - (maximumDamageThisRound - damageInEnemy);

		if (realDamageInPlayer >= 0) {
			_playerBehaviour.GetDamage (realDamageInPlayer);
		} else {
			_playerBehaviour.GetDamage (0);
		}
		if (realDamageInEnemy >= 0) {
			_enemyBehaviour.GetDamage (realDamageInEnemy);
		} else {
			_enemyBehaviour.GetDamage (0);
		}

		if (realDamageInEnemy == levelRoute.Length) {
			_playerBehaviour.TotalUrro ();
		} else if (realDamageInEnemy > 0){
			_playerBehaviour.PartialUrro (realDamageInEnemy);
		}
		if (realDamageInPlayer == levelRoute.Length) {
			_enemyBehaviour.TotalUrro ();
		} else if (realDamageInPlayer > 0){
			_enemyBehaviour.PartialUrro (realDamageInPlayer);
		}

		canCreateNewRound = true;

	}

	public void GameOver(string _whoDied){
		canCreateNewRound = false;
		//Debug.Log ("game over called");
		if (_whoDied == "player") {
			//Do something to indicate the player victory
			GetComponent<GameOverFade>().BeginFade(1);
		}
		else if (_whoDied == "enemy"){
			//Do something to indicate the player defeat
			GetComponent<WinFade>().BeginFade(1);
		}

		StartCoroutine (BackToMenu());
	}

	IEnumerator BackToMenu(){
		yield return new WaitForSeconds (2);
		gameEnded = true;
	}
}
