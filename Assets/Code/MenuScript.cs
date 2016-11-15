using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	private int whereIAm;
	private bool canPushButtons = true;

	// Use this for initialization
	void Start () {
		whereIAm = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (canPushButtons) {
			if (Input.GetKeyDown ("w")) {
				if (whereIAm == 0) {
					whereIAm++;
					gameObject.GetComponent<InstructionsFadeScript> ().ShowInstructions ();
					StartCoroutine (LittleWait ());
				} else if (whereIAm == -1) {
					whereIAm++;
					gameObject.GetComponent<CreditsFadeScript> ().HideCredits ();
					StartCoroutine (LittleWait ());
				} else {
					SceneManager.LoadScene (1); //start the game if already on instructions
				}

			}
			if (Input.GetKeyDown ("s")) {
				if (whereIAm == 0) {
					whereIAm--;
					gameObject.GetComponent<CreditsFadeScript> ().ShowCredits ();
					StartCoroutine (LittleWait ());
				} else if (whereIAm == 1) {
					whereIAm--;
					gameObject.GetComponent<InstructionsFadeScript> ().HideInstructions ();
					StartCoroutine (LittleWait ());

				} else {
					gameObject.GetComponent<CreditsFadeScript> ().HideCredits ();
					StartCoroutine (LittleWait ());
				}
			}
		}
	}

	IEnumerator LittleWait(){
		canPushButtons = false;
		yield return new WaitForSeconds (1);
		canPushButtons = true;
	}
}
	