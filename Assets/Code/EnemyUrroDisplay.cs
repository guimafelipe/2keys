using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyUrroDisplay : MonoBehaviour {


	[SerializeField]
	private RectTransform urroBaloon;
	[SerializeField]
	private Text urroText; 
	//private string spriteName;

	void Start(){
		if (urroBaloon == null) {
			Debug.Log ("No urro baloon");
		}
		if (urroText == null) {
			Debug.Log ("No urro text");
		}

		urroBaloon.GetComponent<Image> ().enabled = false;
		urroText.GetComponent<Text> ().enabled = false;
	}

	public void DisplayUrro(string _urroType){
		urroBaloon.GetComponent<Image> ().sprite = Resources.Load (GetUrro (_urroType), typeof(Sprite)) as Sprite;   //Get the urro text in the urro dictionary class
		StartCoroutine (ShowUrro());
	}

	IEnumerator ShowUrro(){
		urroBaloon.GetComponent<Image> ().enabled = true;
		//urroText.GetComponent<Text> ().enabled = true;
		yield return new WaitForSeconds (1f);
		urroBaloon.GetComponent<Image> ().enabled = false;   // 
		//urroText.GetComponent<Text> ().enabled = false;
	}

	public string GetUrro(string _urroType){  //get the urro sprite from the urroType string
		switch (_urroType){
		case "++":
			return "urro_3";
		case "+-":
			return "urro_1";
		case "--":
			return "urro_2";
		case "-+":

		case "+++":

		case "---":

		case "++-":

		case "--+":

		default:
			return "urro_0";
		}

	}
}
