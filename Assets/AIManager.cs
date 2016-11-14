using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

	public float cooldown = 0.4f;

	public float accuracy = 0.8f;

	private float remainingCD;
	private char rightArrow;

	GameObject enemyManager;

	// Use this for initialization
	void Start () {
		remainingCD = cooldown;
		enemyManager = GameObject.Find ("_enemyTeamManager");
	}
	
	// Update is called once per frame
	void Update () {
		remainingCD -= Time.deltaTime ;
		if (remainingCD <= 0) {
			GetRightArrow ();
			SendArrow (rightArrow);
			remainingCD = cooldown;
		}
	}


	public void GetRightArrow(){
		var _enemyBehaviour = enemyManager.GetComponent<EnemyManager> ();
		var _atualBlock = _enemyBehaviour.atualBlock;
		var _atualBlockBehaviour = _atualBlock.GetComponent<EnemyArrowBlockBehaviour> ();

		rightArrow = _atualBlockBehaviour.arrowsConfig [_atualBlockBehaviour.atualArrow];

	}


	public void SendArrow(char correctedArrow){
		bool gotRight;

		float pointer = Random.Range (0f, 1f);

		if (pointer <= accuracy) {
			gotRight = true;
		} else {
			gotRight = false;
		}
		var _enemyBehaviour = enemyManager.GetComponent<EnemyManager> ();
		var _atualBlock = _enemyBehaviour.atualBlock;
		var _atualBlockBehaviour = _atualBlock.GetComponent<EnemyArrowBlockBehaviour> ();	

		if (gotRight) {
			if (correctedArrow == '+') {
				_atualBlockBehaviour.GetArrowPressed ('+');
			} else {
				_atualBlockBehaviour.GetArrowPressed ('-');
			} 
		}else{
			if (correctedArrow == '+') {
				_atualBlockBehaviour.GetArrowPressed ('-');
			} else {
				_atualBlockBehaviour.GetArrowPressed ('+');
			}
		}
	}

}
