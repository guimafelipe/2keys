using UnityEngine;
using System.Collections;

public class ChinchilaController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FazOUrro(string _urroType){
		var _urroDisplay = gameObject.GetComponentInChildren<UrroDisplay>();  //Get the canvas script to display the urro in the baloon
		StartCoroutine (OpenMouth ());
		if(_urroDisplay) _urroDisplay.DisplayUrro (_urroType);


	}

	IEnumerator OpenMouth(){
		animator.SetBool ("roaring", true);
		Debug.Log ("fez o urro");
		yield return new WaitForSeconds (1f);
		animator.SetBool ("roaring", false);
	}
}
