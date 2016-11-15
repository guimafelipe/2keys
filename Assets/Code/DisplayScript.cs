using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour {

	[SerializeField]
	private Text countdown;

	[SerializeField]
	private Text rounds;

	// Use this for initialization
	void Start () {
		rounds.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCountdown(float _countdown){
		int _floorcountodown = Mathf.FloorToInt (_countdown);
		countdown.text = _floorcountodown + "";
	}

	public void EndOfCountdown(){
		countdown.text = "";
	}

	public void SetRound(int _round){
		rounds.text = _round + "";
	}
}
