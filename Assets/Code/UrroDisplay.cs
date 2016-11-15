using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UrroDisplay : MonoBehaviour {

	[SerializeField]
	private RectTransform urroBaloon;
	[SerializeField]
	private Text urroText; 
	//private string spriteName;

	private AudioManager audioManager;

	void Start(){

		audioManager = AudioManager.instance;

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
		//Debug.Log("Coroutine started");
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
		switch (_urroType.Length){
		case 2:
			audioManager.PlaySound ("Arrr1");
			return "urro_7";
		case 3:
			audioManager.PlaySound ("Rrrr1");
			return "urro_5";
		case 4:
			audioManager.PlaySound ("Rawr1");
			return "urro_4";
		default:
			audioManager.PlaySound ("Birrl2");
			return "urro_6";
		}

	}

}
