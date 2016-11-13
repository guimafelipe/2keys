using UnityEngine;
using System.Collections;

public class ArrowBlockBehaviour : MonoBehaviour {

	private int arrowNum; //Number of arrows in the block
	private int atualArrow = 0; //Atual arrow in the iteration of the block
	public float blockDamage; //The maximum damage output in the block
	private float dist = 2f; //The distance between arrows in the display
	public float correctedArrows = 0; //The number of arrows got right by the player


	public GameObject upArrowPrefab; //Prefabs of up and down arrows
	public GameObject downArrowPrefab;
	private GameObject manager; //The scene manager

	// Use this for initialization
	void Start(){
		
		manager = GameObject.Find ("_Manager"); //Find the scene manager in the scene

	}

	public void CreateArrows (string arrowsConfig) { //Create the arrows based in the information passed by the scene manager 
		arrowNum = arrowsConfig.Length;
		transform.localScale = new Vector3 (transform.localScale.x * arrowNum, transform.localScale.y); //Only make the block larger. I can exclude this if the block don't have a sprite renderer
		for (int i = 0; i < arrowNum; i++) {
			if (arrowsConfig [i] == '+') { //Check is is an up arrow
				GameObject newArrow = Instantiate (upArrowPrefab);
				newArrow.transform.position = gameObject.transform.position + new Vector3((float)i * dist,0f,0f); //Set the arrow after the other
				newArrow.transform.parent = gameObject.transform; //Set this as parent of all arrows
			} else if (arrowsConfig[i] == '-'){ // Or a down arrow
				GameObject newArrow = Instantiate (downArrowPrefab);
				newArrow.transform.position = gameObject.transform.position + new Vector3((float)i * dist,0f,0f);
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
		var fim = manager.transform.GetComponent<Scene1Manager> (); //Call the function in the Manager
		if (fim) {
			fim.NextBlock ();
		}
	}

	void TotalUrro(){
		Debug.Log ("Birl!"); //Function called on the chinchilas when total urro is done
	}

	void PartialUrro(){
		float result = (float)correctedArrows / arrowNum; //Function falled when partial urro
		Debug.Log ("roar!");
	}

	public void GetArrowPressed(char signal){ //Function made to check wich arrow was pressed and called the right function based on the manager information
		if (signal == '+') {
			UpPressed (atualArrow);
		}
		if (signal == '-') {
			DownPressed (atualArrow);
		}
		atualArrow++;
	}

	void UpPressed(int checkArrow){
		Transform nextArrow = transform.GetChild (checkArrow); //Pass the arrow as parameter

		if(nextArrow.CompareTag("Up Arrow")){
			nextArrow.GetComponent<SpriteRenderer>().color = Color.green; //Only change the renderer color to the player
			correctedArrows++;
		} else {
			nextArrow.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	void DownPressed(int checkArrow){
		Transform nextArrow = transform.GetChild (checkArrow);

		if(nextArrow.CompareTag("Down Arrow")){
			nextArrow.GetComponent<SpriteRenderer>().color = Color.green;
			correctedArrows++;
		} else {
			nextArrow.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
