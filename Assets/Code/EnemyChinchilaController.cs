using UnityEngine;
using System.Collections;

public class EnemyChinchilaController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void FazOUrro(string _urroType){
		var _urroDisplay = gameObject.GetComponentInChildren<EnemyUrroDisplay>();  //Get the canvas script to display the urro in the baloon
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
