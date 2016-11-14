using UnityEngine;
using System.Collections;

public class TeamBehaviour : MonoBehaviour {

	public GameObject chinchilaPrefab;
	public float chinchilaInstanceRange;
	private int teamSize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateChinchilas(int _chinchilasNumber){
		teamSize = _chinchilasNumber;
		for(int i = 0; i < _chinchilasNumber; i++){
			GameObject newChinchila = Instantiate(chinchilaPrefab);
			newChinchila.transform.SetParent(gameObject.transform);
			newChinchila.transform.position = transform.position;
			newChinchila.transform.Translate (Random.Range (-chinchilaInstanceRange, chinchilaInstanceRange), Random.Range (-chinchilaInstanceRange,chinchilaInstanceRange), 0f);
		}
	}


	public void FazOUrroPartial(string _urroType, float _ratio){
		Transform selectedChinchila = transform.GetChild ((int)Mathf.Floor (Random.Range (0f, (float)teamSize)));
		var selectedChinchilaBehaviour = selectedChinchila.GetComponent<ChinchilaController> ();
		if (selectedChinchilaBehaviour) {
			selectedChinchilaBehaviour.FazOUrro (_urroType);
			Debug.Log ("Chamou team urro partial");
		}
	}

	public void FazOUrroTotal(string _urroType){
		for (int i = 0; i < teamSize; i++) {
			var selectedChinchilaBehaviour = transform.GetChild (i).GetComponent<ChinchilaController> ();
			if (selectedChinchilaBehaviour) {
				selectedChinchilaBehaviour.FazOUrro (_urroType);
				Debug.Log ("Chamou team urro total");
			}
		
		}
	}
}
