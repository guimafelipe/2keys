using UnityEngine;
using System.Collections;

public class EnemyChinchilaController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void FazOUrro(string _urroType){
		var _urroDisplay = gameObject.GetComponentInChildren<EnemyUrroDisplay>();  //Get the canvas script to display the urro in the baloon
		if(_urroDisplay) _urroDisplay.DisplayUrro (_urroType);
		Debug.Log ("A chinchila inimiga fez seu urro!!!!");
	}
}
