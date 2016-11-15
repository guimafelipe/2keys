using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyUrroDisplay : MonoBehaviour {


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
			audioManager.PlaySound ("Rawr3");
			return "urro_3";
		case 3:
			audioManager.PlaySound ("Rrrr2");
			return "urro_1";
		case 4:
			audioManager.PlaySound ("Rawr2");
			return "urro_0";
		default:
			audioManager.PlaySound ("Birrl1");
			return "urro_2";
		}

	}
}
