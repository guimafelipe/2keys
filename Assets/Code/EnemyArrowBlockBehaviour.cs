using UnityEngine;
using System.Collections;

public class EnemyArrowBlockBehaviour : MonoBehaviour {

	private int arrowNum; //Number of arrows in the block
	private int atualArrow = 0; //Atual arrow in the iteration of the block
	public float blockDamage; //The maximum damage output in the block
	private float dist = 1f; //The distance between arrows in the display
	public float correctedArrows = 0; //The number of arrows got right by the player
	public float aestheticalAdjustmentDistance = 2;

	public GameObject upArrowPrefab; //Prefabs of up and down arrows
	public GameObject downArrowPrefab;
	private GameObject manager; //The scene manager
	private GameObject team;
	private string arrowsConfig;

	// Use this for initialization
	void Start(){
		team = GameObject.Find ("Team2");
		manager = GameObject.Find ("_enemyTeamManager"); //Find the scene manager in the scene

	}

	public void CreateArrows (string _arrowsConfig) { //Create the arrows based in the information passed by the scene manager 
		arrowNum = _arrowsConfig.Length;
		arrowsConfig = _arrowsConfig;
		//transform.localScale = new Vector3 (transform.localScale.x * arrowNum, transform.localScale.y); //Only make the block larger. I can exclude this if the block don't have a sprite renderer
		for (int i = 0; i < arrowNum; i++) {
			if (_arrowsConfig [i] == '+') { //Check is is an up arrow
				GameObject newArrow = Instantiate (upArrowPrefab);
				newArrow.transform.position = gameObject.transform.position + new Vector3(-arrowNum*dist + (float)i * dist,0f,0f); //Set the arrow after the other
				newArrow.transform.parent = gameObject.transform; //Set this as parent of all arrows
			} else if (_arrowsConfig[i] == '-'){ // Or a down arrow
				GameObject newArrow = Instantiate (downArrowPrefab);
				newArrow.transform.position = gameObject.transform.position + new Vector3(-arrowNum*dist + (float)i * dist,0f,0f);
				newArrow.transform.parent = gameObject.transform;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (atualArrow == arrowNum) { //Only needed to check if the block is already done
			UrroResult ();
			atualArrow++;
		}
	}

	void UrroResult(){ //Function Called when all arrows are checked
		if (correctedArrows == arrowNum) {
			TotalUrro (); //Got a 100% strke, useful to maintain the combo
		} else {
			PartialUrro (); //Get some arrow wrong
		}

		StartCoroutine (KillBlock ()); //This coroutine is called only to guarantee a little time to the player see the last color

	}

	IEnumerator KillBlock(){ //Kill block coroutine
		yield return new WaitForSeconds (0.5f);
		var _fim = manager.transform.GetComponent<AIManager> (); //Call the function in the Manager
		if (_fim) {
			_fim.NextBlock ();
		}
	}

	void TotalUrro(){

		var _teamBehaviour = team.GetComponent<EnemyTeamBehaviour> ();
		if (_teamBehaviour) {
			Debug.Log ("entrou aqui");
			_teamBehaviour.FazOUrroTotal (arrowsConfig);
		}
		Debug.Log ("Birl!"); //Function called on the chinchilas when total urro is done
	}

	void PartialUrro(){
		float _result = (float)correctedArrows / arrowNum; //Function called when partial urro
		var _teamBehaviour = team.GetComponent<EnemyTeamBehaviour>();
		if (_teamBehaviour) {
			_teamBehaviour.FazOUrroPartial (arrowsConfig, _result);
		}
		Debug.Log ("roar!");
	}

	public void GetArrowPressed(char _signal){ //Function made to check wich arrow was pressed and called the right function based on the manager information
		if (_signal == '+') {
			UpPressed (atualArrow);
		}
		if (_signal == '-') {
			DownPressed (atualArrow);
		}
		atualArrow++;
	}

	void UpPressed(int _checkArrow){
		Transform nextArrow = transform.GetChild (_checkArrow); //Pass the arrow as parameter

		if(nextArrow.CompareTag("Up Arrow")){
			nextArrow.GetComponent<SpriteRenderer>().color = Color.green; //Only change the renderer color to the player
			correctedArrows++;
		} else {
			nextArrow.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	void DownPressed(int _checkArrow){
		Transform nextArrow = transform.GetChild (_checkArrow);

		if(nextArrow.CompareTag("Down Arrow")){
			nextArrow.GetComponent<SpriteRenderer>().color = Color.green;
			correctedArrows++;
		} else {
			nextArrow.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
