using UnityEngine;
using System.Collections;

public class ChinchilaController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FazOUrro(string _urroType){
		var _urroDisplay = gameObject.GetComponentInChildren<UrroDisplay>();  //Get the canvas script to display the urro in the baloon
		if(_urroDisplay) _urroDisplay.DisplayUrro (_urroType);
		Debug.Log ("A chinchila fez seu urro!!!!");
	}
}
